using DestinyMod.Content.Recipes.RecipeGroups;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace DestinyMod.Content.Items.Weapons.Ranged.Omolon
{
	public class OmolonScoutRifle : OmolonWeapon
	{
		public override void SetStaticDefaults() => Tooltip.SetDefault("Standard Omolon Scout Rifle");

		public override void DestinySetDefaults()
		{
			Item.damage = 30;
			Item.useTime = 19;
			Item.useAnimation = 19;
			Item.knockBack = 4;
			Item.crit = 2;
			Item.value = Item.buyPrice(gold: 1);
			Item.rare = ItemRarityID.LightRed;
			Item.UseSound = SoundLoader.GetLegacySoundSlot(Mod, "Sounds/Item/JadeRabbit");
			Item.shootSpeed = 300f;
		}

		public override bool Shoot(Player player, ProjectileSource_Item_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Projectile.NewProjectile(source, new Vector2(position.X, position.Y - 3), velocity, type, damage, knockback, player.whoAmI);
			return false;
		}

		public override Vector2? HoldoutOffset() => new Vector2(-10, 3);

		public override void AddRecipes() => CreateRecipe(1)
			.AddIngredient(ItemID.SoulofNight, 10)
			.AddRecipeGroup(ModContent.GetInstance<MythrilOrOrichalcumBars>().RecipeGroup, 8)
			.AddTile(TileID.MythrilAnvil)
			.Register();
	}
}