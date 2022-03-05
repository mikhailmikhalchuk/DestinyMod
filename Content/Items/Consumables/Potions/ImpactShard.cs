using DestinyMod.Common.Items.ItemTypes;
using DestinyMod.Content.Buffs;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace DestinyMod.Content.Items.Consumables.Potions
{
    public class ImpactShard : Consumable
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Allows you to find Motes off of enemies easier");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 20;
        }

        public override void DestinySetDefaults()
        {
            Item.useStyle = ItemUseStyleID.DrinkLiquid;
            Item.UseSound = SoundID.Item3;
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.buyPrice(gold: 1);
            Item.buffType = ModContent.BuffType<AncientShard>();
            Item.buffTime = 7200;
        }
    }
}