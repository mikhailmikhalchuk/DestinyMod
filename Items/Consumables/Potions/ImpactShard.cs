using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheDestinyMod.Buffs;

namespace TheDestinyMod.Items.Consumables.Potions
{
	public class ImpactShard : ModItem
	{
        public override void SetStaticDefaults() {
            Tooltip.SetDefault("Allows you to find Motes off of enemies easier");
        }

        public override void SetDefaults() {
            item.width = 20;
            item.height = 32;
            item.useStyle = ItemUseStyleID.EatingUsing;
            item.useAnimation = 15;
            item.useTime = 15;
            item.useTurn = true;
            item.UseSound = SoundID.Item3;
            item.maxStack = 30;
            item.consumable = true;
            item.rare = ItemRarityID.Orange;
            item.value = Item.buyPrice(gold: 1);
            item.buffType = ModContent.BuffType<AncientShard>();
            item.buffTime = 7200;
        }
    }
}