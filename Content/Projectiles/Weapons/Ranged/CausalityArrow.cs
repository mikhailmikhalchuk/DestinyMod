using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using DestinyMod.Common.Projectiles;

namespace DestinyMod.Content.Projectiles.Weapons.Ranged
{
	public class CausalityArrow : DestinyModProjectile //the ticuu's NORMAL one
	{
		public override void DestinySetDefaults()
		{
			Projectile.CloneDefaults(ProjectileID.FireArrow);
			AIType = ProjectileID.FireArrow;
			Projectile.width = 6;
			Projectile.height = 28;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Ranged;
		}

		public override void Kill(int timeLeft)
		{
			Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
			SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
			Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Torch);
			Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Torch);
		}

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (target.HasBuff(ModContent.BuffType<Buffs.Debuffs.SacredFlame>()))
            {
				Projectile.NewProjectile(Projectile.GetSource_FromThis(), target.position.X, target.position.Y, 0, 0, ProjectileID.DD2ExplosiveTrapT3Explosion, damage / 2, 0, Projectile.owner);
				SoundEngine.PlaySound(SoundID.DD2_ExplosiveTrapExplode, target.position);
				target.DelBuff(target.FindBuffIndex(ModContent.BuffType<Buffs.Debuffs.SacredFlame>()));
			}
        }

        public override void OnHitPvp(Player target, int damage, bool crit)
        {
			if (target.HasBuff(ModContent.BuffType<Buffs.Debuffs.SacredFlame>()))
			{
				Projectile.NewProjectile(Projectile.GetSource_FromThis(), target.position.X, target.position.Y, 0, 0, ProjectileID.DD2ExplosiveTrapT3Explosion, damage / 2, 0, Projectile.owner);
				SoundEngine.PlaySound(SoundID.DD2_ExplosiveTrapExplode, target.position);
				target.ClearBuff(ModContent.BuffType<Buffs.Debuffs.SacredFlame>());
			}
		}

        public override Color? GetAlpha(Color lightColor) => new Color(lightColor.R, lightColor.G * 0.75f, lightColor.B * 0.55f, lightColor.A);
	}
}