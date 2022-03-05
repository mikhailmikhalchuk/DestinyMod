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

        public int Counter
        {
            get => (int)NPC.ai[0];
            set => NPC.ai[0] = value;
        }

        public bool SummonedOracle
        {
            get => NPC.ai[1] != 0;
            set => NPC.ai[1] = value ? 1 : 0;
        }

        public int TimesShown
        {
            get => (int)NPC.ai[2];
            set => NPC.ai[2] = value;
        }

        public int OracleOrder
        {
            get => (int)NPC.ai[3];
            set => NPC.ai[3] = value;
        }

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
                TimesShown = 0;
                VaultOfGlassSystem.OraclesKilledOrder = 1;
                SummonedOracle = false;
                AlreadyCalled.Clear();
                OracleIndexes.Clear();
                OracleOrder = 0;
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
                //SetFirstKill();
            }

            Main.NewText("The Oracles prepare to sing their refrain");
        }
    }
}