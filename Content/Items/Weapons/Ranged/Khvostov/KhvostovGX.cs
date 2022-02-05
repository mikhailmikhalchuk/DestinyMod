using DestinyMod.Content.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DestinyMod.Content.Items.Weapons.Ranged.Khvostov
{
	public class KhvostovGX : Khvostov
	{
		public override void SetStaticDefaults() => DisplayName.SetDefault("Khvostov G-X");

		public override void DestinySetDefaults()
		{
			Item.damage = 20;
			Item.useTime = 9;
			Item.useAnimation = 9;
			Item.rare = ItemRarityID.Pink;
		}

		public override void AddRecipes() => CreateRecipe(1)
			.AddIngredient(ModContent.ItemType<Khvostov7G0X>(), 1)
			.AddIngredient(ModContent.ItemType<RelicIron>(), 20)
			.AddIngredient(ItemID.HallowedBar, 9)
			.AddIngredient(ItemID.SoulofFright, 3)
			.AddIngredient(ItemID.SoulofMight, 3)
			.AddIngredient(ItemID.SoulofSight, 3)
			.AddTile(TileID.MythrilAnvil)
			.Register();
	}
}