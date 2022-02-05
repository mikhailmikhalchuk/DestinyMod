using DestinyMod.Common.Items.ItemTypes;
using DestinyMod.Common.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace DestinyMod.Content.Items.Weapons.Ranged
{
	public class Telesto : Gun
	{
		public override void SetStaticDefaults() => Tooltip.SetDefault("Hold down the trigger to fire"
			+ "\nProjectiles attach to surfaces and explode after a short delay"
			+ "\n\"Vestiges of the Queen's Harbingers yet linger among Saturn's moons.\"");

		public override void DestinySetDefaults()
		{
			Item.damage = 35;
			Item.crit = 4;
			Item.channel = true;
			Item.rare = ItemRarityID.Yellow;
			Item.knockBack = 0;
			Item.useTime = 15;
			Item.useAnimation = 15;
			Item.value = Item.buyPrice(gold: 1);
		}

		public override bool Shoot(Player player, ProjectileSource_Item_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			DestinyModProjectile.FireFusionProjectile(player, source, new Vector2(position.X, position.Y - 2), velocity, damage, knockback, 7, ModContent.ProjectileType<TelestoBullet>());
			return false;
		}

		public override Vector2? HoldoutOffset() => new Vector2(-1, 0);
	}
}