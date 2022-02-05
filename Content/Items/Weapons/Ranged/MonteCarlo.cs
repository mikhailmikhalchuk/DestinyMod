using DestinyMod.Common.Items.ItemTypes;
using DestinyMod.Content.Items.Materials;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace DestinyMod.Content.Items.Weapons.Ranged
{
	public class MonteCarlo : Gun
	{
		public override void SetStaticDefaults() => Tooltip.SetDefault("Defeating an enemy with this weapon stacks one second of Monte Carlo Method"
			+ "\n\"There will always be paths to tread and methods to try. Roll with it.\"");

		public override void DestinySetDefaults()
		{
			Item.damage = 30;
			Item.autoReuse = true;
			Item.rare = ItemRarityID.Red;
			Item.knockBack = 0;
			Item.useTime = 9;
			Item.UseSound = SoundLoader.GetLegacySoundSlot(Mod, "Sounds/Item/MonteCarlo");
			Item.shootSpeed = 20f;
			Item.useAnimation = 9;
			Item.value = Item.buyPrice(gold: 1);
		}

		public override bool Shoot(Player player, ProjectileSource_Item_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Projectile.NewProjectile(source, new Vector2(position.X, position.Y - 3), velocity.RotatedByRandom(MathHelper.ToRadians(3)), ModContent.ProjectileType<MonteBullet>(), damage, knockback, player.whoAmI);
			return false;
		}

		public override Vector2? HoldoutOffset() => new Vector2(-15, 0);

		public override void AddRecipes() => CreateRecipe(1)
			.AddIngredient(ModContent.ItemType<GunsmithMaterials>(), 50)
			.AddIngredient(ItemID.FragmentVortex, 15)
			.AddTile(TileID.LunarCraftingStation)
			.Register();
	}
}