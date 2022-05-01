using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using DestinyMod.Common.Items.ItemTypes;

namespace DestinyMod.Content.Items.Weapons.Ranged
{
	public class HauntedEarth : Gun
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Increased critical strike chance while not moving"
			+ "\n'Those we've lost still linger in every place we look. Earth is no place for the living.'");
		}

		public override void DestinySetDefaults()
		{
			Item.damage = 20;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.knockBack = 4;
			Item.crit = 2;
			Item.value = Item.buyPrice(gold: 1);
			Item.rare = ItemRarityID.Orange;
			Item.UseSound = SoundLoader.GetLegacySoundSlot(Mod, "Assets/Sounds/Item/Weapons/Ranged/JadeRabbit");
			Item.shootSpeed = 300f;
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Projectile projectile = Projectile.NewProjectileDirect(source, new Vector2(position.X, position.Y - 4), velocity, type, damage, knockback, player.whoAmI);
			if (projectile.extraUpdates < 3)
			{
				projectile.extraUpdates = 3;
			}
			return false;
		}

		public override void ModifyWeaponCrit(Player player, ref float crit)
		{
			if (player.velocity == Vector2.Zero)
			{
				crit += 5;
			}
		}

		public override Vector2? HoldoutOffset() => new Vector2(-18, 2);
	}
}