using DestinyMod.Common.GlobalItems;
using DestinyMod.Common.Items.Modifiers;
using Terraria;

namespace DestinyMod.Content.Items.Perks.Weapon.Barrels
{
    public class ChamberedCompensator : ItemPerk
    {
        public override void SetDefaults()
        {
            DisplayName = "Chambered Compensator";
            Description = "Stable barrel attachment."
                + "\n- Increases stability"
                + "\n- Moderately controls recoil"
                + "\n- Slightly decreases bullet speed";
        }

        public override void SetItemDefaults(Item item)
        {
            ItemDataItem itemDataItem = item.GetGlobalItem<ItemDataItem>();
            if (itemDataItem.Recoil >= 0)
            {
                itemDataItem.Recoil += 10;
            }

            if (itemDataItem.Stability >= 0)
            {
                itemDataItem.Stability += 10;
            }
        }
    }
}