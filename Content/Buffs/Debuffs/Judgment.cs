using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using DestinyMod.Common.Buffs;
using Terraria.DataStructures;

namespace DestinyMod.Content.Buffs.Debuffs
{
    public class Judgement : DestinyModBuff
    {
        public override void DestinySetDefaults() 
        {
            DisplayName.SetDefault("Judgment");
            Description.SetDefault("20% reduced applied defense");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true;
        }
    }
}