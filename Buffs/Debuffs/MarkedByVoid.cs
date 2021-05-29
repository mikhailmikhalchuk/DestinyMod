using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheDestinyMod.Buffs.Debuffs
{
    public class MarkedByVoid : ModBuff
    {
        public override void SetDefaults() {
            DisplayName.SetDefault("Marked by the Void");
            Description.SetDefault("Time's Conflux blinds you");
            Main.buffNoTimeDisplay[Type] = true;
            Main.buffNoSave[Type] = true;
            Main.debuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex) {
            player.buffTime[buffIndex] = 18000;
        }
    }
}