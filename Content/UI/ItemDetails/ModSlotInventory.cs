using Terraria.UI;
using System;
using System.Collections.Generic;
using Terraria;
using DestinyMod.Content.UI.MouseText;
using Terraria.ModLoader;
using DestinyMod.Common.GlobalItems;
using DestinyMod.Common.ModPlayers;
using DestinyMod.Common.Items.PerksAndMods;
using DestinyMod.Content.Items.Mods;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DestinyMod.Content.UI.ItemDetails
{
    public class ModSlotInventory : UIElement
    {
        public static readonly int CommonSpacing = 8;

        public bool Visible;

        public Vector2 NormalSize { get; private set; }

        public ItemDetailsState ItemDetailsState { get; private set; }

        public ItemModSlot ReferenceModSlot;

        public IList<ItemModSlot> ItemModSlots { get; private set; }

        public int Columns { get; private set; }

        // I am good coder, please no fire
        public ModSlotInventory(ItemDetailsState itemDetailState, ItemModSlot referenceModSlot, int columns)
        {
            if (columns <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(columns), columns, " cannot be less than or equal to zero");
            }

            ItemDetailsState = itemDetailState;
            ItemModSlots = new List<ItemModSlot>();
            Columns = columns;
            SetUpInventorySlots(referenceModSlot);
        }

        public void SetUpInventorySlots(ItemModSlot referenceModSlot)
        {
            ReferenceModSlot = referenceModSlot;
            Player player = Main.LocalPlayer;
            ItemDataPlayer itemDataPlayer = player.GetModPlayer<ItemDataPlayer>();
            int minimumRowsRequired = (itemDataPlayer.UnlockedMods.Count + 1) / Columns + 1;
            ItemModSlots = new List<ItemModSlot>();
            for (int y = 0; y < minimumRowsRequired; y++)
            {
                for (int x = 0; x < Columns; x++)
                {
                    ItemModSlot itemModSlot = new ItemModSlot();
                    itemModSlot.Left.Pixels = (ItemModSlot.BackgroundTexture.Width + CommonSpacing) * x;
                    itemModSlot.Top.Pixels = (ItemModSlot.BackgroundTexture.Height + CommonSpacing) * y;
                    itemModSlot.OnUpdate += HandleModSlotMouseText;
                    itemModSlot.OnMouseDown += HandleModSlotApply;
                    ItemModSlots.Add(itemModSlot);
                    Append(itemModSlot);
                }
            }
            PopulateInventorySlots(player);
            ItemModSlot lastModSlotCreated = ItemModSlots[ItemModSlots.Count - 1];
            NormalSize = new Vector2(lastModSlotCreated.Left.Pixels + lastModSlotCreated.Width.Pixels, lastModSlotCreated.Top.Pixels + lastModSlotCreated.Height.Pixels);
            Width.Pixels = NormalSize.X;
            Height.Pixels = NormalSize.Y;
        }

        public void PopulateInventorySlots(Player player)
        {
            foreach (ItemModSlot itemModSlot in ItemModSlots)
            {
                itemModSlot.UpdateItemMod(null);
            }

            ItemDataPlayer itemDataPlayer = player.GetModPlayer<ItemDataPlayer>();
            ItemModSlots[0].UpdateItemMod(ModAndPerkBase.CreateInstanceOf<NullMod>());
            for (int unlockedModIndexer = 0; unlockedModIndexer < itemDataPlayer.UnlockedMods.Count; unlockedModIndexer++)
            {
                ItemMod itemMod = itemDataPlayer.UnlockedMods[unlockedModIndexer];
                ItemModSlots[unlockedModIndexer + 1].UpdateItemMod(itemMod);
            }
        }

        public void HandleModSlotMouseText(UIElement affectedElement)
        {
            if (!Visible || affectedElement is not ItemModSlot itemModSlot || itemModSlot.ItemMod == null || !affectedElement.ContainsPoint(Main.MouseScreen))
            {
                return;
            }

            string subtitle = itemModSlot.ItemMod.ApplyType == 0 ? string.Empty : itemModSlot.ItemMod.ApplyType.ToString() + " Mod";
            ItemDetailsState.MouseText_TitleAndSubtitle.UpdateData(itemModSlot.ItemMod.DisplayName ?? itemModSlot.ItemMod.Name, subtitle);
            ItemDetailsState.MouseText_BodyText.UpdateData(itemModSlot.ItemMod.Description);

            MouseTextState mouseTextState = ModContent.GetInstance<MouseTextState>();
            mouseTextState.AppendToMasterBackground(ItemDetailsState.MouseText_TitleAndSubtitle);
            mouseTextState.AppendToMasterBackground(ItemDetailsState.MouseText_BodyText);

            if (ReferenceModSlot.ItemMod.Type != itemModSlot.ItemMod.Type) // Thanks :)
            {
                mouseTextState.AppendToMasterBackground(ItemDetailsState.MouseText_ClickIndicator);
            }
        }

        public void HandleModSlotApply(UIMouseEvent evt, UIElement listeningElement)
        {
            if (!Visible || ReferenceModSlot == null || listeningElement is not ItemModSlot itemModSlot || ReferenceModSlot.ItemMod.Type == itemModSlot.ItemMod.Type)
            {
                return;
            }

            ReferenceModSlot.UpdateItemMod(itemModSlot.ItemMod);
            ItemDataItem inspectedItemData = ItemDetailsState.InspectedItem.GetGlobalItem<ItemDataItem>();
            inspectedItemData.ItemMods.Clear();
            foreach (ItemModSlot itemMod in ItemDetailsState.ModSlots)
            {
                inspectedItemData.ItemMods.Add(itemMod.ItemMod);
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (!Visible)
            {
                IgnoresMouseInteraction = true;
            }
            else
            {
                IgnoresMouseInteraction = false;
                base.Update(gameTime);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!Visible)
            {
                return;
            }

            base.Draw(spriteBatch);
        }
    }
}