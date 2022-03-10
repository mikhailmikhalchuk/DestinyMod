using Microsoft.Xna.Framework;
using DestinyMod.Common.Items.ItemTypes;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;

namespace DestinyMod.Content.Items.Weapons.Ranged
{
	public class Witherhoard : Gun
	{
		public override void SetStaticDefaults() => Tooltip.SetDefault("Projectiles blight surrounding tiles on impact"
			+ "\nEnemies that come in contact with the blight will be damaged"
			+ "\n'Like a one-man private security company.'");

		public override void DestinySetDefaults()
		{
			Item.damage = 35;
			Item.useTime = 30;
			Item.useAnimation = 30;
			Item.knockBack = 4;
			Item.value = Item.buyPrice(gold: 1);
			Item.rare = ItemRarityID.Pink;
			Item.UseSound = SoundID.Item61;
			Item.shoot = ProjectileID.GrenadeI;
			Item.shootSpeed = 8f;
			Item.useAmmo = ItemID.Grenade;
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Projectile.NewProjectile(source, new Vector2(position.X, position.Y - 7), velocity, ProjectileID.GrenadeI, damage, knockback, player.whoAmI);
			return false;
		}

		public override Vector2? HoldoutOffset() => new Vector2(-15, 0);
	}
}