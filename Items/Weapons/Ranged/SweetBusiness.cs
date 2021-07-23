using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheDestinyMod.Items.Materials;
using Terraria.Localization;

namespace TheDestinyMod.Items.Weapons.Ranged
{
	public class SweetBusiness : ModItem
	{
		public override void SetStaticDefaults() {
			DisplayName.AddTranslation(GameCulture.Polish, "Słodki Biznes");
			Tooltip.SetDefault("10% chance to not consume ammo\nFires faster the longer this weapon is used\n\"...I love my job.\"");
			Tooltip.AddTranslation(GameCulture.Polish, "10% na nie zużycie ammunicji\nStrzela szybciej im dłużej ta broń jest używana\n\"...Kocham moją robote\"");
		}

		public override void SetDefaults() {
			item.damage = 18;
			item.ranged = true;
			item.noMelee = true;
			item.autoReuse = true;
			item.channel = true;
			item.rare = ItemRarityID.Red;
			item.knockBack = 0;
			item.width = 140;
			item.height = 34;
			item.useTime = 5;
			item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/SweetBusiness");
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.shootSpeed = 20f;
			item.useAnimation = 5;
			item.shoot = 10;
			item.useAmmo = AmmoID.Bullet;
			item.value = Item.buyPrice(0, 1, 0, 0);
			item.scale = 0.6f;
		}

		public override void AddRecipes() {
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Megashark);
			recipe.AddIngredient(ItemID.FragmentVortex, 15);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

		public override bool ConsumeAmmo(Player player) {
			return Main.rand.NextFloat() >= .10f;
		}

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack) {
			if (player.GetModPlayer<DestinyPlayer>().businessReduceUse < 1.3f) {
				player.GetModPlayer<DestinyPlayer>().businessReduceUse += 0.05f;
			}
            Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(8));
			speedX = perturbedSpeed.X;
			speedY = perturbedSpeed.Y;
			position.Y -= 5;
			return true;
        }

        public override float UseTimeMultiplier(Player player) {
            return player.GetModPlayer<DestinyPlayer>().businessReduceUse;
        }

        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI) {
			scale *= 0.6f;
            return true;
        }

		public override Vector2? HoldoutOffset() {
			return new Vector2(-15, -3);
		}
	}
}