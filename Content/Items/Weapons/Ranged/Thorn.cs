using Microsoft.Xna.Framework;
using DestinyMod.Common.Items.ItemTypes;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using DestinyMod.Content.Projectiles.Weapons.Ranged;

namespace DestinyMod.Content.Items.Weapons.Ranged
{
	public class Thorn : Gun
	{
		public override void SetStaticDefaults() => Tooltip.SetDefault("Rounds pierce and poison targets"
			+ "\nKills with this weapon drop Remnants"
			+ "\nPicking up Remnants increases the damage of the poison"
			+ "\n\"To rend one's enemies is to see them not as equals, but objects - hollow of spirit and meaning.\"");

		public override void DestinySetDefaults()
		{
			Item.damage = 95;
			Item.useTime = 15;
			Item.useAnimation = 15;
			Item.knockBack = 4;
			Item.value = Item.buyPrice(gold: 1);
			Item.rare = ItemRarityID.Yellow;
			Item.UseSound = SoundLoader.GetLegacySoundSlot(Mod, "Sounds/Item/Thorn");
			Item.shootSpeed = 40f;
		}

		public override bool Shoot(Player player, ProjectileSource_Item_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Projectile.NewProjectile(source, position + new Vector2(0, -2), velocity, ModContent.ProjectileType<ThornBullet>(), damage, knockback, player.whoAmI);
			return false;
		}

		public override Vector2? HoldoutOffset() => new Vector2(0, 3);
	}
}