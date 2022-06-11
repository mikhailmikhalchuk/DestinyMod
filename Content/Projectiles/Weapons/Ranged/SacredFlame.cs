using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using DestinyMod.Common.Projectiles;

namespace DestinyMod.Content.Projectiles.Weapons.Ranged
{
	public class SacredFlame : DestinyModProjectile //the ticuu's HOMING one
	{
		public override void SetStaticDefaults()
		{
			ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
		}

		public override void DestinySetDefaults()
		{
			Projectile.CloneDefaults(ProjectileID.FireArrow);
			AIType = ProjectileID.FireArrow;
			Projectile.width = 2;
			Projectile.height = 34;
			Projectile.timeLeft = 500;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Ranged;
		}

		public override void Kill(int timeLeft)
		{
			Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
			SoundEngine.PlaySound(SoundID.Item10, Projectile.position);

			for (int count = 0; count < 2; count++)
			{
				Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Torch);
			}
		}

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
			target.AddBuff(ModContent.BuffType<Buffs.Debuffs.SacredFlame>(), 600);
        }

        public override void OnHitPvp(Player target, int damage, bool crit)
        {
			target.AddBuff(ModContent.BuffType<Buffs.Debuffs.SacredFlame>(), 600);
		}

        public override void AI()
		{
			GradualHomeInOnNPC(400, 15f, 0.1f);
		}

		public override Color? GetAlpha(Color lightColor) => new Color(lightColor.R, lightColor.G * 0.75f, lightColor.B * 0.55f, lightColor.A);
	}
}