using Terraria;
using Terraria.UI;
using Terraria.GameContent.UI.Elements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using DestinyMod.Common.ModPlayers;
using Terraria.Localization;

namespace DestinyMod.Content.UI.ClassSelection
{
	public class ClassSelectionUI : UIElement
	{
		public Player Player;

		public UITextPanel<LocalizedText> CreateButton;

		public UISlicedImage DescriptionBackground;

		public UIText Description;

		public ClassOption Titan;

		public ClassOption Hunter;

		public ClassOption Warlock;

		public ClassSelectionUI(Player player, UITextPanel<LocalizedText> createButton)
		{
			Player = player;
			CreateButton = createButton;

			DescriptionBackground = new UISlicedImage(Main.Assets.Request<Texture2D>("Images/UI/CharCreation/CategoryPanelHighlight"))
			{
				HAlign = 1f,
				VAlign = 1f,
				Left = StyleDimension.FromPixels(-4),
				Top = StyleDimension.FromPixels(4),
				Width = StyleDimension.FromPixelsAndPercent(-8, 0.6f),
				Height = StyleDimension.FromPixelsAndPercent(0, 1)
			};
			DescriptionBackground.SetSliceDepths(10);
			DescriptionBackground.Color = Color.LightGray * 0.7f;
			Append(DescriptionBackground);

			Description = new UIText("Select a class")
			{
				HAlign = 0.5f,
				Top = StyleDimension.FromPixelsAndPercent(0, 0.1f),
				Width = StyleDimension.FromPercent(1f),
				Height = StyleDimension.FromPercent(1f),
				TextOriginY = 0.5f,
				IsWrapped = true,
			};
			Description.PaddingLeft = 20f;
			Description.PaddingRight = 20f;
			DescriptionBackground.Append(Description);

			Titan = new ClassOption(Player, DestinyClassType.Titan, CreateButton)
			{
				Left = StyleDimension.FromPixels(2),
				Top = StyleDimension.FromPixels(4),
				Width = StyleDimension.FromPixelsAndPercent(-4, 0.4f),
				Height = StyleDimension.FromPixelsAndPercent(-2, 0.333f)
			};
			Append(Titan);

			Hunter = new ClassOption(Player, DestinyClassType.Hunter, CreateButton)
			{
				Left = StyleDimension.FromPixels(2),
				Top = StyleDimension.FromPixelsAndPercent(6, 0.333f),
				Width = StyleDimension.FromPixelsAndPercent(-3, 0.4f),
				Height = StyleDimension.FromPixelsAndPercent(-2, 0.333f)
			};
			Append(Hunter);

			Warlock = new ClassOption(Player, DestinyClassType.Warlock, CreateButton)
			{
				Left = StyleDimension.FromPixels(2),
				Top = StyleDimension.FromPixelsAndPercent(8, 0.666f),
				Width = StyleDimension.FromPixelsAndPercent(-3, 0.4f),
				Height = StyleDimension.FromPixelsAndPercent(-2f, 0.333f)
			};
			Append(Warlock);
		}

		public override void Update(GameTime gameTime)
		{
			static string GetDescription(DestinyClassType classType)
			{
				switch (classType)
				{
					case DestinyClassType.Titan:
						return "Disciplined and proud, Titans are capable of both aggressive assaults and stalwart defenses";

					case DestinyClassType.Hunter:
						return "Agile and daring, Hunters are quick on their feet and quicker on the draw";

					case DestinyClassType.Warlock:
						return "Warlocks weaponize the mysteries of the universe to sustain themselves and devastate their foes";

					case DestinyClassType.None:
					default:
						return "Select a class to proceed";
				}
			}

			if (Titan.ContainsPoint(Main.MouseScreen))
			{
				Description.SetText(GetDescription(DestinyClassType.Titan));
				return;
			}

			if (Hunter.ContainsPoint(Main.MouseScreen))
			{
				Description.SetText(GetDescription(DestinyClassType.Hunter));
				return;
			}

			if (Warlock.ContainsPoint(Main.MouseScreen))
			{
				Description.SetText(GetDescription(DestinyClassType.Warlock));
				return;
			}

			Description.SetText(GetDescription(Player.GetModPlayer<ClassPlayer>().ClassType));
		}
	}
}