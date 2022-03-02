using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.DataStructures;
using DestinyMod.Common.Items.ItemTypes;

namespace DestinyMod.Content.Items.Weapons.Ranged
{
	public class BorrowedTime : Gun
	{
		public override void SetStaticDefaults() => Tooltip.SetDefault("Scales with world progression"
			+ "\n\"Give a little, but take a little more.\"");

		public override void DestinySetDefaults()
		{
			Item.damage = 4;
			Item.knockBack = 4;
			Item.useTime = 6;
			Item.useAnimation = 6;
			Item.UseSound = SoundID.Item11;
			Item.autoReuse = true;
			Item.shootSpeed = 16f;
			Item.value = Item.buyPrice(gold: 1);
			Item.rare = ItemRarityID.Yellow;
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Vector2 perturbedSpeed = velocity.RotatedByRandom(MathHelper.ToRadians(5));
			Projectile.NewProjectile(source, new Vector2(position.X, position.Y - 4), velocity.RotatedByRandom(MathHelper.ToRadians(5)), type, damage, knockback, player.whoAmI);
			return false;
		}

		public override Vector2? HoldoutOffset() => new Vector2(-5, 1);
	}
}