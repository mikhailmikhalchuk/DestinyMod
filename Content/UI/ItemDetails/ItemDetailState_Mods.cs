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
using Terraria.UI;
using DestinyMod.Content.UI.MouseText;

namespace DestinyMod.Content.UI.ItemDetails
{
	public partial class ItemDetailsState : DestinyModUIState
	{
		public UIText WeaponModsTitle { get; private set; }

		public UISeparator WeaponModsSeparator { get; private set; }

		public UIImageWithBackground InfuseItemSlot { get; private set; }

		public UIImageWithBackground ItemTierSlot { get; private set; }

		public ModSlotInventory ModSlotInventory { get; private set; }

		public IList<ItemModSlot> ModSlots { get; private set; }

		public int InitializeModsSection(int yPos, bool includeItemTierSlot = false)
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
			WeaponModsSeparator.Color = BaseColor_Light;
			MasterBackground.Append(WeaponModsSeparator);

			Texture2D slotBackground = ModContent.Request<Texture2D>("DestinyMod/Content/UI/ItemDetails/ModSlot", AssetRequestMode.ImmediateLoad).Value;
			int xPos = 10;
			yPos += 8;
			Asset<Texture2D> infuseSlot = ModContent.Request<Texture2D>("DestinyMod/Content/UI/ItemDetails/InfuseSlot", AssetRequestMode.ImmediateLoad);
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

			ModSlotInventory = new ModSlotInventory(this, null, 7);
			ModSlotInventory.Left.Pixels = 10;
			ModSlotInventory.Top.Pixels = yPos + slotBackground.Height + 8;
			MasterBackground.Append(ModSlotInventory);
			ModSlotInventory.Visible = false;
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
					modSlot.OnUpdate += HandleModMouseText;
					modSlot.OnMouseOver += HandleBringingUpModSlotInventory;
					modSlot.OnUpdate += HandleClosingModSlotInventory;
					xPos += (int)modSlot.Width.Pixels + 8;
					ModSlots.Add(modSlot);
					MasterBackground.Append(modSlot);
				}
			}

			yPos += slotBackground.Height + 8;
			UIElement modSelectionBackground = new UIElement();
			modSelectionBackground.Left.Pixels = 10;
			modSelectionBackground.Top.Pixels = yPos;

			return yPos;
        }

		public void HandleModMouseText(UIElement affectedElement)
		{
			if (affectedElement is not ItemModSlot itemModSlot || !itemModSlot.ContainsPoint(Main.MouseScreen))
			{
				return;
			}

			string subtitle = itemModSlot.ItemMod.ApplyType == 0 ? string.Empty : itemModSlot.ItemMod.ApplyType.ToString() + " Mod";
			MouseText_TitleAndSubtitle.UpdateData(itemModSlot.ItemMod.DisplayName ?? itemModSlot.ItemMod.Name, subtitle);
			MouseText_BodyText.UpdateData(itemModSlot.ItemMod.Description);

			MouseTextState mouseTextState = ModContent.GetInstance<MouseTextState>();
			mouseTextState.AppendToMasterBackground(MouseText_TitleAndSubtitle);
			mouseTextState.AppendToMasterBackground(MouseText_BodyText);
		}

		public void HandleBringingUpModSlotInventory(UIMouseEvent evt, UIElement listeningElement)
        {
			if (listeningElement is not ItemModSlot itemModSlot)
			{
				return;
			}

			ModSlotInventory.SetUpInventorySlots(itemModSlot);
			ModSlotInventory.Visible = true;
		}

		public void HandleClosingModSlotInventory(UIElement affectedElement)
		{
			if (affectedElement is not ItemModSlot itemModSlot || ModSlotInventory.ReferenceModSlot != itemModSlot)
			{
				return;
			}

			CalculatedStyle inventoryDimensions = ModSlotInventory.GetDimensions();
			Rectangle inflatedInventoryHitbox = new Rectangle((int)inventoryDimensions.X, (int)inventoryDimensions.Y - 8, (int)ModSlotInventory.NormalSize.X, (int)ModSlotInventory.NormalSize.Y + 8);
			if (!itemModSlot.ContainsPoint(Main.MouseScreen) && !inflatedInventoryHitbox.Contains(Main.MouseScreen.ToPoint()))
			{
				ModSlotInventory.ReferenceModSlot = null;
				ModSlotInventory.Visible = false;
			}
		}
	}
}