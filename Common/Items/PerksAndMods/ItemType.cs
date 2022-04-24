using System;

namespace DestinyMod.Common.Items.PerksAndMods
{
    [Flags]
    public enum ItemType
    {
        Weapon = 1,
        Armor = 2,
        Ghost = 4,
    }
}
