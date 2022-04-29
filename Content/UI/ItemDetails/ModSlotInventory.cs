using Terraria.UI;
using System;
using System.Collections.Generic;
using Terraria;

namespace DestinyMod.Content.UI.ItemDetails
{
    public class ModSlotInventory : UIElement
    {
        public static readonly int CommonSpacing = 4;

        public IList<ItemModSlot> ItemModSlots { get; private set; }

        public ModSlotInventory(int rows, int columns)
        {
            if (rows <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(rows), rows, " cannot be less than or equal to zero");
            }

            if (columns <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(columns), columns, " cannot be less than or equal to zero");
            }

            ItemModSlots = new List<ItemModSlot>();
            for (int x = 0; x < rows; x++)
            {
                for (int y = 0; y < columns; y++)
                {
                    ItemModSlot itemModSlot = new ItemModSlot();
                    itemModSlot.Left.Pixels = (ItemModSlot.BackgroundTexture.Width + CommonSpacing) * x;
                    itemModSlot.Top.Pixels = (ItemModSlot.BackgroundTexture.Height + CommonSpacing) * y;
                    itemModSlot.OnUpdate += InventoryLogic;
                    itemModSlot.OnMouseDown += RegisterApply;
                    Append(itemModSlot);
                }
            }

            ItemModSlot lastModSlotCreated = ItemModSlots[ItemModSlots.Count - 1];
            Width.Pixels = lastModSlotCreated.Left.Pixels + lastModSlotCreated.Width.Pixels;
            Height.Pixels = lastModSlotCreated.Top.Pixels + lastModSlotCreated.Height.Pixels;
        }

        private void InventoryLogic(UIElement affectedElement)
        {
            if (!affectedElement.ContainsPoint(Main.MouseScreen))
            {
                return;
            }
        }

        private void RegisterApply(UIMouseEvent evt, UIElement listeningElement)
        {
            throw new NotImplementedException();
        }
    }
}