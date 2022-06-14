using Terraria.GameContent.UI.Elements;
using DestinyMod.Common.UI;
using DestinyMod.Common.GlobalItems;
using Terraria.UI;
using Microsoft.Xna.Framework;
using DestinyMod.Core.Extensions;

namespace DestinyMod.Content.UI.ItemDetails
{
	public class ItemDetailState_ItemStats : UIElement
	{
		public ItemDetailsState ItemDetailsState { get; private set; }

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

		public UIElement PowerDisplayBackground { get; private set; }

		public UIElement StatDisplayBackground { get; private set; }

		public UIText InspectedItemPowerText { get; private set; }

		public UIText InspectedItemPowerValue { get; private set; }

		public UISeparator PowerSeparator { get; private set; }

		public ItemDetailState_ItemStats(ItemDetailsState itemDetailsState)
		{
			ItemDetailsState = itemDetailsState;

			if (ItemDetailsState == null)
			{
				return;
			}

			PowerDisplayBackground = new UIElement();
			PowerDisplayBackground.Width.Pixels = 100;
			PowerDisplayBackground.Height.Pixels = 100;
			Append(PowerDisplayBackground);

			PowerSeparator = new UISeparator();
			PowerSeparator.Left.Pixels = 100;
			PowerSeparator.Width.Pixels = 2f;
			PowerSeparator.Height.Pixels = 100;
			PowerSeparator.Color = ItemDetailsState.BaseColor_Light;
			Append(PowerSeparator);

			StatDisplayBackground = new UIElement();
			StatDisplayBackground.Left.Pixels = 102;
			StatDisplayBackground.Width.Pixels = 200;
			StatDisplayBackground.Height.Pixels = 100;
			Append(StatDisplayBackground);

			InspectedItemPowerText = new UIText("Power", 0.8f);
			InspectedItemPowerText.Left.Pixels = -10;
			InspectedItemPowerText.Top.Pixels = 10;
			InspectedItemPowerText.HAlign = 1;
			PowerDisplayBackground.Append(InspectedItemPowerText);

			InspectedItemPowerValue = new UIText(itemDetailsState.InspectedItem.GetGlobalItem<ItemDataItem>().LightLevel.ToString(), 0.7f, large: true);
			InspectedItemPowerValue.Left.Pixels = -10;
			InspectedItemPowerValue.Top.Pixels = 30;
			InspectedItemPowerValue.HAlign = 1;
			PowerDisplayBackground.Append(InspectedItemPowerValue);

			Vector2 size = this.CalculateChildrenSize();
			Width.Pixels = size.X;
			Height.Pixels = size.Y;
		}
	}
}