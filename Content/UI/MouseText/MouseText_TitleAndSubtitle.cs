using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria.UI;
using ReLogic.Graphics;

namespace DestinyMod.Content.UI.MouseText
{
	// Please feel free to rename this to anything better
	public partial class MouseText_TitleAndSubtitle : UIElement
	{
		public static DynamicSpriteFont MouseFont => FontAssets.MouseText.Value;

		public string Title { get; private set; }

		public float TitleScale { get; private set; }

		public Vector2 TitleSize { get; private set; }

		public string Subtitle { get; private set; }

		public float SubtitleScale { get; private set; }

		public Vector2 SubtitleSize { get; private set; }

		public Color Color { get; private set; }

		public MouseText_TitleAndSubtitle(int width, string title, string subtitle, Color? color = null, float titleScale = 1.5f, float subtitleScale = 1f)
        {
			Width.Pixels = width;
			UpdateData(title, subtitle, color, titleScale, subtitleScale);
		}

		// Exists to reduce the need to use MouseTextState's append to / remove from methods
		public void UpdateData(string title, string subtitle, Color? color = null, float titleScale = 1.5f, float subtitleScale = 1f)
        {
			int widthAdjusted = (int)Width.Pixels - MouseTextState.CommonBorder * 2;

			if (Title != title || TitleScale != titleScale)
			{
				Title = MouseFont.CreateWrappedText(title, widthAdjusted * (1f / titleScale));
				TitleScale = titleScale;
				TitleSize = MouseFont.MeasureString(Title) * titleScale;
			}

			if (Subtitle != subtitle || SubtitleScale != subtitleScale)
			{
				Subtitle = MouseFont.CreateWrappedText(subtitle, widthAdjusted * (1f / subtitleScale));
				SubtitleScale = subtitleScale;
				SubtitleSize = MouseFont.MeasureString(Subtitle) * SubtitleScale;
			}

			Color = color.HasValue ? color.Value : Color.White;
			Height.Pixels = TitleSize.Y + SubtitleSize.Y;
			if (Height.Pixels > 0)
			{
				Height.Pixels += MouseTextState.CommonBorder + 2;
			}
		}

		protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);
			CalculatedStyle dimensions = GetDimensions();
			spriteBatch.DrawString(MouseFont, Title, dimensions.Position() + new Vector2(MouseTextState.CommonBorder), Color, 0f, Vector2.Zero, TitleScale, SpriteEffects.None, 0f);
			spriteBatch.DrawString(MouseFont, Subtitle, dimensions.Position() + new Vector2(MouseTextState.CommonBorder, TitleSize.Y + MouseTextState.CommonBorder + 2), Color, 0f, Vector2.Zero, SubtitleScale, SpriteEffects.None, 0f);
		}
    }
}