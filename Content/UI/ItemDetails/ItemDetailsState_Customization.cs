using Terraria.GameContent.UI.Elements;
using DestinyMod.Common.UI;
using DestinyMod.Common.GlobalItems;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.UI;
using DestinyMod.Content.UI.MouseText;
using Microsoft.Xna.Framework;
using DestinyMod.Core.Extensions;

namespace DestinyMod.Content.UI.ItemDetails
{
	public class ItemDetailsState_Customization : UIElement
	{
		public ItemDetailsState ItemDetailsState { get; private set; }

		public UIText CosmeticsTitle { get; private set; }

		public UISeparator CosmeticsSeparator { get; private set; }

		public UIItemSlotWithBackground DyeSlot { get; private set; }

		private bool InternalVisible;

		public bool Visible
		{
			get => InternalVisible;
			set
			{
				InternalVisible = value;
				IgnoresMouseInteraction = !InternalVisible;
			}
		}

		public ItemDetailsState_Customization(ItemDetailsState itemDetailsState)
		{
			ItemDetailsState = itemDetailsState;

			if (ItemDetailsState == null || !ItemDetailsState.InspectedItemTypeData.Shaderable)
            {
				return;
            }

			CosmeticsTitle = new UIText("Item Cosmetics");
			Append(CosmeticsTitle);

			CosmeticsSeparator = new UISeparator();
			CosmeticsSeparator.Top.Pixels = 20;
			CosmeticsSeparator.Width.Pixels = 300f;
			CosmeticsSeparator.Height.Pixels = 2;
			CosmeticsSeparator.Color = ItemDetailsState.BaseColor_Light;
			Append(CosmeticsSeparator);

			Texture2D dyeSlotBackground = ModContent.Request<Texture2D>("DestinyMod/Content/UI/ItemDetails/DyeSlot", AssetRequestMode.ImmediateLoad).Value;
			DyeSlot = new UIItemSlotWithBackground(dyeSlotBackground, isItemValid: (item) => item.dye > 0);
			DyeSlot.Top.Pixels = 28;
			DyeSlot.BlockItemInput = false;
			DyeSlot.Item = ItemDetailsState.InspectedItemData.Shader;
			DyeSlot.OnUpdate += HandleDyeSlotMouseText;
			DyeSlot.OnUpdateItem += UpdateItemShader;
			Append(DyeSlot);

			Vector2 size = this.CalculateChildrenSize();
			Width.Pixels = size.X;
			Height.Pixels = size.Y;
		}

		public override bool ContainsPoint(Vector2 point) => Visible && base.ContainsPoint(point);

		public override void Update(GameTime gameTime)
		{
			if (!Visible)
			{
				return;
			}

			base.Update(gameTime);
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			if (!Visible)
			{
				return;
			}

			base.Draw(spriteBatch);
		}

		public void HandleDyeSlotMouseText(UIElement affectedElement)
		{
			if (affectedElement is not UIItemSlotWithBackground uIItemSlotWithBackground || !uIItemSlotWithBackground.ContainsPoint(Main.MouseScreen))
			{
				return;
			}

			string title = uIItemSlotWithBackground.Item.IsAir ? "Default Shader" : uIItemSlotWithBackground.Item.HoverName;
			string subTitle = uIItemSlotWithBackground.Item.IsAir ? "None" : "Shader";
			ItemDetailsState.MouseText_TitleAndSubtitle.UpdateData(title, subTitle);

			MouseTextState mouseTextState = ModContent.GetInstance<MouseTextState>();
			mouseTextState.AppendToMasterBackground(ItemDetailsState.MouseText_TitleAndSubtitle);
		}

		public void UpdateItemShader(UIItemSlotWithBackground uIItemSlotWithBackground) => ItemDetailsState.InspectedItemData.Shader = uIItemSlotWithBackground.Item;
    }
}