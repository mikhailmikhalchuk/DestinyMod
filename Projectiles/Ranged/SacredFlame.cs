using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace TheDestinyMod.Projectiles.Ranged
{
	public class SacredFlame : ModProjectile
	{
		public override void SetDefaults() {
			projectile.CloneDefaults(ProjectileID.FireArrow);
			aiType = ProjectileID.FireArrow;
			projectile.width = 2;
			projectile.height = 34;
			projectile.damage = 10;
			projectile.timeLeft = 500;
			projectile.friendly = true;
			projectile.ranged = true;
		}

        public override void Kill(int timeLeft) {
			Collision.HitTiles(projectile.position + projectile.velocity, projectile.velocity, projectile.width, projectile.height);
			Main.PlaySound(SoundID.Item10, projectile.position);
			Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.Fire);
			Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.Fire);
		}

        public override void AI() {
			projectile.HomeInOnNPC(400f, 15f);
		}

		public override Color? GetAlpha(Color lightColor) {
			return new Color(lightColor.R, lightColor.G * 0.75f, lightColor.B * 0.55f, lightColor.A);
		}
	}
}