using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using DestinyMod.Common.Projectiles;
using DestinyMod.Common.ModPlayers;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using DestinyMod.Content.Buffs;

namespace DestinyMod.Content.Projectiles.Weapons.Super
{
	public class SunspotSummon : DestinyModProjectile
    {
        private int TurnProgress { get => (int)Projectile.localAI[0]; set => Projectile.localAI[0] = value; }

        private int FrameSkip { get => (int)Projectile.localAI[1]; set => Projectile.localAI[1] = value; }

        public override void SetStaticDefaults() => DisplayName.SetDefault("Sunspot");

		public override void DestinySetDefaults()
        {
            Projectile.friendly = true;
            Projectile.width = 60;
            Projectile.height = 48;
            Projectile.timeLeft = 500;
            Projectile.damage = 0;
            Projectile.penetrate = -1;
            TurnProgress = 1;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            SuperPlayer superPlayer = player.GetModPlayer<SuperPlayer>();
            if (superPlayer.SuperActiveTime <= 0)
            {
                Projectile.Kill();
                return;
            }

            if (Projectile.Distance(player.Center) < 200)
            {
                player.AddBuff(ModContent.BuffType<SunWarrior>(), 3);
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;
            if (FrameSkip >= 2 && TurnProgress <= 11 || FrameSkip >= 5 && TurnProgress == 12)
            {
                FrameSkip = -1;
                TurnProgress++;
            }
            else if (FrameSkip >= 5 && TurnProgress == 13)
            {
                FrameSkip = -1;
                TurnProgress--;
            }

            FrameSkip++;
            int drawY = 48 * (TurnProgress <= 11 ? TurnProgress : TurnProgress - 2);
            Main.spriteBatch.Draw(texture, Projectile.position - Main.screenPosition, new Rectangle(0, drawY, 60, 48), lightColor, Projectile.rotation, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            return false;
        }
    }
}