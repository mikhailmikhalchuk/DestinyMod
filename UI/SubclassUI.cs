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

		/// <summary>
		/// From top, bottom, right:<br></br>
		/// 1-3: Arc subclasses<br></br>
		/// 4-6: Solar subclasses<br></br>
		/// 7-9: Void subclasses
		/// </summary>
		public int selectedSubclass;

		public int fragmentChoice1;
		public int fragmentChoice2;
		public int fragmentChoice3;
		public int fragmentChoice4;

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

		public UIImageButton fragment1;
		public UIImageButton fragment2;
		public UIImageButton fragment3;
		public UIImageButton fragment4;

		public UIElement fragmentSubchoiceContainer;

		public UIPanel mouseInfo;

		private int _lastHovered;

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

			Append(subclass);

			Append(mouseInfo);
		}

		private void ApplyFragmentTo(Texture2D tex, int num) {
			if (_lastHovered == 1) {
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
			if (_lastHovered == 2) {
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
			if (_lastHovered == 3) {
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
			if (_lastHovered == 4) {
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

        private void FragmentSubchoice1_OnClick(UIMouseEvent evt, UIElement listeningElement) {
			Texture2D fragmentTexture = ModContent.GetTexture("Terraria/UI/CharCreation/CategoryPanel");
			ApplyFragmentTo(fragmentTexture, 1);
        }

		private void FragmentSubchoice2_OnClick(UIMouseEvent evt, UIElement listeningElement) {
			Texture2D fragmentTexture = ModContent.GetTexture("Terraria/UI/CharCreation/CategoryPanel");
			ApplyFragmentTo(fragmentTexture, 2);
		}

		private void FragmentSubchoice3_OnClick(UIMouseEvent evt, UIElement listeningElement) {
			Texture2D fragmentTexture = ModContent.GetTexture("Terraria/UI/CharCreation/CategoryPanel");
			ApplyFragmentTo(fragmentTexture, 3);
		}

		private void FragmentSubchoice4_OnClick(UIMouseEvent evt, UIElement listeningElement) {
			Texture2D fragmentTexture = ModContent.GetTexture("Terraria/UI/CharCreation/CategoryPanel");
			ApplyFragmentTo(fragmentTexture, 4);
		}

		private void FragmentSubchoice5_OnClick(UIMouseEvent evt, UIElement listeningElement) {
			Texture2D fragmentTexture = ModContent.GetTexture("Terraria/UI/CharCreation/CategoryPanel");
			ApplyFragmentTo(fragmentTexture, 5);
		}

		private void FragmentSubchoice6_OnClick(UIMouseEvent evt, UIElement listeningElement) {
			Texture2D fragmentTexture = ModContent.GetTexture("Terraria/UI/CharCreation/CategoryPanel");
			ApplyFragmentTo(fragmentTexture, 6);
		}

		private void Fragment1_OnMouseOut(UIMouseEvent evt, UIElement listeningElement) {
			if (!fragmentSubchoiceContainer.GetDimensions().ToRectangle().Contains(Main.MouseScreen.ToPoint())) {
				subclass.RemoveChild(fragmentSubchoiceContainer);
			}
			_lastHovered = 1;
        }

		private void Fragment2_OnMouseOut(UIMouseEvent evt, UIElement listeningElement) {
			if (!fragmentSubchoiceContainer.GetDimensions().ToRectangle().Contains(Main.MouseScreen.ToPoint())) {
				subclass.RemoveChild(fragmentSubchoiceContainer);
			}
			_lastHovered = 2;
		}

		private void Fragment3_OnMouseOut(UIMouseEvent evt, UIElement listeningElement) {
			if (!fragmentSubchoiceContainer.GetDimensions().ToRectangle().Contains(Main.MouseScreen.ToPoint())) {
				subclass.RemoveChild(fragmentSubchoiceContainer);
			}
			_lastHovered = 3;
		}

		private void Fragment4_OnMouseOut(UIMouseEvent evt, UIElement listeningElement) {
			if (!fragmentSubchoiceContainer.GetDimensions().ToRectangle().Contains(Main.MouseScreen.ToPoint())) {
				subclass.RemoveChild(fragmentSubchoiceContainer);
			}
			_lastHovered = 4;
		}

		private void Fragment1_OnMouseOver(UIMouseEvent evt, UIElement listeningElement) {
			subclass.Append(fragmentSubchoiceContainer);
		}

		private void Fragment2_OnMouseOver(UIMouseEvent evt, UIElement listeningElement) {
			subclass.Append(fragmentSubchoiceContainer);
		}

		private void Fragment3_OnMouseOver(UIMouseEvent evt, UIElement listeningElement) {
			subclass.Append(fragmentSubchoiceContainer);
		}

		private void Fragment4_OnMouseOver(UIMouseEvent evt, UIElement listeningElement) {
			subclass.Append(fragmentSubchoiceContainer);
		}

		private void FragmentSubchoiceContainer_OnMouseOut(UIMouseEvent evt, UIElement listeningElement) {
			if (!fragmentSubchoice1.GetDimensions().ToRectangle().Contains(Main.MouseScreen.ToPoint()) && !fragmentSubchoiceContainer.GetDimensions().ToRectangle().Contains(Main.MouseScreen.ToPoint())) {
				subclass.RemoveChild(fragmentSubchoiceContainer);
			}
		}

		private void DrawFragmentArc(SpriteBatch spriteBatch, CalculatedStyle dimensions) {
			if (fragmentSubchoice1.GetDimensions().ToRectangle().Contains(Main.MouseScreen.ToPoint()) && subclass.HasChild(fragmentSubchoiceContainer)) {
				mouseInfo.Left.Set(Main.MouseScreen.X + 5, 0f);
				mouseInfo.Top.Set(Main.MouseScreen.Y + 5, 0f);
				Utils.DrawBorderStringFourWay(spriteBatch, TheDestinyMod.fontFuturaBold, "Fragment 1", dimensions.X + 10, dimensions.Y + 10, Color.White, Color.Transparent, Vector2.Zero, 0.4f);
				Utils.DrawBorderStringFourWay(spriteBatch, TheDestinyMod.fontFuturaBook, "Dramatically increase damage\nafter killing enemies with your\nArc super", dimensions.X + 10, dimensions.Y + 200, Color.LightGray, Color.Transparent, Vector2.Zero, 0.6f);
			}
			else if (fragmentSubchoice2.GetDimensions().ToRectangle().Contains(Main.MouseScreen.ToPoint()) && subclass.HasChild(fragmentSubchoiceContainer)) {
				mouseInfo.Left.Set(Main.MouseScreen.X + 5, 0f);
				mouseInfo.Top.Set(Main.MouseScreen.Y + 5, 0f);
				Utils.DrawBorderStringFourWay(spriteBatch, TheDestinyMod.fontFuturaBold, "Fragment 2", dimensions.X + 10, dimensions.Y + 10, Color.White, Color.Transparent, Vector2.Zero, 0.4f);
				Utils.DrawBorderStringFourWay(spriteBatch, TheDestinyMod.fontFuturaBook, "Dramatically increase damage\nafter killing enemies with your\nArc super", dimensions.X + 10, dimensions.Y + 200, Color.LightGray, Color.Transparent, Vector2.Zero, 0.6f);
			}
			else if (fragmentSubchoice3.GetDimensions().ToRectangle().Contains(Main.MouseScreen.ToPoint()) && subclass.HasChild(fragmentSubchoiceContainer)) {
				mouseInfo.Left.Set(Main.MouseScreen.X + 5, 0f);
				mouseInfo.Top.Set(Main.MouseScreen.Y + 5, 0f);
				Utils.DrawBorderStringFourWay(spriteBatch, TheDestinyMod.fontFuturaBold, "Fragment 3", dimensions.X + 10, dimensions.Y + 10, Color.White, Color.Transparent, Vector2.Zero, 0.4f);
				Utils.DrawBorderStringFourWay(spriteBatch, TheDestinyMod.fontFuturaBook, "Dramatically increase damage\nafter killing enemies with your\nArc super", dimensions.X + 10, dimensions.Y + 200, Color.LightGray, Color.Transparent, Vector2.Zero, 0.6f);
			}
			else if (fragmentSubchoice4.GetDimensions().ToRectangle().Contains(Main.MouseScreen.ToPoint()) && subclass.HasChild(fragmentSubchoiceContainer)) {
				mouseInfo.Left.Set(Main.MouseScreen.X + 5, 0f);
				mouseInfo.Top.Set(Main.MouseScreen.Y + 5, 0f);
				Utils.DrawBorderStringFourWay(spriteBatch, TheDestinyMod.fontFuturaBold, "Fragment 4", dimensions.X + 10, dimensions.Y + 10, Color.White, Color.Transparent, Vector2.Zero, 0.4f);
				Utils.DrawBorderStringFourWay(spriteBatch, TheDestinyMod.fontFuturaBook, "Dramatically increase damage\nafter killing enemies with your\nArc super", dimensions.X + 10, dimensions.Y + 200, Color.LightGray, Color.Transparent, Vector2.Zero, 0.6f);
			}
			else if (fragmentSubchoice5.GetDimensions().ToRectangle().Contains(Main.MouseScreen.ToPoint()) && subclass.HasChild(fragmentSubchoiceContainer)) {
				mouseInfo.Left.Set(Main.MouseScreen.X + 5, 0f);
				mouseInfo.Top.Set(Main.MouseScreen.Y + 5, 0f);
				Utils.DrawBorderStringFourWay(spriteBatch, TheDestinyMod.fontFuturaBold, "Fragment 5", dimensions.X + 10, dimensions.Y + 10, Color.White, Color.Transparent, Vector2.Zero, 0.4f);
				Utils.DrawBorderStringFourWay(spriteBatch, TheDestinyMod.fontFuturaBook, "Dramatically increase damage\nafter killing enemies with your\nArc super", dimensions.X + 10, dimensions.Y + 200, Color.LightGray, Color.Transparent, Vector2.Zero, 0.6f);
			}
			else if (fragmentSubchoice6.GetDimensions().ToRectangle().Contains(Main.MouseScreen.ToPoint()) && subclass.HasChild(fragmentSubchoiceContainer)) {
				mouseInfo.Left.Set(Main.MouseScreen.X + 5, 0f);
				mouseInfo.Top.Set(Main.MouseScreen.Y + 5, 0f);
				Utils.DrawBorderStringFourWay(spriteBatch, TheDestinyMod.fontFuturaBold, "Fragment 6", dimensions.X + 10, dimensions.Y + 10, Color.White, Color.Transparent, Vector2.Zero, 0.4f);
				Utils.DrawBorderStringFourWay(spriteBatch, TheDestinyMod.fontFuturaBook, "Dramatically increase damage\nafter killing enemies with your\nArc super", dimensions.X + 10, dimensions.Y + 200, Color.LightGray, Color.Transparent, Vector2.Zero, 0.6f);
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
			if (fragment1.GetDimensions().ToRectangle().Contains(Main.MouseScreen.ToPoint()) && fragmentChoice1 > 0) {
				DrawFragmentChoice(spriteBatch, mouseDims, fragmentChoice1);
			}
			if (fragment2.GetDimensions().ToRectangle().Contains(Main.MouseScreen.ToPoint()) && fragmentChoice2 > 0) {
				DrawFragmentChoice(spriteBatch, mouseDims, fragmentChoice2);
			}
			if (fragment3.GetDimensions().ToRectangle().Contains(Main.MouseScreen.ToPoint()) && fragmentChoice3 > 0) {
				DrawFragmentChoice(spriteBatch, mouseDims, fragmentChoice3);
			}
			if (fragment4.GetDimensions().ToRectangle().Contains(Main.MouseScreen.ToPoint()) && fragmentChoice4 > 0) {
				DrawFragmentChoice(spriteBatch, mouseDims, fragmentChoice4);
			}

			Utils.DrawBorderStringFourWay(spriteBatch, TheDestinyMod.fontFuturaBold, "SHADEBINDER", dims.X + 90, dims.Y + 20, Color.White, Color.Transparent, Vector2.Zero, 0.6f);
			Utils.DrawBorderStringFourWay(spriteBatch, TheDestinyMod.fontFuturaBook, "WARLOCK SUBCLASS", dims.X + 90, dims.Y + 65, Color.Gray, Color.Transparent, Vector2.Zero, 0.5f);
		}
    }
}