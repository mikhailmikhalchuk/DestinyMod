using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheDestinyMod.Items.Armor
{
	[AutoloadEquip(EquipType.Body)]
	public class Armamentarium : ExoticArmor, IClassArmor
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("50% increased throwing damage when your Super bar is charged\n\"For this, there is one remedy\"");
		}

        public override void SetDefaults() {
			item.width = 28;
			item.height = 22;
			item.value = Item.sellPrice(0, 5, 0, 0);
			item.rare = ItemRarityID.Yellow;
			item.defense = 22;
		}

		public override void UpdateEquip(Player player) {
			player.GetModPlayer<DestinyPlayer>().exoticEquipped = true;
			if (player.GetModPlayer<DestinyPlayer>().superChargeCurrent >= 100) {
				player.thrownDamage += 0.5f;
			}
		}

		public override void ModifyTooltips(List<TooltipLine> tooltips) {
			if (!Main.LocalPlayer.GetModPlayer<DestinyPlayer>().titan && DestinyConfig.Instance.restrictClassItems) {
				tooltips.Add(new TooltipLine(mod, "HasClass", "You must be a Titan to equip this") { overrideColor = new Color(255, 0, 0) });
			}
		}

		public DestinyClassType ArmorClassType() => DestinyClassType.Titan;
	}
}