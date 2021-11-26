using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Graphics.Effects;

namespace TheDestinyMod.NPCs.Vex.VaultOfGlass
{
    public class DetainmentBubble : ModNPC
    {

        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Detainment Bubble");
            NPCID.Sets.ExcludedFromDeathTally[npc.type] = true;
        }

        public override void SetDefaults() {
            npc.damage = 0;
            npc.width = 300;
            npc.height = 300;
            npc.lifeMax = 1000;
            npc.defense = 5;
            npc.noGravity = true;
            npc.knockBackResist = 0f;
            npc.chaseable = false;
        }

        public override void AI() {
            Player player = Main.player[(int)npc.ai[0]];
            if (player.active && !player.dead && npc.active) {
                npc.Center = player.Center;
            }
            else if (player.dead && npc.active) {
                npc.active = false;
                npc.life = 0;
            }
        }

        public override void NPCLoot() {
            Player player = Main.player[(int)npc.ai[0]];
            player.ClearBuff(ModContent.BuffType<Buffs.Debuffs.Detained>());
        }
    }
}