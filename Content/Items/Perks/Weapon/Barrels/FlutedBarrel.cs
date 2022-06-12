using DestinyMod.Common.Items.Modifiers;
using DestinyMod.Common.ModPlayers;
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
            ItemDataPlayer itemDataPlayer = player.GetModPlayer<ItemDataPlayer>();
            if (itemDataPlayer.Stability >= 0)
            {
                itemDataPlayer.Stability += 5;
            }
        }
    }
}