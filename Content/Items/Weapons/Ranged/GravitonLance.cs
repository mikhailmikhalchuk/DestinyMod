using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using DestinyMod.Common.Items.ItemTypes;
using DestinyMod.Content.Projectiles.Weapons.Ranged;

namespace DestinyMod.Content.Items.Weapons.Ranged
{
	public class GravitonLance : Gun
	{
		public int Shot;

		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Three round burst"
			+ "\nSecond shot of a burst deals double damage"
			+ "\nKills with this shot summon a seeking projectile"
			+ "\n'Think of space-time as a tapestry on a loom. This weapon is the needle.'");
		}

		public override void DestinySetDefaults()
		{
			Item.damage = 19;
			Item.useTime = 7;
			Item.useAnimation = 21;
			Item.knockBack = 4;
			Item.value = Item.buyPrice(gold: 1);
			Item.rare = ItemRarityID.Orange;
			Item.UseSound = SoundLoader.GetLegacySoundSlot(Mod, "Assets/Sounds/Item/Weapons/Ranged/GravitonLance");
			Item.shootSpeed = 16f;
			Item.reuseDelay = 5;
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			if ((++Shot + 1) % 3 == 0)
			{
				type = ModContent.ProjectileType<GravitonBullet>();
				damage *= 3;
			}
			Projectile.NewProjectile(source, new Vector2(position.X, position.Y - 8), velocity, type, damage, knockback, player.whoAmI);
			return false;
		}
		
		public override Vector2? HoldoutOffset() => new Vector2(-10, -2);
	}
}