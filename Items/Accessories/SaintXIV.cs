using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace TheDestinyMod.Items.Accessories
{
	[AutoloadEquip(EquipType.Face)]
	public class SaintXIV : ExoticAccessory
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Helm of Saint-14");
			Tooltip.SetDefault("\"He walked out into the demon light. But at the end he was brighter\"");
		}

		public override void SetDefaults() {
			item.width = 32;
			item.height = 28;
			item.rare = ItemRarityID.Yellow;
			item.value = Item.sellPrice(gold: 1);
			item.accessory = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual) {
			player.GetModPlayer<DestinyPlayer>().exoticEquipped = true;
			player.accRunSpeed = 6f; // The player's maximum run speed with accessories
			player.moveSpeed += 0.05f; // The acceleration multiplier of the player's movement speed
		}

		public override void ModifyTooltips(List<TooltipLine> tooltips) {
			if (!Main.LocalPlayer.GetModPlayer<DestinyPlayer>().titan && DestinyConfig.Instance.restrictClassItems) {
				tooltips.Add(new TooltipLine(mod, "HasClass", "You must be a Titan to equip this") { overrideColor = new Color(255, 0, 0) });
			}
		}

		public override bool CanEquipAccessory(Player player, int slot) {
			if (DestinyConfig.Instance.restrictClassItems) {
				return Main.LocalPlayer.GetModPlayer<DestinyPlayer>().titan;
			}
			return base.CanEquipAccessory(player, slot);
		}
	}
}