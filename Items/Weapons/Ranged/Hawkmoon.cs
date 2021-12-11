using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using TheDestinyMod.Projectiles.Ranged;
using TheDestinyMod.Items.Materials;
using TheDestinyMod.Buffs;

namespace TheDestinyMod.Items.Weapons.Ranged
{
	public class Hawkmoon : ModItem
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("Kills with this weapon stack one second of Paracausal Charge\n\"Stalk thy prey and let loose thy talons upon the Darkness.\"");
		}

		public override void SetDefaults() {
			item.damage = 78;
			item.ranged = true;
			item.noMelee = true;
			item.rare = ItemRarityID.LightRed;
			item.knockBack = 0;
			item.width = 50;
			item.height = 25;
			item.useTime = 20;
			item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/Hawkmoon");
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.shootSpeed = 10f;
			item.useAnimation = 20;
			item.shoot = 10;
			item.useAmmo = AmmoID.Bullet;
			item.value = Item.buyPrice(0, 1, 0, 0);
			item.scale = 0.8f;
		}

		public override void AddRecipes() {
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<GunsmithMaterials>(), 50);
			recipe.AddIngredient(ItemID.LunarBar, 5);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack) {
            if (player.DestinyPlayer().paracausalCharge) {
                Projectile.NewProjectile(position.X, position.Y - 6, speedX, speedY, ModContent.ProjectileType<HawkBullet>(), damage*2, knockBack, player.whoAmI);
                return false;
            }
			Projectile.NewProjectile(position.X, position.Y - 6, speedX, speedY, ModContent.ProjectileType<HawkBullet>(), damage, knockBack, player.whoAmI);
            return false;
        }

		public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI) {
			scale *= 0.8f;
            return true;
        }

		public override Vector2? HoldoutOffset() {
			return new Vector2(5, 3);
		}
	}
}