using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace TheDestinyMod.Items.Weapons.Ranged
{
	public class Thunderlord : ModItem
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("Fires bullets which create lightning on kills\nFires faster the longer this weapon is used\n\"They rest quiet on fields afar...for this is no ending, but the eye.\"");
		}

		public override void SetDefaults() {
			item.damage = 45;
			item.ranged = true;
			item.autoReuse = true;
			item.channel = true;
			item.width = 124;
			item.height = 40;
			item.useTime = 11;
			item.useAnimation = 11;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.noMelee = true;
			item.knockBack = 4;
			item.value = Item.buyPrice(0, 1, 0, 0);
			item.rare = ItemRarityID.Yellow;
			item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/Thunderlord");
			item.shoot = 10;
			item.shootSpeed = 18f;
			item.useAmmo = AmmoID.Bullet;
			item.scale = .80f;
		}

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack) {
			Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(4));
			speedX = perturbedSpeed.X;
			speedY = perturbedSpeed.Y;
			Projectile.NewProjectile(new Vector2(position.X, position.Y - 7), new Vector2(speedX, speedY), ModContent.ProjectileType<Projectiles.Ranged.ThunderlordShot>(), damage, knockBack, player.whoAmI);
			return false;
        }

        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI) {
			scale *= 0.8f;
			return true;
		}

        public override float UseTimeMultiplier(Player player) {
            return player.DestinyPlayer().thunderlordReduceUse;
        }

        public override Vector2? HoldoutOffset() {
			return new Vector2(-15, -3);
		}

		public override void AddRecipes() {
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Megashark);
			recipe.AddIngredient(ModContent.ItemType<Materials.GunsmithMaterials>(), 20);
			recipe.AddIngredient(ItemID.SoulofMight, 15);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
        }
	}
}