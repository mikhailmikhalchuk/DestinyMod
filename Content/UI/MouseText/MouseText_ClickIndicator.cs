using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria.UI;
using ReLogic.Graphics;
using Terraria.ModLoader;
using ReLogic.Content;

namespace DestinyMod.Content.UI.MouseText
{
	// Again, please feel free to rename this to anything better
	public class MouseText_ClickIndicator : UIElement
	{
		public static DynamicSpriteFont MouseFont => FontAssets.MouseText.Value;

		public string Text { get; private set; }

		public float TextScale { get; private set; }

		public Vector2 TextSize { get; private set; }

		private Color BackgroundColor_Internal = new Color(10, 10, 10) * MouseTextState.CommonOpacity;

		public Color? BackgroundColor
		{
			get => BackgroundColor_Internal;
			set => BackgroundColor_Internal = (value == null ? value.Value : new Color(10, 10, 10) * MouseTextState.CommonOpacity);
		}

		public Texture2D IndicatorGraphic { get; private set; }

		public static readonly int YBuffer = 8;

		public MouseText_ClickIndicator(string text = null)
		{
			UpdateText(text ?? "Apply");
			IndicatorGraphic = ModContent.Request<Texture2D>("DestinyMod/Assets/Textures/UI/LeftClick", AssetRequestMode.ImmediateLoad).Value;
			Height.Pixels = IndicatorGraphic.Height + YBuffer;
		}

		public void UpdateText(string newText)
        {
			Text = newText;
			TextScale = 0.8f;
			TextSize = MouseFont.MeasureString(Text) * TextScale;
		}

		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			Texture2D magicPixel = TextureAssets.MagicPixel.Value;
			CalculatedStyle dimensions = GetDimensions();
			spriteBatch.Draw(magicPixel, dimensions.ToRectangle(), BackgroundColor.Value);

			Vector2 textPosition = dimensions.Position() + new Vector2(dimensions.Width, dimensions.Height / 2) - new Vector2(TextSize.X + MouseTextState.CommonBorder, 0);
			spriteBatch.DrawString(MouseFont, Text, textPosition, Color.White, 0f, new Vector2(0, TextSize.Y / 2), TextScale, SpriteEffects.None, 0f);

			int yBufferHalved = YBuffer / 2;
			spriteBatch.Draw(IndicatorGraphic, new Vector2(textPosition.X - IndicatorGraphic.Width - yBufferHalved, dimensions.Y + yBufferHalved), Color.White);
		}
	}
}
