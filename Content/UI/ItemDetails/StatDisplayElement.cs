using Terraria.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using ReLogic.Graphics;
using Terraria;
using System;

namespace DestinyMod.Content.UI.ItemDetails
{
    public class StatDisplayElement : UIElement
    {
        public string Name;

        public int Progress;

        public int MaxProgress = 100;

        public bool DisplayProgressBar;

        public event Action UpdateDataEvent;

        public StatDisplayElement(string name, int progress, int maxProgress = 100, bool displayProgressBar = true)
        {
            Name = name;
            MaxProgress = maxProgress;

            if (MaxProgress > 0)
            {
                Progress = Utils.Clamp(progress, 0, MaxProgress);
            }
            else if (Progress < 0)
            {
                Progress = 0;
            }

            DisplayProgressBar = displayProgressBar;
            Height.Pixels = 12;
        }

        public void UpdateData() => UpdateDataEvent?.Invoke();

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            Texture2D magicPixel = TextureAssets.MagicPixel.Value;
            Rectangle bgDestRect = GetDimensions().ToRectangle();

            if (DisplayProgressBar)
            {
                spriteBatch.Draw(magicPixel, bgDestRect, new Color(60, 60, 60));
                spriteBatch.Draw(magicPixel, new Rectangle(bgDestRect.X, bgDestRect.Y, (int)(bgDestRect.Width * ((float)Progress / MaxProgress)), bgDestRect.Height), Color.White);
            }

            DynamicSpriteFont mouseFont = FontAssets.MouseText.Value;
            float textScale = 0.66f;
            Vector2 nameSize = mouseFont.MeasureString(Name) * textScale;
            Vector2 namePos = new Vector2(bgDestRect.Left - nameSize.X - 4, bgDestRect.Y + bgDestRect.Height / 2);
            Vector2 nameOrigin = new Vector2(0, nameSize.Y / 2);
            spriteBatch.DrawString(mouseFont, Name, namePos, Color.White, 0f, nameOrigin, textScale, SpriteEffects.None, 0f);

            string stat = Progress.ToString();
            Vector2 statPos = new Vector2(bgDestRect.Right + 4, bgDestRect.Y + bgDestRect.Height / 2);
            Vector2 statSize = mouseFont.MeasureString(stat) * textScale;
            Vector2 statOrigin = new Vector2(0, statSize.Y / 2);
            spriteBatch.DrawString(mouseFont, stat, statPos, Color.White, 0f, statOrigin, textScale, SpriteEffects.None, 0f);
        }
    }
}
