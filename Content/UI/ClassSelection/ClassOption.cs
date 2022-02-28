using Terraria;
using Terraria.UI;
using Terraria.GameContent.UI.Elements;
using Terraria.Audio;
using DestinyMod.Common.ModPlayers;
using ReLogic.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Terraria.Localization;

namespace DestinyMod.Content.UI.ClassSelection
{
	public class ClassOption : UIElement
	{
		public Player Player;

		public DestinyClassType ClassType;

		public UITextPanel<LocalizedText> CreateButton;

		public Asset<Texture2D> BaseTexture;

		public Asset<Texture2D> SelectedBorderTexture;

		public Asset<Texture2D> HoveredBorderTexture;

		public Color Color;

		public bool Hovered;

		public bool SoundedHovered;

		public ClassOption(Player player, DestinyClassType classType, UITextPanel<LocalizedText> createButton)
		{
			Player = player;
			ClassType = classType;
			CreateButton = createButton;

			BaseTexture = Main.Assets.Request<Texture2D>("Images/UI/CharCreation/PanelGrayscale");
			SelectedBorderTexture = Main.Assets.Request<Texture2D>("Images/UI/CharCreation/CategoryPanelHighlight");
			HoveredBorderTexture = Main.Assets.Request<Texture2D>("Images/UI/CharCreation/CategoryPanelBorder");

			switch (ClassType)
			{
				case DestinyClassType.Titan:
					Color = Color.Red;
					break;

				case DestinyClassType.Hunter:
					Color = Color.Aqua;
					break;

				case DestinyClassType.Warlock:
					Color = Color.Gold;
					break;
			}

			UIText element = new UIText(ClassType.ToString(), 0.9f)
			{
				HAlign = 0.5f,
				VAlign = 0.5f,
				Width = StyleDimension.FromPixelsAndPercent(-10f, 1f),
				Top = StyleDimension.FromPixels(0),
				TextOriginY = 0.5f
			};
			Append(element);
		}

		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			if (Hovered)
			{
				if (!SoundedHovered)
				{
					SoundEngine.PlaySound(12);
				}

				SoundedHovered = true;
			}
			else
			{
				SoundedHovered = false;
			}

			CalculatedStyle dimensions = GetDimensions();
			Utils.DrawSplicedPanel(spriteBatch, BaseTexture.Value, (int)dimensions.X, (int)dimensions.Y, (int)dimensions.Width, (int)dimensions.Height, 10, 10, 10, 10, Color.Lerp(Color.Black, Color, 0.8f) * 0.5f);
			if (ClassType == Player.GetModPlayer<ClassPlayer>().ClassType)
			{
				Utils.DrawSplicedPanel(spriteBatch, BaseTexture.Value, (int)dimensions.X + 5, (int)dimensions.Y + 5, (int)dimensions.Width - 10, (int)dimensions.Height - 10, 10, 10, 10, 10, Color.Lerp(Color, Color.White, 0.7f) * 0.5f);
			}

			if (Hovered)
			{
				Utils.DrawSplicedPanel(spriteBatch, HoveredBorderTexture.Value, (int)dimensions.X, (int)dimensions.Y, (int)dimensions.Width, (int)dimensions.Height, 10, 10, 10, 10, Color.White);
			}
		}

		public override void MouseDown(UIMouseEvent evt)
		{
			base.MouseDown(evt);
			Player.GetModPlayer<ClassPlayer>().ClassType = ClassType;
			CreateButton.BackgroundColor = new Color(63, 82, 151) * 0.8f;
			SoundEngine.PlaySound(12);
		}

		public override void MouseOver(UIMouseEvent evt)
		{
			base.MouseOver(evt);
			Hovered = true;
		}

		public override void MouseOut(UIMouseEvent evt)
		{
			base.MouseOut(evt);
			Hovered = false;
		}
	}
}