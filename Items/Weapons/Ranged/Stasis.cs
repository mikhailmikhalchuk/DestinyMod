using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheDestinyMod.Projectiles.Ammo;

namespace TheDestinyMod.Items.Weapons.Ranged
{
	public class Stasis : ModItem
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("Freezes enemy players");
		}

		public override void SetDefaults() {
            item.damage = 10;
			item.magic = true;
			item.noMelee = true;
            item.channel = true;
            item.mana = 100;
			item.rare = ItemRarityID.Red;
			item.knockBack = 0;
			item.width = 30;
			item.height = 30;
			item.useTime = 5;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.useAnimation = 5;
            item.shoot = ModContent.ProjectileType<StasisShard>();
            item.shootSpeed = 10f;
			item.value = Item.buyPrice(0, 1, 0, 0);
		}

		public override void AddRecipes() {
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.FrostCore, 10);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}