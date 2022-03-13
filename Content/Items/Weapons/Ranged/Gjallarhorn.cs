using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using DestinyMod.Common.Items.ItemTypes;
using DestinyMod.Content.Projectiles.Weapons.Ranged;

namespace DestinyMod.Content.Items.Weapons.Ranged
{
	public class Gjallarhorn : Gun
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Fires a homing rocket which explodes into mini rockets on impact"
				+ "\n'If there is beauty in destruction, why not also in its delivery?'");
		}

		public override void DestinySetDefaults()
		{
			Item.damage = 520;
			Item.useTime = 35;
			Item.useAnimation = 35;
			Item.knockBack = 4;
			Item.value = Item.buyPrice(gold: 1);
			Item.rare = ItemRarityID.Red;
			Item.UseSound = SoundLoader.GetLegacySoundSlot(Mod, "Assets/Sounds/Item/Weapons/Ranged/Gjallarhorn");
			Item.autoReuse = true;
			Item.shootSpeed = 16f;
			Item.useAmmo = AmmoID.Rocket;
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Projectile.NewProjectile(source, new Vector2(position.X, position.Y - 6), velocity, ModContent.ProjectileType<GjallarhornRocket>(), damage, knockback, player.whoAmI);
			return false;
		}

		public override Vector2? HoldoutOffset() => new Vector2(-50, -5);

		public override void AddRecipes() => CreateRecipe(1)
			.AddIngredient(ItemID.Ectoplasm, 20)
			.AddTile(TileID.Anvils)
			.Register();
	}
}