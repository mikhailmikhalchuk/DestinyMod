using DestinyMod.Content.Recipes.RecipeGroups;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace DestinyMod.Content.Items.Weapons.Ranged.Omolon
{
	public class OmolonSniper : OmolonWeapon
	{
		public override void SetStaticDefaults() => Tooltip.SetDefault("Standard Omolon Sniper Rifle");

		public override void DestinySetDefaults()
		{
			Item.damage = 65;
			Item.useTime = 28;
			Item.useAnimation = 28;
			Item.knockBack = 4;
			Item.value = Item.buyPrice(gold: 1);
			Item.rare = ItemRarityID.LightRed;
			Item.UseSound = new SoundStyle("DestinyMod/Assets/Sounds/Item/Weapons/Ranged/JadeRabbit");
			Item.shootSpeed = 300f;
		}

		public override Vector2? HoldoutOffset() => new Vector2(-10, 3);

		public override void AddRecipes() => CreateRecipe(1)
			.AddIngredient(ItemID.SoulofNight, 5)
			.AddRecipeGroup(ModContent.GetInstance<CobaltOrPalladiumBars>().RecipeGroup, 8)
			.AddTile(TileID.MythrilAnvil)
			.Register();
	}
}