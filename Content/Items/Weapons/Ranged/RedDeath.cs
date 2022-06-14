using DestinyMod.Common.Items.ItemTypes;
using DestinyMod.Content.Projectiles.Weapons.Ranged;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace DestinyMod.Content.Items.Weapons.Ranged
{
	public class RedDeath : Gun
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Three round burst"
			+ "\nOnly the first shot consumes ammo"
			+ "\nKills grant a small amount of health"
			+ "\n'Vanguard policy urges Guardians to destroy this weapon on sight. It is a Guardian killer.'");
		}

		public override void DestinySetDefaults()
		{
			Item.damage = 120;
			Item.useTime = 4;
			Item.useAnimation = 12;
			Item.knockBack = 4;
			Item.value = Item.buyPrice(gold: 1);
			Item.rare = ItemRarityID.Yellow;
			Item.UseSound = new SoundStyle("DestinyMod/Assets/Sounds/Item/Weapons/Ranged/RedDeath");
			Item.autoReuse = true;
			Item.shootSpeed = 16f;
			Item.reuseDelay = 14;
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Projectile.NewProjectile(source, position + new Vector2(0, -3), velocity, ModContent.ProjectileType<DeathBullet>(), damage, knockback, player.whoAmI);
			return false;
		}

		public override Vector2? HoldoutOffset() => new Vector2(-15, -5);

		public override bool CanConsumeAmmo(Item ammo, Player player) => player.itemAnimation >= Item.useAnimation - 2;

		public override void AddRecipes() => CreateRecipe(1)
			.AddIngredient(ItemID.Ectoplasm, 20)
			.AddTile(TileID.Anvils)
			.Register();
	}
}