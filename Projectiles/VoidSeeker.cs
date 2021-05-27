using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheDestinyMod.Buffs;

namespace TheDestinyMod.Projectiles
{
    public class VoidSeeker : ModProjectile
    {
        private int turnProgress = 1;

        private int frameSkip;

        private bool dye;

        public override void SetStaticDefaults() {
            ProjectileID.Sets.Homing[projectile.type] = true;
        }

        public override void SetDefaults() {
            projectile.CloneDefaults(ProjectileID.SnowBallFriendly);
            projectile.width = 12;
            projectile.height = 12;
            projectile.scale = 1.5f;
            projectile.timeLeft = 500;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.ranged = true;
            projectile.penetrate = -1;
        }

        public override void AI() {
            if (!dye) {
                projectile.localAI[1]++;
                if (projectile.localAI[0] == 0f) {
                    AdjustMagnitude(ref projectile.velocity);
                    projectile.localAI[0] = 1f;
                }
                Vector2 move = Vector2.Zero;
                float distance = 200f;
                bool target = DestinyHelper.HomeInOnNPC(distance, projectile, ref move);
                if (target) {
                    AdjustMagnitude(ref move);
                    projectile.velocity = (10 * projectile.velocity + move) / 11f;
                    AdjustMagnitude(ref projectile.velocity);
                }
                else {
                    projectile.velocity.Y += 0.25f;
                }
            }
            else {
                projectile.velocity *= 0;
            }
        }

        private void AdjustMagnitude(ref Vector2 vector) {
            float magnitude = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
            if (magnitude > 15f) {
                vector *= 15f / magnitude;
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {
            projectile.timeLeft = 1000000;
            projectile.tileCollide = false;
            dye = true;
            if (!target.friendly && target.damage > 0 && target.life <= 0) {
                Projectile.NewProjectile(target.position, new Vector2(0, 0), ModContent.ProjectileType<VoidSeeker>(), damage, knockback, Main.LocalPlayer.whoAmI);
            }
        }

        public override bool? CanHitNPC(NPC target) {
            if (dye) {
                return false;
            }
            return null;
        }

        public override bool CanHitPlayer(Player target) {
            return !dye;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor) {
            return !dye;
        }

        public override void PostDraw(SpriteBatch spriteBatch, Color lightColor) {
            Lighting.AddLight(projectile.Center, Color.Purple.ToVector3() * 0.55f * Main.essScale);
            if (dye) {
                Texture2D texture = ModContent.GetTexture("TheDestinyMod/Projectiles/VoidSeekerExplosion");
                if (frameSkip >= 1 && turnProgress <= 11) {
                    frameSkip = -1;
                    turnProgress++;
                }
                frameSkip++;
                Main.spriteBatch.Draw(texture, new Vector2(projectile.position.X - 5, projectile.position.Y) - Main.screenPosition, new Rectangle(0, 44 * turnProgress, 44, 44), Color.Purple, projectile.rotation, Vector2.Zero, 1.4f, SpriteEffects.None, 0f);
                if (frameSkip > 2 && turnProgress == 12) {
                    projectile.Kill();
                }
            }
        }
    }
}