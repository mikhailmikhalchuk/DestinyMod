using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheDestinyMod.Projectiles.Melee
{
    class BoltCasterProjectile : ModProjectile
    {
        public override void SetStaticDefaults() {
            Main.projFrames[projectile.type] = 14;
        }

        public override void SetDefaults() {
            projectile.width = 100;
            projectile.height = 100;
            projectile.aiStyle = 20;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.hide = true;
            projectile.ownerHitCheck = true;
            projectile.melee = true;
            projectile.soundDelay = int.MaxValue;
        }

        public override Color? GetAlpha(Color lightColor) {
            return new Color(185, 237, 229, 0) * (1f - projectile.alpha / 255f);
        }

        public override void AI() {
            if (Main.player[projectile.owner].frozen|| Main.player[projectile.owner].stoned) {
                projectile.Kill();
            }
            projectile.velocity *= 0.70f;
            if (++projectile.frameCounter >= 3) {
                projectile.frameCounter = 0;
                if (++projectile.frame >= 14) {
                    Main.PlaySound(SoundID.Item1, Main.LocalPlayer.position);
                    projectile.frame = 0;
                }
            }

            projectile.direction = (projectile.spriteDirection = ((projectile.velocity.X > 0f) ? 1 : -1));
            projectile.rotation = projectile.velocity.ToRotation();
            if (projectile.velocity.Y > 16f) {
                projectile.velocity.Y = 16f;
            }

            if (projectile.spriteDirection == -1)
                projectile.rotation += MathHelper.Pi;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor) {
            SpriteEffects spriteEffects = SpriteEffects.None;
            if (projectile.spriteDirection == -1) {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }
            Texture2D texture = Main.projectileTexture[projectile.type];
            int frameHeight = Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type];
            int startY = frameHeight * projectile.frame;
            Rectangle sourceRectangle = new Rectangle(0, startY, texture.Width, frameHeight);
            Vector2 origin = sourceRectangle.Size() / 2f;
            origin.X = ((projectile.spriteDirection == 1) ? (sourceRectangle.Width - 80) : 80);

            Color drawColor = projectile.GetAlpha(lightColor);
            Main.spriteBatch.Draw(texture,
            projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY),
            sourceRectangle, drawColor, projectile.rotation, origin, projectile.scale, spriteEffects, 0f);

            return false;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {
            if (Main.rand.NextBool(10)) {
                target.AddBuff(BuffID.Frostburn, 60);
            }
        }

        public override void OnHitPlayer(Player target, int damage, bool crit) {
            if (Main.rand.NextBool(10)) {
                target.AddBuff(BuffID.Frostburn, 60);
            }
        }
    }
}