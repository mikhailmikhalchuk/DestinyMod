using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using DestinyMod.Common.NPCs;
using Terraria.ModLoader;
using System;
using DestinyMod.Content.Projectiles.NPCs.Bosses.SepiksPrime;
using Terraria.GameContent.Bestiary;
using System.Collections.Generic;
using DestinyMod.Common.NPCs.Data;
using Terraria.DataStructures;

namespace DestinyMod.Content.NPCs.SepiksPrime
{
    public class SepiksServitor : DestinyModNPC
    {
        public int Timer
        {
            get => (int)NPC.ai[0];
            set => NPC.ai[0] = value;
        }

        public int RandomFireTime
        {
            get => (int)NPC.ai[1];
            set => NPC.ai[1] = value;
        }

        public override void SetStaticDefaults()
        {
            NPCID.Sets.CantTakeLunchMoney[Type] = true;
            NPCID.Sets.BossBestiaryPriority.Add(Type);

            NPCID.Sets.DebuffImmunitySets.Add(Type, new NPCDebuffImmunityData { ImmuneToAllBuffsThatAreNotWhips = true });
        }

        public override void DestinySetDefaults()
        {
            NPC.aiStyle = -1;
            NPC.noGravity = true;
            NPC.width = 48;
            NPC.height = 48;
            NPC.knockBackResist = 0f;
            NPC.damage = 10;
            NPC.defense = 15;
            NPC.lifeMax = 200;
            NPC.HitSound = SoundID.NPCHit4;
            NPC.lavaImmune = true;
            NPC.noTileCollide = true;
            RandomFireTime = 120;

            for (int k = 0; k < NPC.buffImmune.Length; k++)
            {
                NPC.buffImmune[k] = true;
            }
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.UIInfoProvider = new CommonEnemyUICollectionInfoProvider(ContentSamples.NpcBestiaryCreditIdsByNpcNetIds[ModContent.NPCType<SepiksPrime>()], true);

            bestiaryEntry.Info.AddRange(new List<IBestiaryInfoElement> {
                BestiaryData.CommonTags.Visuals.Cosmodrome,

				new FlavorTextBestiaryInfoElement("Mods.DestinyMod.Bestiary.SepiksServitor")
            });
        }

        public override void AI()
        {
            Timer++;
            NPC.TargetClosest(true);
            Player target = Main.player[NPC.target];
            NPC.rotation = (float)Math.Atan2(NPC.position.Y + NPC.height - 59f - target.Center.Y, NPC.Center.X - target.Center.X) + MathHelper.PiOver2;
            if (Timer >= RandomFireTime)
            {
                Vector2 deltaRing = target.Center - NPC.Center;
                Vector2 velocity = 10 * deltaRing.SafeNormalize(new Vector2(0, 0.5f));
                Projectile.NewProjectile(NPC.GetSpawnSourceForProjectileNPC(), NPC.Center, velocity, ModContent.ProjectileType<ServitorBlast>(), 20, 5, Main.myPlayer, NPC.whoAmI);
                NPC.netUpdate = true;
                RandomFireTime = Main.rand.Next(90, 200);
                Timer = 0;
            }
        }
    }
}