using Microsoft.Xna.Framework;
using DestinyMod.Common.Items.ItemTypes;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using DestinyMod.Content.Projectiles.Weapons.Ranged;

namespace DestinyMod.Content.Items.Weapons.Ranged
{
	public class TwoTailedFox : Gun
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Two-Tailed Fox");
			Tooltip.SetDefault("Fires two rockets, one Void and one Solar"
				+ "\nThe Solar rocket deals damage over time, and the Void rocket disables enemies"
				+ "\n'Adorably murderous.'");
		}

		public override void DestinySetDefaults()
		{
			Item.damage = 520;
			Item.useTime = 35;
			Item.useAnimation = 35;
			Item.knockBack = 4;
			Item.value = Item.buyPrice(gold: 1);
			Item.rare = ItemRarityID.Red;
			Item.UseSound = new SoundStyle("DestinyMod/Assets/Sounds/Item/Weapons/Ranged/Gjallarhorn");
			Item.autoReuse = true;
			Item.shootSpeed = 16f;
			Item.useAmmo = AmmoID.Rocket;
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			position -= new Vector2(0, 6);
			Vector2 perturbedSpeed = velocity.RotatedByRandom(MathHelper.ToRadians(5));
			Projectile.NewProjectile(source, position, perturbedSpeed, ModContent.ProjectileType<TwoTailedVoid>(), damage, knockback, player.whoAmI);
			Vector2 otherPert = velocity.RotatedByRandom(MathHelper.ToRadians(4));
			if (otherPert == perturbedSpeed)
			{
				otherPert += velocity.RotatedByRandom(MathHelper.ToRadians(1));
			}
			Projectile.NewProjectile(source, position, otherPert, ModContent.ProjectileType<TwoTailedSolar>(), damage, knockback, player.whoAmI);
			return false;
		}

		public override Vector2? HoldoutOffset() => new Vector2(-50, -5);
	}
}