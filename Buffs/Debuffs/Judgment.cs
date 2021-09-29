using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheDestinyMod.Buffs.Debuffs
{
    public class Judgment : ModBuff
    {
        public override void SetDefaults() {
            DisplayName.SetDefault("Judgment");
            Description.SetDefault("20% reduced defense");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex) {
            player.GetModPlayer<DestinyPlayer>().judged = true;
            player.statDefense -= player.statDefense / 5;
        }

        public override void Update(NPC npc, ref int buffIndex) {
            npc.GetGlobalNPC<NPCs.DestinyGlobalNPC>().judged = true;
            npc.defense -= npc.defense / 5;
        }
    }
}