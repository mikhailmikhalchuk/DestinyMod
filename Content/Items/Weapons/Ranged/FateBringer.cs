using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using DestinyMod.Common.Items.ItemTypes;

namespace DestinyMod.Content.Items.Weapons.Ranged
{
	public class Fatebringer : Gun
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("'Delivering the inevitable, one pull at a time.'");
		}

		public override void DestinySetDefaults()
		{
			Item.damage = 60;
			Item.rare = ItemRarityID.Red;
			Item.knockBack = 0;
			Item.useTime = 20;
			Item.UseSound = new SoundStyle("DestinyMod/Assets/Sounds/Item/Weapons/Ranged/HandCannon120");
			Item.shootSpeed = 40f;
			Item.useAnimation = 20;
			Item.value = Item.buyPrice(gold: 1);
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Projectile.NewProjectile(source, new Vector2(position.X, position.Y - 3), velocity.RotatedByRandom(MathHelper.ToRadians(3)), type, damage, knockback, player.whoAmI);
			return false;
		}

		public override Vector2? HoldoutOffset() => new Vector2(5, 0);
	}
}