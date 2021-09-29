using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheDestinyMod.Buffs.Debuffs
{
    public class MarkedForNegation : ModBuff
    {
        public override void SetDefaults() {
            DisplayName.SetDefault("Marked for Negation");
            Description.SetDefault("You have been marked");
            Main.buffNoTimeDisplay[Type] = true;
            Main.buffNoSave[Type] = true;
            Main.debuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex) {
            player.GetModPlayer<DestinyPlayer>().markedForNegation = true;
            player.buffTime[buffIndex] = 18000;
        }
    }
}