using DestinyMod.Common.Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.Graphics;
using System;
using Terraria.GameContent;
using Terraria.ModLoader;
using Terraria.UI;

namespace DestinyMod.Content.UI.MouseText
{
    /// <summary>Used for mouseover info objective requirements, such as that seen on catalysts.</summary>
    public class MouseText_Objective : MouseTextElement
    {
        public string Name { get; private set; }

        // Progress?
        public float Progress { get; private set; }

        public Texture2D CheckboxTexture { get; private set; }

        public MouseText_Objective(string name, float progress)
        {
            Name = name;
            Progress = progress;
            Height.Pixels = 20;

            CheckboxTexture = ModContent.Request<Texture2D>("DestinyMod/Assets/Textures/UI/Checkbox", AssetRequestMode.ImmediateLoad).Value;
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            Texture2D magicPixel = TextureAssets.MagicPixel.Value;
            CalculatedStyle dimensions = GetDimensions();

            Color backgroundColor = new Color(10, 10, 10) * CommonOpacity;
            Color completionColor = DestinyColors.MouseTextGreen * CommonOpacity;
            spriteBatch.Draw(magicPixel, new Rectangle((int)dimensions.X, (int)dimensions.Y, CheckboxTexture.Width, CheckboxTexture.Height), Progress >= 1 ? completionColor : backgroundColor);

            Color textColor = Color.White * CommonOpacity;
            spriteBatch.Draw(CheckboxTexture, new Rectangle((int)dimensions.X, (int)dimensions.Y, CheckboxTexture.Width, CheckboxTexture.Height), Progress >= 1 ? completionColor : textColor);

            int textBorderX = CheckboxTexture.Width + 4;
            spriteBatch.Draw(magicPixel, new Rectangle((int)dimensions.X + textBorderX, (int)dimensions.Y, (int)(dimensions.Width - textBorderX), CheckboxTexture.Height), backgroundColor);

            if (Progress < 1)
            {
                spriteBatch.Draw(magicPixel, new Rectangle((int)dimensions.X + textBorderX, (int)dimensions.Y, (int)((dimensions.Width - textBorderX) * Progress), CheckboxTexture.Height), completionColor);
            }

            float textScale = 0.8f;
            Vector2 nameOrigin = new Vector2(0, MouseFont.MeasureString(Name).Y * textScale / 2);
            spriteBatch.DrawString(MouseFont, Name, new Vector2(dimensions.X + textBorderX + 4, dimensions.Y + dimensions.Height / 2), textColor, 0f, nameOrigin, textScale, SpriteEffects.None, 0f);

            string progress = Math.Clamp(Math.Floor(Progress * 100), 0, 100) + "%";
            Vector2 progressSize = MouseFont.MeasureString(progress) * textScale;
            Vector2 progressOrigin = new Vector2(progressSize.X, progressSize.Y / 2);
            spriteBatch.DrawString(MouseFont, progress, new Vector2(dimensions.X + dimensions.Width - progressSize.X - 4, dimensions.Y + dimensions.Height / 2), textColor, 0f, progressOrigin, textScale, SpriteEffects.None, 0f);
        }
    }
}
