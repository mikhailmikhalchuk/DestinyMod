using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace TheDestinyMod.Projectiles.Ranged
{
    public class GjallarhornMiniRocket : ModProjectile
    {
        public override string Texture => "Terraria/Projectile_" + ProjectileID.RocketI;

		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Wolfpack Round");
		}

		public override void SetDefaults() {
            projectile.CloneDefaults(ProjectileID.RocketI);
            aiType = ProjectileID.RocketI;
			projectile.timeLeft = 200;
			projectile.friendly = true;
			projectile.ranged = true;
			projectile.tileCollide = true;
			projectile.scale = 0.5f;
			projectile.penetrate = 1;
        }

        public override void Kill(int timeLeft) {
			Main.PlaySound(SoundID.Item14, projectile.position);
			projectile.position = projectile.Center;
			projectile.width = 11;
			projectile.height = 11;
			projectile.position.X -= projectile.width / 2;
			projectile.position.Y -= projectile.height / 2;
			for (int i = 0; i < 20; i++) {
				Dust dust = Dust.NewDustDirect(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, DustID.Fire, 0f, 0f, 100, default, 3.5f);
				dust.noGravity = true;
				dust.velocity *= 7f;
				dust = Dust.NewDustDirect(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, DustID.Fire, 0f, 0f, 100, default, 1.5f);
				dust.velocity *= 3f;
			}
		}

		public override bool OnTileCollide(Vector2 oldVelocity) {
			Collision.HitTiles(projectile.position + projectile.velocity, projectile.velocity, projectile.width, projectile.height);
			projectile.Kill();
			return true;
		}

        public override void ModifyDamageHitbox(ref Rectangle hitbox) {
			if (projectile.timeLeft > 190)
				hitbox = Rectangle.Empty;
        }

        public override void AI() {
			if (projectile.timeLeft > 190)
				return;

			projectile.HomeInOnNPC(400f, 20f);
		}
    }
}