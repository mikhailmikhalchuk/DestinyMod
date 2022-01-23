using DestinyMod.Content.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DestinyMod.Content.Items.Weapons.Ranged.Khvostov
{
	public class Khvostov7G02 : Khvostov
	{
		public override void SetStaticDefaults() => DisplayName.SetDefault("Khvostov 7G-02");

		public override void DestinySetDefaults()
		{
			Item.damage = 5;
			Item.useTime = 10;
			Item.useAnimation = 10;
			Item.rare = ItemRarityID.Orange;
		}

		public override void AddRecipes() => CreateRecipe(1)
			.AddIngredient(ModContent.ItemType<RelicIron>(), 45)
			.AddIngredient(ItemID.Bone, 25)
			.AddIngredient(ItemID.HellstoneBar, 12)
			.AddTile(TileID.Anvils)
			.Register();
	}
}