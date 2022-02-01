using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using DestinyMod.Common.NPCs;

namespace DestinyMod.Content.NPCs.Fallen
{
    public class Skiff : DestinyModNPC
    {
        public NPC MotherNPC { get => Main.npc[(int)NPC.ai[0]]; }

        public float Timer { get => NPC.ai[1]; set => NPC.ai[1] = value; }

        public float Phase { get => NPC.ai[2]; set => NPC.ai[2] = value; }

        public override void SetStaticDefaults() => DisplayName.SetDefault("Fallen Skiff");

        public override void DestinySetDefaults()
        {
            AIType = -1;
            NPC.knockBackResist = 0f;
            NPC.noGravity = true;
            NPC.lifeMax = 1000;
            NPC.defense = 10;
            NPC.width = 800;
            NPC.height = 600;
            NPC.value = Item.buyPrice(gold: 1);
            NPC.lavaImmune = true;
            NPC.noTileCollide = true;
            NPC.HitSound = SoundID.NPCHit4;
        }

        public override void AI()
        {
            Timer++;
            if (Phase == 3f)
            {
                NPC.velocity = new Vector2(-10, -5);
                if (Timer >= 500f)
                {
                    NPC.life = 0;
                }
            }
            else if (NPC.AnyNPCs(ModContent.NPCType<SepiksPrime.SepiksPrime>()) && (MotherNPC.position - new Vector2(0, 100)) - NPC.Center).Length() > 208)
            {
                Vector2 delta = (Main.npc[(int)NPC.ai[0]].position - new Vector2(0, 100)) - NPC.Center;
                float magnitude = (float)Math.Sqrt(delta.X * delta.X + delta.Y * delta.Y);
                if (magnitude > 0)
                {
                    delta *= 10f / magnitude;
                }
                else
                {
                    delta = new Vector2(0f, 5f);
                }
                NPC.velocity.X = delta.X;
            }
            else if (NPC.AnyNPCs(ModContent.NPCType<SepiksPrime.SepiksPrime>()) && (MotherNPC.position - new Vector2(0, 100)) - NPC.Center).Length() <= 208)
            {
                if (Timer >= 200f)
                {
                    if (Phase == 0f)
                    {
                        NPC.NewNPC((int)NPC.Center.X / 4, (int)NPC.Center.Y, ModContent.NPCType<Vandal>()); // What?
                    }
                    else if (Phase == 1f)
                    {
                        NPC.NewNPC((int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<Vandal>());
                    }
                    else
                    {
                        NPC.NewNPC((int)NPC.Center.X + (int)NPC.Center.X / 4, (int)NPC.Center.Y, ModContent.NPCType<Vandal>());
                    }
                    Timer = 0f;
                    Phase++;
                }
            }
            else if (!NPC.AnyNPCs(ModContent.NPCType<SepiksPrime.SepiksPrime>()))
            {
                NPC.life = 0;
            }
        }
    }
}