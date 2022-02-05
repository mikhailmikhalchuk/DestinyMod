using DestinyMod.Content.Recipes.RecipeGroups;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace DestinyMod.Content.Items.Weapons.Ranged.Omolon
{
	public class OmolonShotgun : OmolonWeapon
	{
		public override void SetStaticDefaults() => Tooltip.SetDefault("Fires a spread of bullets"
			+ "\nStandard Omolon Shotgun");

		public override void DestinySetDefaults()
		{
			Item.damage = 19;
			Item.useTime = 50;
			Item.useAnimation = 50;
			Item.knockBack = 4;
			Item.value = Item.buyPrice(gold: 1);
			Item.rare = ItemRarityID.LightRed;
			Item.UseSound = SoundID.Item36;
			Item.shootSpeed = 16f;
		}

		public override bool Shoot(Player player, ProjectileSource_Item_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			for (int i = 0; i < 4 + Main.rand.Next(2); i++)
			{
				Projectile.NewProjectile(source, position, velocity.RotatedByRandom(MathHelper.ToRadians(15)), type, damage, knockback, player.whoAmI);
			}
			return true;
		}

		public override Vector2? HoldoutOffset() => new Vector2(-10, 0);

		public override void AddRecipes() => CreateRecipe(1)
			.AddIngredient(ItemID.SoulofNight, 10)
			.AddRecipeGroup(ModContent.GetInstance<MythrilOrOrichalcumBars>().RecipeGroup, 8)
			.AddTile(TileID.MythrilAnvil)
			.Register();
	}
}