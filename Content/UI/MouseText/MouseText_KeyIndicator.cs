﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;
using ReLogic.Graphics;
using Terraria.ModLoader;
using ReLogic.Content;
using Terraria;
using Microsoft.Xna.Framework.Input;

namespace DestinyMod.Content.UI.MouseText
{
	// Again, please feel free to rename this to anything better
	public class MouseText_KeyIndicator : MouseTextElement
	{
		public string Text { get; private set; }

		public float TextScale { get; private set; }

		public Vector2 TextSize { get; private set; }

		public override Color BackgroundColor_Default => new Color(10, 10, 10) * CommonOpacity;

		public Texture2D IndicatorGraphic { get; private set; }

		private Keys InternalKey;

		public Keys Key
		{
			get => InternalKey;
			set
			{
				InternalKey = value;
			}
		}

		public MouseText_KeyIndicator(string text = null, Keys key = Keys.F)
		{
			UpdateText(text ?? string.Empty);
			IndicatorGraphic = ModContent.Request<Texture2D>("DestinyMod/Assets/Textures/UI/Key", AssetRequestMode.ImmediateLoad).Value;
			Key = key;
			WidthPercentage = 1f;
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
			CalculatedStyle dimensions = GetDimensions();

			Vector2 textPosition = dimensions.Position() + new Vector2(dimensions.Width, dimensions.Height / 2) - new Vector2(TextSize.X + MouseTextState.CommonBorder, 0);
			spriteBatch.DrawString(MouseFont, Text, textPosition, Color.White, 0f, new Vector2(0, TextSize.Y / 2), TextScale, SpriteEffects.None, 0f);

			Vector2 indicatorGraphicPosition = new Vector2(textPosition.X - IndicatorGraphic.Width - 4, dimensions.Y + dimensions.Height / 2);
			spriteBatch.Draw(IndicatorGraphic, indicatorGraphicPosition, null, Color.White, 0f, IndicatorGraphic.Size() / 2, 1f, SpriteEffects.None, 0f);

			string keyText = Key.ToString();
			spriteBatch.DrawString(MouseFont, keyText, indicatorGraphicPosition, Color.White, 0f, (MouseFont.MeasureString(keyText) * TextScale) / 2, TextScale, SpriteEffects.None, 0f);
		}
	}
}