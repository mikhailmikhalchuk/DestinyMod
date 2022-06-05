using System;

namespace DestinyMod.Common.Items.Modifiers
{
    [Flags]
    public enum ItemType
    {
        Weapon = 1,
        Armor = 2,
        Ghost = 4,
    }
}
