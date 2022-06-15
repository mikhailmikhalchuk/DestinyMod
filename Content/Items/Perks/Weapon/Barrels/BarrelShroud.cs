using DestinyMod.Common.GlobalItems;
using DestinyMod.Common.Items.Modifiers;
using Terraria;

namespace DestinyMod.Content.Items.Perks.Weapon.Barrels
{
    public class BarrelShroud : ItemPerk
    {
        public override void SetDefaults()
        {
            DisplayName = "Barrel Shroud";
            Description = "Balanced shotgun barrel."
                + "\n- Increases stability"
                + "\n- Increases bullet speed";
        }

        public override void SetItemDefaults(Item item)
        {
            ItemDataItem itemDataItem = item.GetGlobalItem<ItemDataItem>();
            if (itemDataItem.Stability >= 0)
            {
                itemDataItem.Stability += 10;
            }
        }
    }
}