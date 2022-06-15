using DestinyMod.Common.GlobalItems;
using DestinyMod.Common.Items.Modifiers;
using Terraria;

namespace DestinyMod.Content.Items.Perks.Weapon.Barrels
{
    public class FlutedBarrel : ItemPerk
    {
        public override void SetDefaults()
        {
            DisplayName = "Fluted Barrel";
            Description = "Ultra-light barrel."
                + "\n- Greatly increases bullet speed"
                + "\n- Slightly increases stability";
        }

        public override void SetItemDefaults(Item item)
        {
            ItemDataItem itemDataItem = item.GetGlobalItem<ItemDataItem>();
            if (itemDataItem.Stability >= 0)
            {
                itemDataItem.Stability += 5;
            }
        }
    }
}