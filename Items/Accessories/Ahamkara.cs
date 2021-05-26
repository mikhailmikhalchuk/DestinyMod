using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheDestinyMod.Items.Accessories
{
	[AutoloadEquip(EquipType.Face)]
	public class Ahamkara : ExoticAccessory
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Skull of Dire Ahamkara");
			Tooltip.SetDefault("Increases movement speed by 10%\n\"Reality is of the finest flesh, oh bearer mine. And are you not...hungry?\"");
		}

		public override void SetDefaults() {
			item.width = 22;
			item.height = 20;
			item.rare = ItemRarityID.Yellow;
			item.value = Item.sellPrice(gold: 1);
			item.accessory = true;
		}

        public override void UpdateAccessory(Player player, bool hideVisual) {
			player.GetModPlayer<DestinyPlayer>().exoticEquipped = true;
			player.moveSpeed += 0.1f;
		}

        public override void ModifyTooltips(List<TooltipLine> tooltips) {
			if (!Main.LocalPlayer.GetModPlayer<DestinyPlayer>().warlock && DestinyConfig.Instance.restrictClassItems) {
				tooltips.Add(new TooltipLine(mod, "HasClass", "You must be a Warlock to equip this") { overrideColor = new Color(255, 0, 0) });
			}
		}

        public override bool CanEquipAccessory(Player player, int slot) {
			if (DestinyConfig.Instance.restrictClassItems) {
				return Main.LocalPlayer.GetModPlayer<DestinyPlayer>().warlock;
			}
			return base.CanEquipAccessory(player, slot);
		}
    }
}