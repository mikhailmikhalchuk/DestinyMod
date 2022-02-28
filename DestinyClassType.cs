using System;

namespace DestinyMod
{
    [Flags]
    public enum DestinyClassType : byte
    {
        None,
        Titan = 1,
        Hunter = 2,
        Warlock = 4,
    }
}