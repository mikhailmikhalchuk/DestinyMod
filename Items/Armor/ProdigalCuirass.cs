using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheDestinyMod.Items.Armor
{
	[AutoloadEquip(EquipType.Body)]
	public class ProdigalCuirass : ModItem, IClassArmor
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("7% increased ranged damage");
		}

		public override void SetDefaults() {
			item.width = 20;
			item.height = 18;
			item.value = Item.sellPrice(0, 0, 60, 0);
			item.rare = ItemRarityID.Orange;
			item.defense = 9;
		}

		public override void UpdateEquip(Player player) {
			player.rangedDamage += 0.07f;
		}

		public override void AddRecipes() {
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<Materials.RelicIron>(), 30);
			recipe.AddIngredient(ModContent.ItemType<Materials.PlasteelPlating>(), 16);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

		public override void ModifyTooltips(List<TooltipLine> tooltips) => tooltips.Add(DestinyHelper.GetRestrictedClassTooltip(DestinyClassType.Titan));

		public DestinyClassType ArmorClassType() => DestinyClassType.Titan;
	}
}