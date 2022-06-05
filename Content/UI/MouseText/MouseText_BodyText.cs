using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;
using ReLogic.Graphics;

namespace DestinyMod.Content.UI.MouseText
{
	// Again, please feel free to rename this to anything better
	public class MouseText_BodyText : MouseTextElement
	{
        public override Color BackgroundColor_Default => new Color(20, 20, 20) * CommonOpacity;

		public string TextPreScale { get; private set; }

		public string Text { get; private set; }

		public float TextScale { get; private set; }

		public Vector2 TextSize { get; private set; }

		private Color TextColor_Internal = Color.White;

		public Color? TextColor
		{
			get => TextColor_Internal;
			set => TextColor_Internal = value == null ? Color.White : value.Value;
		}

		public MouseText_BodyText(string text, float scale = 1f)
		{
			WidthPercentage = 1f;
			TextPreScale = text;
			UpdateData(TextPreScale, scale);
		}

        // Exists to reduce the need to use MouseTextState's append to / remove from methods
        public void UpdateData(string text, float scale = 1f)
		{
			TextPreScale = text;
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
			spriteBatch.DrawString(MouseFont, Text, dimensions.Position() + new Vector2(MouseTextState.CommonBorder * 2, MouseTextState.CommonBorder), TextColor.Value, 0f, Vector2.Zero, TextScale, SpriteEffects.None, 0f);
		}
	}
}
