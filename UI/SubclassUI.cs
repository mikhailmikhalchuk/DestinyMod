using Terraria;
using Terraria.ID;
using Terraria.UI;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Localization;
using Microsoft.Xna.Framework;

namespace TheDestinyMod.UI
{
    internal class SubclassUI : UIState
    {
		public UIElement subclass;

		/// <summary>
		/// 0: Arc<br></br>
		/// 1: Solar<br></br>
		/// 2: Void
		/// </summary>
		public int element;

		/// <summary>
		/// From top, bottom, right:<br></br>
		/// 1-3: Arc subclasses<br></br>
		/// 4-6: Solar subclasses<br></br>
		/// 7-9: Void subclasses
		/// </summary>
		public int selectedSubclass;

		public Texture2D selectionOneTexture = ModContent.GetTexture("TheDestinyMod/UI/SelectionIArc");
		public Texture2D selectionTwoTexture = ModContent.GetTexture("TheDestinyMod/UI/SelectionIIArc");
		public Texture2D selectionThreeTexture = ModContent.GetTexture("TheDestinyMod/UI/SelectionIIIArc");
		public Texture2D elementalBurnTexture = ModContent.GetTexture("TheDestinyMod/UI/ElementalBurnArc");
		public Texture2D borderTexture = ModContent.GetTexture("TheDestinyMod/UI/ElementalBurnArcBorder");

		private UIHoverImageButton selectionOneButton;
		private UIHoverImageButton selectionTwoButton;
		private UIHoverImageButton selectionThreeButton;
		private UIHoverImageButton elementalBurnButton;
		private UIImage border;
		private UIImage selected;
		private UIImage selectedSub;

        public override void OnInitialize() {
			subclass = new UIElement();
			subclass.SetPadding(0);
			subclass.Left.Set(350, 0f);
			subclass.Top.Set(20, 0f);
			subclass.Width.Set(400, 0f);
			subclass.Height.Set(300, 0f);

			Texture2D selectedTexture = ModContent.GetTexture("TheDestinyMod/UI/Selected");
			
			selectionOneButton = new UIHoverImageButton(selectionOneTexture, Language.GetTextValue("LegacyMisc.53"));
			selectionOneButton.Left.Set(280, 0f);
			selectionOneButton.Top.Set(29, 0f);
			selectionOneButton.Width.Set(48, 0f);
			selectionOneButton.Height.Set(48, 0f);
			selectionOneButton.OnClick += new MouseEvent(SelectionOneClicked);
			subclass.Append(selectionOneButton);
			
			selectionTwoButton = new UIHoverImageButton(selectionTwoTexture, Language.GetTextValue("LegacyMisc.53"));
			selectionTwoButton.Left.Set(280, 0f);
			selectionTwoButton.Top.Set(86, 0f);
			selectionTwoButton.Width.Set(40, 0f);
			selectionTwoButton.Height.Set(40, 0f);
			selectionTwoButton.OnClick += new MouseEvent(SelectionTwoClicked);
			subclass.Append(selectionTwoButton);
			
			selectionThreeButton = new UIHoverImageButton(selectionThreeTexture, Language.GetTextValue("LegacyMisc.53"));
			selectionThreeButton.Left.Set(308, 0f);
			selectionThreeButton.Top.Set(57, 0f);
			selectionThreeButton.Width.Set(48, 0f);
			selectionThreeButton.Height.Set(48, 0f);
			selectionThreeButton.OnClick += new MouseEvent(SelectionThreeClicked);
			subclass.Append(selectionThreeButton);
			
			elementalBurnButton = new UIHoverImageButton(elementalBurnTexture, "Switch");
			elementalBurnButton.Left.Set(252, 0f);
			elementalBurnButton.Top.Set(57, 0f);
			elementalBurnButton.Width.Set(58, 0f);
			elementalBurnButton.Height.Set(58, 0f);
			elementalBurnButton.OnClick += new MouseEvent(ElementalBurnClicked);
			elementalBurnButton.OnRightClick += new MouseEvent(ElementalBurnRightClicked);
			subclass.Append(elementalBurnButton);

			border = new UIImage(borderTexture);
			border.Left.Set(239, 0f);
			border.Top.Set(16, 0f);
			border.Width.Set(139, 0f);
			border.Height.Set(139, 0f);
			subclass.Append(border);

			selected = new UIImage(selectedTexture);
			selected.Left.Set(252, 0f);
			selected.Top.Set(57, 0f);
			selected.Width.Set(48, 0f);
			selected.Height.Set(48, 0f);

			selectedSub = new UIImage(selectedTexture);
			selectedSub.Left.Set(280, 0f);
			selectedSub.Top.Set(29, 0f);
			selectedSub.Width.Set(48, 0f);
			selectedSub.Height.Set(48, 0f);

			Append(subclass);
		}

        public override void Update(GameTime gameTime) {
			base.Update(gameTime);

			if (elementalBurnButton.IsMouseHovering || selectionOneButton.IsMouseHovering || selectionTwoButton.IsMouseHovering || selectionThreeButton.IsMouseHovering) {
				Main.LocalPlayer.mouseInterface = true;
			}
        }

        private void SelectionOneClicked(UIMouseEvent evt, UIElement listeningElement) {
			if (!Main.playerInventory) {
				return;
			}
			switch (element) {
				case 0:
					selectedSubclass = 1;
					break;
				case 1:
					selectedSubclass = 4;
					break;
				case 2:
					selectedSubclass = 7;
					break;
			}
			ChangeTextures();
			Main.PlaySound(SoundID.MenuTick);
		}

		private void SelectionTwoClicked(UIMouseEvent evt, UIElement listeningElement) {
			if (!Main.playerInventory) {
				return;
			}
			switch (element) {
				case 0:
					selectedSubclass = 2;
					break;
				case 1:
					selectedSubclass = 5;
					break;
				case 2:
					selectedSubclass = 8;
					break;
			}
			ChangeTextures();
			Main.PlaySound(SoundID.MenuTick);
		}

		private void SelectionThreeClicked(UIMouseEvent evt, UIElement listeningElement) {
			if (!Main.playerInventory) {
				return;
			}
			switch (element) {
				case 0:
					selectedSubclass = 3;
					break;
				case 1:
					selectedSubclass = 6;
					break;
				case 2:
					selectedSubclass = 9;
					break;
			}
			ChangeTextures();
			Main.PlaySound(SoundID.MenuTick);
		}

		private void ElementalBurnClicked(UIMouseEvent evt, UIElement listeningElement) {
			if (!Main.playerInventory) {
				return;
			}
			if (NPC.AnyDanger() && !Main.dedServ) {
				Main.NewText("Cannot change subclass while a boss is alive!", new Color(255, 0, 0));
				return;
			}
			switch (element) {
				case 0:
					element++;
					selectionOneTexture = ModContent.GetTexture("TheDestinyMod/UI/SelectionISolar");
					selectionTwoTexture = ModContent.GetTexture("TheDestinyMod/UI/SelectionIISolar");
					selectionThreeTexture = ModContent.GetTexture("TheDestinyMod/UI/SelectionIIISolar");
					elementalBurnTexture = ModContent.GetTexture("TheDestinyMod/UI/ElementalBurnSolar");
					borderTexture = ModContent.GetTexture("TheDestinyMod/UI/ElementalBurnSolarBorder");
					break;
				case 1:
					element++;
					selectionOneTexture = ModContent.GetTexture("TheDestinyMod/UI/SelectionIVoid");
					selectionTwoTexture = ModContent.GetTexture("TheDestinyMod/UI/SelectionIIVoid");
					selectionThreeTexture = ModContent.GetTexture("TheDestinyMod/UI/SelectionIIIVoid");
					elementalBurnTexture = ModContent.GetTexture("TheDestinyMod/UI/ElementalBurnVoid");
					break;
				case 2:
					element = 0;
					selectionOneTexture = ModContent.GetTexture("TheDestinyMod/UI/SelectionIArc");
					selectionTwoTexture = ModContent.GetTexture("TheDestinyMod/UI/SelectionIIArc");
					selectionThreeTexture = ModContent.GetTexture("TheDestinyMod/UI/SelectionIIIArc");
					elementalBurnTexture = ModContent.GetTexture("TheDestinyMod/UI/ElementalBurnArc");
					borderTexture = ModContent.GetTexture("TheDestinyMod/UI/ElementalBurnArcBorder");
					break;
			}
			ChangeTextures();
			Main.PlaySound(SoundID.MenuTick);
		}

        private void ElementalBurnRightClicked(UIMouseEvent evt, UIElement listeningElement) {
			if (!Main.playerInventory) {
				return;
			}
			if (NPC.AnyDanger() && !Main.dedServ) {
				Main.NewText("Cannot change subclass while a boss is alive!", new Color(255, 0, 0));
				return;
			}
			switch (element) {
				case 0:
					element = 2;
					selectionOneTexture = ModContent.GetTexture("TheDestinyMod/UI/SelectionIVoid");
					selectionTwoTexture = ModContent.GetTexture("TheDestinyMod/UI/SelectionIIVoid");
					selectionThreeTexture = ModContent.GetTexture("TheDestinyMod/UI/SelectionIIIVoid");
					elementalBurnTexture = ModContent.GetTexture("TheDestinyMod/UI/ElementalBurnVoid");
					break;
				case 1:
					element--;
                    selectionOneTexture = ModContent.GetTexture("TheDestinyMod/UI/SelectionIArc");
                    selectionTwoTexture = ModContent.GetTexture("TheDestinyMod/UI/SelectionIIArc");
                    selectionThreeTexture = ModContent.GetTexture("TheDestinyMod/UI/SelectionIIIArc");
                    elementalBurnTexture = ModContent.GetTexture("TheDestinyMod/UI/ElementalBurnArc");
					borderTexture = ModContent.GetTexture("TheDestinyMod/UI/ElementalBurnArcBorder");
					break;
				case 2:
					element--;
					selectionOneTexture = ModContent.GetTexture("TheDestinyMod/UI/SelectionISolar");
					selectionTwoTexture = ModContent.GetTexture("TheDestinyMod/UI/SelectionIISolar");
					selectionThreeTexture = ModContent.GetTexture("TheDestinyMod/UI/SelectionIIISolar");
					elementalBurnTexture = ModContent.GetTexture("TheDestinyMod/UI/ElementalBurnSolar");
					borderTexture = ModContent.GetTexture("TheDestinyMod/UI/ElementalBurnSolarBorder");
					break;
			}
			ChangeTextures();
            Main.PlaySound(SoundID.MenuTick);
        }

		public void ChangeTextures() {
			selectedSub.Remove();
			selected.Remove();
			selected.Left.Set(252, 0f);
			selected.Top.Set(57, 0f);
			selected.Width.Set(48, 0f);
			selected.Height.Set(48, 0f);
			if (selectedSubclass == 1 && element == 0 || selectedSubclass == 4 && element == 1 || selectedSubclass == 7 && element == 2) {
				selectedSub.Left.Set(280, 0f);
				selectedSub.Top.Set(29, 0f);
				selectedSub.Width.Set(48, 0f);
				selectedSub.Height.Set(48, 0f);
				subclass.Append(selectedSub);
				subclass.Append(selected);
			}
			else if (selectedSubclass == 2 && element == 0 || selectedSubclass == 5 && element == 1 || selectedSubclass == 8 && element == 2) {
				selectedSub.Left.Set(280, 0f);
				selectedSub.Top.Set(86, 0f);
				selectedSub.Width.Set(48, 0f);
				selectedSub.Height.Set(48, 0f);
				subclass.Append(selectedSub);
				subclass.Append(selected);
			}
			else if (selectedSubclass == 3 && element == 0 || selectedSubclass == 6 && element == 1 || selectedSubclass == 9 && element == 2) {
				selectedSub.Left.Set(308, 0f);
				selectedSub.Top.Set(57, 0f);
				selectedSub.Width.Set(48, 0f);
				selectedSub.Height.Set(48, 0f);
				subclass.Append(selectedSub);
				subclass.Append(selected);
			}
			selectionOneButton.SetImage(selectionOneTexture);
			selectionTwoButton.SetImage(selectionTwoTexture);
			selectionThreeButton.SetImage(selectionThreeTexture);
			elementalBurnButton.SetImage(elementalBurnTexture);
			border.SetImage(borderTexture);
			selectionOneButton.Width.Set(48, 0f);
			selectionOneButton.Height.Set(48, 0f);
			selectionTwoButton.Width.Set(40, 0f);
			selectionTwoButton.Height.Set(40, 0f);
			selectionThreeButton.Width.Set(48, 0f);
			selectionThreeButton.Height.Set(48, 0f);
			elementalBurnButton.Width.Set(58, 0f);
			elementalBurnButton.Height.Set(58, 0f);
		}
	}

	internal class UIHoverImageButton : UIImageButton
	{
		internal string HoverText;

		public UIHoverImageButton(Texture2D texture, string hoverText) : base(texture) {
			HoverText = hoverText;
		}

		protected override void DrawSelf(SpriteBatch spriteBatch) {
			base.DrawSelf(spriteBatch);

			if (IsMouseHovering) {
				Main.hoverItemName = HoverText;
			}
		}

        public override void MouseOver(UIMouseEvent evt) {
			if (Main.playerInventory) {
				base.MouseOver(evt);
			}
        }
    }
}