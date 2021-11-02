using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheDestinyMod.Items.Armor
{
	[AutoloadEquip(EquipType.Legs)]
	public class Dunemarchers : ModItem, IClassArmor
	{
		private bool duneKill;

		public override void SetStaticDefaults() {
			Tooltip.SetDefault("Increases maximum running speed\nRunning for more than 5 seconds straight will grant the \"Linear Actuators\" buff\n\"Whether on solid rock or shifting sand dune, the inexorable Sand Eaters never slow their pace.\"");
		}

		public override void SetDefaults() {
			item.width = 22;
			item.height = 18;
			item.value = Item.sellPrice(0, 0, 60, 0);
			item.rare = ItemRarityID.Yellow;
			item.defense = 8;
		}

		public override void UpdateEquip(Player player) {
			player.accRunSpeed = 5f;
			DestinyPlayer dPlayer = player.GetModPlayer<DestinyPlayer>();
			if ((player.velocity.X > 0f - player.accRunSpeed && player.velocity.X < 0f - ((player.accRunSpeed + player.maxRunSpeed) / 2f) || player.velocity.X > ((player.accRunSpeed + player.maxRunSpeed) / 2f) && player.velocity.X < player.accRunSpeed) && player.velocity.Y == 0f && !player.mount.Active && player.dashDelay >= 0) {
				dPlayer.duneRunTime++;
				duneKill = false;
				if (dPlayer.duneRunTime >= 300) {
					player.AddBuff(ModContent.BuffType<Buffs.LinearActuators>(), 100);
				}
			}
			else {
				if (duneKill)
					dPlayer.duneRunTime = 0;

				duneKill = true;
			}
		}

		public override void AddRecipes() {
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<Materials.RelicIron>(), 20);
			recipe.AddIngredient(ModContent.ItemType<Materials.PlasteelPlating>(), 10);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

		public override void ModifyTooltips(List<TooltipLine> tooltips) => tooltips.Add(DestinyHelper.GetRestrictedClassTooltip(DestinyClassType.Titan));

		public DestinyClassType ArmorClassType() => DestinyClassType.Titan;
	}
}