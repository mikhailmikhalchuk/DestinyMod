using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace TheDestinyMod.NPCs.Fallen
{
    public class Vandal : ModNPC
    {
        public override void SetStaticDefaults() {
            Main.npcFrameCount[npc.type] = 10;
        }

        public override void SetDefaults() {
            aiType = 0;
            npc.width = 120;
            npc.height = 120;
            npc.lifeMax = 100;
            npc.damage = 5;
            npc.defense = 2;
            npc.value = Item.buyPrice(0, 0, 5, 0);
            npc.lavaImmune = false;
            npc.noTileCollide = false;
            npc.noGravity = false;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath8;
        }

        private bool walking = false;

        private int onFrame = 5;

        private int currentDelay = 0;

        private int waiting = 0;

        private Vector2 previousPos;

        public override void AI() {
            currentDelay++;
            Player target = Main.player[npc.target];
            if (!target.active || target.dead) {
                npc.TargetClosest();
                target = Main.player[npc.target];
            }
            if ((target.Center - npc.Center).Length() < 400) {
                walking = false;
                npc.velocity.X = 0;
                waiting++;
                if (waiting >= 60 && Main.netMode != NetmodeID.MultiplayerClient) {
                    Vector2 delta = target.Center - npc.Center;
                    float magnitude = (float)Math.Sqrt(delta.X * delta.X + delta.Y * delta.Y);
                    if (magnitude > 0) {
                        delta *= 10f / magnitude;
                    }
                    else {
                        delta = new Vector2(0f, 5f);
                    }
                    int p = Projectile.NewProjectile(npc.Center, delta, ProjectileID.Bullet, 5, 0);
                    Main.projectile[p].friendly = false;
                    Main.projectile[p].hostile = true;
                    waiting = 0;
                }
            }
            else {
                Vector2 delta = target.Center - npc.Center;
                float magnitude = (float)Math.Sqrt(delta.X * delta.X + delta.Y * delta.Y);
                if (magnitude > 0) {
                    delta *= 2f / magnitude;
                }
                else {
                    delta = new Vector2(0f, 5f);
                }
                walking = true;
                npc.velocity.X = delta.X;
                if (previousPos != npc.position) {
                    previousPos = npc.position;
                }
            }
        }

        public override void FindFrame(int frameHeight) {
            if (walking && onFrame == 5 && currentDelay > 7) {
                npc.frame.Y = frameHeight * 6;
                currentDelay = 0;
                onFrame++;
            }
            else if (walking && onFrame >= 6 && onFrame < 9 && currentDelay > 7 && previousPos != npc.position) {
                npc.frame.Y += frameHeight;
                onFrame++;
                currentDelay = 0;
            }
            else if (walking && onFrame >= 6 && onFrame >= 9 && currentDelay > 7 && previousPos != npc.position) {
                npc.frame.Y = frameHeight * 6;
                onFrame = 6;
                currentDelay = 0;
            }
            else if (!walking) {
                npc.frame.Y = frameHeight * 2;
            }
            else if (previousPos == npc.position) {
                npc.frame.Y = frameHeight * 3;
            }
        }
    }
}