﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheDestinyMod.Buffs;

namespace TheDestinyMod.Items.Consumables
{
    public class ClassChangeToken : ModItem
    {
        public override void SetStaticDefaults() {
            Tooltip.SetDefault("Grants the ability to switch to a different class");
        }

        public override void SetDefaults() {
            item.width = 16;
            item.height = 16;
            item.useStyle = ItemUseStyleID.HoldingUp;
            item.useAnimation = 15;
            item.useTime = 15;
            item.useTurn = true;
            item.UseSound = SoundID.Item3;
            item.maxStack = 30;
            item.consumable = true;
            item.rare = ItemRarityID.Orange;
            item.value = Item.buyPrice(gold: 1);
        }

        public override bool UseItem(Player player) {
            return base.UseItem(player);
        }
    }
}