using Terraria.GameContent.UI.Elements;
using Microsoft.Xna.Framework;
using DestinyMod.Core.UI;
using DestinyMod.Common.UI;
using System.Collections.Generic;
using DestinyMod.Common.Items.PerksAndMods;
using DestinyMod.Common.GlobalItems;
using System.Linq;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using DestinyMod.Content.UI.MouseText;

namespace DestinyMod.Content.UI.ItemDetails
{
	public partial class ItemDetailsState : DestinyModUIState
	{
		public UIText WeaponPerksTitle { get; private set; }

		public UISeparator WeaponPerksSeparator { get; private set; }

		public IList<ItemPerkDisplay> ItemPerks { get; private set; }

		public int InitialisePerksSection(int yPos)
        {
			WeaponPerksTitle = new UIText("Weapon Perks");
			WeaponPerksTitle.Left.Pixels = 10;
			WeaponPerksTitle.Top.Pixels = yPos;
			MasterBackground.Append(WeaponPerksTitle);

			WeaponPerksSeparator = new UISeparator();
			WeaponPerksSeparator.Left.Pixels = 10;
			WeaponPerksSeparator.Top.Pixels = yPos += 20;
			WeaponPerksSeparator.Width.Pixels = 300f;
			WeaponPerksSeparator.Height.Pixels = 2f;
			WeaponPerksSeparator.Color = BaseColor_Light;
			MasterBackground.Append(WeaponPerksSeparator);

			ItemPerks = new List<ItemPerkDisplay>();

			yPos += 8;
			float lowestPerkPosition = yPos;
			ItemDataItem inspectedItem = InspectedItem.GetGlobalItem<ItemDataItem>();
			for (int i = 0; i < InspectedItemData.PerkPool.Count; i++)
			{
				ItemPerkPool perkTypePool = InspectedItemData.PerkPool[i];
				IList<ItemPerkDisplay> itemPerksWithinType = new List<ItemPerkDisplay>();
				for (int perkIndexer = 0; perkIndexer < perkTypePool.Perks.Count; perkIndexer++)
				{
					ItemPerk perkInQuestion = perkTypePool.Perks[perkIndexer];
					bool isPerkActive = inspectedItem.ActivePerks.Contains(perkInQuestion);
					ItemPerkDisplay perk = new ItemPerkDisplay(perkInQuestion, isPerkActive);
					perk.Left.Pixels = 10 + ItemPerk.TextureSize * (5f / 3f) * i;
					perk.Top.Pixels = yPos + ItemPerk.TextureSize * (6f / 5f) * perkIndexer;
					lowestPerkPosition = Math.Max(lowestPerkPosition, perk.Top.Pixels);

					perk.OnUpdate += (evt) =>
					{
						if (!perk.ContainsPoint(Main.MouseScreen))
						{
							return;
						}

						MouseText_TitleAndSubtitle.UpdateData(perk.ItemPerk.DisplayName ?? perk.ItemPerk.Name, perkTypePool.TypeName);
						MouseText_BodyText.UpdateData(perk.ItemPerk.Description);
						MouseTextState mouseTextState = ModContent.GetInstance<MouseTextState>();
						mouseTextState.AppendToMasterBackground(MouseText_TitleAndSubtitle);
						mouseTextState.AppendToMasterBackground(MouseText_BodyText);

						if (!perk.IsActive)
                        {
							mouseTextState.AppendToMasterBackground(MouseText_ClickIndicator);
						}
					};

					perk.OnMouseDown += (evt, listeningElement) =>
					{
						if (perk.IsActive)
						{
							return;
						}
						perk.ToggleActive();
						SyncActivePerks();
						SoundEngine.PlaySound(SoundID.Grab);
					};

					ItemPerks.Add(perk);
					itemPerksWithinType.Add(perk);
					MasterBackground.Append(perk);
				}

				foreach (ItemPerkDisplay itemPerkDisplay in itemPerksWithinType)
                {
					itemPerkDisplay.InterconnectedPerks = itemPerksWithinType.Where(itemPerk => itemPerk != itemPerkDisplay).ToList();
				}
			}

			for (int i = 1; i < InspectedItemData.PerkPool.Count; i++)
			{
				UISeparator perkSeparator = new UISeparator();
				perkSeparator.Left.Pixels = 10 + ItemPerk.TextureSize * (4f / 3f) * i;
				perkSeparator.Top.Pixels = yPos;
				perkSeparator.Width.Pixels = 2f;
				perkSeparator.Height.Pixels = lowestPerkPosition - yPos + ItemPerk.TextureSize;
				perkSeparator.Color = BaseColor_Light;
				MasterBackground.Append(perkSeparator);
			}

			return (int)lowestPerkPosition + ItemPerk.TextureSize + 10;
        }

		public void SyncActivePerks()
        {
			ItemDataItem inspectedItemData = InspectedItem.GetGlobalItem<ItemDataItem>();
			inspectedItemData.ActivePerks.Clear();
			foreach (ItemPerkDisplay itemPerk in ItemPerks)
            {
				if (!itemPerk.IsActive)
                {
					continue;
                }

				inspectedItemData.ActivePerks.Add(itemPerk.ItemPerk);
            }
        }
	}
}