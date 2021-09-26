using Terraria;
using Terraria.ID;
using Terraria.UI;
using Terraria.UI.Gamepad;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader.UI;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace TheDestinyMod.UI
{
	public class ClassSelectionUI : UIState
	{
		private UIPanel wrapper;

		private UITextPanel<string> titanSelect;

		private UITextPanel<string> hunterSelect;

		private UITextPanel<string> warlockSelect;

		private UITextPanel<string> returnToMenu;

		private UIText titanDescription;

		private UIText warlockDescription;

		private UIText hunterDescription;

		public override void OnInitialize() {
			Width.Set(0f, 1f);
			Height.Set(-220f, 1f);
			Top.Set(0f, 0f);
			Left.Set(0f, 0f);
			HAlign = 0.5f;
			VAlign = 0.5f;

			wrapper = new UIPanel();
			wrapper.HAlign = 0.5f;
			wrapper.VAlign = 0.5f;
			wrapper.Width.Set(600f, 0f);
			wrapper.Height.Set(800f, 0f);
			wrapper.BackgroundColor = UICommon.MainPanelBackground;

			UIText selectClassText = new UIText("Select Class", 0.7f, true);
			selectClassText.HAlign = 0.5f;
			selectClassText.Top.Set(10f, 0);
			selectClassText.Width.Set(50, 0);
			selectClassText.Height.Set(30, 0);
			wrapper.Append(selectClassText);

			titanSelect = new UITextPanel<string>("Titan", 0.7f, true);
			titanSelect.HAlign = 0.5f;
			titanSelect.Top.Set(0, 0.14f);
			titanSelect.Width.Set(200, 0f);
			titanSelect.Height.Set(50, 0);
            titanSelect.OnClick += TitanSelect;
            titanSelect.OnMouseOver += TitanSelect_OnMouseOver;
            titanSelect.OnMouseOut += TitanSelect_OnMouseOut;
			titanSelect.SetSnapPoint("Titan", 0);
			wrapper.Append(titanSelect);

			titanDescription = new UIText("");
			titanDescription.HAlign = 0.5f;
			titanDescription.Top.Set(0, 0.22f);
			titanDescription.Width.Set(100, 0f);
			titanDescription.Height.Set(50, 0);
			wrapper.Append(titanDescription);

			hunterSelect = new UITextPanel<string>("Hunter", 0.7f, true);
			hunterSelect.HAlign = 0.5f;
			hunterSelect.Top.Set(0, 0.39f);
			hunterSelect.Width.Set(200, 0f);
			hunterSelect.Height.Set(50, 0);
			hunterSelect.OnClick += HunterSelect;
			hunterSelect.OnMouseOver += HunterSelect_OnMouseOver;
			hunterSelect.OnMouseOut += HunterSelect_OnMouseOut;
			hunterSelect.SetSnapPoint("Hunter", 0);
			wrapper.Append(hunterSelect);

			hunterDescription = new UIText("");
			hunterDescription.HAlign = 0.5f;
			hunterDescription.Top.Set(0, 0.47f);
			hunterDescription.Width.Set(100, 0f);
			hunterDescription.Height.Set(50, 0);
			wrapper.Append(hunterDescription);

			warlockSelect = new UITextPanel<string>("Warlock", 0.7f, true);
			warlockSelect.HAlign = 0.5f;
			warlockSelect.Top.Set(0, 0.65f);
			warlockSelect.Width.Set(200, 0f);
			warlockSelect.Height.Set(50, 0);
			warlockSelect.OnClick += WarlockSelect;
            warlockSelect.OnMouseOver += WarlockSelect_OnMouseOver;
            warlockSelect.OnMouseOut += WarlockSelect_OnMouseOut;
			warlockSelect.SetSnapPoint("Warlock", 0);
			wrapper.Append(warlockSelect);

			warlockDescription = new UIText("");
			warlockDescription.HAlign = 0.5f;
			warlockDescription.Top.Set(0, 0.73f);
			warlockDescription.Width.Set(100, 0f);
			warlockDescription.Height.Set(50, 0);
			wrapper.Append(warlockDescription);

			returnToMenu = new UITextPanel<string>("Back", 0.7f, true);
			returnToMenu.HAlign = 0.5f;
			returnToMenu.Top.Set(0, 0.86f);
			returnToMenu.Width.Set(250, 0);
			returnToMenu.Height.Set(50, 0);
            returnToMenu.OnClick += ReturnToMenu;
            returnToMenu.OnMouseOver += ReturnToMenu_OnMouseOver;
            returnToMenu.OnMouseOut += ReturnToMenu_OnMouseOut;
			returnToMenu.SetSnapPoint("Back", 0);
			Append(returnToMenu);

			Append(wrapper);
		}

        private void ReturnToMenu_OnMouseOut(UIMouseEvent evt, UIElement listeningElement) {
			((UITextPanel<string>)evt.Target).BackgroundColor = UICommon.DefaultUIBlueMouseOver;
		}

        private void ReturnToMenu_OnMouseOver(UIMouseEvent evt, UIElement listeningElement) {
			Main.PlaySound(SoundID.MenuTick);
			((UITextPanel<string>)evt.Target).BackgroundColor = UICommon.DefaultUIBlue;
		}

        private void HunterSelect_OnMouseOut(UIMouseEvent evt, UIElement listeningElement) {
			hunterDescription.SetText("");
			((UITextPanel<string>)evt.Target).BackgroundColor = UICommon.DefaultUIBlueMouseOver;
		}

        private void HunterSelect_OnMouseOver(UIMouseEvent evt, UIElement listeningElement) {
			if (Main.PendingResolutionHeight > 750)
				hunterDescription.SetText("Agile and daring, Hunters are quick on their feet and\nquicker on the draw");

			Main.PlaySound(SoundID.MenuTick);
			((UITextPanel<string>)evt.Target).BackgroundColor = UICommon.DefaultUIBlue;
		}

        private void WarlockSelect_OnMouseOut(UIMouseEvent evt, UIElement listeningElement) {
			warlockDescription.SetText("");
			((UITextPanel<string>)evt.Target).BackgroundColor = UICommon.DefaultUIBlueMouseOver;
		}

        private void WarlockSelect_OnMouseOver(UIMouseEvent evt, UIElement listeningElement) {
			if (Main.PendingResolutionHeight > 750)
				warlockDescription.SetText("Warlocks weaponize the mysteries of the universe to\nsustain themselves and devastate their foes");

			Main.PlaySound(SoundID.MenuTick);
			((UITextPanel<string>)evt.Target).BackgroundColor = UICommon.DefaultUIBlue;
		}

        private void TitanSelect_OnMouseOut(UIMouseEvent evt, UIElement listeningElement) {
			titanDescription.SetText("");
			((UITextPanel<string>)evt.Target).BackgroundColor = UICommon.DefaultUIBlueMouseOver;
		}

        private void TitanSelect_OnMouseOver(UIMouseEvent evt, UIElement listeningElement) {
			if (Main.PendingResolutionHeight > 750)
				titanDescription.SetText("Disciplined and proud, Titans are capable of both\naggressive assaults and stalwart defenses");

			Main.PlaySound(SoundID.MenuTick);
			((UITextPanel<string>)evt.Target).BackgroundColor = UICommon.DefaultUIBlue;
		}

		private void TitanSelect(UIMouseEvent evt, UIElement listeningElement) {
			Main.PlaySound(SoundID.MenuOpen);
			Main.menuMode = 2;
			DestinyPlayer.classAwaitingAssign = DestinyClassType.Titan;
			ModContent.GetInstance<TheDestinyMod>().classSelectionInterface?.SetState(null);
        }

		private void WarlockSelect(UIMouseEvent evt, UIElement listeningElement) {
			Main.PlaySound(SoundID.MenuOpen);
			Main.menuMode = 2;
			DestinyPlayer.classAwaitingAssign = DestinyClassType.Warlock;
			ModContent.GetInstance<TheDestinyMod>().classSelectionInterface?.SetState(null);
		}

		private void HunterSelect(UIMouseEvent evt, UIElement listeningElement) {
			Main.PlaySound(SoundID.MenuOpen);
			Main.menuMode = 2;
			DestinyPlayer.classAwaitingAssign = DestinyClassType.Hunter;
			ModContent.GetInstance<TheDestinyMod>().classSelectionInterface?.SetState(null);
		}

        public override void Draw(SpriteBatch spriteBatch) {
            base.Draw(spriteBatch);
			UILinkPointNavigator.Shortcuts.BackButtonCommand = 1;
			UILinkPointNavigator.SetPosition(3000, titanSelect.GetInnerDimensions().ToRectangle().Center.ToVector2());
			UILinkPointNavigator.SetPosition(3001, hunterSelect.GetInnerDimensions().ToRectangle().Center.ToVector2());
			UILinkPointNavigator.SetPosition(3002, warlockSelect.GetInnerDimensions().ToRectangle().Center.ToVector2());
			UILinkPointNavigator.SetPosition(3003, returnToMenu.GetInnerDimensions().ToRectangle().Center.ToVector2());
			UILinkPoint uiLinkPoint = UILinkPointNavigator.Points[3000];
			uiLinkPoint.Unlink();
			uiLinkPoint.Down = 3001;
			uiLinkPoint = UILinkPointNavigator.Points[3001];
			uiLinkPoint.Unlink();
			uiLinkPoint.Up = 3000;
			uiLinkPoint.Down = 3002;
			uiLinkPoint = UILinkPointNavigator.Points[3002];
			uiLinkPoint.Unlink();
			uiLinkPoint.Up = 3001;
            uiLinkPoint.Down = 3003;
			uiLinkPoint = UILinkPointNavigator.Points[3003];
			uiLinkPoint.Unlink();
			uiLinkPoint.Up = 3002;
		}

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);
			if (Terraria.GameInput.PlayerInput.Triggers.JustPressed.Inventory)
				ReturnToMenu();
		}

        private void ReturnToMenu(UIMouseEvent evt = null, UIElement listeningElement = null) {
			TheDestinyMod.Instance.Logger.Info(Main.menuMode);
			Main.PlaySound(SoundID.MenuClose);
			Main.menuMode = 1;
			ModContent.GetInstance<TheDestinyMod>().classSelectionInterface?.SetState(null);
		}
    }
}