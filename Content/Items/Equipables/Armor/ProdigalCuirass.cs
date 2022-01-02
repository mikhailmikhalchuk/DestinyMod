using DestinyMod.Common.Items.ItemTypes;
using DestinyMod.Content.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DestinyMod.Content.Items.Armor
{
	[AutoloadEquip(EquipType.Body)]
	public class ProdigalCuirass : ClassArmor
	{
		public override DestinyClassType ArmorClassType => DestinyClassType.Titan;

		public override void SetStaticDefaults() => Tooltip.SetDefault("5% increased ranged damage");

		public override void DestinySetDefaults()
		{
			Item.value = Item.sellPrice(silver: 60);
			Item.rare = ItemRarityID.Orange;
			Item.defense = 9;
		}

		public override void UpdateEquip(Player player) => player.GetDamage(DamageClass.Ranged) += 0.05f;

		public override void AddRecipes() => CreateRecipe(1)
			.AddIngredient(ModContent.ItemType<RelicIron>(), 30)
			.AddIngredient(ModContent.ItemType<PlasteelPlating>(), 16)
			.AddTile(TileID.Anvils)
			.Register();
	}
}