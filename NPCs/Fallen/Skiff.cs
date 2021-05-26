using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace TheDestinyMod.NPCs.Fallen
{
    public class Skiff : ModNPC
    {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Fallen Skiff");
        }

        public override void SetDefaults() {
            aiType = -1;
            npc.knockBackResist = 0f;
            npc.noGravity = true;
            npc.lifeMax = 1000;
            npc.damage = 0;
            npc.defense = 10;
            npc.friendly = false;
            npc.width = 800;
            npc.height = 600;
            npc.value = Item.buyPrice(0, 1, 0, 0);
            npc.lavaImmune = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit4;
        }

        public override void AI() {
            npc.ai[1]++;
            if (npc.ai[2] == 3f) {
                npc.velocity.X = -10;
                npc.velocity.Y = -5;
                if (npc.ai[1] >= 500f) {
                    npc.life = 0;
                }
            }
            else if (NPC.AnyNPCs(ModContent.NPCType<SepiksPrime.SepiksPrime>()) && ((Main.npc[(int)npc.ai[0]].position - new Vector2(0, 100)) - npc.Center).Length() > 208) {
                Vector2 delta = (Main.npc[(int)npc.ai[0]].position - new Vector2(0, 100)) - npc.Center;
                float magnitude = (float)Math.Sqrt(delta.X * delta.X + delta.Y * delta.Y);
                if (magnitude > 0) {
                    delta *= 10f / magnitude;
                }
                else {
                    delta = new Vector2(0f, 5f);
                }
                npc.velocity.X = delta.X;
            }
            else if (NPC.AnyNPCs(ModContent.NPCType<SepiksPrime.SepiksPrime>()) && ((Main.npc[(int)npc.ai[0]].position - new Vector2(0, 100)) - npc.Center).Length() <= 208) {
                if (npc.ai[1] >= 200f) {
                    if (npc.ai[2] == 0f) {
                        NPC.NewNPC((int)npc.Center.X / 4, (int)npc.Center.Y, ModContent.NPCType<Vandal>());
                    }
                    else if (npc.ai[2] == 1f) {
                        NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, ModContent.NPCType<Vandal>());
                    }
                    else {
                        NPC.NewNPC((int)npc.Center.X + (int)npc.Center.X / 4, (int)npc.Center.Y, ModContent.NPCType<Vandal>());
                    }
                    npc.ai[1] = 0f;
                    npc.ai[2]++;
                }
            }
            else if (!NPC.AnyNPCs(ModContent.NPCType<SepiksPrime.SepiksPrime>())) {
                npc.life = 0;
            }
        }
    }
}