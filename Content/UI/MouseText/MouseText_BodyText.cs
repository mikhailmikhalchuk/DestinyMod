using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria.UI;
using ReLogic.Graphics;

namespace DestinyMod.Content.UI.MouseText
{
	// Again, please feel free to rename this to anything better
	public partial class MouseText_BodyText : UIElement
	{
		public static DynamicSpriteFont MouseFont => FontAssets.MouseText.Value;

		public string Text { get; private set; }

		public float TextScale { get; private set; }

		public Vector2 TextSize { get; private set; }

		public Color Color { get; private set; }

		public MouseText_BodyText(int width, string text, Color? color = null, float scale = 1f)
		{
			Width.Pixels = width;
			UpdateData(text, color, scale);
		}

		// Exists to reduce the need to use MouseTextState's append to / remove from methods
		public void UpdateData(string text, Color? color = null, float scale = 1f)
		{
			if (Text == text && TextScale == scale)
            {
				return; // Lazy fix to avoid repeated call when subscribing to UIElement.OnMouseOver
            }

			int widthAdjusted = (int)Width.Pixels - MouseTextState.CommonBorder * 2;
			Text = MouseFont.CreateWrappedText(text, widthAdjusted * (1f / scale));
			TextScale = scale;
			TextSize = MouseFont.MeasureString(Text) * scale;
			Color = color.HasValue ? color.Value : Color.White;
			Height.Pixels = TextSize.Y;
			if (Height.Pixels > 0)
			{
				Height.Pixels += MouseTextState.CommonBorder;
			}
		}

		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			base.DrawSelf(spriteBatch);
			spriteBatch.DrawString(MouseFont, Text, GetDimensions().Position() + new Vector2(MouseTextState.CommonBorder), Color, 0f, Vector2.Zero, TextScale, SpriteEffects.None, 0f);
		}
	}
}
