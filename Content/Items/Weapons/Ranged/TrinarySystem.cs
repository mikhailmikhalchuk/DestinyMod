using Microsoft.Xna.Framework;
using DestinyMod.Common.Items.ItemTypes;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using DestinyMod.Common.Projectiles;

namespace DestinyMod.Content.Items.Weapons.Ranged
{
	public class TrinarySystem : Gun
	{
		public override void SetStaticDefaults() => Tooltip.SetDefault("Hold down the trigger to fire"
			+ "\nScales with world progression"
			+ "\n'The mathematics are quite complicated.'");

		public override void DestinySetDefaults()
		{
			Item.damage = 5;
			Item.crit = 1;
			Item.channel = true;
			Item.rare = ItemRarityID.Green;
			Item.knockBack = 0;
			Item.useTime = 15;
			Item.useAnimation = 15;
			Item.value = Item.buyPrice(gold: 1);
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			DestinyModProjectile.FireFusionProjectile(player, source, new Vector2(position.X, position.Y - 2), velocity, damage, knockback, 7, type);
			return false;
		}

		public override void ModifyWeaponDamage(Player player, ref StatModifier damage, ref float flat)
		{
			if (Main.hardMode)
			{
				flat += 20;
			}

			if (NPC.downedMechBossAny)
			{
				flat += 10;
			}

			if (NPC.downedPlantBoss)
			{
				flat += 10;
			}
		}

		public override void ModifyWeaponCrit(Player player, ref int crit)
		{
			if (Main.hardMode)
			{
				crit += 3;
			}

			if (NPC.downedMechBossAny)
			{
				crit += 5;
			}

			if (NPC.downedPlantBoss)
			{
				crit += 7;
			}
		}

		public override Vector2? HoldoutOffset() => new Vector2(-1, 0);
	}
}