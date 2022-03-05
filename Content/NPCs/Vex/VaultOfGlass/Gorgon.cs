using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using DestinyMod.Common.NPCs;
using Terraria.DataStructures;
using DestinyMod.Common.ModPlayers;
using Terraria.GameContent.Bestiary;
using System.Collections.Generic;

namespace DestinyMod.Content.NPCs.Vex.VaultOfGlass
{
    public class Gorgon : DestinyModNPC
    {
        public override string Texture => "Terraria/Images/NPC_" + NPCID.DemonEye;

        public int StepsTaken
        {
            get => (int)NPC.ai[0];
            set => NPC.ai[0] = value;
        }

        public bool Forward
        {
            get => NPC.ai[1] == 0;
            set => NPC.ai[1] = value ? 0 : 1;
        }

        public int TimeUntilEveryoneIsDead
        {
            get => (int)NPC.ai[2];
            set => NPC.ai[2] = value;
        }

        public override void SetStaticDefaults()
        {
            NPCID.Sets.CantTakeLunchMoney[Type] = true;
            NPCID.Sets.DebuffImmunitySets.Add(Type, new NPCDebuffImmunityData { ImmuneToAllBuffsThatAreNotWhips = true });
        }

        public override void DestinySetDefaults()
        {
            NPC.CloneDefaults(NPCID.DemonEye);
            AIType = 0;
            NPC.aiStyle = 0;
            NPC.lifeMax = 10000;
            NPC.defense = 50;

            for (int k = 0; k < NPC.buffImmune.Length; k++)
            {
                NPC.buffImmune[k] = true;
            }
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new List<IBestiaryInfoElement> {
                new MoonLordPortraitBackgroundProviderBestiaryInfoElement(),
                new FlavorTextBestiaryInfoElement("Mods.DestinyMod.Bestiary.Gorgon")
            });
        }

        public override void AI()
        {
            if (TimeUntilEveryoneIsDead == 0)
			{
                if (StepsTaken >= 40 && Forward)
                {
                    Forward = false;
                    NPC.velocity.X = -1;
                    NPC.spriteDirection = -1;
                }
                else if (StepsTaken <= -40 && !Forward)
                {
                    Forward = true;
                    NPC.velocity.X = 1;
                    NPC.spriteDirection = 1;
                }

                if (Forward)
                {
                    StepsTaken++;
                    NPC.velocity.X = 1;
                    NPC.spriteDirection = 1;
                }
                else if (!Forward)
                {
                    StepsTaken--;
                    NPC.velocity.X = -1;
                    NPC.spriteDirection = -1;
                }

                foreach (Player player in Main.player)
                {
                    if (NPC.DistanceSQ(player.Center) < 10000 && player.active)
                    {
                        TimeUntilEveryoneIsDead++;
                        Main.NewText("A Gorgon has found its prey");
                        player.GetModPlayer<NPCPlayer>().SpottedGorgon = true;
                    }
                }
            }

            if (TimeUntilEveryoneIsDead > 0 && TimeUntilEveryoneIsDead < 600)
            {
                TimeUntilEveryoneIsDead++;
            }
            else if (TimeUntilEveryoneIsDead >= 600)
            {
                for (int playerCount = 0; playerCount < Main.maxPlayers; playerCount++)
                {
                    Player player = Main.player[playerCount];
                    if (!player.active || player.dead)
                    {
                        continue;
                    }

                    PlayerDeathReason deathReason = new PlayerDeathReason
                    {
                        SourceCustomReason = player.name + " was spotted.",
                        SourceNPCIndex = NPC.type
                    };
                    player.KillMe(deathReason, 0, 0);
                }

                TimeUntilEveryoneIsDead = -1;
                // DestinyPlayer.gorgonsHaveSpotted = false;
            }
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            for (int npcCount = 0; npcCount < Main.maxNPCs; npcCount++)
			{
                NPC npc = Main.npc[npcCount];
                if (npc.active && npc.type == ModContent.NPCType<Gorgon>())
                {
                    npc.lifeMax += Main.expertMode ? 2000 : 1000;
                    npc.defense += 5;

                    if (npc.life == 10000 && !Main.expertMode || npc.life == 20000 && Main.expertMode)
                    {
                        npc.life = npc.lifeMax;
                    }
                }
            }

            Main.NewText("The Gorgons become stronger");
        }
    }
}