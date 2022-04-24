using Terraria.GameContent.UI.Elements;
using Microsoft.Xna.Framework;
using DestinyMod.Core.UI;
using Terraria;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using ReLogic.Graphics;

namespace DestinyMod.Content.UI.ItemDetails
{
	public partial class ItemDetailsState : DestinyModUIState
	{
        public string MouseTitle;

        public string MouseSubtitle;

        public string MouseText;

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            Texture2D magicPixel = TextureAssets.MagicPixel.Value;

            int originX = (int)Main.MouseScreen.X;
            int originY = (int)Main.MouseScreen.Y;
            int mouseTextWidth = 360;
            int borderBuffer = 10;
            int buffer = 4;

            DynamicSpriteFont mouseFont = FontAssets.MouseText.Value;
            int yPos = originY + borderBuffer;
            int heightMeasurer = 2 * borderBuffer;

            string titleFormatted = string.Empty;
            float titleScale = 1.5f;
            int titleHeight = 0;
            if (!string.IsNullOrEmpty(MouseTitle))
            {
                titleFormatted = mouseFont.CreateWrappedText(MouseTitle, mouseTextWidth - 2 * buffer);
                titleHeight = (int)(mouseFont.MeasureString(titleFormatted).Y * titleScale) + buffer;
                heightMeasurer += titleHeight;
            }

            string subtitleFormatted = string.Empty;
            float subtitleScale = 1f;
            int subtitleHeight = 0;
            if (!string.IsNullOrEmpty(MouseSubtitle))
            {
                subtitleFormatted = mouseFont.CreateWrappedText(MouseSubtitle, mouseTextWidth - 2 * buffer);
                subtitleHeight = (int)(mouseFont.MeasureString(subtitleFormatted).Y * subtitleScale) + buffer;
                heightMeasurer += subtitleHeight;
            }

            int nameHeight = heightMeasurer;
            spriteBatch.Draw(magicPixel, new Rectangle(originX, originY, mouseTextWidth, heightMeasurer), Color.Black);

            string textFormatted = string.Empty;
            float textScale = 1f;
            int textHeight;
            if (!string.IsNullOrEmpty(MouseText))
            {
                textFormatted = mouseFont.CreateWrappedText(MouseText, mouseTextWidth - 2 * buffer);
                textHeight = (int)(mouseFont.MeasureString(textFormatted).Y * textScale) + buffer;
                heightMeasurer += textHeight;
            }

            Color col = Color.DarkGray * 0.1f;
            col.A = 255;
            spriteBatch.Draw(magicPixel, new Rectangle(originX, originY + nameHeight, mouseTextWidth, heightMeasurer - nameHeight), col);

            if (!string.IsNullOrEmpty(MouseTitle))
            {
                spriteBatch.DrawString(mouseFont, titleFormatted, new Vector2(originX + buffer, yPos), Color.WhiteSmoke, 0, Vector2.Zero, titleScale, SpriteEffects.None, 0f);
                yPos += titleHeight;
            }

            if (!string.IsNullOrEmpty(MouseSubtitle))
            {
                spriteBatch.DrawString(mouseFont, subtitleFormatted, new Vector2(originX + buffer, yPos), Color.WhiteSmoke, 0, Vector2.Zero, subtitleScale, SpriteEffects.None, 0f);
                yPos += (int)(subtitleHeight * 1.5f);
            }

            if (!string.IsNullOrEmpty(MouseText))
            {
                spriteBatch.DrawString(mouseFont, textFormatted, new Vector2(originX + buffer, yPos), Color.WhiteSmoke, 0, Vector2.Zero, textScale, SpriteEffects.None, 0f);
            }
        }
    }
}