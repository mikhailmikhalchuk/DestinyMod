using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheDestinyMod.Items.Armor
{
	[AutoloadEquip(EquipType.Head)]
	public class CrownOfTempests : ExoticArmor
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Crown of Tempests");
			Tooltip.SetDefault("\"Mighty are they of the stormcloud thrones, and quick to anger\"");
		}

		public override void SetDefaults() {
			item.width = 22;
			item.height = 22;
			item.rare = ItemRarityID.Yellow;
			item.value = Item.sellPrice(gold: 1);
			item.defense = 20;
		}

		public override void UpdateAccessory(Player player, bool hideVisual) {
			player.GetModPlayer<DestinyPlayer>().exoticEquipped = true;
			player.accRunSpeed = 6f; // The player's maximum run speed with accessories
			player.moveSpeed += 0.05f; // The acceleration multiplier of the player's movement speed
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