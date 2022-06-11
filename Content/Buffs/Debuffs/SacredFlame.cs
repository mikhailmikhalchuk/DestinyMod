using DestinyMod.Common.Buffs;
using Terraria;

namespace DestinyMod.Content.Buffs.Debuffs
{
    public class SacredFlame : DestinyModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sacred Flame");
            Description.SetDefault("You are going to explode");
            Main.buffNoTimeDisplay[Type] = true;
            Main.buffNoSave[Type] = true;
            Main.debuff[Type] = true;
        }
    }
}