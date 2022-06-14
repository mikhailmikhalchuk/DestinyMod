using Microsoft.Xna.Framework;
using DestinyMod.Common.Items.ItemTypes;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using DestinyMod.Content.Projectiles.Weapons.Ranged;

namespace DestinyMod.Content.Items.Weapons.Ranged
{
	public class TicuusDivination : Gun
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ticuu's Divination");
			Tooltip.SetDefault("Fires 3 homing arrows"
				+ "\nRight Click to fire a single, more powerful arrow"
				+ "\n'Three points, pushed through forever.'");
		}

		public override void DestinySetDefaults()
		{
			Item.damage = 65;
			Item.useTime = 30;
			Item.useAnimation = 30;
			Item.value = Item.buyPrice(gold: 1);
			Item.rare = ItemRarityID.Purple;
			Item.shootSpeed = 16f;
			Item.useAmmo = AmmoID.Arrow;
			Item.UseSound = new SoundStyle("DestinyMod/Assets/Sounds/Item/Weapons/Ranged/SacredShot");
		}

		public override bool AltFunctionUse(Player player) => true;

		public override bool CanUseItem(Player player)
		{
			Item.UseSound = new SoundStyle(player.altFunctionUse == 2 ? "DestinyMod/Assets/Sounds/Item/Weapons/Ranged/CausalityShot" : "DestinyMod/Assets/Sounds/Item/Weapons/Ranged/SacredShot");
			return base.CanUseItem(player);
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			if (player.altFunctionUse == 2)
			{
				Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<CausalityArrow>(), 110, knockback, player.whoAmI);
			}
			else
			{
				for (int i = 0; i < 3; i++)
				{
					Vector2 perturbedSpeed = velocity.RotatedByRandom(MathHelper.ToRadians(20));
					Projectile.NewProjectile(source, position, perturbedSpeed, ModContent.ProjectileType<SacredFlame>(), 35, knockback, player.whoAmI);
				}
			}
			return false;
		}

		public override Vector2? HoldoutOffset() => new Vector2(0, 2);
	}
}