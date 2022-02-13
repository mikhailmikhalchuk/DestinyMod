using Terraria;
using Terraria.ID;
using Terraria.UI;
using Terraria.UI.Gamepad;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;
using DestinyMod.Core.UI;
using Terraria.GameInput;

namespace DestinyMod.Content.UI.ClassSelection
{
	public class ClassSelectionUI : DestinyModUIState
	{
		public UIPanel Wrapper;

		public UIText Title;

		public ClassOption Titan;

		public ClassOption Hunter;

		public ClassOption Warlock;

		public UITextPanel<string> Back;

		public override void PreLoad(ref string name)
		{
			AutoSetState = false;
			AutoAddHandler = true;
		}

		public override UIHandler Load() => new UIHandler(UserInterface, string.Empty, LayerName);

		public override void OnInitialize()
		{
			Width.Percent = 1f;
			Height.Set(-220f, 1f);
			HAlign = 0.5f;
			VAlign = 0.5f;

			Wrapper = new UIPanel
			{
				HAlign = 0.5f,
				VAlign = 0.5f
			};
			Wrapper.Width.Pixels = 600;
			Wrapper.Height.Pixels = 800;
			Wrapper.BackgroundColor = UICommon.MainPanelBackground;

			Title = new UIText("Select Class", 0.7f, true)
			{
				HAlign = 0.5f
			};
			Title.Top.Pixels = 10;
			Title.Width.Pixels = 50;
			Title.Height.Pixels = 30;
			Wrapper.Append(Title);

			Titan = new ClassOption(DestinyClassType.Titan, "Disciplined and proud, Titans are capable of both"
				+ "\naggressive assaults and stalwart defenses");
			Titan.Top.Percent = 0.14f;
			Wrapper.Append(Titan);

			Hunter = new ClassOption(DestinyClassType.Hunter, "Agile and daring, Hunters are quick on their feet and"
				+ "\nquicker on the draw");
			Hunter.Top.Percent = 0.39f;
			Wrapper.Append(Hunter);

			Hunter = new ClassOption(DestinyClassType.Warlock, "Warlocks weaponize the mysteries of the universe to"
				+ "\nsustain themselves and devastate their foes");
			Hunter.Top.Percent = 0.65f;
			Wrapper.Append(Hunter);

			Back = new UITextPanel<string>("Back", 0.7f, true)
			{
				HAlign = 0.5f
			};
			Back.Top.Percent = 0.86f;
			Back.Width.Pixels = 250;
			Back.Height.Pixels = 50;
			Back.OnClick += ReturnToMenu;
			Back.OnMouseOver += ReturnToMenu_OnMouseOver;
			Back.OnMouseOut += ReturnToMenu_OnMouseOut;
			Back.SetSnapPoint("Back", 0);
			Wrapper.Append(Back);

			Append(Wrapper);
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);

			if (PlayerInput.Triggers.JustPressed.Inventory)
			{
				ReturnToMenu();
			}
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);

			UILinkPointNavigator.Shortcuts.BackButtonCommand = 1;
			UILinkPointNavigator.SetPosition(3000, Titan.Select.GetInnerDimensions().Center());
			UILinkPointNavigator.SetPosition(3001, Hunter.Select.GetInnerDimensions().Center());
			UILinkPointNavigator.SetPosition(3002, Warlock.Select.GetInnerDimensions().Center());
			UILinkPointNavigator.SetPosition(3003, Back.GetInnerDimensions().Center());

			for (int index = 3000; index <= 3003; index++)
			{
				UILinkPoint uiLinkPoint = UILinkPointNavigator.Points[index];
				uiLinkPoint.Unlink();

				int previous = index - 1;
				if (previous >= 3000)
				{
					uiLinkPoint.Up = previous;
				}

				int next = index + 1;
				if (next <= 3003)
				{
					uiLinkPoint.Down = next;
				}
			}
		}

		private void ReturnToMenu(UIMouseEvent evt = null, UIElement listeningElement = null)
		{
			DestinyMod.Instance.Logger.Info(Main.menuMode);
			SoundEngine.PlaySound(SoundID.MenuClose);
			Main.menuMode = 1;
			UIHandler.Interface.SetState(null);
		}

		private void ReturnToMenu_OnMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(SoundID.MenuTick);
			Back.BackgroundColor = UICommon.DefaultUIBlue;
		}

		private void ReturnToMenu_OnMouseOut(UIMouseEvent evt, UIElement listeningElement) => Back.BackgroundColor = UICommon.DefaultUIBlueMouseOver;
	}
}