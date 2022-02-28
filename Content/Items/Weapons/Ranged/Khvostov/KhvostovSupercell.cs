using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DestinyMod.Content.Items.Weapons.Ranged.Khvostov
{
	public class KhvostovSupercell : Khvostov
	{
		public override void SetStaticDefaults() => DisplayName.SetDefault("Khvostov Supercell");

		public override void DestinySetDefaults()
		{
			Item.damage = 40;
			Item.useTime = 7;
			Item.useAnimation = 7;
			Item.rare = ItemRarityID.Yellow;
		}

		public override void AddRecipes() => CreateRecipe(1)
			.AddIngredient(ModContent.ItemType<KhvostovGX>(), 1)
			.AddIngredient(ItemID.ChlorophyteBar, 10)
			.AddIngredient(ItemID.Ectoplasm, 15)
			.AddIngredient(ItemID.PixieDust, 20)
			.AddTile(TileID.MythrilAnvil)
			.Register();
	}
}