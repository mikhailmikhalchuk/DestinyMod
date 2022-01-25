using DestinyMod.Common.Items.ItemTypes;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace DestinyMod.Content.Items.Weapons.Ranged.Suros
{
	public class SurosPulseRifle : Gun
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("SUROS Pulse Rifle");
			Tooltip.SetDefault("Three round burst"
				+ "\nStandard SUROS Pulse Rifle");
		}

		public override void DestinySetDefaults()
		{
			Item.damage = 32;
			Item.useTime = 4;
			Item.useAnimation = 12;
			Item.knockBack = 4;
			Item.value = Item.buyPrice(gold: 1);
			Item.autoReuse = true;
			Item.rare = ItemRarityID.Pink;
			Item.UseSound = SoundLoader.GetLegacySoundSlot(Mod, "Sounds/Item/JadeRabbitBurst");
			Item.shootSpeed = 16f;
			Item.reuseDelay = 14;
		}

		public override bool Shoot(Player player, ProjectileSource_Item_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Projectile.NewProjectile(source, new Vector2(position.X, position.Y - 2), velocity, type, damage, knockback, player.whoAmI);
			return false;
		}

		public override Vector2? HoldoutOffset() => new Vector2(-12, 1);

		public override void AddRecipes() => CreateRecipe(1)
			.AddIngredient(ItemID.Obsidian, 25)
			.AddIngredient(ItemID.HallowedBar, 15)
			.AddTile(TileID.MythrilAnvil)
			.Register();
	}
}