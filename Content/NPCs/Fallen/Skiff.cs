using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using DestinyMod.Common.NPCs;
using Terraria.GameContent.Bestiary;
using System.Collections.Generic;
using DestinyMod.Common.NPCs.Data;

namespace DestinyMod.Content.NPCs.Fallen
{
    public class Skiff : DestinyModNPC
    {
        public Vector2 PositionToDrop = Vector2.Zero;

        public float Timer
        {
            get => NPC.ai[1];
            set => NPC.ai[1] = value;
        }

        public float Phase
        {
            get => NPC.ai[2];
            set => NPC.ai[2] = value;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fallen Skiff");
        }

        public override void DestinySetDefaults()
        {
            AIType = -1;
            NPC.knockBackResist = 0f;
            NPC.noGravity = true;
            NPC.lifeMax = 1000;
            NPC.defense = 10;
            NPC.width = 860;
            NPC.height = 610;
            NPC.value = Item.buyPrice(gold: 1);
            NPC.lavaImmune = true;
            NPC.noTileCollide = true;
            NPC.HitSound = SoundID.NPCHit4;
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new List<IBestiaryInfoElement> {
                BestiaryData.CommonTags.Visuals.Cosmodrome,

                new FlavorTextBestiaryInfoElement("Mods.DestinyMod.Bestiary.Skiff")
            });
        }

        public override void AI()
        {
            Timer++;
            if (Phase == 2f)
            {
                NPC.velocity = new Vector2(-10, -5);
                if (Timer >= 500f)
                {
                    NPC.life = 0;
                }
            }
            else if (PositionToDrop.Distance(NPC.Center) > 1f && Phase == 0f)
            {
                NPC.velocity = NPC.DirectionTo(PositionToDrop) * 5f;
            }
            else if (PositionToDrop.Distance(NPC.Center) <= 1f && Phase < 2f)
            {
                NPC.velocity = Vector2.Zero;
                Phase = 1f;
                if (Timer == 200f)
                {
                    NPC.NewNPC(NPC.GetSpawnSourceForNPCFromNPCAI(), (int)NPC.Center.X - NPC.width / 4, (int)NPC.Center.Y, ModContent.NPCType<Vandal>());
                    NPC.NewNPC(NPC.GetSpawnSourceForNPCFromNPCAI(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<Vandal>());
                    NPC.NewNPC(NPC.GetSpawnSourceForNPCFromNPCAI(), (int)NPC.Center.X + NPC.width / 4, (int)NPC.Center.Y, ModContent.NPCType<Vandal>());
                }
                if (Timer >= 300f)
                {
                    Phase++;
                }
            }
        }
    }
}