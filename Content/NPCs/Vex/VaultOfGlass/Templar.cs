using Terraria;
using Terraria.ID;
using Terraria.Graphics.Effects;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using DestinyMod.Common.NPCs;
using DestinyMod.Content.Buffs.Debuffs;

namespace DestinyMod.Content.NPCs.Vex.VaultOfGlass
{
    public class Templar : DestinyModNPC
    {
        public int Counter { get => (int)NPC.ai[0]; set => NPC.ai[0] = value; }

        public float Progress { get => NPC.ai[1]; set => NPC.ai[1] = value; }

        public override string Texture => "Terraria/Images/NPC_" + NPCID.DemonEye;

        public override void SetStaticDefaults() => DisplayName.SetDefault("The Templar");

        public override void DestinySetDefaults()
        {
            NPC.CloneDefaults(NPCID.DemonEye);
            AIType = 0;
            NPC.aiStyle = -1;
            NPC.damage = 0;
            NPC.lifeMax = 200;
            NPC.defense = 2;
            NPC.noGravity = true;
            NPC.dontTakeDamage = true;
            NPC.knockBackResist = 0f;
            // npc.chaseable = false;
        }

        public override void AI()
        {
            Counter++;
            if (Counter == 600)
            {
                Main.NewText("The Templar prepares the Ritual of Negation");
            }

            if (Main.netMode != NetmodeID.Server)
			{
                Filter shockwave = Filters.Scene["TheDestinyMod:Shockwave"];
                if (!shockwave.IsActive() && Counter > 900)
                {
                    Main.NewText("RITUAL OF NEGATION!", new Color(255, 255, 0));
                    Filters.Scene.Activate("TheDestinyMod:Shockwave", NPC.Center).GetShader()
                        .UseColor(1, 7, 15)
                        .UseTargetPosition(NPC.Center);
                }

                if (shockwave.IsActive())
                {
                    Progress += 0.01f;

                    shockwave.GetShader()
                        .UseProgress(Progress)
                        .UseOpacity(100f * (1 - Progress / 3f));

                    if (Progress >= 2.5f)
                    {
                        shockwave.Deactivate();

                        for (int playerCount = 0; playerCount < Main.maxPlayers; playerCount++)
						{
                            Player allPlayers = Main.player[playerCount];
                            if (allPlayers.active && allPlayers.HasBuff<MarkedForNegation>())
                            {
                                PlayerDeathReason deathReason = new PlayerDeathReason
                                {
                                    SourceCustomReason = allPlayers.name + " was negated.",
                                    SourceNPCIndex = NPC.type
                                };
                                allPlayers.KillMe(deathReason, 0, 0);
                            }
                        }
                        
                        Counter = 0;
                        Progress = 0;
                    }
                }
            }
        }
    }
}