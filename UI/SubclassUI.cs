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
		/// 2: Void<br></br>
		/// 3: Stasis
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
		private int _lastHoveredAbility;

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

			ability1 = new UIImageButton(fragmentTexture); //rift, shield
			ability1.Left.Set(150, 0f);
			ability1.Top.Set(100, 0f);
			ability1.Width.Set(44, 0f);
			ability1.Height.Set(44, 0f);
            ability1.OnMouseOver += Ability1_OnMouseOver;
            ability1.OnMouseOut += Ability1_OnMouseOut;
			subclass.Append(ability1);

			ability2 = new UIImageButton(fragmentTexture); //jumps
			ability2.Left.Set(200, 0f);
			ability2.Top.Set(100, 0f);
			ability2.Width.Set(44, 0f);
			ability2.Height.Set(44, 0f);
			ability2.OnMouseOver += Ability2_OnMouseOver;
			ability2.OnMouseOut += Ability2_OnMouseOut;
			subclass.Append(ability2);

			ability3 = new UIImageButton(fragmentTexture); //melee
			ability3.Left.Set(250, 0f);
			ability3.Top.Set(100, 0f);
			ability3.Width.Set(44, 0f);
			ability3.Height.Set(44, 0f);
			ability3.OnMouseOver += Ability3_OnMouseOver;
			ability3.OnMouseOut += Ability3_OnMouseOut;
			subclass.Append(ability3);

			ability4 = new UIImageButton(fragmentTexture); //grenade
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
			if (!abilitySubchoiceContainer.ContainsPoint(Main.MouseScreen)) {
				RestoreAndRemoveAbilitySubchoice();
			}
			_lastHoveredAbility = 1;
		}

		private void Ability2_OnMouseOut(UIMouseEvent evt, UIElement listeningElement) {
			if (!abilitySubchoiceContainer.ContainsPoint(Main.MouseScreen)) {
				RestoreAndRemoveAbilitySubchoice();
			}
			_lastHoveredAbility = 2;
		}

		private void Ability3_OnMouseOut(UIMouseEvent evt, UIElement listeningElement) {
			if (!abilitySubchoiceContainer.ContainsPoint(Main.MouseScreen)) {
				RestoreAndRemoveAbilitySubchoice();
			}
			_lastHoveredAbility = 3;
		}

		private void Ability4_OnMouseOut(UIMouseEvent evt, UIElement listeningElement) {
			if (!abilitySubchoiceContainer.ContainsPoint(Main.MouseScreen)) {
				RestoreAndRemoveAbilitySubchoice();
			}
			_lastHoveredAbility = 4;
		}

		private void Ability1_OnMouseOver(UIMouseEvent evt, UIElement listeningElement) {
			GetRidOfAbilitySubchoicesAndRestore(2);
			subclass.Append(abilitySubchoiceContainer);
			RemoveFragmentSubchoice();
		}

		private void Ability2_OnMouseOver(UIMouseEvent evt, UIElement listeningElement) {
			GetRidOfAbilitySubchoicesAndRestore(3);
			subclass.Append(abilitySubchoiceContainer);
			RemoveFragmentSubchoice();
		}

		private void Ability3_OnMouseOver(UIMouseEvent evt, UIElement listeningElement) {
			GetRidOfAbilitySubchoicesAndRestore(1);
			subclass.Append(abilitySubchoiceContainer);
			RemoveFragmentSubchoice();
		}

		private void Ability4_OnMouseOver(UIMouseEvent evt, UIElement listeningElement) {
			GetRidOfAbilitySubchoicesAndRestore(3);
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

		private void ApplyAbilityTo(int num) {
			Main.PlaySound(SoundID.Unlock);
			DestinyClassType playerClass = Main.LocalPlayer.GetModPlayer<DestinyPlayer>().classType;
			Texture2D textureToApply = ModContent.GetTexture("Terraria/UI/CharCreation/CategoryPanel");
			if (_lastHoveredAbility == 1) { //CROSS-CHECK WITH MOUSEINFO DRAWS FOR CORRECT TEXTURES
				subclass.RemoveChild(ability1);
				switch (num) {
					case 1 when playerClass == DestinyClassType.Titan:
						textureToApply = ModContent.GetTexture("Terraria/UI/CharCreation/CategoryPanel"); //barrier
						break;
					case 1 when playerClass == DestinyClassType.Warlock:
						textureToApply = ModContent.GetTexture("Terraria/UI/CharCreation/CategoryPanel"); //rift
						break;
					case 1 when playerClass == DestinyClassType.Hunter:
						textureToApply = ModContent.GetTexture("Terraria/UI/CharCreation/CategoryPanel"); //something
						break;
					case 2 when playerClass == DestinyClassType.Titan:
						textureToApply = ModContent.GetTexture("Terraria/UI/CharCreation/CategoryPanel"); //barrier
						break;
					case 2 when playerClass == DestinyClassType.Warlock:
						textureToApply = ModContent.GetTexture("Terraria/UI/CharCreation/CategoryPanel"); //rift
						break;
					case 2 when playerClass == DestinyClassType.Hunter:
						textureToApply = ModContent.GetTexture("Terraria/UI/CharCreation/CategoryPanel"); //something
						break;
				}
				ability1 = new UIImageButton(textureToApply);
				ability1.Left.Set(150, 0f);
				ability1.Top.Set(100, 0f);
				ability1.Width.Set(44, 0f);
				ability1.Height.Set(44, 0f);
				ability1.OnMouseOver += Ability1_OnMouseOver;
				ability1.OnMouseOut += Ability1_OnMouseOut;
				subclass.Append(ability1);
				abilityChoice1 = num;
			}
			else if (_lastHoveredAbility == 2) { //CROSS-CHECK WITH MOUSEINFO DRAWS FOR CORRECT TEXTURES
				subclass.RemoveChild(ability2);
				switch (num) {
					case 1 when playerClass == DestinyClassType.Titan:
						textureToApply = ModContent.GetTexture("Terraria/UI/CharCreation/CategoryPanel"); //jump
						break;
					case 1 when playerClass == DestinyClassType.Warlock:
						textureToApply = ModContent.GetTexture("Terraria/UI/CharCreation/CategoryPanel"); //blink
						break;
					case 1 when playerClass == DestinyClassType.Hunter:
						textureToApply = ModContent.GetTexture("Terraria/UI/CharCreation/CategoryPanel"); //double jump w/ momentum
						break;
					case 2 when playerClass == DestinyClassType.Titan:
						textureToApply = ModContent.GetTexture("Terraria/UI/CharCreation/CategoryPanel"); //jump
						break;
					case 2 when playerClass == DestinyClassType.Warlock:
						textureToApply = ModContent.GetTexture("Terraria/UI/CharCreation/CategoryPanel"); //glide
						break;
					case 2 when playerClass == DestinyClassType.Hunter:
						textureToApply = ModContent.GetTexture("Terraria/UI/CharCreation/CategoryPanel"); //something
						break;
					case 3 when playerClass == DestinyClassType.Titan:
						textureToApply = ModContent.GetTexture("Terraria/UI/CharCreation/CategoryPanel"); //jump
						break;
					case 3 when playerClass == DestinyClassType.Warlock:
						textureToApply = ModContent.GetTexture("Terraria/UI/CharCreation/CategoryPanel"); //glide 2
						break;
					case 3 when playerClass == DestinyClassType.Hunter:
						textureToApply = ModContent.GetTexture("Terraria/UI/CharCreation/CategoryPanel"); //something 2
						break;
				}
				ability2 = new UIImageButton(textureToApply);
				ability2.Left.Set(200, 0f);
				ability2.Top.Set(100, 0f);
				ability2.Width.Set(44, 0f);
				ability2.Height.Set(44, 0f);
				ability2.OnMouseOver += Ability2_OnMouseOver;
				ability2.OnMouseOut += Ability2_OnMouseOut;
				subclass.Append(ability2);
				abilityChoice2 = num;
			}
			else if (_lastHoveredAbility == 3) { //CROSS-CHECK WITH MOUSEINFO DRAWS FOR CORRECT TEXTURES
				subclass.RemoveChild(ability3);
				switch (num) {
					case 1 when playerClass == DestinyClassType.Titan:
						textureToApply = ModContent.GetTexture("Terraria/UI/CharCreation/CategoryPanel"); //melee
						break;
					case 1 when playerClass == DestinyClassType.Warlock:
						textureToApply = ModContent.GetTexture("Terraria/UI/CharCreation/CategoryPanel"); //melee
						break;
					case 1 when playerClass == DestinyClassType.Hunter:
						textureToApply = ModContent.GetTexture("Terraria/UI/CharCreation/CategoryPanel"); //melee
						break;
				}
				ability3 = new UIImageButton(textureToApply);
				ability3.Left.Set(250, 0f);
				ability3.Top.Set(100, 0f);
				ability3.Width.Set(44, 0f);
				ability3.Height.Set(44, 0f);
				ability3.OnMouseOver += Ability3_OnMouseOver;
				ability3.OnMouseOut += Ability3_OnMouseOut;
				subclass.Append(ability3);
				abilityChoice3 = num;
			}
			else if (_lastHoveredAbility == 4) { //CROSS-CHECK WITH MOUSEINFO DRAWS FOR CORRECT TEXTURES
				subclass.RemoveChild(ability4);
				switch (num) {
					case 1 when playerClass == DestinyClassType.Titan:
						textureToApply = ModContent.GetTexture("Terraria/UI/CharCreation/CategoryPanel"); //grenade
						break;
					case 1 when playerClass == DestinyClassType.Warlock:
						textureToApply = ModContent.GetTexture("Terraria/UI/CharCreation/CategoryPanel"); //grenade
						break;
					case 1 when playerClass == DestinyClassType.Hunter:
						textureToApply = ModContent.GetTexture("Terraria/UI/CharCreation/CategoryPanel"); //grenade
						break;
					case 2 when playerClass == DestinyClassType.Titan:
						textureToApply = ModContent.GetTexture("Terraria/UI/CharCreation/CategoryPanel"); //grenade
						break;
					case 2 when playerClass == DestinyClassType.Warlock:
						textureToApply = ModContent.GetTexture("Terraria/UI/CharCreation/CategoryPanel"); //grenade
						break;
					case 2 when playerClass == DestinyClassType.Hunter:
						textureToApply = ModContent.GetTexture("Terraria/UI/CharCreation/CategoryPanel"); //grenade
						break;
					case 3 when playerClass == DestinyClassType.Titan:
						textureToApply = ModContent.GetTexture("Terraria/UI/CharCreation/CategoryPanel"); //grenade
						break;
					case 3 when playerClass == DestinyClassType.Warlock:
						textureToApply = ModContent.GetTexture("Terraria/UI/CharCreation/CategoryPanel"); //grenade
						break;
					case 3 when playerClass == DestinyClassType.Hunter:
						textureToApply = ModContent.GetTexture("Terraria/UI/CharCreation/CategoryPanel"); //grenade
						break;
				}
				ability4 = new UIImageButton(textureToApply);
				ability4.Left.Set(300, 0f);
				ability4.Top.Set(100, 0f);
				ability4.Width.Set(44, 0f);
				ability4.Height.Set(44, 0f);
				ability4.OnMouseOver += Ability4_OnMouseOver;
				ability4.OnMouseOut += Ability4_OnMouseOut;
				subclass.Append(ability4);
				abilityChoice4 = num;
			}
		}

		private void AbilitySubchoice1_OnClick(UIMouseEvent evt, UIElement listeningElement) {
			if (AnyAbilityChosenOfType(1))
				return;
			Texture2D abilityTexture = ModContent.GetTexture("Terraria/UI/CharCreation/CategoryPanel");
			ApplyAbilityTo(1);
		}

		private void AbilitySubchoice2_OnClick(UIMouseEvent evt, UIElement listeningElement) {
			if (AnyAbilityChosenOfType(2))
				return;
			Texture2D abilityTexture = ModContent.GetTexture("Terraria/UI/CharCreation/CategoryPanel");
			ApplyAbilityTo(2);
		}

		private void AbilitySubchoice3_OnClick(UIMouseEvent evt, UIElement listeningElement) {
			if (AnyAbilityChosenOfType(3))
				return;
			Texture2D abilityTexture = ModContent.GetTexture("Terraria/UI/CharCreation/CategoryPanel");
			ApplyAbilityTo(3);
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
			if (!fragmentSubchoiceContainer.ContainsPoint(Main.MouseScreen)) {
				subclass.RemoveChild(fragmentSubchoiceContainer);
			}
			_lastHoveredFragment = 1;
        }

		private void Fragment2_OnMouseOut(UIMouseEvent evt, UIElement listeningElement) {
			if (!fragmentSubchoiceContainer.ContainsPoint(Main.MouseScreen)) {
				subclass.RemoveChild(fragmentSubchoiceContainer);
			}
			_lastHoveredFragment = 2;
		}

		private void Fragment3_OnMouseOut(UIMouseEvent evt, UIElement listeningElement) {
			if (!fragmentSubchoiceContainer.ContainsPoint(Main.MouseScreen)) {
				subclass.RemoveChild(fragmentSubchoiceContainer);
			}
			_lastHoveredFragment = 3;
		}

		private void Fragment4_OnMouseOut(UIMouseEvent evt, UIElement listeningElement) {
			if (!fragmentSubchoiceContainer.ContainsPoint(Main.MouseScreen)) {
				subclass.RemoveChild(fragmentSubchoiceContainer);
			}
			_lastHoveredFragment = 4;
		}

		private void GetRidOfAbilitySubchoicesAndRestore(int toRestore) {
			abilitySubchoiceContainer.RemoveChild(abilitySubchoice1);
			abilitySubchoiceContainer.RemoveChild(abilitySubchoice2);
			abilitySubchoiceContainer.RemoveChild(abilitySubchoice3);
			if (toRestore == 1) {
				abilitySubchoiceContainer.Append(abilitySubchoice1);
			}
			else if (toRestore == 2) {
				abilitySubchoiceContainer.Append(abilitySubchoice1);
				abilitySubchoiceContainer.Append(abilitySubchoice2);
			}
			else if (toRestore == 3) {
				abilitySubchoiceContainer.Append(abilitySubchoice1);
				abilitySubchoiceContainer.Append(abilitySubchoice2);
				abilitySubchoiceContainer.Append(abilitySubchoice3);
			}
		}

		private void RestoreAndRemoveAbilitySubchoice() {
			if (!subclass.HasChild(abilitySubchoiceContainer))
				return;
			if (!abilitySubchoiceContainer.HasChild(abilitySubchoice2))
				abilitySubchoiceContainer.Append(abilitySubchoice2);
			if (!abilitySubchoiceContainer.HasChild(abilitySubchoice3))
				abilitySubchoiceContainer.Append(abilitySubchoice3);
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
				subclass.RemoveChild(abilitySubchoiceContainer);
			}
			if (!abilitySubchoiceContainer.HasChild(abilitySubchoice2) && !abilitySubchoice1.ContainsPoint(Main.MouseScreen) && !abilitySubchoiceContainer.ContainsPoint(Main.MouseScreen)) {
				ActualExecute();
			}
			else if (!abilitySubchoiceContainer.HasChild(abilitySubchoice3) && !abilitySubchoice1.ContainsPoint(Main.MouseScreen) && !abilitySubchoice2.ContainsPoint(Main.MouseScreen) && !abilitySubchoiceContainer.ContainsPoint(Main.MouseScreen)) {
				ActualExecute();
			}
		}

		private bool AnyFragmentChosenOfType(int type) => fragmentChoice1 == type || fragmentChoice2 == type || fragmentChoice3 == type || fragmentChoice4 == type;

		private bool AnyAbilityChosenOfType(int type) => _lastHoveredAbility == 1 && abilityChoice1 == type || _lastHoveredAbility == 2 && abilityChoice2 == type || _lastHoveredAbility == 3 && abilityChoice3 == type || _lastHoveredAbility == 4 && abilityChoice4 == type;

		private void DrawSlotsArc(SpriteBatch spriteBatch, CalculatedStyle dimensions) {
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
			else if (abilitySubchoice1.ContainsPoint(Main.MouseScreen) && abilitySubchoiceContainer.HasChild(abilitySubchoice1) && subclass.HasChild(abilitySubchoiceContainer)) {
				mouseInfo.Left.Set(Main.MouseScreen.X + 5, 0f);
				mouseInfo.Top.Set(Main.MouseScreen.Y + 5, 0f);
				string abilityName = string.Empty;
				string abilityDesc = string.Empty;
				DestinyClassType playerClass = Main.LocalPlayer.GetModPlayer<DestinyPlayer>().classType;
				switch (_lastHoveredAbility) {
					case 1 when playerClass == DestinyClassType.Titan:
						abilityName = "Small Barrier";
						abilityDesc = "A small barrier";
						break;
					case 1 when playerClass == DestinyClassType.Warlock:
						abilityName = "Healing Rift";
						abilityDesc = "A healing rift";
						break;
					case 1 when playerClass == DestinyClassType.Hunter:
						abilityName = "Hunter's subclass thing";
						abilityDesc = "I don't know what this is";
						break;
					case 2 when playerClass == DestinyClassType.Titan:
						abilityName = "Propelled Jump";
						abilityDesc = "The jump with momentum at the beginning";
						break;
					case 2 when playerClass == DestinyClassType.Warlock:
						abilityName = "Blink";
						abilityDesc = "The teleporting one";
						break;
					case 2 when playerClass == DestinyClassType.Hunter:
						abilityName = "double jump keep momentum";
						abilityDesc = "yeah i don't know";
						break;
					case 3 when playerClass == DestinyClassType.Titan:
						abilityName = "Don't know titan melee";
						abilityDesc = "figure out";
						break;
					case 3 when playerClass == DestinyClassType.Warlock:
						abilityName = "A punch";
						abilityDesc = "for warlock";
						break;
					case 3 when playerClass == DestinyClassType.Hunter:
						abilityName = "knife probably";
						abilityDesc = "hunter one";
						break;
					case 4 when playerClass == DestinyClassType.Titan:
						abilityName = "glacial grenade?";
						abilityDesc = "who knows";
						break;
					case 4 when playerClass == DestinyClassType.Warlock:
						abilityName = "warlock stasis grenade";
						abilityDesc = "for warlock";
						break;
					case 4 when playerClass == DestinyClassType.Hunter:
						abilityName = "hunter smoke bomb probably";
						abilityDesc = "hunter smoke";
						break;
				}
				Utils.DrawBorderStringFourWay(spriteBatch, TheDestinyMod.fontFuturaBold, abilityName, dimensions.X + 10, dimensions.Y + 10, Color.White, Color.Transparent, Vector2.Zero, 0.4f);
				Utils.DrawBorderStringFourWay(spriteBatch, TheDestinyMod.fontFuturaBook, abilityDesc, dimensions.X + 10, dimensions.Y + 200, Color.LightGray, Color.Transparent, Vector2.Zero, 0.6f);
				if (AnyAbilityChosenOfType(1)) {
					Utils.DrawBorderStringFourWay(spriteBatch, TheDestinyMod.fontFuturaBook, "Already Applied", dimensions.X + 10, dimensions.Y + 40, Color.Red, Color.Transparent, Vector2.Zero, 0.6f);
				}
			}
			else if (abilitySubchoice2.ContainsPoint(Main.MouseScreen) && abilitySubchoiceContainer.HasChild(abilitySubchoice2) && subclass.HasChild(abilitySubchoiceContainer)) {
				mouseInfo.Left.Set(Main.MouseScreen.X + 5, 0f);
				mouseInfo.Top.Set(Main.MouseScreen.Y + 5, 0f);
				string abilityName = string.Empty;
				string abilityDesc = string.Empty;
				DestinyClassType playerClass = Main.LocalPlayer.GetModPlayer<DestinyPlayer>().classType;
				switch (_lastHoveredAbility) {
					case 1 when playerClass == DestinyClassType.Titan:
						abilityName = "Big Barrier";
						abilityDesc = "A big barrier";
						break;
					case 1 when playerClass == DestinyClassType.Warlock:
						abilityName = "Damage Rift";
						abilityDesc = "A damage boosting rift";
						break;
					case 1 when playerClass == DestinyClassType.Hunter:
						abilityName = "Hunter's subclass thing 2";
						abilityDesc = "I don't know what this is 2";
						break;
					case 2 when playerClass == DestinyClassType.Titan:
						abilityName = "something Jump titan 2";
						abilityDesc = "The jump with boosting 2";
						break;
					case 2 when playerClass == DestinyClassType.Warlock:
						abilityName = "Glide";
						abilityDesc = "The gliding one";
						break;
					case 2 when playerClass == DestinyClassType.Hunter:
						abilityName = "jump hunter 2";
						abilityDesc = "yeah i don't know";
						break;
					case 4 when playerClass == DestinyClassType.Titan:
						abilityName = "another grenade";
						abilityDesc = "who knows 2";
						break;
					case 4 when playerClass == DestinyClassType.Warlock:
						abilityName = "warlock stasis grenade 2";
						abilityDesc = "for warlock 2";
						break;
					case 4 when playerClass == DestinyClassType.Hunter:
						abilityName = "hunter grenade 2";
						abilityDesc = "hunter 2";
						break;
				}
				Utils.DrawBorderStringFourWay(spriteBatch, TheDestinyMod.fontFuturaBold, abilityName, dimensions.X + 10, dimensions.Y + 10, Color.White, Color.Transparent, Vector2.Zero, 0.4f);
				Utils.DrawBorderStringFourWay(spriteBatch, TheDestinyMod.fontFuturaBook, abilityDesc, dimensions.X + 10, dimensions.Y + 200, Color.LightGray, Color.Transparent, Vector2.Zero, 0.6f);
				if (AnyAbilityChosenOfType(2)) {
					Utils.DrawBorderStringFourWay(spriteBatch, TheDestinyMod.fontFuturaBook, "Already Applied", dimensions.X + 10, dimensions.Y + 40, Color.Red, Color.Transparent, Vector2.Zero, 0.6f);
				}
			}
			else if (abilitySubchoice3.ContainsPoint(Main.MouseScreen) && abilitySubchoiceContainer.HasChild(abilitySubchoice3) && subclass.HasChild(abilitySubchoiceContainer)) {
				mouseInfo.Left.Set(Main.MouseScreen.X + 5, 0f);
				mouseInfo.Top.Set(Main.MouseScreen.Y + 5, 0f);
				string abilityName = string.Empty;
				string abilityDesc = string.Empty;
				DestinyClassType playerClass = Main.LocalPlayer.GetModPlayer<DestinyPlayer>().classType;
				switch (_lastHoveredAbility) {
					case 2 when playerClass == DestinyClassType.Titan:
						abilityName = "something Jump titan 3";
						abilityDesc = "The jump with boosting 3";
						break;
					case 2 when playerClass == DestinyClassType.Warlock:
						abilityName = "Glide 2";
						abilityDesc = "The gliding one 2";
						break;
					case 2 when playerClass == DestinyClassType.Hunter:
						abilityName = "jump hunter 3";
						abilityDesc = "yeah i don't know";
						break;
					case 4 when playerClass == DestinyClassType.Titan:
						abilityName = "another grenade 3";
						abilityDesc = "who knows 3";
						break;
					case 4 when playerClass == DestinyClassType.Warlock:
						abilityName = "warlock stasis grenade 3";
						abilityDesc = "for warlock 3";
						break;
					case 4 when playerClass == DestinyClassType.Hunter:
						abilityName = "hunter grenade 3";
						abilityDesc = "hunter 3";
						break;
				}
				Utils.DrawBorderStringFourWay(spriteBatch, TheDestinyMod.fontFuturaBold, abilityName, dimensions.X + 10, dimensions.Y + 10, Color.White, Color.Transparent, Vector2.Zero, 0.4f);
				Utils.DrawBorderStringFourWay(spriteBatch, TheDestinyMod.fontFuturaBook, abilityDesc, dimensions.X + 10, dimensions.Y + 200, Color.LightGray, Color.Transparent, Vector2.Zero, 0.6f);
				if (AnyAbilityChosenOfType(3)) {
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

		private void DrawAbilityChoice(SpriteBatch spriteBatch, CalculatedStyle dimensions, int ability, int choiceToSwitch) {
			mouseInfo.Left.Set(Main.MouseScreen.X + 5, 0f);
			mouseInfo.Top.Set(Main.MouseScreen.Y + 5, 0f);
			string choiceName = string.Empty;
			string choiceDesc = string.Empty;
			DestinyClassType playerClass = Main.LocalPlayer.GetModPlayer<DestinyPlayer>().classType;
			if (ability == 1) {
				switch (choiceToSwitch) {
					case 1 when playerClass == DestinyClassType.Titan:
						choiceName = "Small Barrier";
						choiceDesc = "A small barrier";
						break;
					case 1 when playerClass == DestinyClassType.Warlock:
						choiceName = "Healing Rift";
						choiceDesc = "A healing rift";
						break;
					case 1 when playerClass == DestinyClassType.Hunter:
						choiceName = "Hunter's subclass thing";
						choiceDesc = "I don't know what this is";
						break;
					case 2 when playerClass == DestinyClassType.Titan:
						choiceName = "Big Barrier";
						choiceDesc = "A big barrier";
						break;
					case 2 when playerClass == DestinyClassType.Warlock:
						choiceName = "Damage Rift";
						choiceDesc = "A damage boosting rift";
						break;
					case 2 when playerClass == DestinyClassType.Hunter:
						choiceName = "Hunter's subclass thing 2";
						choiceDesc = "I don't know what this is 2";
						break;
				}
			}
			if (ability == 2) {
				switch (choiceToSwitch) {
					case 1 when playerClass == DestinyClassType.Titan:
						choiceName = "Propelled Jump";
						choiceDesc = "The jump with momentum at the beginning";
						break;
					case 1 when playerClass == DestinyClassType.Warlock:
						choiceName = "Blink";
						choiceDesc = "The teleporting one";
						break;
					case 1 when playerClass == DestinyClassType.Hunter:
						choiceName = "double jump keep momentum";
						choiceDesc = "yeah i don't know";
						break;
					case 2 when playerClass == DestinyClassType.Titan:
						choiceName = "something Jump titan 2";
						choiceDesc = "The jump with boosting 2";
						break;
					case 2 when playerClass == DestinyClassType.Warlock:
						choiceName = "Glide";
						choiceDesc = "The gliding one";
						break;
					case 2 when playerClass == DestinyClassType.Hunter:
						choiceName = "jump hunter 2";
						choiceDesc = "yeah i don't know";
						break;
					case 3 when playerClass == DestinyClassType.Titan:
						choiceName = "something Jump titan 3";
						choiceDesc = "The jump with boosting 3";
						break;
					case 3 when playerClass == DestinyClassType.Warlock:
						choiceName = "Glide 2";
						choiceDesc = "The gliding one 2";
						break;
					case 3 when playerClass == DestinyClassType.Hunter:
						choiceName = "jump hunter 3";
						choiceDesc = "yeah i don't know";
						break;
				}
			}
			if (ability == 3) {
				switch (choiceToSwitch) {
					case 1 when playerClass == DestinyClassType.Titan:
						choiceName = "Don't know titan melee";
						choiceDesc = "figure out";
						break;
					case 1 when playerClass == DestinyClassType.Warlock:
						choiceName = "A punch";
						choiceDesc = "for warlock";
						break;
					case 1 when playerClass == DestinyClassType.Hunter:
						choiceName = "knife probably";
						choiceDesc = "hunter one";
						break;
				}
			}
			if (ability == 4) {
				switch (choiceToSwitch) {
					case 1 when playerClass == DestinyClassType.Titan:
						choiceName = "glacial grenade?";
						choiceDesc = "who knows";
						break;
					case 1 when playerClass == DestinyClassType.Warlock:
						choiceName = "warlock stasis grenade";
						choiceDesc = "for warlock";
						break;
					case 1 when playerClass == DestinyClassType.Hunter:
						choiceName = "hunter smoke bomb probably";
						choiceDesc = "hunter smoke";
						break;
					case 2 when playerClass == DestinyClassType.Titan:
						choiceName = "another grenade";
						choiceDesc = "who knows 2";
						break;
					case 2 when playerClass == DestinyClassType.Warlock:
						choiceName = "warlock stasis grenade 2";
						choiceDesc = "for warlock 2";
						break;
					case 2 when playerClass == DestinyClassType.Hunter:
						choiceName = "hunter grenade 2";
						choiceDesc = "hunter 2";
						break;
					case 3 when playerClass == DestinyClassType.Titan:
						choiceName = "another grenade 3";
						choiceDesc = "who knows 3";
						break;
					case 3 when playerClass == DestinyClassType.Warlock:
						choiceName = "warlock stasis grenade 3";
						choiceDesc = "for warlock 3";
						break;
					case 3 when playerClass == DestinyClassType.Hunter:
						choiceName = "hunter grenade 3";
						choiceDesc = "hunter 3";
						break;
				}
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
					DrawSlotsArc(spriteBatch, mouseDims);
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
			if (ability1.ContainsPoint(Main.MouseScreen) && abilityChoice1 > 0) {
				DrawAbilityChoice(spriteBatch, mouseDims, 1, abilityChoice1);
			}
			if (ability2.ContainsPoint(Main.MouseScreen) && abilityChoice2 > 0) {
				DrawAbilityChoice(spriteBatch, mouseDims, 2, abilityChoice2);
			}
			if (ability3.ContainsPoint(Main.MouseScreen) && abilityChoice3 > 0) {
				DrawAbilityChoice(spriteBatch, mouseDims, 3, abilityChoice3);
			}
			if (ability4.ContainsPoint(Main.MouseScreen) && abilityChoice4 > 0) {
				DrawAbilityChoice(spriteBatch, mouseDims, 4, abilityChoice4);
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