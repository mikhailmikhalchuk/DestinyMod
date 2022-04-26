using Terraria.GameContent.UI.Elements;
using Microsoft.Xna.Framework;
using DestinyMod.Core.UI;
using DestinyMod.Common.UI;
using System.Collections.Generic;
using DestinyMod.Common.Items.PerksAndMods;
using DestinyMod.Common.GlobalItems;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;

namespace DestinyMod.Content.UI.ItemDetails
{
	public partial class ItemDetailsState : DestinyModUIState
	{
		public UIText WeaponModsTitle { get; private set; }

		public UISeparator WeaponModsSeparator { get; private set; }

		public UIImageWithBackground InfuseItemSlot { get; private set; }

		public UIImageWithBackground ItemTierSlot { get; private set; }

		public IList<ItemModSlot> ModSlots { get; private set; }

		public int InitialiseModsSection(int yPos, bool includeItemTierSlot = false)
        {
			WeaponModsTitle = new UIText("Weapon Mods");
			WeaponModsTitle.Left.Pixels = 10;
			WeaponModsTitle.Top.Pixels = yPos;
			MasterBackground.Append(WeaponModsTitle);

			WeaponModsSeparator = new UISeparator();
			WeaponModsSeparator.Left.Pixels = 10;
			WeaponModsSeparator.Top.Pixels = yPos += 20;
			WeaponModsSeparator.Width.Pixels = 300f;
			WeaponModsSeparator.Height.Pixels = 2f;
			WeaponModsSeparator.Color = SeparatorColor;
			MasterBackground.Append(WeaponModsSeparator);

			Texture2D slotBackground = ModContent.Request<Texture2D>("DestinyMod/Content/UI/ItemDetails/ModSlot", AssetRequestMode.ImmediateLoad).Value;
			int xPos = 10;
			yPos += 8;
			Asset<Texture2D> infuseSlot = ModContent.Request<Texture2D>("DestinyMod/Content/UI/ItemDetails/InfuseSlot");
			InfuseItemSlot = new UIImageWithBackground(slotBackground, infuseSlot, 34);
			InfuseItemSlot.Left.Pixels = xPos;
			InfuseItemSlot.Top.Pixels = yPos;
			xPos += infuseSlot.Width() + 8;
			MasterBackground.Append(InfuseItemSlot);

			if (includeItemTierSlot)
            {
				ItemTierSlot = new UIImageWithBackground(slotBackground, infuseSlot, 34);
				ItemTierSlot.Left.Pixels = xPos;
				ItemTierSlot.Top.Pixels = yPos;
				xPos += infuseSlot.Width() + 8;
				MasterBackground.Append(ItemTierSlot);
			}

			ItemDataItem inspectedItemData = InspectedItem.GetGlobalItem<ItemDataItem>();
			if (inspectedItemData.ItemMods != null)
			{
				ModSlots = new List<ItemModSlot>();
				for (int modSlotIndexer = 0; modSlotIndexer < inspectedItemData.ItemMods.Count; modSlotIndexer++)
				{
					ItemMod mod = inspectedItemData.ItemMods[modSlotIndexer];
					ItemModSlot modSlot = new ItemModSlot(mod, 34);
					modSlot.Left.Pixels = xPos;
					modSlot.Top.Pixels = yPos;
					xPos += (int)modSlot.Width.Pixels + 8;
					ModSlots.Add(modSlot);
					MasterBackground.Append(modSlot);
				}
			}

			return yPos + slotBackground.Height + 10;
        }
	}
}