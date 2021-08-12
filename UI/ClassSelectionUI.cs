using Terraria;
using Terraria.ID;
using Terraria.UI;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader.UI;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheDestinyMod.UI
{
	public class ClassSelectionUI : UIState
	{
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

			UIPanel wrapper = new UIPanel();
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

			UITextPanel<string> titanSelect = new UITextPanel<string>("Titan", 0.7f, true);
			titanSelect.HAlign = 0.5f;
			titanSelect.Top.Set(100, 0);
			titanSelect.Width.Set(200, 0f);
			titanSelect.Height.Set(50, 0);
            titanSelect.OnClick += TitanSelect;
            titanSelect.OnMouseOver += TitanSelect_OnMouseOver;
            titanSelect.OnMouseOut += TitanSelect_OnMouseOut;
			wrapper.Append(titanSelect);

			titanDescription = new UIText("");
			titanDescription.HAlign = 0.5f;
			titanDescription.Top.Set(170, 0);
			titanDescription.Width.Set(100, 0f);
			titanDescription.Height.Set(50, 0);
			wrapper.Append(titanDescription);

			UITextPanel<string> hunterSelect = new UITextPanel<string>("Hunter", 0.7f, true);
			hunterSelect.HAlign = 0.5f;
			hunterSelect.Top.Set(300, 0);
			hunterSelect.Width.Set(200, 0f);
			hunterSelect.Height.Set(50, 0);
			hunterSelect.OnClick += HunterSelect;
			hunterSelect.OnMouseOver += HunterSelect_OnMouseOver;
			hunterSelect.OnMouseOut += HunterSelect_OnMouseOut;
			wrapper.Append(hunterSelect);

			hunterDescription = new UIText("");
			hunterDescription.HAlign = 0.5f;
			hunterDescription.Top.Set(370, 0);
			hunterDescription.Width.Set(100, 0f);
			hunterDescription.Height.Set(50, 0);
			wrapper.Append(hunterDescription);

			UITextPanel<string> warlockSelect = new UITextPanel<string>("Warlock", 0.7f, true);
			warlockSelect.HAlign = 0.5f;
			warlockSelect.Top.Set(500, 0);
			warlockSelect.Width.Set(200, 0f);
			warlockSelect.Height.Set(50, 0);
			warlockSelect.OnClick += WarlockSelect;
            warlockSelect.OnMouseOver += WarlockSelect_OnMouseOver;
            warlockSelect.OnMouseOut += WarlockSelect_OnMouseOut;
			wrapper.Append(warlockSelect);

			warlockDescription = new UIText("");
			warlockDescription.HAlign = 0.5f;
			warlockDescription.Top.Set(570, 0);
			warlockDescription.Width.Set(100, 0f);
			warlockDescription.Height.Set(50, 0);
			wrapper.Append(warlockDescription);

			UITextPanel<string> returnToMenu = new UITextPanel<string>("Back", 0.7f, true);
			returnToMenu.HAlign = 0.5f;
			returnToMenu.Top.Set(1000f, 0f);
			returnToMenu.Width.Set(250, 0);
			returnToMenu.Height.Set(50, 0);
            returnToMenu.OnClick += ReturnToMenu;
            returnToMenu.OnMouseOver += ReturnToMenu_OnMouseOver;
            returnToMenu.OnMouseOut += ReturnToMenu_OnMouseOut;
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
			hunterDescription.SetText("Agile and daring, Hunters are quick on their feet and\nquicker on the draw");
			Main.PlaySound(SoundID.MenuTick);
			((UITextPanel<string>)evt.Target).BackgroundColor = UICommon.DefaultUIBlue;
		}

        private void WarlockSelect_OnMouseOut(UIMouseEvent evt, UIElement listeningElement) {
			warlockDescription.SetText("");
			((UITextPanel<string>)evt.Target).BackgroundColor = UICommon.DefaultUIBlueMouseOver;
		}

        private void WarlockSelect_OnMouseOver(UIMouseEvent evt, UIElement listeningElement) {
			warlockDescription.SetText("Warlocks weaponize the mysteries of the universe to\nsustain themselves and devastate their foes");
			Main.PlaySound(SoundID.MenuTick);
			((UITextPanel<string>)evt.Target).BackgroundColor = UICommon.DefaultUIBlue;
		}

        private void TitanSelect_OnMouseOut(UIMouseEvent evt, UIElement listeningElement) {
			titanDescription.SetText("");
			((UITextPanel<string>)evt.Target).BackgroundColor = UICommon.DefaultUIBlueMouseOver;
		}

        private void TitanSelect_OnMouseOver(UIMouseEvent evt, UIElement listeningElement) {
			titanDescription.SetText("Disciplined and proud, Titans are capable of both\naggressive assaults and stalwart defenses");
			Main.PlaySound(SoundID.MenuTick);
			((UITextPanel<string>)evt.Target).BackgroundColor = UICommon.DefaultUIBlue;
		}

		private void TitanSelect(UIMouseEvent evt, UIElement listeningElement) {
			Main.PlaySound(SoundID.MenuOpen);
			Main.menuMode = 2;
			DestinyPlayer.classAwaitingAssign = DestinyClassType.Titan;
        }

		private void WarlockSelect(UIMouseEvent evt, UIElement listeningElement) {
			Main.PlaySound(SoundID.MenuOpen);
			Main.menuMode = 2;
			DestinyPlayer.classAwaitingAssign = DestinyClassType.Warlock;
		}

		private void HunterSelect(UIMouseEvent evt, UIElement listeningElement) {
			Main.PlaySound(SoundID.MenuOpen);
			Main.menuMode = 2;
			DestinyPlayer.classAwaitingAssign = DestinyClassType.Hunter;
		}

		private void ReturnToMenu(UIMouseEvent evt, UIElement listeningElement) {
			Main.PlaySound(SoundID.MenuClose);
			Main.menuMode = 1;
        }
    }
}