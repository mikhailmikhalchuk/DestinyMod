using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Graphics.Effects;
using System.Collections.Generic;

namespace TheDestinyMod.NPCs.Vex.VaultOfGlass
{
    public class OracleSpawner : ModNPC
    {
        public override string Texture => "Terraria/NPC_" + NPCID.DemonEye;

        internal bool alreadySummoned = false;

        internal int timesShown = 0;

        internal int counter = 0;

        internal List<int> oraclePositions = new List<int>();

        internal List<int> alreadyCalled = new List<int>();

        internal List<int> orderOfTheOracles = new List<int>();

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
            int checker = 0;
            foreach (NPC npc in Main.npc) {
                if (npc.type == ModContent.NPCType<Oracle>() && npc.active) {
                    checker++;
                }
            }
            if (checker == 0) {
                counter = -120;
                timesShown = 0;
                alreadySummoned = false;
                DestinyWorld.oraclesKilledOrder = 1;
                alreadyCalled.Clear();
                oraclePositions.Clear();
                orderOfTheOracles.Clear();
            }
            if (DestinyWorld.oraclesKilledOrder == 6 && DestinyWorld.oraclesTimesRefrained == 0 || DestinyWorld.oraclesKilledOrder == 8 && DestinyWorld.oraclesTimesRefrained == 1) {
                DestinyWorld.oraclesTimesRefrained++;
                counter = 0;
                timesShown = 0;
                DestinyWorld.oraclesKilledOrder = 1;
                alreadySummoned = false;
                alreadyCalled.Clear();
                oraclePositions.Clear();
                orderOfTheOracles.Clear();
            }
            if (!alreadySummoned && Main.netMode != NetmodeID.MultiplayerClient && counter > 0) {
                Main.NewText("The Oracles prepare to sing their refrain");
                DestinyWorld.oraclesKilledOrder = 1;
                int one = NPC.NewNPC((int)(npc.position.X + 100), (int)npc.position.Y, ModContent.NPCType<Oracle>(), 0, 1);
                Main.npc[one].hide = true;
                oraclePositions.Add(one);
                one = NPC.NewNPC((int)(npc.position.X + 50), (int)npc.position.Y, ModContent.NPCType<Oracle>(), 0, 2);
                Main.npc[one].hide = true;
                oraclePositions.Add(one);
                one = NPC.NewNPC((int)(npc.position.X - 50), (int)npc.position.Y, ModContent.NPCType<Oracle>(), 0, 3);
                Main.npc[one].hide = true;
                oraclePositions.Add(one);
                one = NPC.NewNPC((int)(npc.position.X - 100), (int)npc.position.Y, ModContent.NPCType<Oracle>(), 0, 4);
                Main.npc[one].hide = true;
                oraclePositions.Add(one);
                one = NPC.NewNPC((int)(npc.position.X - 150), (int)npc.position.Y, ModContent.NPCType<Oracle>(), 0, 5);
                Main.npc[one].hide = true;
                oraclePositions.Add(one);
                if (DestinyWorld.oraclesTimesRefrained >= 1) {
                    one = NPC.NewNPC((int)(npc.position.X - 200), (int)npc.position.Y, ModContent.NPCType<Oracle>(), 0, 6);
                    Main.npc[one].hide = true;
                    oraclePositions.Add(one);
                    one = NPC.NewNPC((int)(npc.position.X - 250), (int)npc.position.Y, ModContent.NPCType<Oracle>(), 0, 7);
                    Main.npc[one].hide = true;
                    oraclePositions.Add(one);
                }
                if (DestinyWorld.oraclesTimesRefrained == 2) {
                    one = NPC.NewNPC((int)(npc.position.X - 300), (int)npc.position.Y, ModContent.NPCType<Oracle>(), 0, 8);
                    Main.npc[one].hide = true;
                    oraclePositions.Add(one);
                    one = NPC.NewNPC((int)(npc.position.X - 350), (int)npc.position.Y, ModContent.NPCType<Oracle>(), 0, 9);
                    Main.npc[one].hide = true;
                    oraclePositions.Add(one);
                }
                alreadySummoned = true;
            }
            else if (alreadySummoned && counter > 90 && (alreadyCalled.Count < 5 && DestinyWorld.oraclesTimesRefrained == 0 || alreadyCalled.Count < 7 && DestinyWorld.oraclesTimesRefrained == 1 || alreadyCalled.Count < 9 && DestinyWorld.oraclesTimesRefrained == 2) && timesShown == 0) {
                Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, $"Sounds/NPC/Oracle{(Main.rand.NextBool() ? "1" : "2")}"), npc.position);
                int randCheck = Main.rand.Next(0, DestinyWorld.oraclesTimesRefrained == 0 ? 5 : DestinyWorld.oraclesTimesRefrained == 1 ? 7 : 9);
                while (alreadyCalled.Contains(randCheck)) {
                    randCheck = Main.rand.Next(0, DestinyWorld.oraclesTimesRefrained == 0 ? 5 : DestinyWorld.oraclesTimesRefrained == 1 ? 7 : 9);
                }
                alreadyCalled.Add(randCheck);
                Main.npc[oraclePositions[randCheck]].hide = false;
                Main.npc[oraclePositions[randCheck]].ai[0] = alreadyCalled.Count;
                counter = 0;
            }
            else if (alreadySummoned && counter > 90 && (alreadyCalled.Count <= 5 && DestinyWorld.oraclesTimesRefrained == 0 || alreadyCalled.Count <= 7 && DestinyWorld.oraclesTimesRefrained == 1 || alreadyCalled.Count <= 9 && DestinyWorld.oraclesTimesRefrained == 2) && alreadyCalled.Count > 0 && timesShown == 1) {
                Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, $"Sounds/NPC/Oracle{(Main.rand.NextBool() ? "1" : "2")}"), npc.position);
                orderOfTheOracles.Add(alreadyCalled[0]);
                alreadyCalled.RemoveAt(0);
                foreach (NPC npc in Main.npc) {
                    if (npc.ai[0] == orderOfTheOracles.Count && npc.type == ModContent.NPCType<Oracle>() && npc.active) {
                        npc.hide = false;
                        break;
                    }
                }
                counter = 0;
            }
            else if (alreadySummoned && counter > 90 && (alreadyCalled.Count == 5 && DestinyWorld.oraclesTimesRefrained == 0 || alreadyCalled.Count == 7 && DestinyWorld.oraclesTimesRefrained == 1 || alreadyCalled.Count == 9 && DestinyWorld.oraclesTimesRefrained == 2) && timesShown == 0 || alreadySummoned && counter > 120 && alreadyCalled.Count == 0 && timesShown == 1) {
                timesShown++;
                foreach (int oracle in oraclePositions) {
                    Main.npc[oracle].hide = true;
                }
                counter = 0;
            }
            else if (alreadySummoned && counter > 120 && alreadyCalled.Count == 0 && timesShown == 2) {
                foreach (int oracle in oraclePositions) {
                    Main.npc[oracle].hide = false;
                    Main.npc[oracle].dontTakeDamage = false;
                }
            }
        }
    }
}