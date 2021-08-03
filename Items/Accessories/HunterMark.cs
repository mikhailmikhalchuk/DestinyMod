using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.Utilities;

namespace TheDestinyMod.Items.Accessories
{
	//[AutoloadEquip(EquipType.Waist)]
	public class HunterMark : ModItem
	{
        public override void SetStaticDefaults() {
			DisplayName.SetDefault("Mark of the Hunter");
			Tooltip.SetDefault("Equip this to change your class to Hunter");
		}

		public override void SetDefaults() {
			item.width = 20;
			item.height = 24;
			item.rare = ItemRarityID.Yellow;
			item.accessory = true;
		}

		public override void ModifyTooltips(List<TooltipLine> tooltips) {
			DestinyPlayer dPlayer = Main.LocalPlayer.GetModPlayer<DestinyPlayer>();
			if (dPlayer.titan || dPlayer.warlock) {
				tooltips.Add(new TooltipLine(mod, "HasClass", "You already have another class item equipped") { overrideColor = new Color(255, 0, 0) });
			}
		}

        public override bool CanEquipAccessory(Player player, int slot) {
			DestinyPlayer dPlayer = player.GetModPlayer<DestinyPlayer>();
			return !dPlayer.warlock && !dPlayer.titan || Main.LocalPlayer.armor[slot].type == ModContent.ItemType<WarlockMark>() || Main.LocalPlayer.armor[slot].type == ModContent.ItemType<TitanMark>();
		}

		public override void UpdateAccessory(Player player, bool hideVisual) {
			player.GetModPlayer<DestinyPlayer>().hunter = true;
		}

        public override bool? PrefixChance(int pre, UnifiedRandom rand) {
			return pre != -1;
        }

        public override bool ReforgePrice(ref int reforgePrice, ref bool canApplyDiscount) {
			reforgePrice = Item.buyPrice(0, 6, 0, 0);
            return base.ReforgePrice(ref reforgePrice, ref canApplyDiscount);
        }
    }
}