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

        public override void UseSpeedMultiplier(Player player, Item item, ref float multiplier)
        {
            multiplier *= 1.1f;
        }

        public override void Update(Player player)
        {
            if (SocketedItem == null)
            {
                return;
            }

            SocketedItem.GetGlobalItem<ItemDataItem>().Recoil += 30;
        }
    }
}