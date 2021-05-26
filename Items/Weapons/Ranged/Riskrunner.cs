using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using TheDestinyMod.Items.Materials;

namespace TheDestinyMod.Items.Weapons.Ranged
{
	public class Riskrunner : ModItem
	{
		public override void SetStaticDefaults() {
			DisplayName.AddTranslation(GameCulture.Polish, "Ryzykant");
			Tooltip.SetDefault("33% chance to not consume ammo\nReplaces regular bullets with nano bullets\n\"Charge your soul and let the electrons sing.\"");
			Tooltip.AddTranslation(GameCulture.Polish, "33% na nie zużycie ammunicji\nZmienia zwykłe naboje na nano pociski\n\"Napełnia twoją duszę i pozwala elektroną śpiewać\"");
		}

		public override void SetDefaults() {
			item.damage = 45;
			item.ranged = true;
			item.width = 76;
			item.height = 40;
			item.useTime = 6;
			item.useAnimation = 6;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.noMelee = true;
			item.knockBack = 4;
			item.value = Item.buyPrice(0, 1, 0, 0);
			item.rare = ItemRarityID.Yellow;
			item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/Riskrunner");
			item.autoReuse = true;
			item.shoot = 10;
			item.shootSpeed = 16f;
			item.useAmmo = AmmoID.Bullet;
			item.scale = .80f;
		}
		
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack) {
			Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(5));
			speedX = perturbedSpeed.X;
			speedY = perturbedSpeed.Y;
			Projectile.NewProjectile(position.X, position.Y - 1, speedX, speedY, ProjectileID.NanoBullet, damage, knockBack, player.whoAmI);
            return false;
		}

		public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI) {
			scale *= 0.8f;
            return true;
        }

		public override bool ConsumeAmmo(Player player) {
			return Main.rand.NextFloat() >= .33f;
		}

		public override Vector2? HoldoutOffset() {
			return new Vector2(-20, 1);
		}

		/*public override void AddRecipes() {
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Ectoplasm, 20);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
        }*/
	}
}