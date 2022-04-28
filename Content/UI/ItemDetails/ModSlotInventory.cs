using Terraria.UI;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Microsoft.Xna.Framework;
using Terraria;
using System;
using System.Collections.Generic;

namespace DestinyMod.Content.UI.ItemDetails
{
    public class ModSlotInventory : UIElement
    {
        public IList<ItemModSlot> ItemModSlots { get; private set; }

        public ModSlotInventory(int rows, int columns)
        {
            ItemModSlots = new List<ItemModSlot>();
            for (int x = 0; x < rows; x++)
            {
                for (int y = 0; y < columns; y++)
                {
                    ItemModSlot itemModSlot = new ItemModSlot();

                }
            }
        }
    }
}