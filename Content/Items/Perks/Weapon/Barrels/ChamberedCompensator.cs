using DestinyMod.Common.Items.Modifiers;
using DestinyMod.Common.ModPlayers;
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

        public override void Update(Player player)
        {
            ItemDataPlayer itemDataPlayer = player.GetModPlayer<ItemDataPlayer>();
            if (itemDataPlayer.Recoil >= 0)
            {
                itemDataPlayer.Recoil += 10;
            }

            if (itemDataPlayer.Stability >= 0)
            {
                itemDataPlayer.Stability += 10;
            }
        }
    }
}