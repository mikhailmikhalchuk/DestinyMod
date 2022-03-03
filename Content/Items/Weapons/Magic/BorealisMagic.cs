using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.DataStructures;
using DestinyMod.Content.Projectiles.Weapons.Misc;
using DestinyMod.Content.Items.Weapons.Ranged;
using DestinyMod.Common.Items;

namespace DestinyMod.Content.Items.Weapons.Magic
{
	public class BorealisMagic : DestinyModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Borealis");
			Tooltip.SetDefault("Right click while holding this weapon to cycle between damage types"
				+ "\n\"Light is a spectrum. Why limit yourself to a single hue?\"");
		}

		public override void DestinySetDefaults()
		{
			Item.damage = 195;
			Item.DamageType = DamageClass.Magic;
			Item.useTime = 40;
			Item.useAnimation = 40;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true;
			Item.knockBack = 4;
			Item.value = Item.buyPrice(gold: 1);
			Item.rare = ItemRarityID.Pink;
			Item.UseSound = SoundLoader.GetLegacySoundSlot(Mod, "Sounds/Item/BorealisMagic");
			Item.shoot = ProjectileID.PurificationPowder;
			Item.shootSpeed = 300f;
			Item.useAmmo = AmmoID.Bullet;
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Projectile.NewProjectile(source, new Vector2(position.X, position.Y - 5), velocity, ModContent.ProjectileType<BorealisProjectile>(), damage, knockback, player.whoAmI, 1, type);
			return false;
		}

		public override bool CanUseItem(Player player)
		{
			if (player.altFunctionUse == 2 && player.itemTime <= 0)
			{
				player.itemTime = player.itemTimeMax;

				for (int i = 0; i < Main.InventorySlotsTotal; i++)
				{
					Item item = player.inventory[i];
					if (item == Item)
					{
						int prefix = Item.prefix;
						item.SetDefaults(ModContent.ItemType<BorealisRanged>());
						item.Prefix(prefix);
						SoundEngine.PlaySound(SoundID.Item101);
						break;
					}
				}
			}
			return player.altFunctionUse != 2;
		}

		public override bool AltFunctionUse(Player player) => true;

		public override Vector2? HoldoutOffset() => new Vector2(-15, -2);
	}
}