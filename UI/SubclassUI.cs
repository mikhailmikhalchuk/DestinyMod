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
		public UIPanel subclass;

		/// <summary>
		/// 0: Arc<br></br>
		/// 1: Solar<br></br>
		/// 2: Void
		/// </summary>
		public int element;

		public int fragmentChoice1;
		public int fragmentChoice2;
		public int fragmentChoice3;
		public int fragmentChoice4;

		public int abilityChoice1;
		public int abilityChoice2;
		public int abilityChoice3;
		public int abilityChoice4;

		public Texture2D selectionOneTexture = ModContent.GetTexture("TheDestinyMod/UI/SelectionIArc");
		public Texture2D selectionTwoTexture = ModContent.GetTexture("TheDestinyMod/UI/SelectionIIArc");
		public Texture2D selectionThreeTexture = ModContent.GetTexture("TheDestinyMod/UI/SelectionIIIArc");
		public Texture2D elementalBurnTexture = ModContent.GetTexture("TheDestinyMod/UI/ArcSubclassIcon");
		public Texture2D borderTexture = ModContent.GetTexture("TheDestinyMod/UI/ElementalBurnArcBorder");

		public UIImageButton fragmentSubchoice1;
		public UIImageButton fragmentSubchoice2;
		public UIImageButton fragmentSubchoice3;
		public UIImageButton fragmentSubchoice4;
		public UIImageButton fragmentSubchoice5;
		public UIImageButton fragmentSubchoice6;

		public UIImageButton abilitySubchoice1;
		public UIImageButton abilitySubchoice2;
		public UIImageButton abilitySubchoice3;
		public UIImageButton abilitySubchoice4;

		public UIImageButton fragment1;
		public UIImageButton fragment2;
		public UIImageButton fragment3;
		public UIImageButton fragment4;

		public UIImageButton ability1;
		public UIImageButton ability2;
		public UIImageButton ability3;
		public UIImageButton ability4;

		public UIElement fragmentSubchoiceContainer;
		public UIElement abilitySubchoiceContainer;

		public UIPanel mouseInfo;

		private int _lastHoveredFragment;

		public override void OnInitialize() {
			subclass = new UIPanel();
			subclass.SetPadding(0);
			subclass.Left.Set(1100, 0f);
			subclass.Top.Set(300, 0f);
			subclass.Width.Set(600, 0f);
			subclass.Height.Set(400, 0f);
			subclass.BackgroundColor = Terraria.ModLoader.UI.UICommon.MainPanelBackground;

			UIImage elementalBurn = new UIImage(elementalBurnTexture);
			elementalBurn.Left.Set(10, 0f);
			elementalBurn.Top.Set(10, 0f);
			elementalBurn.Width.Set(76, 0f);
			elementalBurn.Height.Set(76, 0f);
			subclass.Append(elementalBurn);

			Texture2D fragmentTexture = ModContent.GetTexture("Terraria/UI/CharCreation/CategoryPanel");
			fragmentSubchoiceContainer = new UIElement();
			fragmentSubchoiceContainer.Left.Set(150, 0f);
			fragmentSubchoiceContainer.Top.Set(280, 0f);
			fragmentSubchoiceContainer.Width.Set(400, 0f);
			fragmentSubchoiceContainer.Height.Set(60, 0f);
			fragmentSubchoiceContainer.OnMouseOut += FragmentSubchoiceContainer_OnMouseOut;

			abilitySubchoiceContainer = new UIElement();
			abilitySubchoiceContainer.Left.Set(150, 0f);
			abilitySubchoiceContainer.Top.Set(130, 0f);
			abilitySubchoiceContainer.Width.Set(400, 0f);
			abilitySubchoiceContainer.Height.Set(60, 0f);
			abilitySubchoiceContainer.OnMouseOut += AbilitySubchoiceContainer_OnMouseOut;

			fragmentSubchoice1 = new UIImageButton(fragmentTexture);
			fragmentSubchoice1.Left.Set(0, 0f);
			fragmentSubchoice1.Top.Set(20, 0f);
			fragmentSubchoice1.Width.Set(44, 0f);
			fragmentSubchoice1.Height.Set(44, 0f);
            fragmentSubchoice1.OnClick += FragmentSubchoice1_OnClick;
			fragmentSubchoiceContainer.Append(fragmentSubchoice1);

			fragmentSubchoice2 = new UIImageButton(fragmentTexture);
			fragmentSubchoice2.Left.Set(50, 0f);
			fragmentSubchoice2.Top.Set(20, 0f);
			fragmentSubchoice2.Width.Set(44, 0f);
			fragmentSubchoice2.Height.Set(44, 0f);
			fragmentSubchoice2.OnClick += FragmentSubchoice2_OnClick;
			fragmentSubchoiceContainer.Append(fragmentSubchoice2);

			fragmentSubchoice3 = new UIImageButton(fragmentTexture);
			fragmentSubchoice3.Left.Set(100, 0f);
			fragmentSubchoice3.Top.Set(20, 0f);
			fragmentSubchoice3.Width.Set(44, 0f);
			fragmentSubchoice3.Height.Set(44, 0f);
			fragmentSubchoice3.OnClick += FragmentSubchoice3_OnClick;
			fragmentSubchoiceContainer.Append(fragmentSubchoice3);

			fragmentSubchoice4 = new UIImageButton(fragmentTexture);
			fragmentSubchoice4.Left.Set(150, 0f);
			fragmentSubchoice4.Top.Set(20, 0f);
			fragmentSubchoice4.Width.Set(44, 0f);
			fragmentSubchoice4.Height.Set(44, 0f);
			fragmentSubchoice4.OnClick += FragmentSubchoice4_OnClick;
			fragmentSubchoiceContainer.Append(fragmentSubchoice4);

			fragmentSubchoice5 = new UIImageButton(fragmentTexture);
			fragmentSubchoice5.Left.Set(200, 0f);
			fragmentSubchoice5.Top.Set(20, 0f);
			fragmentSubchoice5.Width.Set(44, 0f);
			fragmentSubchoice5.Height.Set(44, 0f);
			fragmentSubchoice5.OnClick += FragmentSubchoice5_OnClick;
			fragmentSubchoiceContainer.Append(fragmentSubchoice5);

			fragmentSubchoice6 = new UIImageButton(fragmentTexture);
			fragmentSubchoice6.Left.Set(250, 0f);
			fragmentSubchoice6.Top.Set(20, 0f);
			fragmentSubchoice6.Width.Set(44, 0f);
			fragmentSubchoice6.Height.Set(44, 0f);
			fragmentSubchoice6.OnClick += FragmentSubchoice6_OnClick;
			fragmentSubchoiceContainer.Append(fragmentSubchoice6);

			abilitySubchoice1 = new UIImageButton(fragmentTexture);
			abilitySubchoice1.Left.Set(0, 0f);
			abilitySubchoice1.Top.Set(20, 0f);
			abilitySubchoice1.Width.Set(44, 0f);
			abilitySubchoice1.Height.Set(44, 0f);
			abilitySubchoice1.OnClick += AbilitySubchoice1_OnClick;
			abilitySubchoiceContainer.Append(abilitySubchoice1);

			abilitySubchoice2 = new UIImageButton(fragmentTexture);
			abilitySubchoice2.Left.Set(50, 0f);
			abilitySubchoice2.Top.Set(20, 0f);
			abilitySubchoice2.Width.Set(44, 0f);
			abilitySubchoice2.Height.Set(44, 0f);
			abilitySubchoice2.OnClick += AbilitySubchoice2_OnClick;
			abilitySubchoiceContainer.Append(abilitySubchoice2);

			abilitySubchoice3 = new UIImageButton(fragmentTexture);
			abilitySubchoice3.Left.Set(100, 0f);
			abilitySubchoice3.Top.Set(20, 0f);
			abilitySubchoice3.Width.Set(44, 0f);
			abilitySubchoice3.Height.Set(44, 0f);
			abilitySubchoice3.OnClick += AbilitySubchoice3_OnClick;
			abilitySubchoiceContainer.Append(abilitySubchoice3);

			abilitySubchoice4 = new UIImageButton(fragmentTexture);
			abilitySubchoice4.Left.Set(150, 0f);
			abilitySubchoice4.Top.Set(20, 0f);
			abilitySubchoice4.Width.Set(44, 0f);
			abilitySubchoice4.Height.Set(44, 0f);
			abilitySubchoice4.OnClick += AbilitySubchoice4_OnClick;
			abilitySubchoiceContainer.Append(abilitySubchoice4);

			mouseInfo = new UIPanel();
			mouseInfo.Left.Set(0, 1f);
			mouseInfo.Top.Set(0, 1f);
			mouseInfo.Width.Set(250, 0f);
			mouseInfo.Height.Set(300, 0f);

			fragment1 = new UIImageButton(fragmentTexture);
			fragment1.Left.Set(150, 0f);
			fragment1.Top.Set(250, 0f);
			fragment1.Width.Set(44, 0f);
			fragment1.Height.Set(44, 0f);
            fragment1.OnMouseOver += Fragment1_OnMouseOver;
            fragment1.OnMouseOut += Fragment1_OnMouseOut;
			subclass.Append(fragment1);

			fragment2 = new UIImageButton(fragmentTexture);
			fragment2.Left.Set(200, 0f);
			fragment2.Top.Set(250, 0f);
			fragment2.Width.Set(44, 0f);
			fragment2.Height.Set(44, 0f);
			fragment2.OnMouseOver += Fragment2_OnMouseOver;
			fragment2.OnMouseOut += Fragment2_OnMouseOut;
			subclass.Append(fragment2);

			fragment3 = new UIImageButton(fragmentTexture);
			fragment3.Left.Set(250, 0f);
			fragment3.Top.Set(250, 0f);
			fragment3.Width.Set(44, 0f);
			fragment3.Height.Set(44, 0f);
			fragment3.OnMouseOver += Fragment3_OnMouseOver;
			fragment3.OnMouseOut += Fragment3_OnMouseOut;
			subclass.Append(fragment3);

			fragment4 = new UIImageButton(fragmentTexture);
			fragment4.Left.Set(300, 0f);
			fragment4.Top.Set(250, 0f);
			fragment4.Width.Set(44, 0f);
			fragment4.Height.Set(44, 0f);
			fragment4.OnMouseOver += Fragment4_OnMouseOver;
			fragment4.OnMouseOut += Fragment4_OnMouseOut;
			subclass.Append(fragment4);

			ability1 = new UIImageButton(fragmentTexture);
			ability1.Left.Set(150, 0f);
			ability1.Top.Set(100, 0f);
			ability1.Width.Set(44, 0f);
			ability1.Height.Set(44, 0f);
            ability1.OnMouseOver += Ability1_OnMouseOver;
            ability1.OnMouseOut += Ability1_OnMouseOut;
			subclass.Append(ability1);

			ability2 = new UIImageButton(fragmentTexture);
			ability2.Left.Set(200, 0f);
			ability2.Top.Set(100, 0f);
			ability2.Width.Set(44, 0f);
			ability2.Height.Set(44, 0f);
			ability2.OnMouseOver += Ability2_OnMouseOver;
			ability2.OnMouseOut += Ability2_OnMouseOut;
			subclass.Append(ability2);

			ability3 = new UIImageButton(fragmentTexture);
			ability3.Left.Set(250, 0f);
			ability3.Top.Set(100, 0f);
			ability3.Width.Set(44, 0f);
			ability3.Height.Set(44, 0f);
			ability3.OnMouseOver += Ability3_OnMouseOver;
			ability3.OnMouseOut += Ability3_OnMouseOut;
			subclass.Append(ability3);

			ability4 = new UIImageButton(fragmentTexture);
			ability4.Left.Set(300, 0f);
			ability4.Top.Set(100, 0f);
			ability4.Width.Set(44, 0f);
			ability4.Height.Set(44, 0f);
			ability4.OnMouseOver += Ability4_OnMouseOver;
			ability4.OnMouseOut += Ability4_OnMouseOut;
			subclass.Append(ability4);

			Append(subclass);

			Append(mouseInfo);
		}

        private void Ability1_OnMouseOut(UIMouseEvent evt, UIElement listeningElement) {
			if (!abilitySubchoiceContainer.GetDimensions().ToRectangle().Contains(Main.MouseScreen.ToPoint())) {
				abilitySubchoiceContainer.Append(abilitySubchoice3);
				abilitySubchoiceContainer.Append(abilitySubchoice4);
				subclass.RemoveChild(abilitySubchoiceContainer);
			}
		}

		private void Ability2_OnMouseOut(UIMouseEvent evt, UIElement listeningElement) {
			if (!abilitySubchoiceContainer.GetDimensions().ToRectangle().Contains(Main.MouseScreen.ToPoint())) {
				abilitySubchoiceContainer.Append(abilitySubchoice3);
				abilitySubchoiceContainer.Append(abilitySubchoice4);
				subclass.RemoveChild(abilitySubchoiceContainer);
			}
		}

		private void Ability3_OnMouseOut(UIMouseEvent evt, UIElement listeningElement) {
			if (!abilitySubchoiceContainer.GetDimensions().ToRectangle().Contains(Main.MouseScreen.ToPoint())) {
				abilitySubchoiceContainer.Append(abilitySubchoice2);
				abilitySubchoiceContainer.Append(abilitySubchoice3);
				abilitySubchoiceContainer.Append(abilitySubchoice4);
				subclass.RemoveChild(abilitySubchoiceContainer);
			}
		}

		private void Ability4_OnMouseOut(UIMouseEvent evt, UIElement listeningElement) {
			if (!abilitySubchoiceContainer.GetDimensions().ToRectangle().Contains(Main.MouseScreen.ToPoint())) {
				abilitySubchoiceContainer.Append(abilitySubchoice2);
				abilitySubchoiceContainer.Append(abilitySubchoice3);
				abilitySubchoiceContainer.Append(abilitySubchoice4);
				subclass.RemoveChild(abilitySubchoiceContainer);
			}
		}

		private void Ability1_OnMouseOver(UIMouseEvent evt, UIElement listeningElement) {
			abilitySubchoiceContainer.RemoveChild(abilitySubchoice3);
			abilitySubchoiceContainer.RemoveChild(abilitySubchoice4);
			subclass.Append(abilitySubchoiceContainer);
			RemoveFragmentSubchoice();
		}

		private void Ability2_OnMouseOver(UIMouseEvent evt, UIElement listeningElement) {
			abilitySubchoiceContainer.RemoveChild(abilitySubchoice3);
			abilitySubchoiceContainer.RemoveChild(abilitySubchoice4);
			subclass.Append(abilitySubchoiceContainer);
			RemoveFragmentSubchoice();
		}

		private void Ability3_OnMouseOver(UIMouseEvent evt, UIElement listeningElement) {
			abilitySubchoiceContainer.RemoveChild(abilitySubchoice2);
			abilitySubchoiceContainer.RemoveChild(abilitySubchoice3);
			abilitySubchoiceContainer.RemoveChild(abilitySubchoice4);
			subclass.Append(abilitySubchoiceContainer);
			RemoveFragmentSubchoice();
		}

		private void Ability4_OnMouseOver(UIMouseEvent evt, UIElement listeningElement) {
			abilitySubchoiceContainer.RemoveChild(abilitySubchoice2);
			abilitySubchoiceContainer.RemoveChild(abilitySubchoice3);
			abilitySubchoiceContainer.RemoveChild(abilitySubchoice4);
			subclass.Append(abilitySubchoiceContainer);
			RemoveFragmentSubchoice();
		}

		private void ApplyFragmentTo(Texture2D tex, int num) {
			Main.PlaySound(SoundID.Unlock);
			if (_lastHoveredFragment == 1) {
				subclass.RemoveChild(fragment1);
				fragment1 = new UIImageButton(tex);
				fragment1.Left.Set(150, 0f);
				fragment1.Top.Set(250, 0f);
				fragment1.Width.Set(44, 0f);
				fragment1.Height.Set(44, 0f);
				fragment1.OnMouseOver += Fragment1_OnMouseOver;
				fragment1.OnMouseOut += Fragment1_OnMouseOut;
				subclass.Append(fragment1);
				fragmentChoice1 = num;
			}
			if (_lastHoveredFragment == 2) {
				subclass.RemoveChild(fragment2);
				fragment2 = new UIImageButton(tex);
				fragment2.Left.Set(200, 0f);
				fragment2.Top.Set(250, 0f);
				fragment2.Width.Set(44, 0f);
				fragment2.Height.Set(44, 0f);
				fragment2.OnMouseOver += Fragment2_OnMouseOver;
				fragment2.OnMouseOut += Fragment2_OnMouseOut;
				subclass.Append(fragment2);
				fragmentChoice2 = num;
			}
			if (_lastHoveredFragment == 3) {
				subclass.RemoveChild(fragment3);
				fragment3 = new UIImageButton(tex);
				fragment3.Left.Set(250, 0f);
				fragment3.Top.Set(250, 0f);
				fragment3.Width.Set(44, 0f);
				fragment3.Height.Set(44, 0f);
				fragment3.OnMouseOver += Fragment3_OnMouseOver;
				fragment3.OnMouseOut += Fragment3_OnMouseOut;
				subclass.Append(fragment3);
				fragmentChoice3 = num;
			}
			if (_lastHoveredFragment == 4) {
				subclass.RemoveChild(fragment4);
				fragment4 = new UIImageButton(tex);
				fragment4.Left.Set(300, 0f);
				fragment4.Top.Set(250, 0f);
				fragment4.Width.Set(44, 0f);
				fragment4.Height.Set(44, 0f);
				fragment4.OnMouseOver += Fragment4_OnMouseOver;
				fragment4.OnMouseOut += Fragment4_OnMouseOut;
				subclass.Append(fragment4);
				fragmentChoice4 = num;
			}
		}

		private void AbilitySubchoice1_OnClick(UIMouseEvent evt, UIElement listeningElement) {
			if (AnyAbilityChosenOfType(1))
				return;
			Texture2D abilityTexture = ModContent.GetTexture("Terraria/UI/CharCreation/CategoryPanel");
			ApplyFragmentTo(abilityTexture, 1);
		}

		private void AbilitySubchoice2_OnClick(UIMouseEvent evt, UIElement listeningElement) {
			if (AnyAbilityChosenOfType(2))
				return;
			Texture2D abilityTexture = ModContent.GetTexture("Terraria/UI/CharCreation/CategoryPanel");
			ApplyFragmentTo(abilityTexture, 2);
		}

		private void AbilitySubchoice3_OnClick(UIMouseEvent evt, UIElement listeningElement) {
			if (AnyAbilityChosenOfType(3))
				return;
			Texture2D abilityTexture = ModContent.GetTexture("Terraria/UI/CharCreation/CategoryPanel");
			ApplyFragmentTo(abilityTexture, 3);
		}

		private void AbilitySubchoice4_OnClick(UIMouseEvent evt, UIElement listeningElement) {
			if (AnyAbilityChosenOfType(4))
				return;
			Texture2D abilityTexture = ModContent.GetTexture("Terraria/UI/CharCreation/CategoryPanel");
			ApplyFragmentTo(abilityTexture, 4);
		}

		private void FragmentSubchoice1_OnClick(UIMouseEvent evt, UIElement listeningElement) {
			if (AnyFragmentChosenOfType(1))
				return;
			Texture2D fragmentTexture = ModContent.GetTexture("Terraria/UI/CharCreation/CategoryPanel");
			ApplyFragmentTo(fragmentTexture, 1);
        }

		private void FragmentSubchoice2_OnClick(UIMouseEvent evt, UIElement listeningElement) {
			if (AnyFragmentChosenOfType(2))
				return;
			Texture2D fragmentTexture = ModContent.GetTexture("Terraria/UI/CharCreation/CategoryPanel");
			ApplyFragmentTo(fragmentTexture, 2);
		}

		private void FragmentSubchoice3_OnClick(UIMouseEvent evt, UIElement listeningElement) {
			if (AnyFragmentChosenOfType(3))
				return;
			Texture2D fragmentTexture = ModContent.GetTexture("Terraria/UI/CharCreation/CategoryPanel");
			ApplyFragmentTo(fragmentTexture, 3);
		}

		private void FragmentSubchoice4_OnClick(UIMouseEvent evt, UIElement listeningElement) {
			if (AnyFragmentChosenOfType(4))
				return;
			Texture2D fragmentTexture = ModContent.GetTexture("Terraria/UI/CharCreation/CategoryPanel");
			ApplyFragmentTo(fragmentTexture, 4);
		}

		private void FragmentSubchoice5_OnClick(UIMouseEvent evt, UIElement listeningElement) {
			if (AnyFragmentChosenOfType(5))
				return;
			Texture2D fragmentTexture = ModContent.GetTexture("Terraria/UI/CharCreation/CategoryPanel");
			ApplyFragmentTo(fragmentTexture, 5);
		}

		private void FragmentSubchoice6_OnClick(UIMouseEvent evt, UIElement listeningElement) {
			if (AnyFragmentChosenOfType(6))
				return;
			Texture2D fragmentTexture = ModContent.GetTexture("Terraria/UI/CharCreation/CategoryPanel");
			ApplyFragmentTo(fragmentTexture, 6);
		}

		private void Fragment1_OnMouseOut(UIMouseEvent evt, UIElement listeningElement) {
			if (!fragmentSubchoiceContainer.GetDimensions().ToRectangle().Contains(Main.MouseScreen.ToPoint())) {
				subclass.RemoveChild(fragmentSubchoiceContainer);
			}
			_lastHoveredFragment = 1;
        }

		private void Fragment2_OnMouseOut(UIMouseEvent evt, UIElement listeningElement) {
			if (!fragmentSubchoiceContainer.GetDimensions().ToRectangle().Contains(Main.MouseScreen.ToPoint())) {
				subclass.RemoveChild(fragmentSubchoiceContainer);
			}
			_lastHoveredFragment = 2;
		}

		private void Fragment3_OnMouseOut(UIMouseEvent evt, UIElement listeningElement) {
			if (!fragmentSubchoiceContainer.GetDimensions().ToRectangle().Contains(Main.MouseScreen.ToPoint())) {
				subclass.RemoveChild(fragmentSubchoiceContainer);
			}
			_lastHoveredFragment = 3;
		}

		private void Fragment4_OnMouseOut(UIMouseEvent evt, UIElement listeningElement) {
			if (!fragmentSubchoiceContainer.GetDimensions().ToRectangle().Contains(Main.MouseScreen.ToPoint())) {
				subclass.RemoveChild(fragmentSubchoiceContainer);
			}
			_lastHoveredFragment = 4;
		}

		private void RestoreAndRemoveAbilitySubchoice() {
			if (!subclass.HasChild(abilitySubchoiceContainer))
				return;
			if (!abilitySubchoiceContainer.HasChild(abilitySubchoice2))
				abilitySubchoiceContainer.Append(abilitySubchoice2);
			if (!abilitySubchoiceContainer.HasChild(abilitySubchoice3))
				abilitySubchoiceContainer.Append(abilitySubchoice3);
			if (!abilitySubchoiceContainer.HasChild(abilitySubchoice4))
				abilitySubchoiceContainer.Append(abilitySubchoice4);
			subclass.RemoveChild(abilitySubchoiceContainer);
		}

		private void RemoveFragmentSubchoice() {
			if (!subclass.HasChild(fragmentSubchoiceContainer))
				return;
			subclass.RemoveChild(fragmentSubchoiceContainer);
		}

		private void Fragment1_OnMouseOver(UIMouseEvent evt, UIElement listeningElement) {
			subclass.Append(fragmentSubchoiceContainer);
			RestoreAndRemoveAbilitySubchoice();
		}

		private void Fragment2_OnMouseOver(UIMouseEvent evt, UIElement listeningElement) {
			subclass.Append(fragmentSubchoiceContainer);
			RestoreAndRemoveAbilitySubchoice();
		}

		private void Fragment3_OnMouseOver(UIMouseEvent evt, UIElement listeningElement) {
			subclass.Append(fragmentSubchoiceContainer);
			RestoreAndRemoveAbilitySubchoice();
		}

		private void Fragment4_OnMouseOver(UIMouseEvent evt, UIElement listeningElement) {
			subclass.Append(fragmentSubchoiceContainer);
			RestoreAndRemoveAbilitySubchoice();
		}

		private void FragmentSubchoiceContainer_OnMouseOut(UIMouseEvent evt, UIElement listeningElement) {
			if (!fragmentSubchoice1.ContainsPoint(Main.MouseScreen) && !fragmentSubchoice2.ContainsPoint(Main.MouseScreen) && !fragmentSubchoice3.ContainsPoint(Main.MouseScreen) && !fragmentSubchoice4.ContainsPoint(Main.MouseScreen) && !fragmentSubchoice5.ContainsPoint(Main.MouseScreen) && !fragmentSubchoice6.ContainsPoint(Main.MouseScreen) && !fragmentSubchoiceContainer.ContainsPoint(Main.MouseScreen)) {
				subclass.RemoveChild(fragmentSubchoiceContainer);
			}
		}

		private void AbilitySubchoiceContainer_OnMouseOut(UIMouseEvent evt, UIElement listeningElement) {
			void ActualExecute() {
				if (!abilitySubchoiceContainer.HasChild(abilitySubchoice2))
					abilitySubchoiceContainer.Append(abilitySubchoice2);
				if (!abilitySubchoiceContainer.HasChild(abilitySubchoice3))
					abilitySubchoiceContainer.Append(abilitySubchoice3);
				if (!abilitySubchoiceContainer.HasChild(abilitySubchoice4))
					abilitySubchoiceContainer.Append(abilitySubchoice4);
				subclass.RemoveChild(abilitySubchoiceContainer);
			}
			if (!abilitySubchoiceContainer.HasChild(abilitySubchoice2) && !abilitySubchoice1.ContainsPoint(Main.MouseScreen) && !abilitySubchoiceContainer.ContainsPoint(Main.MouseScreen)) {
				ActualExecute();
			}
			else if (!abilitySubchoiceContainer.HasChild(abilitySubchoice3) && !abilitySubchoice1.ContainsPoint(Main.MouseScreen) && !abilitySubchoice2.ContainsPoint(Main.MouseScreen) && !abilitySubchoiceContainer.ContainsPoint(Main.MouseScreen)) {
				ActualExecute();
			}
			else if (!abilitySubchoiceContainer.HasChild(abilitySubchoice4) && !abilitySubchoice1.ContainsPoint(Main.MouseScreen) && !abilitySubchoice2.ContainsPoint(Main.MouseScreen) && !abilitySubchoice3.ContainsPoint(Main.MouseScreen) && !abilitySubchoiceContainer.ContainsPoint(Main.MouseScreen)) {
				ActualExecute();
			}
		}

		private bool AnyFragmentChosenOfType(int type) => fragmentChoice1 == type || fragmentChoice2 == type || fragmentChoice3 == type || fragmentChoice4 == type;

		private bool AnyAbilityChosenOfType(int type) => abilityChoice1 == type || abilityChoice2 == type || abilityChoice3 == type || abilityChoice4 == type;

		private void DrawFragmentArc(SpriteBatch spriteBatch, CalculatedStyle dimensions) {
			if (fragmentSubchoice1.ContainsPoint(Main.MouseScreen) && subclass.HasChild(fragmentSubchoiceContainer)) {
				mouseInfo.Left.Set(Main.MouseScreen.X + 5, 0f);
				mouseInfo.Top.Set(Main.MouseScreen.Y + 5, 0f);
				Utils.DrawBorderStringFourWay(spriteBatch, TheDestinyMod.fontFuturaBold, "Fragment 1", dimensions.X + 10, dimensions.Y + 10, Color.White, Color.Transparent, Vector2.Zero, 0.4f);
				Utils.DrawBorderStringFourWay(spriteBatch, TheDestinyMod.fontFuturaBook, "Dramatically increase damage\nafter killing enemies with your\nArc super", dimensions.X + 10, dimensions.Y + 200, Color.LightGray, Color.Transparent, Vector2.Zero, 0.6f);
				if (AnyFragmentChosenOfType(1)) {
					Utils.DrawBorderStringFourWay(spriteBatch, TheDestinyMod.fontFuturaBook, "Already Applied", dimensions.X + 10, dimensions.Y + 40, Color.Red, Color.Transparent, Vector2.Zero, 0.6f);
				}
			}
			else if (fragmentSubchoice2.ContainsPoint(Main.MouseScreen) && subclass.HasChild(fragmentSubchoiceContainer)) {
				mouseInfo.Left.Set(Main.MouseScreen.X + 5, 0f);
				mouseInfo.Top.Set(Main.MouseScreen.Y + 5, 0f);
				Utils.DrawBorderStringFourWay(spriteBatch, TheDestinyMod.fontFuturaBold, "Fragment 2", dimensions.X + 10, dimensions.Y + 10, Color.White, Color.Transparent, Vector2.Zero, 0.4f);
				Utils.DrawBorderStringFourWay(spriteBatch, TheDestinyMod.fontFuturaBook, "Dramatically increase damage\nafter killing enemies with your\nArc super", dimensions.X + 10, dimensions.Y + 200, Color.LightGray, Color.Transparent, Vector2.Zero, 0.6f);
				if (AnyFragmentChosenOfType(2)) {
					Utils.DrawBorderStringFourWay(spriteBatch, TheDestinyMod.fontFuturaBook, "Already Applied", dimensions.X + 10, dimensions.Y + 40, Color.Red, Color.Transparent, Vector2.Zero, 0.6f);
				}
			}
			else if (fragmentSubchoice3.ContainsPoint(Main.MouseScreen) && subclass.HasChild(fragmentSubchoiceContainer)) {
				mouseInfo.Left.Set(Main.MouseScreen.X + 5, 0f);
				mouseInfo.Top.Set(Main.MouseScreen.Y + 5, 0f);
				Utils.DrawBorderStringFourWay(spriteBatch, TheDestinyMod.fontFuturaBold, "Fragment 3", dimensions.X + 10, dimensions.Y + 10, Color.White, Color.Transparent, Vector2.Zero, 0.4f);
				Utils.DrawBorderStringFourWay(spriteBatch, TheDestinyMod.fontFuturaBook, "Dramatically increase damage\nafter killing enemies with your\nArc super", dimensions.X + 10, dimensions.Y + 200, Color.LightGray, Color.Transparent, Vector2.Zero, 0.6f);
				if (AnyFragmentChosenOfType(3)) {
					Utils.DrawBorderStringFourWay(spriteBatch, TheDestinyMod.fontFuturaBook, "Already Applied", dimensions.X + 10, dimensions.Y + 40, Color.Red, Color.Transparent, Vector2.Zero, 0.6f);
				}
			}
			else if (fragmentSubchoice4.ContainsPoint(Main.MouseScreen) && subclass.HasChild(fragmentSubchoiceContainer)) {
				mouseInfo.Left.Set(Main.MouseScreen.X + 5, 0f);
				mouseInfo.Top.Set(Main.MouseScreen.Y + 5, 0f);
				Utils.DrawBorderStringFourWay(spriteBatch, TheDestinyMod.fontFuturaBold, "Fragment 4", dimensions.X + 10, dimensions.Y + 10, Color.White, Color.Transparent, Vector2.Zero, 0.4f);
				Utils.DrawBorderStringFourWay(spriteBatch, TheDestinyMod.fontFuturaBook, "Dramatically increase damage\nafter killing enemies with your\nArc super", dimensions.X + 10, dimensions.Y + 200, Color.LightGray, Color.Transparent, Vector2.Zero, 0.6f);
				if (AnyFragmentChosenOfType(4)) {
					Utils.DrawBorderStringFourWay(spriteBatch, TheDestinyMod.fontFuturaBook, "Already Applied", dimensions.X + 10, dimensions.Y + 40, Color.Red, Color.Transparent, Vector2.Zero, 0.6f);
				}
			}
			else if (fragmentSubchoice5.ContainsPoint(Main.MouseScreen) && subclass.HasChild(fragmentSubchoiceContainer)) {
				mouseInfo.Left.Set(Main.MouseScreen.X + 5, 0f);
				mouseInfo.Top.Set(Main.MouseScreen.Y + 5, 0f);
				Utils.DrawBorderStringFourWay(spriteBatch, TheDestinyMod.fontFuturaBold, "Fragment 5", dimensions.X + 10, dimensions.Y + 10, Color.White, Color.Transparent, Vector2.Zero, 0.4f);
				Utils.DrawBorderStringFourWay(spriteBatch, TheDestinyMod.fontFuturaBook, "Dramatically increase damage\nafter killing enemies with your\nArc super", dimensions.X + 10, dimensions.Y + 200, Color.LightGray, Color.Transparent, Vector2.Zero, 0.6f);
				if (AnyFragmentChosenOfType(5)) {
					Utils.DrawBorderStringFourWay(spriteBatch, TheDestinyMod.fontFuturaBook, "Already Applied", dimensions.X + 10, dimensions.Y + 40, Color.Red, Color.Transparent, Vector2.Zero, 0.6f);
				}
			}
			else if (fragmentSubchoice6.ContainsPoint(Main.MouseScreen) && subclass.HasChild(fragmentSubchoiceContainer)) {
				mouseInfo.Left.Set(Main.MouseScreen.X + 5, 0f);
				mouseInfo.Top.Set(Main.MouseScreen.Y + 5, 0f);
				Utils.DrawBorderStringFourWay(spriteBatch, TheDestinyMod.fontFuturaBold, "Fragment 6", dimensions.X + 10, dimensions.Y + 10, Color.White, Color.Transparent, Vector2.Zero, 0.4f);
				Utils.DrawBorderStringFourWay(spriteBatch, TheDestinyMod.fontFuturaBook, "Dramatically increase damage\nafter killing enemies with your\nArc super", dimensions.X + 10, dimensions.Y + 200, Color.LightGray, Color.Transparent, Vector2.Zero, 0.6f);
				if (AnyFragmentChosenOfType(6)) {
					Utils.DrawBorderStringFourWay(spriteBatch, TheDestinyMod.fontFuturaBook, "Already Applied", dimensions.X + 10, dimensions.Y + 40, Color.Red, Color.Transparent, Vector2.Zero, 0.6f);
				}
			}
			else {
				mouseInfo.Left.Set(0, 1f);
				mouseInfo.Top.Set(0, 1f);
			}
		}

		private void DrawFragmentChoice(SpriteBatch spriteBatch, CalculatedStyle dimensions, int choiceToSwitch) {
			mouseInfo.Left.Set(Main.MouseScreen.X + 5, 0f);
			mouseInfo.Top.Set(Main.MouseScreen.Y + 5, 0f);
			string choiceName = string.Empty;
			string choiceDesc = string.Empty;
			switch (choiceToSwitch) {
				case 1:
					choiceName = "Fragment 1";
					choiceDesc = "Dramatically increase damage\nafter killing enemies with your\nArc super";
					break;
				case 2:
					choiceName = "Fragment 2";
					choiceDesc = "Dramatically increase damage\nafter killing enemies with your\nArc super";
					break;
				case 3:
					choiceName = "Fragment 3";
					choiceDesc = "Dramatically increase damage\nafter killing enemies with your\nArc super";
					break;
				case 4:
					choiceName = "Fragment 4";
					choiceDesc = "Dramatically increase damage\nafter killing enemies with your\nArc super";
					break;
				case 5:
					choiceName = "Fragment 5";
					choiceDesc = "Dramatically increase damage\nafter killing enemies with your\nArc super";
					break;
				case 6:
					choiceName = "Fragment 6";
					choiceDesc = "Dramatically increase damage\nafter killing enemies with your\nArc super";
					break;
			}
			Utils.DrawBorderStringFourWay(spriteBatch, TheDestinyMod.fontFuturaBold, choiceName, dimensions.X + 10, dimensions.Y + 10, Color.White, Color.Transparent, Vector2.Zero, 0.4f);
			Utils.DrawBorderStringFourWay(spriteBatch, TheDestinyMod.fontFuturaBook, choiceDesc, dimensions.X + 10, dimensions.Y + 200, Color.LightGray, Color.Transparent, Vector2.Zero, 0.6f);
		}

		public override void Draw(SpriteBatch spriteBatch) {
			base.Draw(spriteBatch);
			CalculatedStyle dims = subclass.GetDimensions();
			CalculatedStyle mouseDims = mouseInfo.GetDimensions();
			switch (element) {
				case 0:
					DrawFragmentArc(spriteBatch, mouseDims);
					break;
			}
			if (fragment1.ContainsPoint(Main.MouseScreen) && fragmentChoice1 > 0) {
				DrawFragmentChoice(spriteBatch, mouseDims, fragmentChoice1);
			}
			if (fragment2.ContainsPoint(Main.MouseScreen) && fragmentChoice2 > 0) {
				DrawFragmentChoice(spriteBatch, mouseDims, fragmentChoice2);
			}
			if (fragment3.ContainsPoint(Main.MouseScreen) && fragmentChoice3 > 0) {
				DrawFragmentChoice(spriteBatch, mouseDims, fragmentChoice3);
			}
			if (fragment4.ContainsPoint(Main.MouseScreen) && fragmentChoice4 > 0) {
				DrawFragmentChoice(spriteBatch, mouseDims, fragmentChoice4);
			}
			if (subclass.ContainsPoint(Main.MouseScreen)) {
				Main.LocalPlayer.mouseInterface = true;
			}

            string subclassName = "UNKNOWN";
			DestinyClassType playerClass = Main.LocalPlayer.GetModPlayer<DestinyPlayer>().classType;
			switch (playerClass) {
				case DestinyClassType.Warlock when element == 0:
					subclassName = "STORMCALLER";
					break;
				case DestinyClassType.Titan when element == 0:
					subclassName = "STRIKER";
					break;
				case DestinyClassType.Hunter when element == 0:
					subclassName = "ARCSTRIDER";
					break;
				case DestinyClassType.Warlock when element == 1:
					subclassName = "DAWNBLADE";
					break;
				case DestinyClassType.Titan when element == 1:
					subclassName = "SUNBREAKER";
					break;
				case DestinyClassType.Hunter when element == 1:
					subclassName = "GUNSLINGER";
					break;
				case DestinyClassType.Warlock when element == 2:
					subclassName = "VOIDWALKER";
					break;
				case DestinyClassType.Titan when element == 2:
					subclassName = "SENTINEL";
					break;
				case DestinyClassType.Hunter when element == 2:
					subclassName = "NIGHTSTALKER";
					break;
				case DestinyClassType.Warlock when element == 3:
					subclassName = "SHADEBINDER";
					break;
				case DestinyClassType.Titan when element == 3:
					subclassName = "BEHEMOTH";
					break;
				case DestinyClassType.Hunter when element == 3:
					subclassName = "REVENANT";
					break;
			}
			Utils.DrawBorderStringFourWay(spriteBatch, TheDestinyMod.fontFuturaBold, subclassName.ToUpper(), dims.X + 90, dims.Y + 20, Color.White, Color.Transparent, Vector2.Zero, 0.6f);

			switch (playerClass) {
				case DestinyClassType.Warlock:
					Utils.DrawBorderStringFourWay(spriteBatch, TheDestinyMod.fontFuturaBook, "WARLOCK SUBCLASS", dims.X + 90, dims.Y + 65, Color.Gray, Color.Transparent, Vector2.Zero, 0.5f);
					break;
				case DestinyClassType.Titan:
					Utils.DrawBorderStringFourWay(spriteBatch, TheDestinyMod.fontFuturaBook, "TITAN SUBCLASS", dims.X + 90, dims.Y + 65, Color.Gray, Color.Transparent, Vector2.Zero, 0.5f);
					break;
				case DestinyClassType.Hunter:
					Utils.DrawBorderStringFourWay(spriteBatch, TheDestinyMod.fontFuturaBook, "HUNTER SUBCLASS", dims.X + 90, dims.Y + 65, Color.Gray, Color.Transparent, Vector2.Zero, 0.5f);
					break;
				default:
					Utils.DrawBorderStringFourWay(spriteBatch, TheDestinyMod.fontFuturaBook, "UNKNOWN SUBCLASS", dims.X + 90, dims.Y + 65, Color.Gray, Color.Transparent, Vector2.Zero, 0.5f);
					break;
			}
		}
    }
}