using System;
using System.IO;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;
using Terraria.GameContent.ItemDropRules;
using DestinyMod.Content.Items.Bosses.SepiksPrime;
using DestinyMod.Content.Projectiles.NPCs.Bosses.SepiksPrime;
using DestinyMod.Common.NPCs;
using Terraria.ModLoader.IO;
using DestinyMod.Content.Items.Misc;
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;
using DestinyMod.Common.NPCs.Data;
using Terraria.Graphics.Shaders;
using DestinyMod.Common.Configs;

namespace DestinyMod.Content.NPCs.SepiksPrime
{
	[AutoloadBossHead]
    public class SepiksPrime : DestinyModNPC
    {
        private int TimesFiredThisCycle;

        public int Phase = 1;
        //phases:
        //1 - initial
        //2 - 75% health shield
        //3 - 75%-50
        //4 - 50-25%
        //5 - 25% health shield
        //6 - last

        private bool Shielded;

        private Vector2 NewCenter;

        private List<Dust> VelocityChanger = new List<Dust>();

        private int Rad = 120;

        public static bool DownedSepiksPrime;

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 4;

            NPCID.Sets.MPAllowedEnemies[Type] = true;
            NPCID.Sets.BossBestiaryPriority.Add(Type);

            NPCID.Sets.DebuffImmunitySets.Add(Type, new NPCDebuffImmunityData { ImmuneToAllBuffsThatAreNotWhips = true });
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, new NPCID.Sets.NPCBestiaryDrawModifiers(0) {
                PortraitScale = 0.55f,
                Position = new Vector2(-7, -70),
                PortraitPositionYOverride = -50f,
                PortraitPositionXOverride = 0f
            });
        }

        public override void DestinySetDefaults()
        {
            NPC.aiStyle = -1;
            NPC.noGravity = true;
            NPC.boss = true;
            NPC.npcSlots = 40f;
            NPC.width = 198;
            NPC.height = 186;
            NPC.knockBackResist = 0f;
            NPC.damage = 10;
            NPC.defense = 10;
            NPC.lifeMax = 7500;
            NPC.value = Item.buyPrice(0, 6, 0, 0);
            NPC.DeathSound = null;
            NPC.HitSound = SoundID.NPCHit4;
            NPC.lavaImmune = true;
            NPC.noTileCollide = true;

            NPC.BossBar = ModContent.GetInstance<Common.BossBars.ShieldBossBar>();

            for (int k = 0; k < NPC.buffImmune.Length; k++)
            {
                NPC.buffImmune[k] = true;
            }

            if (!Main.dedServ)
            {
                Music = SoundLoader.GetSoundSlot("Sounds/Music/SepiksPrime");
            }
        }

        /*public override void ScaleExpertStats(int numPlayers, float bossLifeScale) {
            NPC.lifeMax = (int)(NPC.lifeMax / Main.expertLife * 1.2f * bossLifeScale);
            NPC.defense = 25;
            NPC.damage = 20;
        }*/

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new List<IBestiaryInfoElement> {
                BestiaryData.CommonTags.Visuals.Cosmodrome,

                new FlavorTextBestiaryInfoElement("Mods.DestinyMod.Bestiary.SepiksPrime")
            });
        }

        public override bool? CanBeHitByProjectile(Projectile projectile)
        {
            if (projectile.damage > NPC.life && DestinyClientConfig.Instance.SepiksDeathAnimation)
            {
                NPC.life = 49;
                return false;
            }
            return base.CanBeHitByProjectile(projectile);
        }

        public override void AI()
        {
            NPC.ai[0]++;
            Player target = Main.player[NPC.target];
            if (NPC.life > 50 && DestinyClientConfig.Instance.SepiksDeathAnimation || !DestinyClientConfig.Instance.SepiksDeathAnimation)
            {
                NPC.rotation = (float)Math.Atan2(NPC.position.Y + NPC.height - 80f - target.position.Y - (target.height / 2), NPC.position.X + (NPC.width / 2) - target.position.X - (target.width / 2)) + (float)Math.PI / 2f;
            }
            else if (NPC.life < 50 && !NPC.dontTakeDamage && DestinyClientConfig.Instance.SepiksDeathAnimation)
            { //doesnt work with others
                NPC.dontTakeDamage = true;
                NPC.rotation += 1;
                SoundEngine.PlaySound(SoundLoader.GetLegacySoundSlot(Mod, "Assets/Sounds/NPC/SepiksDie"), NPC.Center);
                NPC.ai[0] = 0;
                NPC.alpha = 0;
                return;
            }
            else if (NPC.life < 50 && NPC.dontTakeDamage && DestinyClientConfig.Instance.SepiksDeathAnimation)
            {
                NPC.rotation += 1;
                if (NPC.ai[0] > 192)
                {
                    NPC.StrikeNPC(9999, 0, 0); //has to be 9999 damage because...terraria
                }
                return;
            }
            if (!target.active || target.dead)
            {
                NPC.TargetClosest(true);
                target = Main.player[NPC.target];
                if (!target.active || target.dead)
                {
                    SoundEngine.PlaySound(SoundID.Item78, NPC.Center);
                    NPC.position = new Vector2(NPC.position.X + 1000, NPC.position.Y);
                    if (NPC.timeLeft > 10)
                    {
                        NPC.timeLeft = 10;
                    }
                }
            }
            if (NewCenter != Vector2.Zero && NPC.ai[3] < 40)
            {
                if (NPC.ai[3] < 20)
                {
                    NPC.alpha += 13;
                    float num = NewCenter.X + (NPC.width / 2) - (NPC.Center.X + NPC.width / 2);
                    float num2 = NewCenter.Y + (NPC.height / 2) - (NPC.Center.Y + NPC.height / 2);
                    float numFinal = (float)Math.Sqrt(num * num + num2 * num2);
                    num *= 5f / numFinal;
                    num2 *= 5f / numFinal;
                    for (int i = 0; i < 50; i++)
                    {
                        double radius = Math.Sqrt(Main.rand.NextDouble());
                        double rand = Main.rand.NextDouble() * (Math.PI * 2);
                        Vector2 vector = NPC.Center + new Vector2((float)(radius * Math.Cos(rand)), (float)(radius * Math.Sin(rand))) * ((NPC.width - Rad) / 2);
                        Dust dust = Dust.NewDustDirect(vector, 1, 1, DustID.WhiteTorch, Scale: 1.4f);
                        VelocityChanger.Add(dust);
                        dust.noGravity = true;
                        dust.velocity.X = num;
                        dust.velocity.Y = num2;
                    }
                    Rad -= 4;
                    //npc.scale -= 0.01f;
                }
                foreach (Dust dust in VelocityChanger)
                {
                    dust.velocity *= 1.05f;
                }
                NPC.ai[3]++;
                return;
            }
            if (NPC.ai[3] >= 40)
            {
                if (NewCenter != Vector2.Zero)
                {
                    NPC.Center = NewCenter;
                    VelocityChanger.Clear();
                    Rad = 120;
                }
                NewCenter = Vector2.Zero;
                if (NPC.alpha > 0)
                {
                    NPC.alpha -= 13;
                    //npc.scale += 0.01f;
                }
                else
                {
                    NPC.ai[0] = 0;
                    NPC.alpha = 0;
                    NPC.ai[3] = 0;
                    TimesFiredThisCycle = 0;
                    NPC.defense /= 3;
                    if (NPC.dontTakeDamage && NPC.life > 50)
                    {
                        SummonServitors();
                    }
                }
                return;
            }
            if (NPC.timeLeft <= 10)
            {
                return;
            }
            if (NPC.dontTakeDamage && NPC.life > 50)
            {
                if ((Phase == 2 || Phase == 5) && !AnyServitorActive())
                {
                    Phase = Phase == 2 ? 3 : 6;
                    NPC.ai[0] = 0f;
                    Shielded = false;
                    NPC.damage = Main.expertMode ? 20 : 10;
                    NPC.dontTakeDamage = false;
                    TeleportNearTarget();
                    if (Main.netMode == NetmodeID.Server)
                    {
                        ModPacket netMessage = GetPacket(SepiksBossMessageType.DontTakeDamage);
                        netMessage.Write(false);
                        netMessage.Send();
                    }
                }
            }
            if ((Phase == 1 && NPC.life <= NPC.lifeMax - NPC.lifeMax / 4) || (Phase == 4 && NPC.life <= NPC.lifeMax / 4))
            {
                Phase = Phase == 1 ? 2 : 5;
                Shielded = true;
                NPC.damage = Main.expertMode ? 40 : 20;
                NPC.dontTakeDamage = true;
                TeleportNearTarget(target.Center.X + Main.rand.Next(-50, 50), target.Center.Y - 350);
                SoundEngine.PlaySound(SoundID.Item78, NPC.Center);
                SoundEngine.PlaySound(SoundLoader.GetLegacySoundSlot(Mod, $"Assets/Sounds/NPC/SepiksGroan{(Main.rand.NextBool() ? "1" : "2")}"), NPC.Center);
                if (Main.netMode == NetmodeID.Server)
                {
                    ModPacket netMessage = GetPacket(SepiksBossMessageType.DontTakeDamage);
                    netMessage.Write(true);
                    netMessage.Send();
                }
            }
            else if (Phase == 3 && NPC.life <= NPC.lifeMax / 2)
            {
                Phase = 4;
                NPC.ai[0] = 0f;
                TimesFiredThisCycle = 0;
            }
            if (Phase == 1)
            {
                if ((target.Center - NPC.Center).Length() > 1000)
                {
                    TeleportNearTarget();
                    NPC.ai[0] = 0f;
                    TimesFiredThisCycle = 0;
                }
                if (NPC.ai[0] > 50f && TimesFiredThisCycle < 3)
                {
                    FireBlastAtTarget();
                    NPC.ai[0] = 0f;
                    TimesFiredThisCycle++;
                }
                if (NPC.ai[0] > 120f && TimesFiredThisCycle >= 3)
                {
                    TeleportNearTarget();
                    TimesFiredThisCycle = 0;
                    NPC.ai[0] = 0f;
                    Main.LocalPlayer.AddBuff(195, 3);
                }
            }
            else if (Phase == 3)
            {
                if (NPC.ai[0] == 40f || NPC.ai[0] == 55f || NPC.ai[0] == 70f)
                {
                    if (TimesFiredThisCycle >= 3)
                        return;
                    TimesFiredThisCycle++;
                    FireBlastAtTarget();
                }
                else if (TimesFiredThisCycle >= 3 && NPC.ai[0] > 130f)
                {
                    NPC.ai[0] = 0f;
                    TimesFiredThisCycle = 0;
                    TeleportNearTarget();
                }
            }
            else if (Phase == 4)
            {
                if (NPC.ai[0] == 100f && TimesFiredThisCycle < 3 || NPC.ai[0] == 80f && TimesFiredThisCycle < 3 && Main.expertMode)
                {
                    FireHomingAtTarget();
                    NPC.ai[0] = 0f;
                    TimesFiredThisCycle++;
                }
                else if (NPC.ai[0] > 90f && TimesFiredThisCycle >= 3 || NPC.ai[0] > 70f && TimesFiredThisCycle >= 3 && Main.expertMode)
                {
                    TeleportNearTarget(target.Center.X + Main.rand.Next(-50, 50), target.Center.Y - 250);
                    SoundEngine.PlaySound(SoundID.Item78, NPC.position);
                    NPC.ai[0] = 0f;
                    TimesFiredThisCycle = 0;
                }
            }
            else if (Phase == 6)
            {
                if (NPC.ai[0] == 30f)
                {
                    FireBlastAtTarget();
                }
                else if (NPC.ai[0] == 65f)
                {
                    NPC.ai[0] = 0f;
                    TeleportNearTarget();
                }
            }
            if (Shielded)
            {
                if (NPC.ai[0] > 80f || NPC.ai[0] > 70f && Main.expertMode)
                {
                    FireBlastAtTarget();
                    NPC.ai[0] = 0f;
                }
            }
            if ((target.Center - NPC.Center).Length() < 100 && !Shielded)
            {
                TeleportNearTarget();
                NPC.ai[0] = 0f;
            }
        }

        public override void FindFrame(int frameHeight)
        {
            if (Phase == 2 || Phase == 3)
            {
                NPC.frame.Y = frameHeight;
            }
            else if (Phase == 4)
            {
                NPC.frame.Y = frameHeight * 2;
            }
            else if (Phase >= 5)
            {
                NPC.frame.Y = frameHeight * 3;
            }
        }

        public override void DrawEffects(ref Color drawColor)
        {
            if (NewCenter != Vector2.Zero)
            {
                drawColor = Color.White;
            }
        }
        
        private static bool AnyServitorActive()
        {
            for (int k = 0; k < Main.maxNPCs; k++)
            {
                if (Main.npc[k].active && Main.npc[k].type == ModContent.NPCType<SepiksServitor>())
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Teleports Sepiks near a target
        /// </summary>
        /// <param name="x">The X value of where to force Sepiks in world coordinates</param>
        /// <param name="y">The Y value of where to force Sepiks in world coordinates</param>
        private void TeleportNearTarget(float x = 0, float y = 0)
        {
            Vector2 global = Vector2.Zero;
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                Player target = Main.player[NPC.target];
                bool teleportSuccess = false;
                int attempts = 0;
                while (!teleportSuccess)
                {
                    attempts++;
                    Vector2 teleportTo = new Vector2(target.Center.X + Main.rand.Next(-200, 200), target.Center.Y - Main.rand.Next(50, 300));
                    if (Phase == 3 || Phase == 6)
                    { //because it's so spontaneous this gives the player more breathing room so sepiks isn't so close as he is normally
                        teleportTo = new Vector2(target.Center.X + Main.rand.Next(-300, 300), target.Center.Y - Main.rand.Next(50, 400));
                    }
                    if (x > 0 && y > 0)
                    {
                        teleportTo = new Vector2(x, y);
                    }
                    Point tileToGoTo = teleportTo.ToTileCoordinates();
                    //ensures that sepiks won't spawn in tiles, and that he'll spawn a moderate distance from the ground
                    if (WorldGen.EmptyTileCheck((int)(tileToGoTo.X - 3.125), (int)(tileToGoTo.X + 3.125), (int)(tileToGoTo.Y - 3.125), (int)(tileToGoTo.Y + 3.125)) && WorldGen.EmptyTileCheck(tileToGoTo.X, tileToGoTo.X, tileToGoTo.Y, tileToGoTo.Y + 20) && WorldGen.InWorld(tileToGoTo.X, tileToGoTo.Y) || x > 0 && y > 0)
                    { // && (target.Center - teleportTo).Length() > 200
                        global = teleportTo;
                        SoundEngine.PlaySound(SoundID.Item78, NPC.Center);
                        NPC.defense *= 3;
                        teleportSuccess = true;
                        NPC.netUpdate = true;
                    }
                    if (attempts >= 30000)
                    {
                        return;
                    }
                }
            }
            NewCenter = global;
        }

        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (NewCenter == Vector2.Zero)
            {
                spriteBatch.Draw(Mod.Assets.Request<Texture2D>("Content/NPCs/SepiksPrime/SepiksPrime_Glow").Value, NPC.Center - screenPos + new Vector2(0, 4), NPC.frame, Color.LightYellow, NPC.rotation, NPC.frame.Size() / 2, NPC.scale, SpriteEffects.None, 0);
            }

            if (AnyServitorActive())
            {
                float servHealthCombined = 0f;
                float servHealthMax = 0f;
                for (int k = 0; k < Main.maxNPCs; k++)
                {
                    NPC serv = Main.npc[k];
                    if (serv.active && serv.type == ModContent.NPCType<SepiksServitor>())
                    {
                        servHealthCombined += serv.life;
                        servHealthMax = serv.lifeMax * 4;
                    }
                }
                DrawForcefield(spriteBatch, screenPos, Color.White * (servHealthCombined / servHealthMax * 0.6f + 0.2f), new Vector2(-2, -5), 0.65f);
                ((Common.BossBars.ShieldBossBar)NPC.BossBar).ShieldPercentToSet = servHealthCombined / servHealthMax;
            }
            else
            {
                ((Common.BossBars.ShieldBossBar)NPC.BossBar).ShieldPercentToSet = 0f;
            }
        }

        /// <summary>
        /// Summons Sepiks' servitors
        /// </summary>
        private void SummonServitors()
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                NPC.NewNPC(NPC.GetSpawnSourceForNPCFromNPCAI(), (int)NPC.Center.X + 150, (int)NPC.Center.Y + 20, ModContent.NPCType<SepiksServitor>());
                NPC.NewNPC(NPC.GetSpawnSourceForNPCFromNPCAI(), (int)NPC.Center.X - 150, (int)NPC.Center.Y + 20, ModContent.NPCType<SepiksServitor>());
                NPC.NewNPC(NPC.GetSpawnSourceForNPCFromNPCAI(), (int)NPC.Center.X + 130, (int)NPC.Center.Y + 120, ModContent.NPCType<SepiksServitor>());
                NPC.NewNPC(NPC.GetSpawnSourceForNPCFromNPCAI(), (int)NPC.Center.X - 130, (int)NPC.Center.Y + 120, ModContent.NPCType<SepiksServitor>());
                int skiff = NPC.NewNPC(NPC.GetSpawnSourceForNPCFromNPCAI(), (int)NPC.Center.X + 500, (int)NPC.Center.Y - 100, ModContent.NPCType<Fallen.Skiff>());
                ((Fallen.Skiff)Main.npc[skiff].ModNPC).PositionToDrop = new Vector2(NPC.Center.X, NPC.Center.Y - 300);
            }
        }

        /// <summary>
        /// Fires an Eye Blast at the current target
        /// </summary>
        private void FireBlastAtTarget()
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                Player target = Main.player[NPC.target];
                Vector2 delta = target.Center - NPC.Center;
                float magnitude = (float)Math.Sqrt(delta.X * delta.X + delta.Y * delta.Y);
                if (magnitude > 0)
                {
                    delta *= 10f / magnitude;
                }
                else
                {
                    delta = new Vector2(0f, 5f);
                }
                Projectile.NewProjectile(NPC.GetSpawnSourceForProjectileNPC(), NPC.Center, delta.RotatedByRandom(MathHelper.ToRadians(10)), ModContent.ProjectileType<SepiksBlast>(), 20, 5, Main.myPlayer, NPC.whoAmI);
                SoundEngine.PlaySound(SoundID.Item8, NPC.Center);
                NPC.netUpdate = true;
            }
        }

        /// <summary>
        /// Fires a Homing Eye Blast at the current target
        /// </summary>
        private void FireHomingAtTarget()
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                NPC.NewNPC(NPC.GetSpawnSourceForNPCFromNPCAI(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<SepiksHoming>(), ai0: NPC.whoAmI);
                SoundEngine.PlaySound(SoundID.Item8, NPC.Center);
                NPC.netUpdate = true;
            }
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            if (NewCenter != Vector2.Zero)
            {
                scale = 0f;
            }
            return null;
        }

        public override void OnKill() => NPC.SetEventFlagCleared(ref DownedSepiksPrime, -1);

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<SepiksPrimeBag>()));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SepiksPrimeTrophy>(), 1));

            LeadingConditionRule notExpert = new LeadingConditionRule(new Conditions.NotExpert());
            notExpert.OnSuccess(ItemDropRule.Common(ModContent.ItemType<SepiksPrimeMask>(), 7));
            npcLoot.Add(notExpert);

            //Item.NewItem(NPC.GetItemSource_Loot(), (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ModContent.ItemType<ExoticCipher>());
            //figure out b/c of OnKill replacement
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.PurpleCrystalShard, hitDirection);
        }

        public void HandlePacket(BinaryReader reader)
        {
            SepiksBossMessageType type = (SepiksBossMessageType)reader.ReadByte();
            switch (type)
            {
                case SepiksBossMessageType.DontTakeDamage:
                    NPC.dontTakeDamage = reader.ReadBoolean();
                    break;
                default:
                    DestinyMod.Instance.Logger.Error($"Sepiks Prime Packet Handler: Encountered unknown packet of type {type}");
                    break;
            }
        }

        private ModPacket GetPacket(SepiksBossMessageType type)
        {
            ModPacket packet = Mod.GetPacket();
            packet.Write(NPC.whoAmI);
            packet.Write((byte)type);
            return packet;
        }

        public override void Save(TagCompound tagCompound) => tagCompound.Add("Downed", DownedSepiksPrime);

        public override void Load(TagCompound tagCompound) => DownedSepiksPrime = tagCompound.Get<bool>("Downed");
    }

    internal enum SepiksBossMessageType : byte
    {
        DontTakeDamage
    }
}