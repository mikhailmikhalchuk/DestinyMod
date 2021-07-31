using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheDestinyMod.Buffs;

namespace TheDestinyMod.Projectiles
{
    public class SIVANanite : ModProjectile
    {

        public override string Texture => "Terraria/Projectile_" + ProjectileID.SnowBallFriendly;

        public override void SetStaticDefaults() {
            DisplayName.SetDefault("SIVA Nanite");
            ProjectileID.Sets.Homing[projectile.type] = true;
        }

        public override void SetDefaults() {
            projectile.CloneDefaults(ProjectileID.SnowBallFriendly);
            projectile.timeLeft = 500;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.ranged = true;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
        }

        public override void AI() {
            projectile.localAI[1]++;
            if (projectile.localAI[0] == 0f) {
                AdjustMagnitude(ref projectile.velocity);
                projectile.localAI[0] = 1f;
            }
            Vector2 move = Vector2.Zero;
            float distance = 200f;
            bool target = projectile.HomeInOnNPC(distance, ref move);
            if (target) {
                AdjustMagnitude(ref move);
                projectile.velocity = (10 * projectile.velocity + move) / 11f;
                AdjustMagnitude(ref projectile.velocity);
            }
            if (!target && projectile.velocity.X > 0 && projectile.localAI[1] > 1) {
                projectile.velocity.X -= 0.5f;
            }
            if (!target && projectile.velocity.Y > 0 && projectile.localAI[1] > 1) {
                projectile.velocity.Y -= 0.5f;
            }
            if (!target && projectile.velocity.X < 0 && projectile.localAI[1] > 3) {
                projectile.velocity.X = 0f;
            }
            if (!target && projectile.velocity.Y < 0 && projectile.localAI[1] > 3) {
                projectile.velocity.Y = 0f;
            }
        }

        private void AdjustMagnitude(ref Vector2 vector) {
            float magnitude = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
            if (magnitude > 15f) {
                vector *= 15f / magnitude;
            }
        }

        public override void PostDraw(SpriteBatch spriteBatch, Color lightColor) {
            Lighting.AddLight(projectile.Center, Color.Red.ToVector3() * 0.55f * Main.essScale);
        }
    }
}