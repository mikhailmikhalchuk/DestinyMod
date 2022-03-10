using DestinyMod.Common.Items.ItemTypes;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace DestinyMod.Content.Items.Weapons.Ranged.Suros
{
	public class SurosAutoRifle : Gun
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("SUROS Auto Rifle");
			Tooltip.SetDefault("Standard SUROS Auto Rifle");
		}

		public override void DestinySetDefaults()
		{
			Item.damage = 35;
			Item.useTime = 8;
			Item.useAnimation = 8;
			Item.knockBack = 4;
			Item.value = Item.buyPrice(gold: 1);
			Item.rare = ItemRarityID.Pink;
			Item.UseSound = SoundLoader.GetLegacySoundSlot(Mod, "Assets/Sounds/Item/Weapons/Ranged/JadeRabbit");
			Item.autoReuse = true;
			Item.shootSpeed = 30f;
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Projectile.NewProjectile(source, new Vector2(position.X, position.Y - 2), velocity, type, damage, knockback, player.whoAmI);
			return false;
		}

		public override Vector2? HoldoutOffset() => new Vector2(-4, 0);

		public override void AddRecipes() => CreateRecipe(1)
			.AddIngredient(ItemID.Obsidian, 25)
			.AddIngredient(ItemID.HallowedBar, 15)
			.AddTile(TileID.MythrilAnvil)
			.Register();
	}
}