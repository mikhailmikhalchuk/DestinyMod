using System;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Graphics.Effects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheDestinyMod.NPCs.SepiksPrime
{
    [AutoloadBossHead]
    public class SepiksPrime : ModNPC
    {
        private int timesFiredThisCycle = 0;

        private int phase = 1;
        //phases:
        //1 - initial
        //2 - 75% health shield
        //3 - 75%-50
        //4 - 50-25%
        //5 - 25% health shield
        //6 - last

        private bool shielded = false;

        public override void SetStaticDefaults() {
            Main.npcFrameCount[npc.type] = 4;
        }

        public override void SetDefaults() {
            npc.aiStyle = -1;
            npc.noGravity = true;
            npc.boss = true;
            npc.npcSlots = 40f;
            npc.width = 198;
            npc.height = 186;
            npc.knockBackResist = 0f;
            npc.damage = 10;
            npc.defense = 10;
            npc.lifeMax = 7500;
            npc.value = Item.buyPrice(0, 6, 0, 0);
            npc.DeathSound = null;
            npc.HitSound = SoundID.NPCHit4;
            npc.lavaImmune = true;
            npc.noTileCollide = true;
            for (int k = 0; k < npc.buffImmune.Length; k++) {
                npc.buffImmune[k] = true;
            }
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/SepiksPrime");
            bossBag = ModContent.ItemType<Items.BossBags.SepiksPrimeBag>();
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale) {
            npc.lifeMax = (int)(npc.lifeMax / Main.expertLife * 1.2f * bossLifeScale);
            npc.defense = 25;
            npc.damage = 20;
        }

        public override void AI() {
            npc.ai[0]++;
            Player target = Main.player[npc.target];
            npc.rotation = (float)Math.Atan2(npc.position.Y + npc.height - 80f - target.position.Y - (target.height/2), npc.position.X + (npc.width/2) - target.position.X - (target.width/2)) + (float)Math.PI / 2f;
            if (!target.active || target.dead) {
                npc.TargetClosest(true);
                target = Main.player[npc.target];
                if (!target.active || target.dead) {
                    Main.PlaySound(SoundID.Item78, npc.position);
                    npc.position = new Vector2(npc.position.X + 1000, npc.position.Y);
                    if (npc.timeLeft > 10) {
                        npc.timeLeft = 10;
                    }
                }
            }
            if (npc.timeLeft <= 10) {
                return;
            }
            if (npc.dontTakeDamage == true) {
                if ((phase == 2 || phase == 5) && !CheckShieldedPhase()) {
                    phase = phase == 2 ? 3 : 6;
                    npc.damage = Main.expertMode ? 20 : 10;
                    npc.dontTakeDamage = false;
                    shielded = false;
                    npc.ai[0] = 0f;
                    TeleportNearTarget();
                    if (Main.netMode == NetmodeID.Server) {
                        ModPacket netMessage = GetPacket(SepiksBossMessageType.DontTakeDamage);
                        netMessage.Write(false);
                        netMessage.Send();
                    }
                }
            }
            if (phase == 1 && npc.life <= npc.lifeMax - npc.lifeMax / 4 || phase == 4 && npc.life <= npc.lifeMax / 4) {
                phase = phase == 1 ? 2 : 5;
                shielded = true;
                npc.damage = Main.expertMode ? 40 : 20;
                npc.dontTakeDamage = true;
                npc.position = new Vector2(target.position.X + Main.rand.Next(-50, 50), target.position.Y - 350);
                Main.PlaySound(SoundID.Item78, npc.position);
                Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, $"Sounds/NPC/SepiksGroan{(Main.rand.NextBool() ? "1" : "2")}"), npc.position);
                SummonServitors();
                if (Main.netMode == NetmodeID.Server) {
                    ModPacket netMessage = GetPacket(SepiksBossMessageType.DontTakeDamage);
                    netMessage.Write(true);
                    netMessage.Send();
                }
            }
            else if (phase == 3 && npc.life <= npc.lifeMax / 2) {
                phase = 4;
                npc.ai[0] = 0f;
                timesFiredThisCycle = 0;
            }
            if (phase == 1) {
                if ((target.Center - npc.Center).Length() > 1000) {
                    TeleportNearTarget();
                    npc.ai[0] = 0f;
                    timesFiredThisCycle = 0;
                }
                if (npc.ai[0] > 50f && timesFiredThisCycle < 3) {
                    FireBlastAtTarget();
                    npc.ai[0] = 0f;
                    timesFiredThisCycle++;
                }
                if (npc.ai[0] > 120f && timesFiredThisCycle >= 3) {
                    TeleportNearTarget();
                    timesFiredThisCycle = 0;
                    npc.ai[0] = 0f;
                }
            }
            else if (phase == 3) {
                if (npc.ai[0] == 40f || npc.ai[0] == 55f || npc.ai[0] == 70f) {
                    if (timesFiredThisCycle >= 3) return;
                    timesFiredThisCycle++;
                    FireBlastAtTarget();
                }
                else if (timesFiredThisCycle >= 3 && npc.ai[0] > 130f) {
                    npc.ai[0] = 0f;
                    timesFiredThisCycle = 0;
                    TeleportNearTarget();
                }
            }
            else if (phase == 4) {
                if (npc.ai[0] == 100f && timesFiredThisCycle < 3 || npc.ai[0] == 80f && timesFiredThisCycle < 3 && Main.expertMode) {
                    FireHomingAtTarget();
                    npc.ai[0] = 0f;
                    timesFiredThisCycle++;
                }
                else if (npc.ai[0] > 90f && timesFiredThisCycle >= 3 || npc.ai[0] > 70f && timesFiredThisCycle >= 3 && Main.expertMode) {
                    npc.position = new Vector2(target.position.X + Main.rand.Next(-50, 50), target.position.Y - 250);
                    Main.PlaySound(SoundID.Item78, npc.position);
                    npc.ai[0] = 0f;
                    timesFiredThisCycle = 0;
                }
            }
            else if (phase == 6) {
                if (npc.ai[0] == 30f) {
                    FireBlastAtTarget();
                }
                else if (npc.ai[0] == 65f) {
                    npc.ai[0] = 0f;
                    TeleportNearTarget();
                }
            }
            if (shielded == true) {
                if (npc.ai[0] > 80f || npc.ai[0] > 70f && Main.expertMode) {
                    FireBlastAtTarget();
                    npc.ai[0] = 0f;
                }
            }
            if ((target.Center - npc.Center).Length() < 100 && shielded == false) {
                TeleportNearTarget();
                npc.ai[0] = 0f;
            }
        }

        public override void FindFrame(int frameHeight) {
            if (phase == 2 || phase == 3) {
                npc.frame.Y = frameHeight;
            }
            else if (phase == 4) {
                npc.frame.Y = frameHeight * 2;
            }
            else if (phase >= 5) {
                npc.frame.Y = frameHeight * 3;
            }
        }

        private void TeleportNearTarget() {
            if (Main.netMode != NetmodeID.MultiplayerClient) {
                Player target = Main.player[npc.target];
                bool teleportSuccess = false;
                int attempts = 0;
                while (!teleportSuccess) {
                    attempts++;
                    Vector2 teleportTo = new Vector2(target.position.X + Main.rand.Next(-200, 200), target.position.Y - Main.rand.Next(0, 300));
                    if (phase == 3 || phase == 6) { //because it's so spontaneous this gives the player more breathing room so sepiks isn't so close as he is normally
                        teleportTo = new Vector2(target.position.X + Main.rand.Next(-300, 300), target.position.Y - Main.rand.Next(0, 400));
                    }
                    var tileToGoTo = teleportTo.ToTileCoordinates();
                    //ensures that sepiks won't spawn in tiles, and that he'll spawn a moderate distance from the ground
                    if (WorldGen.EmptyTileCheck((int)(tileToGoTo.X - 3.125), (int)(tileToGoTo.X + 3.125), (int)(tileToGoTo.Y - 3.125), (int)(tileToGoTo.Y + 3.125)) && WorldGen.EmptyTileCheck(tileToGoTo.X, tileToGoTo.X, tileToGoTo.Y, tileToGoTo.Y + 20)) {
                        npc.Center = teleportTo;
                        Main.PlaySound(SoundID.Item78, npc.position);
                        teleportSuccess = true;
                    }
                    if (attempts >= 30000) {
                        return;
                    }
                }
            }
        }

        private bool CheckShieldedPhase() {
            for (int k = 0; k < 200; k++) {
                if (Main.npc[k].active && Main.npc[k].type == ModContent.NPCType<SepiksServitor>()) {
                    return true;
                }
            }
            return false;
        }

        private void SummonServitors() {
            if (Main.netMode != NetmodeID.MultiplayerClient) {
                NPC.NewNPC((int)npc.Center.X + 150, (int)npc.Center.Y + 20, ModContent.NPCType<SepiksServitor>());
                NPC.NewNPC((int)npc.Center.X - 150, (int)npc.Center.Y + 20, ModContent.NPCType<SepiksServitor>());
                NPC.NewNPC((int)npc.Center.X + 130, (int)npc.Center.Y + 120, ModContent.NPCType<SepiksServitor>());
                NPC.NewNPC((int)npc.Center.X - 130, (int)npc.Center.Y + 120, ModContent.NPCType<SepiksServitor>());
                NPC.NewNPC((int)npc.Center.X + 500, (int)npc.Center.Y - 100, ModContent.NPCType<Fallen.Skiff>(), 0, npc.whoAmI);
            }
        }

        private void FireBlastAtTarget() {
            if (Main.netMode != NetmodeID.MultiplayerClient) {
                Player target = Main.player[npc.target];
                Vector2 delta = target.Center - npc.Center;
                float magnitude = (float)Math.Sqrt(delta.X * delta.X + delta.Y * delta.Y);
                if (magnitude > 0) {
                    delta *= 10f / magnitude;
                }
                else {
                    delta = new Vector2(0f, 5f);
                }
                Projectile.NewProjectile(npc.Center, delta, ModContent.ProjectileType<SepiksBlast>(), 20, 5, Main.myPlayer, npc.whoAmI);
                Main.PlaySound(SoundID.Item8, npc.Center);
                npc.netUpdate = true;
            }
        }

        private void FireHomingAtTarget() {
            if (Main.netMode != NetmodeID.MultiplayerClient) {
                NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, ModContent.NPCType<SepiksHoming>(), 0, npc.whoAmI);
                Main.PlaySound(SoundID.Item8, npc.Center);
                npc.netUpdate = true;
            }
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position) {
            scale = 1.5f;
            return null;
        }

        public override void NPCLoot() {
            if (Main.expertMode) {
                npc.DropBossBags();
            }
            if (Main.rand.NextBool(7) && !Main.expertMode) {
                Item.NewItem(npc.Hitbox, ModContent.ItemType<Items.Vanity.SepiksPrimeMask>());
            }
            if (Main.rand.NextBool(10)) {
                Item.NewItem(npc.Hitbox, ModContent.ItemType<Items.Placeables.SepiksPrimeTrophy>());
            }
            if (!DestinyWorld.downedPrime) {
                DestinyWorld.downedPrime = true;
                if (Main.netMode == NetmodeID.Server) {
                    NetMessage.SendData(MessageID.WorldData);
                }
            }
        }

        public override void HitEffect(int hitDirection, double damage) {
            Dust.NewDust(npc.position, npc.width, npc.height, DustID.PurpleCrystalShard, hitDirection);
        }

        public void HandlePacket(BinaryReader reader) {
            SepiksBossMessageType type = (SepiksBossMessageType)reader.ReadByte();
            switch (type) {
                case SepiksBossMessageType.DontTakeDamage:
                    npc.dontTakeDamage = reader.ReadBoolean();
                    break;
            }
        }
        
        private ModPacket GetPacket(SepiksBossMessageType type) {
            ModPacket packet = mod.GetPacket();
			packet.Write((byte)DestinyModMessageType.SepiksPrime);
			packet.Write(npc.whoAmI);
			packet.Write((byte)type);
			return packet;
        }
    }

    internal enum SepiksBossMessageType : byte
    {
        DontTakeDamage
    }
}