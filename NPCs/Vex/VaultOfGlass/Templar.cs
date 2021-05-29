using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Graphics.Effects;

namespace TheDestinyMod.NPCs.Vex.VaultOfGlass
{
    public class Templar : ModNPC
    {
        private int counter;

        private float progress = 0;

        public override string Texture => "Terraria/NPC_" + NPCID.DemonEye;

        public override void SetStaticDefaults() {
            DisplayName.SetDefault("The Templar");
        }

        public override void SetDefaults() {
            npc.CloneDefaults(NPCID.DemonEye);
            aiType = 0;
            npc.aiStyle = -1;
            npc.damage = 0;
            npc.lifeMax = 200;
            npc.defense = 2;
            npc.noGravity = true;
            npc.dontTakeDamage = true;
            npc.knockBackResist = 0f;
            npc.chaseable = false;
        }

        public override void AI() {
            counter++;
            if (counter == 600) {
                Main.NewText("The Templar prepares the Ritual of Negation");
            }
            if (Main.netMode != NetmodeID.Server && !Filters.Scene["TheDestinyMod:Shockwave"].IsActive() && counter > 900) {
                Main.NewText("RITUAL OF NEGATION!", new Microsoft.Xna.Framework.Color(255, 255, 0));
                Filters.Scene.Activate("TheDestinyMod:Shockwave", npc.Center).GetShader().UseColor(1, 7, 15).UseTargetPosition(npc.Center);
            }
            if (Main.netMode != NetmodeID.Server && Filters.Scene["TheDestinyMod:Shockwave"].IsActive()) {
                progress += 0.01f;
                Filters.Scene["TheDestinyMod:Shockwave"].GetShader().UseProgress(progress).UseOpacity(100f * (1 - progress / 3f));
                if (progress >= 2.5f) {
                    Filters.Scene["TheDestinyMod:Shockwave"].Deactivate();
                    foreach (Player player in Main.player) {
                        if (player.active && player.HasBuff(ModContent.BuffType<Buffs.Debuffs.MarkedForNegation>())) {
                            Terraria.DataStructures.PlayerDeathReason deathReason = new Terraria.DataStructures.PlayerDeathReason
                            {
                                SourceCustomReason = player.name + " was negated.",
                                SourceNPCIndex = npc.type
                            };
                            player.KillMe(deathReason, 0, 0);
                        }
                    }
                    counter = 0;
                    progress = 0;
                }
            }
        }
    }
}