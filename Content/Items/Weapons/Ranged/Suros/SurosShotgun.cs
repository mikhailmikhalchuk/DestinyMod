using DestinyMod.Common.Items.ItemTypes;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;

namespace DestinyMod.Content.Items.Weapons.Ranged.Suros
{
	public class SurosShotgun : Gun
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("SUROS Shotgun");
			Tooltip.SetDefault("Fires a spread of bullets"
				+ "\nStandard SUROS Shotgun");
		}

		public override void DestinySetDefaults()
		{
			Item.damage = 35;
			Item.useTime = 50;
			Item.useAnimation = 50;
			Item.knockBack = 4;
			Item.value = Item.buyPrice(gold: 1);
			Item.rare = ItemRarityID.Pink;
			Item.UseSound = SoundID.Item36;
			Item.shootSpeed = 16f;
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			for (int count = 0; count < 4 + Main.rand.Next(2); count++)
			{
				Projectile.NewProjectile(source, position, velocity.RotatedByRandom(MathHelper.ToRadians(15)), type, damage, knockback, player.whoAmI);
			}
			return true;
		}

		public override Vector2? HoldoutOffset() => new Vector2(-10, 0);

		public override void AddRecipes() => CreateRecipe(1)
			.AddIngredient(ItemID.Obsidian, 20)
			.AddIngredient(ItemID.HallowedBar, 10)
			.AddTile(TileID.MythrilAnvil)
			.Register();
	}
}