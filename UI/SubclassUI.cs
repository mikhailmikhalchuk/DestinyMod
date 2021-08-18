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

		public Texture2D selectionOneTexture = ModContent.GetTexture("TheDestinyMod/UI/SelectionIArc");
		public Texture2D selectionTwoTexture = ModContent.GetTexture("TheDestinyMod/UI/SelectionIIArc");
		public Texture2D selectionThreeTexture = ModContent.GetTexture("TheDestinyMod/UI/SelectionIIIArc");
		public Texture2D elementalBurnTexture = ModContent.GetTexture("TheDestinyMod/UI/ArcSubclassIcon");
		public Texture2D borderTexture = ModContent.GetTexture("TheDestinyMod/UI/ElementalBurnArcBorder");

        public override void OnInitialize() {
			subclass = new UIPanel();
			subclass.SetPadding(0);
			subclass.Left.Set(1100, 0f);
			subclass.Top.Set(500, 0f);
			subclass.Width.Set(500, 0f);
			subclass.Height.Set(300, 0f);
			subclass.BackgroundColor = Terraria.ModLoader.UI.UICommon.MainPanelBackground;

			UIImage elementalBurn = new UIImage(elementalBurnTexture);
			elementalBurn.Left.Set(10, 0f);
			elementalBurn.Top.Set(10, 0f);
			elementalBurn.Width.Set(76, 0f);
			elementalBurn.Height.Set(76, 0f);
			subclass.Append(elementalBurn);

			Append(subclass);
		}

        public override void Draw(SpriteBatch spriteBatch) {
			base.Draw(spriteBatch);
			CalculatedStyle dims = subclass.GetDimensions();

			Utils.DrawBorderStringFourWay(spriteBatch, TheDestinyMod.fontFuturaBold, "SHADEBINDER", dims.X + 90, dims.Y + 15, Color.White, Color.Transparent, Vector2.Zero, 0.6f);
			Utils.DrawBorderStringFourWay(spriteBatch, TheDestinyMod.fontFuturaBook, "WARLOCK SUBCLASS", dims.X + 90, dims.Y + 60, Color.Gray, Color.Transparent, Vector2.Zero, 0.5f);
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