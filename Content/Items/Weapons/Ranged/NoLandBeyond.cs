using DestinyMod.Common.Items.ItemTypes;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace DestinyMod.Content.Items.Weapons.Ranged
{
	public class NoLandBeyond : Gun
	{
		public override void SetStaticDefaults() => Tooltip.SetDefault("'Every hit blazes the path to our reclamation.'");

		public override void DestinySetDefaults()
		{
			Item.damage = 150;
			Item.rare = ItemRarityID.Yellow;
			Item.knockBack = 0;
			Item.useTime = 120;
			Item.UseSound = SoundLoader.GetLegacySoundSlot(Mod, "Assets/Sounds/Item/Weapons/Ranged/NoLandBeyond");
			Item.shootSpeed = 16f;
			Item.useAnimation = 120;
			Item.value = Item.buyPrice(0, 1, 0, 0);
			DestinyModReuseDelay = 20;
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Projectile.NewProjectile(source, new Vector2(position.X, position.Y - 2), velocity, type, damage, knockback, player.whoAmI);
			return false;
		}

		public override Vector2? HoldoutOffset() => new Vector2(-10, 0);

		public override void AddRecipes() => CreateRecipe(1)
			.AddIngredient(ItemID.Ectoplasm, 20)
			.AddTile(TileID.Anvils)
			.Register();
	}
}