using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Graphics.Effects;
using System.Collections.Generic;
using System.Linq;

namespace TheDestinyMod.NPCs.Vex.VaultOfGlass
{
    public class OracleSpawner : ModNPC
    {
        public override string Texture => "Terraria/NPC_" + NPCID.DemonEye;

        private bool alreadySummoned;

        private int timesShown;

        private int counter;

        private int oracleOrder;

        private List<int> oraclePositions = new List<int>();

        private List<int> alreadyCalled = new List<int>();

        public override void SetDefaults() {
            npc.CloneDefaults(NPCID.DemonEye);
            aiType = 0;
            npc.aiStyle = 0;
            npc.dontTakeDamage = true;
            npc.noGravity = true;
            npc.knockBackResist = 0f;
        }

        public override void AI() {
            counter++;
            if (DestinyWorld.oraclesKilledOrder == 4 && DestinyWorld.oraclesTimesRefrained == 0 || DestinyWorld.oraclesKilledOrder == 6 && DestinyWorld.oraclesTimesRefrained == 1 || Main.npc.FirstOrDefault(n => n.type == ModContent.NPCType<Oracle>() && n.active) == null && alreadySummoned) {
                if (DestinyWorld.oraclesKilledOrder == 4 && DestinyWorld.oraclesTimesRefrained == 0 || DestinyWorld.oraclesKilledOrder == 6 && DestinyWorld.oraclesTimesRefrained == 1) DestinyWorld.oraclesTimesRefrained++;
                counter = -200;
                timesShown = 0;
                DestinyWorld.oraclesKilledOrder = 1;
                alreadySummoned = false;
                alreadyCalled.Clear();
                oraclePositions.Clear();
                oracleOrder = 0;
            }
            if (!alreadySummoned && Main.netMode != NetmodeID.MultiplayerClient && counter > 0) {
                Main.NewText("The Oracles prepare to sing their refrain");
                DestinyWorld.oraclesKilledOrder = 1;
                int localAdd = 100;
                for (int i = 0; i < (DestinyWorld.oraclesTimesRefrained == 1 ? 5 : DestinyWorld.oraclesTimesRefrained == 2 ? 7 : 3); i++) {
                    int one = NPC.NewNPC((int)(npc.position.X + localAdd), (int)npc.position.Y, ModContent.NPCType<Oracle>(), 0, i);
                    Main.npc[one].hide = true;
                    oraclePositions.Add(one);
                    localAdd -= 50;
                }
                alreadySummoned = true;
            }
            else if (alreadySummoned && counter > 90 && (alreadyCalled.Count < 3 && DestinyWorld.oraclesTimesRefrained == 0 || alreadyCalled.Count < 5 && DestinyWorld.oraclesTimesRefrained == 1 || alreadyCalled.Count < 7 && DestinyWorld.oraclesTimesRefrained == 2) && timesShown == 0) {
                int randCheck = Main.rand.Next(0, DestinyWorld.oraclesTimesRefrained == 0 ? 3 : DestinyWorld.oraclesTimesRefrained == 1 ? 5 : 7);
                while (alreadyCalled.Contains(randCheck)) {
                    randCheck = Main.rand.Next(0, DestinyWorld.oraclesTimesRefrained == 0 ? 3 : DestinyWorld.oraclesTimesRefrained == 1 ? 5 : 7);
                }
                alreadyCalled.Add(randCheck);
                Main.npc[oraclePositions[randCheck]].hide = false;
                Main.npc[oraclePositions[randCheck]].ai[0] = alreadyCalled.Count;
                Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, $"Sounds/NPC/Oracle{randCheck + 1}"), Main.npc[oraclePositions[randCheck]].position);
                counter = 0;
            }
            else if (alreadySummoned && counter > 90 && (alreadyCalled.Count <= 3 && DestinyWorld.oraclesTimesRefrained == 0 || alreadyCalled.Count <= 5 && DestinyWorld.oraclesTimesRefrained == 1 || alreadyCalled.Count <= 7 && DestinyWorld.oraclesTimesRefrained == 2) && alreadyCalled.Count > 0 && timesShown == 1) {
                oracleOrder++;
                foreach (NPC npc in Main.npc) {
                    if (npc.ai[0] == oracleOrder && npc.type == ModContent.NPCType<Oracle>() && npc.active) {
                        npc.hide = false;
                        Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, $"Sounds/NPC/Oracle{alreadyCalled[0] + 1}"), npc.position);
                        break;
                    }
                }
                alreadyCalled.RemoveAt(0);
                counter = 0;
            }
            else if (alreadySummoned && counter > 90 && (alreadyCalled.Count == 3 && DestinyWorld.oraclesTimesRefrained == 0 || alreadyCalled.Count == 5 && DestinyWorld.oraclesTimesRefrained == 1 || alreadyCalled.Count == 7 && DestinyWorld.oraclesTimesRefrained == 2) && timesShown == 0 || alreadySummoned && counter > 120 && alreadyCalled.Count == 0 && timesShown == 1) {
                timesShown++;
                foreach (int oracle in oraclePositions) {
                    Main.npc[oracle].hide = true;
                }
                counter = 0;
            }
            else if (alreadySummoned && counter == 120 && alreadyCalled.Count == 0 && timesShown == 2) {
                Main.NewText("The Templar summons the Oracles");
                foreach (int oracle in oraclePositions) {
                    Main.npc[oracle].hide = false;
                    Main.npc[oracle].dontTakeDamage = false;
                }
            }
            else if (alreadySummoned && counter > 600 && alreadyCalled.Count == 0 && timesShown == 2) {
                Main.NewText("MARKED BY AN ORACLE!");
                foreach (Player player in Main.player) {
                    if (player.active && !player.HasBuff(ModContent.BuffType<Buffs.Debuffs.MarkedForNegation>())) {
                        player.AddBuff(ModContent.BuffType<Buffs.Debuffs.MarkedForNegation>(), 1);
                    }
                }
                foreach (NPC npc in Main.npc) {
                    if (npc.type == ModContent.NPCType<Oracle>()) {
                        npc.active = false;
                    }
                }
            }
        }
    }
}