using Terraria.GameContent.UI.Elements;
using DestinyMod.Common.UI;
using DestinyMod.Common.GlobalItems;
using Terraria.UI;
using Microsoft.Xna.Framework;
using DestinyMod.Core.Extensions;
using System.Collections.Generic;
using Terraria;
using DestinyMod.Common.ModPlayers;

namespace DestinyMod.Content.UI.ItemDetails
{
	public class ItemDetailsState_ItemStats : UIElement
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

		public List<StatDisplayElement> StatsElement { get; private set; }

		public ItemDetailsState_ItemStats(ItemDetailsState itemDetailsState)
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

			Item inspectedItem = itemDetailsState.InspectedItem;
			ItemDataItem inspectedItemData = inspectedItem.GetGlobalItem<ItemDataItem>();

			InspectedItemPowerValue = new UIText(inspectedItemData.LightLevel.ToString(), 0.7f, large: true);
			InspectedItemPowerValue.Left.Pixels = -10;
			InspectedItemPowerValue.Top.Pixels = 30;
			InspectedItemPowerValue.HAlign = 1;
			PowerDisplayBackground.Append(InspectedItemPowerValue);

			StatsElement = new List<StatDisplayElement>();
			StatDisplayElement range = new StatDisplayElement("Range", inspectedItemData.Range);
			range.UpdateDataEvent += () => range.Progress = inspectedItemData.Range;

			StatDisplayElement stability = new StatDisplayElement("Stability", inspectedItemData.Stability);
			stability.UpdateDataEvent += () => stability.Progress = inspectedItemData.Stability;

			StatDisplayElement recoil = new StatDisplayElement("Recoil", inspectedItemData.Recoil);
			recoil.UpdateDataEvent += () => recoil.Progress = inspectedItemData.Recoil;

			StatDisplayElement impact = new StatDisplayElement("Impact", inspectedItem.damage, displayProgressBar: false);
			impact.UpdateDataEvent += () => impact.Progress = inspectedItem.damage;

			StatsElement.Add(range, stability, recoil, impact);

			for (int i = 0; i < StatsElement.Count; i++)
            {
				StatDisplayElement statDisplayElement = StatsElement[i];
				statDisplayElement.Left.Pixels = 80;
				statDisplayElement.Top.Pixels = 16 * i + 10;

				if (!statDisplayElement.DisplayProgressBar)
                {
					statDisplayElement.Top.Pixels += 4;
				}
				else
                {
					statDisplayElement.Width.Pixels = StatDisplayBackground.Width.Pixels - 100;
				}

				StatDisplayBackground.Append(statDisplayElement);
			}

			Vector2 statDisplayBackgroundSize = StatDisplayBackground.CalculateChildrenSize();
			StatDisplayBackground.Width.Pixels = statDisplayBackgroundSize.X;
			StatDisplayBackground.Height.Pixels = statDisplayBackgroundSize.Y;

			Vector2 size = this.CalculateChildrenSize();
			Width.Pixels = size.X + 10;
			Height.Pixels = size.Y;
		}

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

			foreach (StatDisplayElement statDisplayElement in StatsElement)
            {
				statDisplayElement?.UpdateData();
            }
        }
    }
}