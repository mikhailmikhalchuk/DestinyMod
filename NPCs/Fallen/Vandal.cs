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
            npc.width = 60;
            npc.height = 70;
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

        private bool walking;

        private int onFrame = 5;

        private int currentDelay;

        private int waiting;

        private bool triedJump;

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
                triedJump = false;
                if (waiting >= 60 && Main.netMode != NetmodeID.MultiplayerClient) {
                    Vector2 delta = target.Center - new Vector2(npc.Center.X + (npc.width / 2), npc.Center.Y - 20);
                    float magnitude = (float)Math.Sqrt(delta.X * delta.X + delta.Y * delta.Y);
                    if (magnitude > 0) {
                        delta *= 10f / magnitude;
                    }
                    else {
                        delta = new Vector2(0f, 5f);
                    }
                    Main.PlaySound(SoundID.Item11, new Vector2(npc.Center.X + (npc.width / 2), npc.Center.Y - 20));
                    Projectile p = Projectile.NewProjectileDirect(new Vector2(npc.Center.X + (npc.width / 2), npc.Center.Y - 20), delta, ProjectileID.Bullet, 5, 0);
                    p.friendly = false;
                    p.hostile = true;
                    waiting = 0;
                }
            }
            else {
                Vector2 delta = target.Center - npc.Center;
                walking = true;
                if (delta.X < 0) {
                    npc.velocity.X = -3;
                }
                else {
                    npc.velocity.X = 3;
                }
            }
        }

        public override void FindFrame(int frameHeight) {
            if (npc.oldPosition == npc.position && triedJump) {
                npc.frame.Y = frameHeight * 3;
            }
            else if (walking && onFrame == 5 && currentDelay > 5) {
                npc.frame.Y = frameHeight * 6;
                currentDelay = 0;
                onFrame++;
            }
            else if (walking && onFrame >= 6 && onFrame < 9 && currentDelay > 5) {
                npc.frame.Y += frameHeight;
                onFrame++;
                currentDelay = 0;
            }
            else if (walking && onFrame >= 6 && onFrame >= 9 && currentDelay > 5) {
                npc.frame.Y = frameHeight * 6;
                onFrame = 6;
                currentDelay = 0;
            }
            else if (!walking) {
                float rotRelative = (Main.player[npc.target].Center - npc.Center).ToRotation();
                if (rotRelative < -0.3 && rotRelative > -1.3 || rotRelative < -1.6 && rotRelative > -2.8) {
                    npc.frame.Y = frameHeight;
                }
                else if (rotRelative < -1.3 && rotRelative > -1.6) {
                    npc.frame.Y = 0;
                }
                else if (rotRelative > 0.3 && rotRelative < 1.3 || rotRelative > 1.6 && rotRelative < 2.8) {
                    npc.frame.Y = frameHeight * 3;
                }
                else if (rotRelative > 1.3 && rotRelative < 1.6) {
                    npc.frame.Y = frameHeight * 4;
                }
                else {
                    npc.frame.Y = frameHeight * 2;
                }
            }
        }

        public override void HitEffect(int hitDirection, double damage) {
            if (Main.rand.NextBool(5)) {
                Dust.NewDust(npc.position, npc.width, npc.height, DustID.Ash, 1.5f * hitDirection, -1f, Scale: 0.4f);
                Dust.NewDust(npc.position, npc.width, npc.height, DustID.Ash, 1.5f * hitDirection, -1f, Scale: 0.6f);
            }
            if (npc.life <= 0) {
                for (int i = 0; i < 20; i++) {
                    Dust dust = Dust.NewDustDirect(npc.position, npc.width, npc.height, 16, Scale: 1.5f);
                    dust.velocity *= 2f;
                    dust.noGravity = true;
                }
            }
        }
    }
}