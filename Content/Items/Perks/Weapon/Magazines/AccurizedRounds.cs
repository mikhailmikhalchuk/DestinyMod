using DestinyMod.Common.GlobalItems;
using DestinyMod.Common.Items.Modifiers;
using Terraria;

namespace DestinyMod.Content.Items.Perks.Weapon.Magazines
{
    public class AccurizedRounds : ItemPerk
    {
        public override void SetDefaults()
        {
            DisplayName = "Accurized Rounds";
            Description = "This weapon can fire longer distances."
                + "\n- Increases range";
        }

        public override void SetItemDefaults(Item item)
        {
            ItemDataItem itemDataItem = item.GetGlobalItem<ItemDataItem>();
            if (itemDataItem.Range >= 0)
            {
                itemDataItem.Range += 10;
            }
        }
    }
}