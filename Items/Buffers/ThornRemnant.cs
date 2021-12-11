using Terraria;
using System;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.DataStructures;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace TheDestinyMod.Items.Buffers
{
    public class ThornRemnant : ModItem
    {
        public Player RemnantOwner { get; set; }

        private int Age;

        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Remnant");
            ItemID.Sets.ItemNoGravity[item.type] = true;
        }

        public override void SetDefaults() {
            item.width = 24;
            item.height = 24;
            item.rare = ItemRarityID.Gray;
        }

        public override bool ItemSpace(Player player) => true;

        public override bool CanPickup(Player player) {
            return player == RemnantOwner;
        }

        public override bool OnPickup(Player player) {
            var modPlayer = player.DestinyPlayer();
            if (modPlayer.necroticDamageMult < 1f) {
                modPlayer.necroticDamageMult += 0.2f;
            }
            player.AddBuff(ModContent.BuffType<Buffs.MarkOfTheDevourer>(), 600);
            Main.PlaySound(SoundID.Grab, player.Center);
            return false;
        }

        public override void Update(ref float gravity, ref float maxFallSpeed) {
            Age++;
            if (Age > 900) {
                item.TurnToAir();
                for (int i = 0; i < 100; i++) {
                    Dust dust = Dust.NewDustDirect(item.Center, 6, 6, DustID.GreenTorch, Scale: 2.5f);
                    float dustX = dust.velocity.X;
                    float dustY = dust.velocity.Y;
                    if (dustX == 0f && dustY == 0f) {
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

        public override void GrabRange(Player player, ref int grabRange) {
            grabRange = 128;
        }

        public override void PostUpdate() {
            Lighting.AddLight(item.Center, Color.Green.ToVector3() * 1.5f);
        }
    }
}