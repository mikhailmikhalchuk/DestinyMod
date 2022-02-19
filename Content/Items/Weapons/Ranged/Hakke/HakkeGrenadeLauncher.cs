using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using DestinyMod.Common.Items.ItemTypes;
using DestinyMod.Content.Items.Materials;

namespace DestinyMod.Content.Items.Weapons.Ranged.Hakke
{
	public class HakkeGrenadeLauncher : Gun
	{
		public override void SetStaticDefaults() => Tooltip.SetDefault("Standard Hakke Grenade Launcher");

		public override void DestinySetDefaults()
		{
			Item.damage = 15;
			Item.useTime = 45;
			Item.useAnimation = 45;
			Item.knockBack = 4;
			Item.value = Item.buyPrice(gold: 1);
			Item.rare = ItemRarityID.Blue;
			Item.UseSound = SoundID.Item61;
			Item.shoot = ProjectileID.GrenadeI;
			Item.shootSpeed = 3f;
			Item.useAmmo = ItemID.Grenade;
		}

		public override bool Shoot(Player player, ProjectileSource_Item_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Projectile.NewProjectile(source, new Vector2(position.X, position.Y - 7), velocity, ProjectileID.GrenadeI, damage, knockback, player.whoAmI);
			return false;
		}

		public override Vector2? HoldoutOffset() => new Vector2(-10, -3);

		public override void AddRecipes() => CreateRecipe(1)
			.AddIngredient(ModContent.ItemType<RelicIron>(), 40)
			.AddIngredient(ItemID.Bone, 40)
			.AddIngredient(ItemID.HellstoneBar, 8)
			.AddTile(TileID.Anvils)
			.Register();
	}
}