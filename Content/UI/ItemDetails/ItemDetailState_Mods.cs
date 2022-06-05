using Terraria.GameContent.UI.Elements;
using Microsoft.Xna.Framework;
using DestinyMod.Core.UI;
using DestinyMod.Common.UI;
using System.Collections.Generic;
using DestinyMod.Common.Items.Modifiers;
using DestinyMod.Common.GlobalItems;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.UI;
using DestinyMod.Content.UI.MouseText;
using DestinyMod.Core.Extensions;
using DestinyMod.Content.Items.Weapons.Ranged.Hakke;
using DestinyMod.Common.ModPlayers;

namespace DestinyMod.Content.UI.ItemDetails
{
	public class ItemDetailsState_Mods : UIElement
	{
		public ItemDetailsState ItemDetailsState { get; private set; }

		public UIText WeaponModsTitle { get; private set; }

		public UISeparator WeaponModsSeparator { get; private set; }

		public InfuseSlot InfuseItemSlot { get; private set; }

		public CatalystSlot CatalystSlot { get; private set; }

		public UIImageWithBackground ItemTierSlot { get; private set; }

		public ModSlotInventory ModSlotInventory { get; private set; }

		public IList<ItemModSlot> ModSlots { get; private set; }

		public int NormalHeight { get; private set; }

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

		public ItemDetailsState_Mods(ItemDetailsState itemDetailsState)
		{
			ItemDetailsState = itemDetailsState;

			WeaponModsTitle = new UIText("Weapon Mods");
			Append(WeaponModsTitle);

			WeaponModsSeparator = new UISeparator();
			WeaponModsSeparator.Top.Pixels = 20;
			WeaponModsSeparator.Width.Pixels = 300f;
			WeaponModsSeparator.Height.Pixels = 2f;
			WeaponModsSeparator.Color = ItemDetailsState.BaseColor_Light;
			Append(WeaponModsSeparator);

			Texture2D slotBackground = ModContent.Request<Texture2D>("DestinyMod/Content/UI/ItemDetails/ModSlot", AssetRequestMode.ImmediateLoad).Value;
			int xPos = 0;
			Asset<Texture2D> infuseSlot = ModContent.Request<Texture2D>("DestinyMod/Content/UI/ItemDetails/InfuseSlot", AssetRequestMode.ImmediateLoad);
			InfuseItemSlot = new InfuseSlot(120);
			InfuseItemSlot.Left.Pixels = xPos;
			InfuseItemSlot.Top.Pixels = 28;
			xPos += infuseSlot.Width() + 8;
			Append(InfuseItemSlot);

			if (ItemDetailsState.InspectedItemData.ItemCatalyst >= 0)
			{
				ItemDataPlayer itemDataPlayer = Main.LocalPlayer.GetModPlayer<ItemDataPlayer>();
				CatalystSlot = new CatalystSlot(itemDataPlayer.CatalystData[ItemDetailsState.InspectedItemData.ItemCatalyst]);
				CatalystSlot.Left.Pixels = xPos;
				CatalystSlot.Top.Pixels = 28;
				xPos += infuseSlot.Width() + 8;
				Append(CatalystSlot);
			}

			ModSlotInventory = new ModSlotInventory(ItemDetailsState, null, 7);
			ModSlotInventory.Top.Pixels = 36 + slotBackground.Height;
			ModSlotInventory.Visible = false;
			ItemDataItem inspectedItemData = ItemDetailsState.InspectedItem.GetGlobalItem<ItemDataItem>();
			if (inspectedItemData.ItemMods != null)
			{
				ModSlots = new List<ItemModSlot>();
				for (int modSlotIndexer = 0; modSlotIndexer < inspectedItemData.ItemMods.Count; modSlotIndexer++)
				{
					ItemMod mod = inspectedItemData.ItemMods[modSlotIndexer];
					ItemModSlot modSlot = new ItemModSlot(mod);
					modSlot.Left.Pixels = xPos;
					modSlot.Top.Pixels = 28;
					modSlot.OnUpdate += HandleModMouseText;
					modSlot.OnMouseOver += HandleBringingUpModSlotInventory;
					modSlot.OnUpdate += HandleClosingModSlotInventory;
					xPos += (int)modSlot.Width.Pixels + 8;
					ModSlots.Add(modSlot);
					Append(modSlot);
				}
			}

			Vector2 size = this.CalculateChildrenSize();
			Width.Pixels = size.X;
			Height.Pixels = size.Y;
			NormalHeight = (int)size.Y;

			Append(ModSlotInventory);
		}

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

		public void HandleModMouseText(UIElement affectedElement)
		{
			if (affectedElement is not ItemModSlot itemModSlot || !itemModSlot.ContainsPoint(Main.MouseScreen))
			{
				return;
			}

			string subtitle = itemModSlot.ItemMod.ApplyType == 0 ? string.Empty : itemModSlot.ItemMod.ApplyType.ToString() + " Mod";
			ItemDetailsState.MouseText_TitleAndSubtitle.UpdateData(itemModSlot.ItemMod.DisplayName ?? itemModSlot.ItemMod.Name, subtitle);
			ItemDetailsState.MouseText_BodyText.UpdateData(itemModSlot.ItemMod.Description);

			MouseTextState mouseTextState = ModContent.GetInstance<MouseTextState>();
			mouseTextState.AppendToMasterBackground(ItemDetailsState.MouseText_TitleAndSubtitle);
			mouseTextState.AppendToMasterBackground(ItemDetailsState.MouseText_BodyText);
		}

		public void HandleBringingUpModSlotInventory(UIMouseEvent evt, UIElement listeningElement)
        {
			if (listeningElement is not ItemModSlot itemModSlot)
			{
				return;
			}

			ModSlotInventory.SetUpInventorySlots(itemModSlot);
			ModSlotInventory.Visible = true;
			ItemDetailsState.Customization.Visible = false;
			Height.Pixels = this.CalculateChildrenSize().Y;
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
				ItemDetailsState.Customization.Visible = true;
				Height.Pixels = this.CalculateChildrenSize().Y;
			}
		}
	}
}