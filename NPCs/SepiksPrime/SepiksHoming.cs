using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheDestinyMod.NPCs.SepiksPrime
{
    public class SepiksHoming : ModNPC
    {
        internal int currentFrame = 1;

        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Homing Eye Blast");
            Main.npcFrameCount[npc.type] = 8;
        }

        public override void SetDefaults() {
            npc.lifeMax = 1;
            npc.damage = Main.expertMode ? 25 : 15;
            npc.width = 60;
            npc.height = 60;
            npc.noGravity = true;
            npc.noTileCollide = false;
            npc.friendly = false;
            npc.DeathSound = SoundID.NPCDeath3;
            npc.chaseable = false;
        }

        public override Color? GetAlpha(Color lightColor) {
            return new Color(174, 56, 207, npc.alpha);
        }

        public override void AI() {
            NPC source = Main.npc[(int)npc.ai[0]];
            npc.localAI[1] += 1f;
            if (npc.localAI[1] > 500f) {
                Dust.NewDust(npc.position, 2, 2, DustID.PurpleCrystalShard, npc.velocity.X, npc.velocity.Y);
                npc.life = 0;
            }
            if (npc.alpha > 70) {
                npc.alpha -= 15;
                if (npc.alpha < 70) {
                    npc.alpha = 70;
                }
            }
            if (npc.localAI[0] == 0f) {
                AdjustMagnitude(ref npc.velocity);
                npc.localAI[0] = 1f;
            }
            Vector2 move = Vector2.Zero;
            float distance = 4000f;
            bool target = false;
            Vector2 newMove = Main.player[source.target].Center - npc.Center;
            float distanceTo = (float)Math.Sqrt(newMove.X * newMove.X + newMove.Y * newMove.Y);
            if (distanceTo < distance) {
                move = newMove;
                distance = distanceTo;
                target = true;
            }
            if (target) {
                AdjustMagnitude(ref move);
                npc.velocity = (10 * npc.velocity + move) / 11f;
                AdjustMagnitude(ref npc.velocity);
            }
		}

        public override void FindFrame(int frameHeight) {
            npc.frame.Y = frameHeight * currentFrame;
            if (++npc.frameCounter >= 5) {
                if (++currentFrame >= 8) {
                    currentFrame = 0;
                }
                npc.frameCounter = 0;
            }
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position) {
            return false;
        }

        private void AdjustMagnitude(ref Vector2 vector) {
			float magnitude = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
			if (magnitude > 20f && !Main.expertMode) {
				vector *= 7f / magnitude;
			}
            else if (magnitude > 20f && Main.expertMode) {
				vector *= 9f / magnitude;
			}
		}
    }
}