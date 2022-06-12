using DestinyMod.Common.Items.Modifiers;
using DestinyMod.Common.ModPlayers;
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
            ItemDataPlayer itemDataPlayer = player.GetModPlayer<ItemDataPlayer>();
            if (itemDataPlayer.Range >= 0)
            {
                itemDataPlayer.Range += 15;
            }

            if (itemDataPlayer.Stability >= 0)
            {
                itemDataPlayer.Stability -= 10;
            }
        }
    }
}