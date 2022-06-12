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

        public override void Update(Player player)
        {
            if (SocketedItem == null)
            {
                return;
            }

            SocketedItem.GetGlobalItem<ItemDataItem>().Stability += 5;
        }
    }
}