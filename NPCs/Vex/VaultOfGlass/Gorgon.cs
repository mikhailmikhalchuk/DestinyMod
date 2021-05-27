using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Graphics.Effects;

namespace TheDestinyMod.NPCs.Vex.VaultOfGlass
{
    public class Gorgon : ModNPC
    {
        public override string Texture => "Terraria/NPC_" + NPCID.DemonEye;

        private int stepsTaken;

        private bool forward = true;

        private int timeUntilEveryoneIsDead;

        public override void SetDefaults() {
            npc.CloneDefaults(NPCID.DemonEye);
            aiType = 0;
            npc.aiStyle = 0;
            npc.lifeMax = 10000;
            npc.defense = 50;
        }

        public override void AI() {
            if (stepsTaken >= 40 && forward && timeUntilEveryoneIsDead == 0) {
                forward = false;
                npc.velocity.X = -1;
                npc.spriteDirection = -1;
            }
            else if (stepsTaken <= -40 && !forward && timeUntilEveryoneIsDead == 0) {
                forward = true;
                npc.velocity.X = 1;
                npc.spriteDirection = 1;
            }
            if (forward && timeUntilEveryoneIsDead == 0) {
                stepsTaken++;
                npc.velocity.X = 1;
                npc.spriteDirection = 1;
            }
            else if (!forward && timeUntilEveryoneIsDead == 0) {
                stepsTaken--;
                npc.velocity.X = -1;
                npc.spriteDirection = -1;
            }
            if (timeUntilEveryoneIsDead > 0 && timeUntilEveryoneIsDead < 600) {
                timeUntilEveryoneIsDead++;
            }
            else if (timeUntilEveryoneIsDead >= 600) {
                foreach (Player player in Main.player) {
                    if (player.active) {
                        Terraria.DataStructures.PlayerDeathReason deathReason = new Terraria.DataStructures.PlayerDeathReason
                        {
                            SourceCustomReason = player.name + " was spotted.",
                            SourceNPCIndex = npc.type
                        };
                        player.KillMe(deathReason, 0, 0);
                    }
                }
                timeUntilEveryoneIsDead = -1;
                DestinyPlayer.gorgonsHaveSpotted = false;
            }
            foreach (Player player in Main.player) {
                if ((player.Center - npc.Center).Length() < 100 && timeUntilEveryoneIsDead == 0 && player.active) {
                    timeUntilEveryoneIsDead++;
                    Main.NewText("A Gorgon has found its prey");
                    DestinyPlayer.gorgonsHaveSpotted = true;
                }
            }
        }

        public override void NPCLoot() {
            foreach (NPC npc in Main.npc) {
                if (npc.active && npc.type == ModContent.NPCType<Gorgon>()) {
                    npc.lifeMax += Main.expertMode ? 2000 : 1000;
                    npc.defense += 5;
                    if (npc.life == 10000 && !Main.expertMode || npc.life == 20000 && Main.expertMode) {
                        npc.life = npc.lifeMax;
                    }
                }
            }
            Main.NewText("The Gorgons become stronger");
        }
    }
}