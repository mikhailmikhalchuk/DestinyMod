using System;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using DestinyMod.Common.NPCs;
using Terraria.Audio;

namespace DestinyMod.Content.NPCs.Fallen
{
    public class Vandal : DestinyModNPC
    {
        public override void SetStaticDefaults() => Main.npcFrameCount[NPC.type] = 10;

        public override void DestinySetDefaults()
        {
            NPC.width = 60;
            NPC.height = 70;
            NPC.lifeMax = 100;
            NPC.damage = 5;
            NPC.defense = 2;
            NPC.value = Item.buyPrice(silver: 5);
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath8;
            NPC.frameCounter = 0;
        }

        public bool Walking
        {
            get => NPC.ai[0] != 0;
            set => NPC.ai[0] = value ? 1 : 0;
        }

        public int ProjectileTimer
        {
            get => (int)NPC.ai[1];
            set => NPC.ai[1] = value;
        }

        public int CurrentFrame = 5;

        public bool TriedJumping;

        public override void AI()
        {
            Player target = Main.player[NPC.target];
            if (!target.active || target.dead)
            {
                NPC.TargetClosest();
                target = Main.player[NPC.target];
            }

            if (NPC.DistanceSQ(target.Center) < 160000)
            {
                Walking = false;
                NPC.velocity.X = 0;
                TriedJumping = false;
                if (++ProjectileTimer >= 60 && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Vector2 projectilePosition = new Vector2(NPC.Right.X, NPC.Center.Y - 20);
                    Vector2 projectileVelocity = 10 * (target.Center - projectilePosition).SafeNormalize(new Vector2(0, 0.5f));
                    SoundEngine.PlaySound(SoundID.Item11, projectilePosition);
                    Projectile projectile = Projectile.NewProjectileDirect(NPC.GetProjectileSpawnSource(), projectilePosition, projectileVelocity, ProjectileID.Bullet, 5, 0);
                    projectile.hostile = true;
                    ProjectileTimer = 0;
                }
            }
            else
            {
                Walking = true;
                NPC.velocity.X = Math.Sign(target.Center.X - NPC.Center.X) * 3;
            }
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter++;
            if (NPC.oldPosition == NPC.position && TriedJumping)
            {
                NPC.frame.Y = frameHeight * 3;
            }
            else if (Walking && CurrentFrame == 5 && NPC.frameCounter > 5)
            {
                NPC.frame.Y = frameHeight * 6;
                NPC.frameCounter = 0;
                CurrentFrame++;
            }
            else if (Walking && CurrentFrame >= 6 && CurrentFrame < 9 && NPC.frameCounter > 5)
            {
                NPC.frame.Y += frameHeight;
                CurrentFrame++;
                NPC.frameCounter = 0;
            }
            else if (Walking && CurrentFrame >= 6 && CurrentFrame >= 9 && NPC.frameCounter > 5)
            {
                NPC.frame.Y = frameHeight * 6;
                CurrentFrame = 6;
                NPC.frameCounter = 0;
            }
            else if (!Walking)
            {
                float rotRelative = (Main.player[NPC.target].Center - NPC.Center).ToRotation();
                if (rotRelative < -0.3 && rotRelative > -1.3 || rotRelative < -1.6 && rotRelative > -2.8)
                {
                    NPC.frame.Y = frameHeight;
                }
                else if (rotRelative < -1.3 && rotRelative > -1.6)
                {
                    NPC.frame.Y = 0;
                }
                else if (rotRelative > 0.3 && rotRelative < 1.3 || rotRelative > 1.6 && rotRelative < 2.8)
                {
                    NPC.frame.Y = frameHeight * 3;
                }
                else if (rotRelative > 1.3 && rotRelative < 1.6)
                {
                    NPC.frame.Y = frameHeight * 4;
                }
                else
                {
                    NPC.frame.Y = frameHeight * 2;
                }
            }
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (Main.rand.NextBool(5))
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Ash, 1.5f * hitDirection, -1f, Scale: 0.4f);
                Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Ash, 1.5f * hitDirection, -1f, Scale: 0.6f);
            }

            if (NPC.life <= 0)
            {
                for (int i = 0; i < 20; i++)
                {
                    Dust dust = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, DustID.Cloud, Scale: 1.5f);
                    dust.velocity *= 2f;
                    dust.noGravity = true;
                }
            }
        }
    }
}