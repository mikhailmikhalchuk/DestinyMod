using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using TheDestinyMod.Projectiles.Ammo;
using TheDestinyMod.Items.Materials;

namespace TheDestinyMod.Items.Weapons.Ranged
{
	public class VexMythoclast : ModItem
	{
        public override void SetStaticDefaults() {
			DisplayName.AddTranslation(GameCulture.Spanish, "Mitocrasta Vex");
			Tooltip.SetDefault("\"...a causal loop within the weapon's mechanism, suggesting that the firing process somehow binds space and time into...\"");
        }

        public override void SetDefaults() {
			item.damage = 84;
			item.ranged = true;
			item.noMelee = true;
			item.autoReuse = true;
			item.rare = ItemRarityID.Red;
			item.knockBack = 0;
			item.width = 104;
			item.height = 46;
			item.useTime = 15;
			item.crit = 10;
			item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/AceOfSpades");
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.shootSpeed = 10f;
			item.useAnimation = 15;
			item.shoot = 10;
			item.useAmmo = AmmoID.Bullet;
			item.value = Item.buyPrice(0, 1, 0, 0);
			item.scale = 0.7f;
		}

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack) {
			Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 30f;
			if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0)) {
				position += muzzleOffset;
			}
            Projectile.NewProjectile(position.X, position.Y - 2, speedX, speedY, type, damage, knockBack, player.whoAmI);
			return false;
        }

		public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI) {
			scale *= 0.7f;
            return true;
        }

		public override Vector2? HoldoutOffset() {
			return new Vector2(-3, -2);
		}

		public override void AddRecipes() {
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<Placeables.MicrophasicDatalattice>(), 50);
			recipe.AddIngredient(ItemID.ChlorophyteBar, 10);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}