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

        public override void SetDefaults() {
            projectile.CloneDefaults(ProjectileID.RocketI);
            aiType = ProjectileID.RocketI;
			projectile.timeLeft = 200;
			projectile.friendly = true;
			projectile.ranged = true;
			projectile.scale = 0.5f;
        }

        public override bool OnTileCollide(Vector2 oldVelocity) {
            Collision.HitTiles(projectile.position + projectile.velocity, projectile.velocity, projectile.width, projectile.height);
            Main.PlaySound(SoundID.Item10, projectile.position);
            projectile.Kill();
			return true;
		}

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {
			projectile.timeLeft = 1;
        }

        public override void AI() {
			if (projectile.localAI[0] == 0f) {
				AdjustMagnitude(ref projectile.velocity);
				projectile.localAI[0] = 1f;
			}
			Vector2 move = Vector2.Zero;
			float distance = 400f;
			bool target = TheDestinyMod.HomeIn(distance, projectile, ref move);
			if (target) {
				AdjustMagnitude(ref move);
				projectile.velocity = (10 * projectile.velocity + move) / 11f;
				AdjustMagnitude(ref projectile.velocity);
			}
		}

        private void AdjustMagnitude(ref Vector2 vector) {
			float magnitude = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
			if (magnitude > 20f) {
				vector *= 20f / magnitude;
			}
		}
    }
}