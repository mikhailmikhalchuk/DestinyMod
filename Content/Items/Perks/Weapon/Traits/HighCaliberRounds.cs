using DestinyMod.Common.Items.Modifiers;

namespace DestinyMod.Content.Items.Perks.Weapon.Traits
{
    public class HighCaliberRounds : ItemPerk
    {
        public override void SetDefaults()
        {
            DisplayName = "High-Caliber Rounds";
            Description = "Bullets fired from this weapon have greater knockback"
                + "\n- Slightly increases range";
        }
    }
}