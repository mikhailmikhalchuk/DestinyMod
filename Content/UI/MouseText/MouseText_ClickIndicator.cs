using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria.UI;
using ReLogic.Graphics;
using Terraria.ModLoader;
using ReLogic.Content;
using Terraria;

namespace DestinyMod.Content.UI.MouseText
{
	// Again, please feel free to rename this to anything better
	public class MouseText_ClickIndicator : UIElement
	{
		public enum ClickTypes
        {
			Left,
			Middle,
			Right,
        }

		public static DynamicSpriteFont MouseFont => FontAssets.MouseText.Value;

		public string Text { get; private set; }

		public float TextScale { get; private set; }

		public Vector2 TextSize { get; private set; }

		private Color BackgroundColor_Internal = new Color(10, 10, 10) * MouseTextState.CommonOpacity;

		public Color? BackgroundColor
		{
			get => BackgroundColor_Internal;
			set => BackgroundColor_Internal = value == null ? new Color(10, 10, 10) * MouseTextState.CommonOpacity : value.Value;
		}


		private ClickTypes InternalClickType;

		public ClickTypes ClickType
        {
			get => InternalClickType;
			set
            {
				InternalClickType = value;
				string indicatorGraphicPath = "DestinyMod/Assets/Textures/UI/" + InternalClickType.ToString() + "Click";
				IndicatorGraphic = ModContent.Request<Texture2D>(indicatorGraphicPath, AssetRequestMode.ImmediateLoad).Value;
			}
        }


		public Texture2D IndicatorGraphic { get; private set; }

		public MouseText_ClickIndicator(string text = null, ClickTypes clickType = ClickTypes.Left)
		{
			UpdateText(text ?? "Apply");
			ClickType = clickType;
			Height.Pixels = 32;
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
			spriteBatch.Draw(IndicatorGraphic, new Vector2(textPosition.X - IndicatorGraphic.Width - 4, dimensions.Y + dimensions.Height / 2), null, Color.White, 0f, IndicatorGraphic.Size() / 2, 1f, SpriteEffects.None, 0f );
		}
	}
}
