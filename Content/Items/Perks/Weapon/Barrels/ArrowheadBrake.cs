using DestinyMod.Common.GlobalItems;
using DestinyMod.Common.Items.Modifiers;
using Terraria;

namespace DestinyMod.Content.Items.Perks.Weapon.Barrels
{
    public class ArrowheadBrake : ItemPerk
    {
        public override void SetDefaults()
        {
            DisplayName = "Arrowhead Brake";
            Description = "Lightly vented barrel."
                + "\n- Greatly controls recoil"
                + "\n- Increases bullet speed";
        }

        public override void SetItemDefaults(Item item)
        {
            ItemDataItem itemDataItem = item.GetGlobalItem<ItemDataItem>();
            if (itemDataItem.Recoil >= 0)
            {
                itemDataItem.Recoil += 30;
            }
        }

        public override void UseSpeedMultiplier(Player player, Item item, ref float multiplier)
        {
            multiplier *= 1.1f;
        }
    }
}