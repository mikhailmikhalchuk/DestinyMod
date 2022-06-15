using DestinyMod.Common.GlobalItems;
using DestinyMod.Common.Items.Modifiers;
using Terraria;

namespace DestinyMod.Content.Items.Perks.Weapon.Barrels
{
    public class FullBore : ItemPerk
    {
        public override void SetDefaults()
        {
            DisplayName = "Full Bore";
            Description = "Barrel optimized for distance."
                + "\n- Greatly increases range"
                + "\n- Decreases stability"
                + "\n- Slightly decreases bullet speed";
        }

        public override void SetItemDefaults(Item item)
        {
            ItemDataItem itemDataItem = item.GetGlobalItem<ItemDataItem>();
            if (itemDataItem.Range >= 0)
            {
                itemDataItem.Range += 15;
            }

            if (itemDataItem.Stability >= 0)
            {
                itemDataItem.Stability -= 10;
            }
        }
    }
}