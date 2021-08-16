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
                if (!projectile.HomeInOnNPC(200f, 15f)) {
                    projectile.velocity.Y += 0.25f;
                }
            }
            else {
                projectile.velocity *= 0;
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {
            projectile.timeLeft = 1000000;
            projectile.tileCollide = false;
            dye = true;
            Main.PlaySound(SoundID.Item14, projectile.position);
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