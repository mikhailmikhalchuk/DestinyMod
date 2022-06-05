using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;
using ReLogic.Graphics;

namespace DestinyMod.Content.UI.MouseText
{
	// Please feel free to rename this to anything better
	public class MouseText_TitleAndSubtitle : MouseTextElement
	{
		public override Color BackgroundColor_Default => new Color(68, 70, 74) * CommonOpacity;

		public string TitlePreScale { get; private set; }

		public string Title { get; private set; }

		public float TitleScale { get; private set; }

		public Vector2 TitleSize { get; private set; }

		public string SubtitlePreScale { get; private set; }

		public string Subtitle { get; private set; }

		public float SubtitleScale { get; private set; }

		public Vector2 SubtitleSize { get; private set; }

		private Color TextColor_Internal = Color.White;

		public Color? TextColor
		{
			get => TextColor_Internal;
			set => TextColor_Internal = value == null ? Color.White : value.Value;
		}

		public MouseText_TitleAndSubtitle(string title, string subtitle, float titleScale = 1.5f, float subtitleScale = 1f)
        {
			WidthPercentage = 1f;
			TitlePreScale = title;
			SubtitlePreScale = subtitle;
			UpdateData(TitlePreScale, SubtitlePreScale, titleScale, subtitleScale);
		}

        // Exists to reduce the need to use MouseTextState's append to / remove from methods
        public void UpdateData(string title, string subtitle, float titleScale = 1.5f, float subtitleScale = 1f)
        {
			int widthAdjusted = (int)Width.Pixels - MouseTextState.CommonBorder * 2;

			TitlePreScale = title;
			Title = MouseFont.CreateWrappedText(TitlePreScale, widthAdjusted * (1f / titleScale));
			TitleScale = titleScale;
			TitleSize = MouseFont.MeasureString(Title) * titleScale;

			SubtitlePreScale = subtitle;
			Subtitle = MouseFont.CreateWrappedText(SubtitlePreScale, widthAdjusted * (1f / subtitleScale));
			SubtitleScale = subtitleScale;
			SubtitleSize = MouseFont.MeasureString(Subtitle) * SubtitleScale;

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
			spriteBatch.DrawString(MouseFont, Title, dimensions.Position() + new Vector2(MouseTextState.CommonBorder * 2, MouseTextState.CommonBorder), TextColor.Value, 0f, Vector2.Zero, TitleScale, SpriteEffects.None, 0f);
			spriteBatch.DrawString(MouseFont, Subtitle, dimensions.Position() + new Vector2(MouseTextState.CommonBorder * 2, TitleSize.Y + MouseTextState.CommonBorder + 2), TextColor.Value, 0f, Vector2.Zero, SubtitleScale, SpriteEffects.None, 0f);
		}
    }
}