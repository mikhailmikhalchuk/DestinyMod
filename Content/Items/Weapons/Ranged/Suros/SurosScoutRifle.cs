using DestinyMod.Common.Items.ItemTypes;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace DestinyMod.Content.Items.Weapons.Ranged.Suros
{
	public class SurosScoutRifle : Gun
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("SUROS Scout Rifle");
			Tooltip.SetDefault("Standard SUROS Scout Rifle");
		}

		public override void DestinySetDefaults()
		{
			Item.damage = 50;
			Item.useTime = 19;
			Item.useAnimation = 19;
			Item.knockBack = 4;
			Item.crit = 2;
			Item.value = Item.buyPrice(gold: 1);
			Item.rare = ItemRarityID.Pink;
			Item.UseSound = SoundLoader.GetLegacySoundSlot(Mod, "Sounds/Item/JadeRabbit");
			Item.shootSpeed = 300f;
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Projectile.NewProjectile(source, new Vector2(position.X, position.Y - 3), velocity, type, damage, knockback, player.whoAmI);
			return false;
		}

		public override Vector2? HoldoutOffset() => new Vector2(-10, 0);

		public override void AddRecipes() => CreateRecipe(1)
			.AddIngredient(ItemID.Obsidian, 20)
			.AddIngredient(ItemID.HallowedBar, 10)
			.AddTile(TileID.MythrilAnvil)
			.Register();
	}
}