using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using DestinyMod.Common.Items.ItemTypes;

namespace DestinyMod.Content.Items.Weapons.Ranged
{
	public class BottomDollar : Gun
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Scales with world progression"
			+ "\n'Never count yourself out.'");
		}

		public override void DestinySetDefaults()
		{
			Item.damage = 15;
			Item.crit = 5;
			Item.rare = ItemRarityID.Green;
			Item.knockBack = 0;
			Item.useTime = 20;
			Item.UseSound = SoundLoader.GetLegacySoundSlot(Mod, "Assets/Sounds/Item/Weapons/Ranged/HandCannon120");
			Item.useAnimation = 20;
			Item.value = Item.buyPrice(gold: 1);
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Projectile.NewProjectile(source, new Vector2(position.X, position.Y - 5), velocity, type, damage, knockback, player.whoAmI);
			return false;
		}

		public override void ModifyWeaponDamage(Player player, ref StatModifier damage, ref float flat)
		{
			if (Main.hardMode)
			{
				flat += 25;
			}
		}

		public override void ModifyWeaponCrit(Player player, ref int crit)
		{
			if (Main.hardMode)
			{
				crit += 8;
			}
		}

		public override Vector2? HoldoutOffset() => new Vector2(7, -2);
	}
}