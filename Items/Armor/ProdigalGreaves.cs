using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheDestinyMod.Items.Armor
{
	[AutoloadEquip(EquipType.Legs)]
	public class ProdigalGreaves : ModItem, IClassArmor
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("5% increased ranged damage");
		}

		public override void SetDefaults() {
			item.width = 20;
			item.height = 18;
			item.value = Item.sellPrice(0, 0, 60, 0);
			item.rare = ItemRarityID.Orange;
			item.defense = 8;
		}

		public override void UpdateEquip(Player player) {
			player.rangedDamage += 0.06f;
		}

        public override void AddRecipes() {
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<Materials.RelicIron>(), 20);
			recipe.AddIngredient(ModContent.ItemType<Materials.PlasteelPlating>(), 10);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

		public int ArmorType() {
			return 0;
		}
	}
}