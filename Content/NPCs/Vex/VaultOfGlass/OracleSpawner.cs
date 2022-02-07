using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using DestinyMod.Common.NPCs;
using DestinyMod.Common.ModSystems;
using System.Collections.Generic;
using System.Linq;
using Terraria.Audio;
using DestinyMod.Content.Buffs.Debuffs;

namespace DestinyMod.Content.NPCs.Vex.VaultOfGlass
{
    // Since NPCs at the moment are half baked, I'll get around to introducing an AI system later
    public class OracleSpawner : DestinyModNPC
    {
        public override string Texture => "Terraria/Images/NPC_" + NPCID.DemonEye;

        public int timesShown;

        public int Counter { get => (int)NPC.ai[0]; set => NPC.ai[0] = value; }

        public bool SummonedOracle { get => NPC.ai[1] != 0; set => NPC.ai[1] = value ? 1 : 0; }

        private int oracleOrder;

        public List<int> OracleIndexes = new List<int>();

        public List<int> AlreadyCalled = new List<int>();

        public override void DestinySetDefaults()
        {
            NPC.CloneDefaults(NPCID.DemonEye);
            AIType = 0;
            NPC.aiStyle = -1;
            NPC.dontTakeDamage = true;
            NPC.noGravity = true;
            NPC.knockBackResist = 0f;
        }

        public override void AI()
        {
            Counter++;

            void SetFirstKill()
            {
                Counter = -200;
                timesShown = 0;
                VaultOfGlassSystem.OraclesKilledOrder = 1;
                SummonedOracle = false;
                AlreadyCalled.Clear();
                OracleIndexes.Clear();
                oracleOrder = 0;
            }

            int GetOracleSpawnCount() => VaultOfGlassSystem.OraclesTimesRefrained == 1 ? 5 : VaultOfGlassSystem.OraclesTimesRefrained == 2 ? 7 : 3;

            if (VaultOfGlassSystem.OraclesKilledOrder == 4 && VaultOfGlassSystem.OraclesTimesRefrained == 0
                || VaultOfGlassSystem.OraclesKilledOrder == 6 && VaultOfGlassSystem.OraclesTimesRefrained == 1)
            {
                VaultOfGlassSystem.OraclesTimesRefrained++;
                SetFirstKill();
            }
            else if (Main.npc.Any(n => n.type == ModContent.NPCType<Oracle>() && n.active) && SummonedOracle)
            {
                SetFirstKill();
            }

            if (!SummonedOracle && Main.netMode != NetmodeID.MultiplayerClient && Counter > 0)
            {
                Main.NewText("The Oracles prepare to sing their refrain");

                VaultOfGlassSystem.OraclesKilledOrder = 1;
                List<int> localPositions = new List<int> { 150, 100, 50, 0, -50, -100, -150 };
                int spawnLimit = GetOracleSpawnCount();
                for (int spawnCounter = 0; spawnCounter < spawnLimit; spawnCounter++)
                {
                    int rand = Main.rand.Next(localPositions.Count);
                    int oracle = NPC.NewNPC((int)NPC.Center.X + localPositions[rand], (int)NPC.Center.Y - 50, ModContent.NPCType<Oracle>(), 0, spawnCounter);
                    Main.npc[oracle].hide = true;
                    OracleIndexes.Add(oracle);
                    localPositions.Remove(rand);
                }
                SummonedOracle = true;
            }
            else if (SummonedOracle)
            {
                if (Counter > 90 && (AlreadyCalled.Count < 3 && VaultOfGlassSystem.OraclesTimesRefrained == 0 || AlreadyCalled.Count < 5 && VaultOfGlassSystem.OraclesTimesRefrained == 1 || AlreadyCalled.Count < 7 && VaultOfGlassSystem.OraclesTimesRefrained == 2)
                    && timesShown == 0)
                {
                    //show oracles randomly
                    int spawnLimit = GetOracleSpawnCount();
                    int randCheck = Main.rand.Next(0, spawnLimit);
                    while (AlreadyCalled.Contains(randCheck))
                    {
                        randCheck = Main.rand.Next(0, spawnLimit);
                    }
                    AlreadyCalled.Add(randCheck);
                    NPC randomOracle = Main.npc[OracleIndexes[randCheck]];
                    // randomOracle.hide = false;
                    randomOracle.ai[0] = AlreadyCalled.Count;
                    SoundEngine.PlaySound(SoundLoader.GetLegacySoundSlot(Mod, "Sounds/NPC/Oracle" + (randCheck + 1)), randomOracle.position);
                    Counter = 0;
                }
                if (Counter > 90 && (AlreadyCalled.Count <= 3 && VaultOfGlassSystem.OraclesTimesRefrained == 0 || AlreadyCalled.Count <= 5 && VaultOfGlassSystem.OraclesTimesRefrained == 1 || AlreadyCalled.Count <= 7 && VaultOfGlassSystem.OraclesTimesRefrained == 2)
                    && AlreadyCalled.Count > 0 && timesShown == 1)
                {
                    //reshow oracles
                    oracleOrder++;
                    for (int npcCount = 0; npcCount < Main.maxNPCs; npcCount++)
                    {
                        NPC allNPC = Main.npc[npcCount];
                        if (allNPC.active && allNPC.type == ModContent.NPCType<Oracle>() && allNPC.ai[0] == oracleOrder)
                        {
                            // npc.hide = false;
                            SoundEngine.PlaySound(SoundLoader.GetLegacySoundSlot(Mod, "Sounds/NPC/Oracle" + (AlreadyCalled[0] + 1)), NPC.position);
                            break;
                        }
                    }
                    AlreadyCalled.RemoveAt(0);
                    Counter = 0;
                }
                else if (Counter > 90 && (AlreadyCalled.Count == 3 && VaultOfGlassSystem.OraclesTimesRefrained == 0 || AlreadyCalled.Count == 5 && VaultOfGlassSystem.OraclesTimesRefrained == 1 || AlreadyCalled.Count == 7 && VaultOfGlassSystem.OraclesTimesRefrained == 2)
                    && timesShown == 0 || SummonedOracle && Counter > 120 && AlreadyCalled.Count == 0 && timesShown == 1)
                {
                    //hide them after first and second show
                    timesShown++;
                    foreach (int oracle in OracleIndexes)
                    {
                        Main.npc[oracle].hide = true;
                    }
                    Counter = 0;
                }
                else if (Counter == 120 && AlreadyCalled.Count == 0 && timesShown == 2)
                {
                    //summon and show oracles
                    Main.NewText("The Templar summons the Oracles");
                    foreach (int oracle in OracleIndexes)
                    {
                        // Main.npc[oracle].hide = false;
                        // Main.npc[oracle].dontTakeDamage = false;
                    }
                }
                else if (SummonedOracle && Counter > 600 && AlreadyCalled.Count == 0 && timesShown == 2)
                {
                    //debuff players if they failed
                    Main.NewText("MARKED BY AN ORACLE!");
                    for (int playerCount = 0; playerCount < Main.maxPlayers; playerCount++)
                    {
                        Player allPlayers = Main.player[playerCount];
                        if (allPlayers.active && allPlayers.HasBuff(ModContent.BuffType<MarkedForNegation>()))
                        {
                            allPlayers.AddBuff(ModContent.BuffType<MarkedForNegation>(), 1);
                        }
                    }

                    /*foreach (NPC npc in Main.npc)
                    {
                        if (npc.type == ModContent.NPCType<Oracle>())
                        {
                            // npc.active = false;
                        }
                    }*/
                }
            }
        }
    }
}