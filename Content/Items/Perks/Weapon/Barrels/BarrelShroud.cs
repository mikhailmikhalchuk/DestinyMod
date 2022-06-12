using DestinyMod.Common.Items.Modifiers;
using DestinyMod.Common.ModPlayers;
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

        public override void Update(Player player)
        {
            ItemDataPlayer itemDataPlayer = player.GetModPlayer<ItemDataPlayer>();
            if (itemDataPlayer.Stability < 0)
            {
                return;
            }
            itemDataPlayer.Stability += 10;
        }
    }
}