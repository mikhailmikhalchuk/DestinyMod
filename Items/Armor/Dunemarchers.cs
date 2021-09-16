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
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("Increases maximum running speed\nRunning for more than 5 seconds straight will grant the \"Linear Actuators\" buff\n\"Whether on solid rock or shifting sand dune, the inexorable Sand Eaters never slow their pace.\"");
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
			player.accRunSpeed = 5;
			DestinyPlayer dPlayer = player.GetModPlayer<DestinyPlayer>();
			if (player.velocity.X > 0f - player.accRunSpeed && player.velocity.X < 0f - ((player.accRunSpeed + player.maxRunSpeed) / 2f) && player.velocity.Y == 0f && !player.mount.Active && player.dashDelay >= 0 && player.controlLeft || player.velocity.X > ((player.accRunSpeed + player.maxRunSpeed) / 2f) && player.velocity.X < player.accRunSpeed && player.velocity.Y == 0f && !player.mount.Active && player.dashDelay >= 0 && player.controlRight) {
				dPlayer.duneRunTime++;
				dPlayer.duneKill = false;
				Main.NewText(dPlayer.duneRunTime);
				if (dPlayer.duneRunTime >= 300) {
					player.AddBuff(ModContent.BuffType<Buffs.LinearActuators>(), 10);
				}
			}
			else {
				if (dPlayer.duneKill)
					dPlayer.duneRunTime = 0;

				dPlayer.duneKill = true;
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

		public override void ModifyTooltips(List<TooltipLine> tooltips) {
			if (Main.LocalPlayer.GetModPlayer<DestinyPlayer>().classType != DestinyClassType.Titan && DestinyConfig.Instance.RestrictClassItems) {
				tooltips.Add(new TooltipLine(mod, "HasClass", "You must be a Titan to equip this") { overrideColor = new Color(255, 0, 0) });
			}
		}

		public DestinyClassType ArmorClassType() => DestinyClassType.Titan;
	}
}