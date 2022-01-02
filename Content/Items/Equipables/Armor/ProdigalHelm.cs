using DestinyMod.Common.Items.ItemTypes;
using DestinyMod.Content.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DestinyMod.Content.Items.Armor
{
	[AutoloadEquip(EquipType.Head)]
	public class ProdigalHelm : ClassArmor
	{
		public override DestinyClassType ArmorClassType => DestinyClassType.Titan;

		public override void SetStaticDefaults() => Tooltip.SetDefault("7% increased ranged critical strike chance");

		public override void DestinySetDefaults()
		{
			Item.value = Item.sellPrice(silver: 90);
			Item.rare = ItemRarityID.Orange;
			Item.defense = 8;
		}

		public override void UpdateEquip(Player player) => player.GetCritChance(DamageClass.Ranged) += 7;

		public override bool IsArmorSet(Item head, Item body, Item legs) => 
			body.type == ModContent.ItemType<ProdigalCuirass>() && legs.type == ModContent.ItemType<ProdigalGreaves>();

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "7% increased ranged damage";
			player.GetDamage(DamageClass.Ranged) += 0.07f;
		}

		public override void AddRecipes() => CreateRecipe(1)
			.AddIngredient(ModContent.ItemType<RelicIron>(), 20)
			.AddIngredient(ModContent.ItemType<PlasteelPlating>(), 10)
			.AddTile(TileID.Anvils)
			.Register();
	}
}