using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using TheDestinyMod.Items.Materials;
using TheDestinyMod.Projectiles.Ranged;

namespace TheDestinyMod.Items.Weapons.Ranged
{
	public class RedDeath : ModItem
	{
		public override void SetStaticDefaults() {
			DisplayName.AddTranslation(GameCulture.Polish, "Czerwona Śmierć");
			Tooltip.SetDefault("Three round burst\nOnly the first shot consumes ammo\nKills grant a small amount of health\n\"Vanguard policy urges Guardians to destroy this weapon on sight. It is a Guardian killer.\"");
			Tooltip.AddTranslation(GameCulture.Polish, "Seria trzech rund\nTylko pierwszy strzał używa ammunicji\n\"Polityka Straży Pierwotnej nakazuje Strażnikom zniszczenie tej broni. jest to zabójca Strażników\"");
		}

		public override void SetDefaults() {
			item.damage = 120;
			item.ranged = true;
			item.width = 40;
			item.height = 20;
			item.useTime = 4;
			item.useAnimation = 12;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.noMelee = true;
			item.knockBack = 4;
			item.value = Item.buyPrice(0, 1, 0, 0);
			item.rare = ItemRarityID.Yellow;
			item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/RedDeath");
			item.autoReuse = true;
			item.shoot = 10;
			item.shootSpeed = 16f;
			item.useAmmo = AmmoID.Bullet;
			item.scale = .75f;
			item.reuseDelay = 14;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack) {
			Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(2));
			speedX = perturbedSpeed.X;
			speedY = perturbedSpeed.Y;
			Projectile.NewProjectile(position.X, position.Y - 3, speedX, speedY, ModContent.ProjectileType<DeathBullet>(), damage, knockBack, player.whoAmI);
            return false;
		}

		public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI) {
			scale *= 0.75f;
            return true;
        }

		public override Vector2? HoldoutOffset() {
			return new Vector2(-15, -5);
		}

		public override bool ConsumeAmmo(Player player) {
			return !(player.itemAnimation < item.useAnimation - 2);
		}

		public override void AddRecipes() {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Ectoplasm, 20);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
	}
}