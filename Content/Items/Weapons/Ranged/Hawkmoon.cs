using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terraria.Audio;
using DestinyMod.Common.Items.ItemTypes;
using DestinyMod.Content.Items.Materials;
using DestinyMod.Content.Buffs;
using DestinyMod.Content.Projectiles.Weapons.Ranged;

namespace DestinyMod.Content.Items.Weapons.Ranged
{
	public class Hawkmoon : Gun
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Kills with this weapon stack one second of Paracausal Charge"
			+ "\n'Stalk thy prey and let loose thy talons upon the Darkness.'");
		}

		public override void DestinySetDefaults()
		{
			Item.damage = 78;
			Item.rare = ItemRarityID.LightRed;
			Item.knockBack = 0;
			Item.useTime = 20;
			Item.UseSound = new SoundStyle("DestinyMod/Assets/Sounds/Item/Weapons/Ranged/Hawkmoon");
			Item.useAnimation = 20;
			Item.value = Item.buyPrice(gold: 1);
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			if (player.HasBuff(ModContent.BuffType<ParacausalCharge>()))
			{
				damage *= 2;
			}
			Projectile.NewProjectile(source, new Vector2(position.X, position.Y - 6), velocity, ModContent.ProjectileType<HawkBullet>(), damage, knockback, player.whoAmI);
			return false;
		}

        public override Vector2? HoldoutOffset() => new Vector2(5, 3);

		public override void AddRecipes() => CreateRecipe(1)
			.AddIngredient(ModContent.ItemType<GunsmithMaterials>(), 50)
			.AddIngredient(ItemID.LunarBar, 5)
			.AddTile(TileID.LunarCraftingStation)
			.Register();

	}
}