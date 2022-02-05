using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using DestinyMod.Common.Projectiles;

namespace DestinyMod.Content.Projectiles.Weapons.Ranged
{
	public class CausalityArrow : DestinyModProjectile
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

		public override Color? GetAlpha(Color lightColor) => new Color(lightColor.R, lightColor.G * 0.75f, lightColor.B * 0.55f, lightColor.A);
	}
}