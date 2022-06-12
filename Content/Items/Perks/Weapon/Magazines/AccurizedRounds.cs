using DestinyMod.Common.Items.Modifiers;
using DestinyMod.Common.ModPlayers;
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

        public override void Update(Player player)
        {
            ItemDataPlayer itemDataPlayer = player.GetModPlayer<ItemDataPlayer>();
            if (itemDataPlayer.Range >= 0)
            {
                itemDataPlayer.Range += 10;
            }
        }
    }
}