using DestinyMod.Common.Buffs;
using Terraria;

namespace DestinyMod.Content.Buffs.Debuffs
{
    public class MarkedForNegation : DestinyModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Marked for Negation");
            Description.SetDefault("You have been marked");
            Main.buffNoTimeDisplay[Type] = true;
            Main.buffNoSave[Type] = true;
            Main.debuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex) => player.buffTime[buffIndex] = 18000;
    }
}