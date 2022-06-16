using Terraria.UI;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using DestinyMod.Content.UI.MouseText;
using Terraria.ModLoader;
using DestinyMod.Common.GlobalItems;
using DestinyMod.Common.ModPlayers;
using DestinyMod.Common.Items.Modifiers;
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

        public ModSlot ReferenceModSlot;

        public IList<ModSlot> ItemModSlots { get; private set; }

        public int Columns { get; private set; }

        // I am good coder, please no fire
        public ModSlotInventory(ItemDetailsState itemDetailState, ModSlot referenceModSlot, int columns)
        {
            if (columns <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(columns), columns, " cannot be less than or equal to zero");
            }

            ItemDetailsState = itemDetailState;
            ItemModSlots = new List<ModSlot>();
            Columns = columns;
            SetUpInventorySlots(referenceModSlot);
        }

        public void SetUpInventorySlots(ModSlot referenceModSlot)
        {
            ReferenceModSlot = referenceModSlot;
            Player player = Main.LocalPlayer;
            ItemDataPlayer itemDataPlayer = player.GetModPlayer<ItemDataPlayer>();
            int minimumRowsRequired = (itemDataPlayer.UnlockedMods.Count + 1) / Columns + 1;
            ItemModSlots = new List<ModSlot>();
            for (int y = 0; y < minimumRowsRequired; y++)
            {
                for (int x = 0; x < Columns; x++)
                {
                    ModSlot itemModSlot = new ModSlot();
                    itemModSlot.Left.Pixels = (ModSlot.BackgroundTexture.Width + CommonSpacing) * x;
                    itemModSlot.Top.Pixels = (ModSlot.BackgroundTexture.Height + CommonSpacing) * y;
                    itemModSlot.OnUpdate += HandleModSlotMouseText;
                    itemModSlot.OnMouseDown += HandleModSlotApply;
                    ItemModSlots.Add(itemModSlot);
                    Append(itemModSlot);
                }
            }

            PopulateInventorySlots(player);
            ModSlot lastModSlotCreated = ItemModSlots[ItemModSlots.Count - 1];
            NormalSize = new Vector2(lastModSlotCreated.Left.Pixels + lastModSlotCreated.Width.Pixels, lastModSlotCreated.Top.Pixels + lastModSlotCreated.Height.Pixels);
            Width.Pixels = NormalSize.X;
            Height.Pixels = NormalSize.Y;
        }

        public void PopulateInventorySlots(Player player)
        {
            foreach (ModSlot itemModSlot in ItemModSlots)
            {
                itemModSlot.UpdateItemMod(null);
            }

            ItemDataPlayer itemDataPlayer = player.GetModPlayer<ItemDataPlayer>();
            ItemModSlots[0].UpdateItemMod(ModifierBase.CreateInstanceOf<NullMod>());
            for (int unlockedModIndexer = 0; unlockedModIndexer < itemDataPlayer.UnlockedMods.Count; unlockedModIndexer++)
            {
                ItemModSlots[unlockedModIndexer + 1].UpdateItemMod(ItemMod.GetInstance(itemDataPlayer.UnlockedMods[unlockedModIndexer]));
            }
        }

        public void HandleModSlotMouseText(UIElement affectedElement)
        {
            if (!Visible || affectedElement is not ModSlot itemModSlot || itemModSlot.ItemMod == null || !affectedElement.ContainsPoint(Main.MouseScreen))
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
            if (!Visible || ReferenceModSlot == null || listeningElement is not ModSlot itemModSlot || itemModSlot.ItemMod == null || ReferenceModSlot.ItemMod.Type == itemModSlot.ItemMod.Type)
            {
                return;
            }

            ReferenceModSlot.UpdateItemMod(itemModSlot.ItemMod);
            ItemDataItem inspectedItemData = ItemDetailsState.InspectedItem.GetGlobalItem<ItemDataItem>();
            SoundEngine.PlaySound(SoundID.Grab);
            inspectedItemData.ItemMods.Clear();
            foreach (ModSlot itemMod in ItemDetailsState.Mods.ModSlots)
            {
                inspectedItemData.ItemMods.Add(itemMod.ItemMod);
            }

            ItemDetailsState.InspectedItem.ModItem?.SetDefaults();
            inspectedItemData.SetDefaults(ItemDetailsState.InspectedItem);
            ItemDetailsState.Stats.UpdateElementData();
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