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

        public override void Update(Player player)
        {
            if (SocketedItem == null)
            {
                return;
            }

            SocketedItem.GetGlobalItem<ItemDataItem>().Range += 15;
            SocketedItem.GetGlobalItem<ItemDataItem>().Stability -= 10;
        }
    }
}