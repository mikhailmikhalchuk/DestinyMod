using DestinyMod.Common.Items.Modifiers;
using DestinyMod.Common.ModPlayers;
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