using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;
using ReLogic.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DestinyMod.Content.UI.MouseText
{
	// Again, please feel free to rename this to anything better
	public class MouseText_LineText : MouseTextElement
	{
		public string Text { get; private set; }

		public float TextScale { get; private set; }

		public Vector2 TextSize { get; private set; }

		public override Color BackgroundColor_Default => new Color(10, 10, 10) * CommonOpacity;

		public MouseText_LineText(string text = null)
		{
			UpdateText(text ?? string.Empty);
			WidthPercentage = 1f;
			Height.Pixels = TextSize.Y + 6;
		}

		public void UpdateText(string newText)
		{
			Text = newText;
			TextScale = 1f;
			TextSize = MouseFont.MeasureString(Text) * TextScale;
		}

		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			base.DrawSelf(spriteBatch);

			CalculatedStyle dimensions = GetDimensions();

			Vector2 textPosition = dimensions.Position() + new Vector2(MouseTextState.CommonBorder, dimensions.Height / 2);
			spriteBatch.DrawString(MouseFont, Text, textPosition, Color.White, 0f, new Vector2(0, TextSize.Y / 2), TextScale, SpriteEffects.None, 0f);
		}
	}
}