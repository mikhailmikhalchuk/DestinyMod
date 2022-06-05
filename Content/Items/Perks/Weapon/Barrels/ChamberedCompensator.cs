using DestinyMod.Common.Items.Modifiers;

namespace DestinyMod.Content.Items.Perks.Weapon.Barrels
{
    public class ChamberedCompensator : ItemPerk
    {
        public override void SetDefaults()
        {
            DisplayName = "Chambered Compensator";
            Description = "- Moderately decreases bullet spread"
                + "\n- Slightly decreases firing speed";
        }
    }
}