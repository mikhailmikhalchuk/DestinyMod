using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace TheDestinyMod.Items.Accessories
{
	//[AutoloadEquip(EquipType.Waist)]
	public class WarlockMark : ModItem
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Mark of the Warlock");
			Tooltip.SetDefault("Equip this to change your class to Warlock");
		}

		public override void SetDefaults() {
			item.width = 34;
			item.height = 20;
			item.rare = ItemRarityID.Yellow;
			item.value = Item.sellPrice(gold: 1);
			item.accessory = true;
		}

		public override void ModifyTooltips(List<TooltipLine> tooltips) {
			DestinyPlayer dPlayer = Main.LocalPlayer.GetModPlayer<DestinyPlayer>();
			if (dPlayer.hunter || dPlayer.titan) {
				tooltips.Add(new TooltipLine(mod, "HasClass", "You already have another class item equipped") { overrideColor = new Color(255, 0, 0) });
			}
		}

		public override bool CanEquipAccessory(Player player, int slot) {
			DestinyPlayer dPlayer = player.GetModPlayer<DestinyPlayer>();
			return !dPlayer.titan && !dPlayer.hunter || Main.LocalPlayer.armor[slot].type == ModContent.ItemType<HunterMark>() || Main.LocalPlayer.armor[slot].type == ModContent.ItemType<TitanMark>();
		}

		public override void UpdateAccessory(Player player, bool hideVisual) {
			DestinyPlayer dPlayer = player.GetModPlayer<DestinyPlayer>();
			dPlayer.warlock = true;
		}
	}
}