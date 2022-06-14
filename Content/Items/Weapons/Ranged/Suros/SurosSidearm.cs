using DestinyMod.Common.Items.ItemTypes;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace DestinyMod.Content.Items.Weapons.Ranged.Suros
{
	public class SurosSidearm : Gun
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("SUROS Sidearm");
			Tooltip.SetDefault("Standard SUROS Sidearm");
		}

		public override void DestinySetDefaults()
		{
			Item.damage = 30;
			Item.useTime = 10;
			Item.useAnimation = 10;
			Item.knockBack = 4;
			Item.value = Item.buyPrice(0, 1, 0, 0);
			Item.rare = ItemRarityID.Pink;
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
			.AddIngredient(ItemID.Obsidian, 15)
			.AddIngredient(ItemID.HallowedBar, 8)
			.AddTile(TileID.MythrilAnvil)
			.Register();
	}
}