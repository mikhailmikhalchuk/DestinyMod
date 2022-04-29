using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria.UI;
using ReLogic.Graphics;

namespace DestinyMod.Content.UI.MouseText
{
	// Again, please feel free to rename this to anything better
	public class MouseText_BodyText : UIElement
	{
		public static DynamicSpriteFont MouseFont => FontAssets.MouseText.Value;

		private Color BackgroundColor_Internal = new Color(20, 20, 20) * MouseTextState.CommonOpacity;

		public Color? BackgroundColor
		{
			get => BackgroundColor_Internal;
			set => BackgroundColor_Internal = (value == null ? value.Value : new Color(20, 20, 20) * MouseTextState.CommonOpacity);
		}

		public string Text { get; private set; }

		public float TextScale { get; private set; }

		public Vector2 TextSize { get; private set; }

		private Color TextColor_Internal = Color.White;

		public Color? TextColor
		{
			get => TextColor_Internal;
			set => TextColor_Internal = (value == null ? value.Value : Color.White);
		}

		public MouseText_BodyText(int width, string text, float scale = 1f)
		{
			Width.Pixels = width;
			UpdateData(text, scale);
		}

		// Exists to reduce the need to use MouseTextState's append to / remove from methods
		public void UpdateData(string text, float scale = 1f)
		{
			if (Text == text && TextScale == scale)
            {
				return; // Lazy fix to avoid repeated call when subscribing to UIElement.OnMouseOver
            }

			int widthAdjusted = (int)Width.Pixels - MouseTextState.CommonBorder * 2;
			Text = MouseFont.CreateWrappedText(text, widthAdjusted * (1f / scale));
			TextScale = scale;
			TextSize = MouseFont.MeasureString(Text) * scale;
			Height.Pixels = TextSize.Y;
			if (Height.Pixels > 0)
			{
				Height.Pixels += MouseTextState.CommonBorder;
			}
		}

		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			base.DrawSelf(spriteBatch);
			CalculatedStyle dimensions = GetDimensions();
			spriteBatch.Draw(TextureAssets.MagicPixel.Value, dimensions.ToRectangle(), BackgroundColor.Value);
			spriteBatch.DrawString(MouseFont, Text, dimensions.Position() + new Vector2(MouseTextState.CommonBorder * 2, MouseTextState.CommonBorder), TextColor.Value, 0f, Vector2.Zero, TextScale, SpriteEffects.None, 0f);
		}
	}
}
