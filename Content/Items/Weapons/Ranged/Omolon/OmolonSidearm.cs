using DestinyMod.Content.Recipes.RecipeGroups;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace DestinyMod.Content.Items.Weapons.Ranged.Omolon
{
	public class OmolonSidearm : OmolonWeapon
	{
		public override void SetStaticDefaults() => Tooltip.SetDefault("Standard Omolon Sidearm");

		public override void DestinySetDefaults()
		{
			Item.damage = 20;
			Item.useTime = 10;
			Item.useAnimation = 10;
			Item.knockBack = 4;
			Item.value = Item.buyPrice(gold: 1);
			Item.rare = ItemRarityID.LightRed;
			Item.UseSound = new SoundStyle("DestinyMod/Assets/Sounds/Item/Weapons/Ranged/JadeRabbit");
			Item.shootSpeed = 16f;
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Projectile.NewProjectile(source, new Vector2(position.X, position.Y - 3), velocity, type, damage, knockback, player.whoAmI);
			return false;
		}

		public override Vector2? HoldoutOffset() => new Vector2(5, 2);

		public override void AddRecipes() => CreateRecipe(1)
			.AddIngredient(ItemID.SoulofLight, 5)
			.AddRecipeGroup(ModContent.GetInstance<CobaltOrPalladiumBars>().RecipeGroup, 8)
			.AddTile(TileID.MythrilAnvil)
			.Register();
	}
}