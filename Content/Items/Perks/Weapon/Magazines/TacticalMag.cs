using DestinyMod.Common.GlobalItems;
using DestinyMod.Common.Items.Modifiers;
using Terraria;

namespace DestinyMod.Content.Items.Perks.Weapon.Magazines
{
    public class TacticalMag : ItemPerk
    {
        public override void SetDefaults()
        {
            DisplayName = "Tactical Mag";
            Description = "This weapon has multiple tactical improvements."
                + "\n- Slightly increases stability"
                + "\n- Increases reload speed"
                + "\n- Slightly increases magazine size";
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