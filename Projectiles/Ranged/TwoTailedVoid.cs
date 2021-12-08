using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace TheDestinyMod.Projectiles.Ranged
{
	public class TwoTailedVoid : ModProjectile
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Void Rocket");
			ProjectileID.Sets.Homing[projectile.type] = true;
		}

		public override void SetDefaults() {
			projectile.width = 10;
			projectile.height = 26;
			projectile.timeLeft = 500;
			projectile.friendly = true;
			projectile.ranged = true;
			projectile.penetrate = 1;
		}

        public override Color? GetAlpha(Color lightColor) {
			return new Color(lightColor.R, lightColor.G * 0.1f, lightColor.B * 0.8f, lightColor.A);
		}

        public override void Kill(int timeLeft) {
			Main.PlaySound(SoundID.Item14, projectile.position);
			projectile.position = projectile.Center;
			projectile.width = 22;
			projectile.height = 22;
			projectile.position.X -= projectile.width / 2;
			projectile.position.Y -= projectile.height / 2;
			for (int i = 0; i < 20; i++) {
				Dust dust = Dust.NewDustDirect(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, DustID.PurpleCrystalShard, 0f, 0f, 100, default, 2f);
				dust.noGravity = true;
				dust.velocity *= 4f;
				dust = Dust.NewDustDirect(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, DustID.PurpleCrystalShard, 0f, 0f, 100, default, 1.5f);
				dust.velocity *= 3f;
			}
		}

		public override bool OnTileCollide(Vector2 oldVelocity) {
			Collision.HitTiles(projectile.position + projectile.velocity, projectile.velocity, projectile.width, projectile.height);
			projectile.Kill();
			return true;
		}

		public override void AI() {
			projectile.rotation = projectile.velocity.ToRotation() + MathHelper.Pi / 2f;
			projectile.HomeInOnNPC(400f, 20f);
		}
	}
}