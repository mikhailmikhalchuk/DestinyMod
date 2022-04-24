using DestinyMod.Common.Items.PerksAndMods;

namespace DestinyMod.Content.Items.Perks.Weapon.Traits
{
    public class Frenzy : ItemPerk
    {
        public override void SetDefaults()
        {
            DisplayName = "Frenzy";
            Description = "Being in combat for an extended time increases this weapon's damage and firing speed until you are out of combat";
        }
    }
}