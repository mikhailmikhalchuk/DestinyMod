using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.GameContent;
using DestinyMod.Common.Projectiles;

namespace DestinyMod.Content.Projectiles.Weapons.Melee
{
    public class AnimatedSwordProjectile : DestinyModProjectile
    {
        public override void SetStaticDefaults() => Main.projFrames[Projectile.type] = 14;

        public LegacySoundStyle FrameCycleSound;

		public override void AutomaticSetDefaults()
		{
            Projectile.width = 100;
            Projectile.height = 100;
            Projectile.aiStyle = 20;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.hide = true;
            Projectile.ownerHitCheck = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.soundDelay = int.MaxValue;
            FrameCycleSound = SoundID.Item1;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            if (player.frozen || player.stoned)
            {
                Projectile.Kill();
            }

            Projectile.velocity *= 0.70f;

            if (++Projectile.frameCounter % 3 == 0)
            {
                if (++Projectile.frame % Main.projFrames[Projectile.type] == 0)
                {
                    SoundEngine.PlaySound(FrameCycleSound, player.position);
                }
            }

            Projectile.direction = (Projectile.velocity.X > 0f) ? 1 : -1;
            Projectile.spriteDirection = Projectile.direction;
            Projectile.rotation = Projectile.velocity.ToRotation();

            if (Projectile.spriteDirection == -1)
            {
                Projectile.rotation += MathHelper.Pi;
            }

            if (Projectile.velocity.Y > 16f)
            {
                Projectile.velocity.Y = 16f;
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            SpriteEffects spriteEffects = SpriteEffects.None;
            if (Projectile.spriteDirection == -1)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }
            Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;
            int frameHeight = texture.Height / Main.projFrames[Projectile.type];
            int startY = frameHeight * Projectile.frame;
            Rectangle sourceRectangle = new Rectangle(0, startY, texture.Width, frameHeight);
            Vector2 origin = sourceRectangle.Size() / 2f;
            origin.X = (Projectile.spriteDirection == 1) ? (sourceRectangle.Width - 80) : 80;

            Color drawColor = Projectile.GetAlpha(lightColor);
            Main.spriteBatch.Draw(texture,
            Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY),
            sourceRectangle, drawColor, Projectile.rotation, origin, Projectile.scale, spriteEffects, 0f);

            return false;
        }
    }
}