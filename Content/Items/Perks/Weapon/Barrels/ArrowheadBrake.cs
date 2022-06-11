using DestinyMod.Common.Items.Modifiers;
using Terraria;

namespace DestinyMod.Content.Items.Perks.Weapon.Barrels
{
    public class ArrowheadBrake : ItemPerk
    {
        public override void SetDefaults()
        {
            DisplayName = "Arrowhead Brake";
            Description = "- Decreases bullet spread"
                + "\n- Increases firing speed";
        }

        public override void UseSpeedMultiplier(Player player, Item item, ref float multiplier)
        {
            multiplier *= 1.1f;
        }
    }
}