using DestinyMod.Common.Items.Modifiers;
using DestinyMod.Common.ModPlayers;
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
            ItemDataPlayer itemDataPlayer = player.GetModPlayer<ItemDataPlayer>();
            if (itemDataPlayer.Recoil < 0)
            {
                return;
            }
            itemDataPlayer.Recoil += 30;
        }
    }
}