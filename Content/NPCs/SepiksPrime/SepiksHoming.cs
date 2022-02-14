using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using DestinyMod.Common.NPCs;

namespace DestinyMod.Content.NPCs.SepiksPrime
{
	public class SepiksHoming : DestinyModNPC
    {
        public int CurrentFrame
        {
            get => (int)NPC.localAI[0];
            set => NPC.localAI[0] = value;
        }

        public bool Initialized
        {
            get => NPC.ai[0] != 0;
            set => NPC.ai[0] = value ? 1 : 0;
        }

        public int Timer
        {
            get => (int)NPC.ai[1];
            set => NPC.ai[1] = value;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Homing Eye Blast");
            Main.npcFrameCount[NPC.type] = 8;
            NPCID.Sets.ProjectileNPC[NPC.type] = true;
        }

        public override void DestinySetDefaults()
        {
            NPC.lifeMax = 1;
            NPC.damage = Main.expertMode ? 25 : 15;
            NPC.width = 60;
            NPC.height = 60;
            NPC.noGravity = true;
            NPC.DeathSound = SoundID.NPCDeath3;
            CurrentFrame = 1;
        }

        public override Color? GetAlpha(Color lightColor) => new Color(174, 56, 207, NPC.alpha);

        public override void AI()
        {
            NPC source = Main.npc[(int)NPC.ai[0]];
            if (++Timer > 500f)
            {
                Dust.NewDust(NPC.position, 2, 2, DustID.PurpleCrystalShard, NPC.velocity.X, NPC.velocity.Y);
                NPC.life = 0;
            }

            if (NPC.alpha > 70)
            {
                NPC.alpha -= 15;
                if (NPC.alpha < 70)
                {
                    NPC.alpha = 70;
                }
            }

            if (!Initialized)
            {
				AdjustMagnitude(ref NPC.velocity);
                Initialized = true;
            }

            Player target = Main.player[source.target];
            float distance = 4000f;
            Vector2 newMove = target.Center - NPC.Center;
            float distanceTo = newMove.Length();
            if (distanceTo < distance)
            {
                AdjustMagnitude(ref newMove);
                NPC.velocity = (10 * NPC.velocity + newMove) / 11f;
                AdjustMagnitude(ref NPC.velocity);
            }
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.frame.Y = frameHeight * CurrentFrame;

            if (++NPC.frameCounter >= 5)
            {
                NPC.frameCounter = 0;
                if (++CurrentFrame >= 8)
                {
                    CurrentFrame = 0;
                }
            }
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position) => false;

        private static void AdjustMagnitude(ref Vector2 velocity)
        {
            float length = velocity.Length();
            if (length > 20f)
            {
                float speed = Main.expertMode ? 9f : 7f;
                velocity *= speed / length;
            }
        }
    }
}