using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using DestinyMod.Common.Items.ItemTypes;

namespace DestinyMod.Content.Items.Weapons.Ranged
{
	public class HezenVengeance : Gun
	{
		public override void SetStaticDefaults() => Tooltip.SetDefault("'The Hezen Axis Mind is tireless and infinite. While it cannot be stopped... it can be paused.'");

		public override void DestinySetDefaults()
		{
			Item.damage = 20;
			Item.useTime = 35;
			Item.useAnimation = 35;
			Item.knockBack = 4;
			Item.value = Item.buyPrice(gold: 1);
			Item.rare = ItemRarityID.Blue;
			Item.UseSound = SoundLoader.GetLegacySoundSlot(Mod, "Assets/Sounds/Item/Weapons/Ranged/RocketLauncher");
			Item.shoot = ProjectileID.RocketI;
			Item.shootSpeed = 16f;
			Item.useAmmo = AmmoID.Rocket;
		}

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
			Projectile.NewProjectile(source, new Vector2(position.X, position.Y - 7), velocity, ProjectileID.RocketI, damage / 3, knockback, player.whoAmI);
			return false;
		}

		public override Vector2? HoldoutOffset() => new Vector2(-50, -5);
	}
}