using Terraria;
using System;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using DestinyMod.Common.Items;
using DestinyMod.Common.ModPlayers;

namespace DestinyMod.Content.Items.Buffers
{
	public class ThornRemnant : DestinyModItem
    {
        public Player RemnantOwner { get; set; }

        private int Age;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Remnant");
            ItemID.Sets.ItemNoGravity[Item.type] = true;
        }

        public override void DestinySetDefaults()
        {
            Item.rare = ItemRarityID.Gray;
        }

        public override bool ItemSpace(Player player) => true;

        public override bool CanPickup(Player player) => player == RemnantOwner;

        public override bool OnPickup(Player player)
        {
            int addValue = 420;
            if (player.FindBuffIndex(ModContent.BuffType<Buffs.MarkOfTheDevourer>()) != -1)
            {
                addValue = Math.Clamp(player.buffTime[player.FindBuffIndex(ModContent.BuffType<Buffs.MarkOfTheDevourer>())] + 420, 0, 600);
            }
            player.AddBuff(ModContent.BuffType<Buffs.MarkOfTheDevourer>(), addValue);
            SoundEngine.PlaySound(SoundID.Grab, player.Center);
            return false;
        }

        public override void Update(ref float gravity, ref float maxFallSpeed)
        {
            Age++;
            if (Age > 900)
            {
                Item.TurnToAir();
                for (int i = 0; i < 100; i++)
                {
                    Dust dust = Dust.NewDustDirect(Item.Center, 6, 6, DustID.GreenTorch, Scale: 2.5f);
                    float dustX = dust.velocity.X;
                    float dustY = dust.velocity.Y;
                    if (dustX == 0f && dustY == 0f)
                    {
                        dustX = 1f;
                    }
                    float num = (float)Math.Sqrt(dustX * dustX + dustY * dustY);
                    num = 13f / num;
                    dustX *= num;
                    dustY *= num;
                    dust.velocity *= 0.1f;
                    dust.velocity.X += dustX / 1.2f;
                    dust.velocity.Y += dustY / 1.2f;
                    dust.velocity *= 0.5f;
                    dust.noGravity = true;
                }
            }
        }

        public override void GrabRange(Player player, ref int grabRange) => grabRange = 128;

        public override void PostUpdate() => Lighting.AddLight(Item.Center, Color.Green.ToVector3() * 1.5f);
    }
}