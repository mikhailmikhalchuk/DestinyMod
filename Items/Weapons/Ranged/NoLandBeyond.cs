using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using TheDestinyMod.Items.Materials;

namespace TheDestinyMod.Items.Weapons.Ranged
{
	public class NoLandBeyond : ModItem
	{
		public override void SetStaticDefaults() {
			DisplayName.AddTranslation(GameCulture.Polish, "Żadnej ziemi poza nią");
			Tooltip.SetDefault("\"Every hit blazes the path to our reclamation.\"");
			Tooltip.AddTranslation(GameCulture.Polish, "\"Każde trafienie przeciera drogę do naszego uzdrowienia\"");
		}

		public override void SetDefaults() {
			item.damage = 300;
			item.ranged = true;
			item.noMelee = true;
			item.rare = ItemRarityID.Yellow;
			item.knockBack = 0;
			item.width = 108;
			item.height = 28;
			item.useTime = 100;
			item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/NoLandBeyond");
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.shootSpeed = 16f;
			item.useAnimation = 100;
			item.shoot = 10;
			item.useAmmo = AmmoID.Bullet;
			item.value = Item.buyPrice(0, 1, 0, 0);
		}

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack) {
			position.Y -= 2;
			player.GetModPlayer<DestinyPlayer>().destinyWeaponDelay = 20;
			return true;
        }

		public override bool CanUseItem(Player player) {
			if (player.GetModPlayer<DestinyPlayer>().destinyWeaponDelay > 0) {
				return false;
			}
			return base.CanUseItem(player);
		}

		public override void AddRecipes() {
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Ectoplasm, 20);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

		public override Vector2? HoldoutOffset() {
			return new Vector2(-10, 0);
		}
	}
}