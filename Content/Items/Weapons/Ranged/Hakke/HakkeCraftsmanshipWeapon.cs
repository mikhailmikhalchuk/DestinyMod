using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;
using DestinyMod.Common.Items.ItemTypes;
using DestinyMod.Content.Projectiles.Weapons.Ranged;
using DestinyMod.Content.Buffs;

namespace DestinyMod.Content.Items.Weapons.Ranged.Hakke
{
	public abstract class HakkeCraftsmanshipWeapon : Gun
	{
		public Vector2 ShootOffset;

		public float SpreadRadians;

		public override void SetStaticDefaults() => Tooltip.SetDefault("Has a chance to grant the \"Hakke Craftsmanship\" buff on use");

		public static void HandleMuzzleOffset(ref Vector2 position, Vector2 velocity)
		{
			Vector2 muzzleOffset = Vector2.Normalize(velocity) * 10f;
			if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
			{
				position += muzzleOffset;
			}
		}

		public static void HandleApplyingHakke(Player player)
		{
			int hakkeBuff = ModContent.BuffType<HakkeBuff>();
			if (Main.rand.NextBool(10) && !player.HasBuff(hakkeBuff))
			{
				player.AddBuff(hakkeBuff, 90);
			}
		}

		public override bool Shoot(Player player, ProjectileSource_Item_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			HandleMuzzleOffset(ref position, velocity);
			Projectile.NewProjectile(source, position + ShootOffset, velocity.RotatedByRandom(SpreadRadians), ModContent.ProjectileType<HakkeBullet>(), damage, knockback, player.whoAmI);
			HandleApplyingHakke(player);
			return false;
		}
	}
}