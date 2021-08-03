using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheDestinyMod.Items.Armor
{
	[AutoloadEquip(EquipType.Head)]
	public class InsurmountableSkullfort : ExoticArmor, IClassArmor
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("An Insurmountable Skullfort");
			Tooltip.SetDefault("\"BRAINVAULT Sigma-ACTIUM-IX Cranial Dreadnought (Invincible Type)\"");
		}

		public override void SetDefaults() {
			item.width = 26;
			item.height = 28;
			item.rare = ItemRarityID.Yellow;
			item.value = Item.sellPrice(gold: 1);
			item.defense = 20;
		}

		public override void UpdateEquip(Player player) {
			player.GetModPlayer<DestinyPlayer>().exoticEquipped = true;
			player.accRunSpeed = 6f; // The player's maximum run speed with accessories
			player.moveSpeed += 0.05f; // The acceleration multiplier of the player's movement speed
		}

		public override void ModifyTooltips(List<TooltipLine> tooltips) {
			if (!Main.LocalPlayer.GetModPlayer<DestinyPlayer>().titan && DestinyConfig.Instance.restrictClassItems) {
				tooltips.Add(new TooltipLine(mod, "HasClass", "You must be a Titan to equip this") { overrideColor = new Color(255, 0, 0) });
			}
		}

		public int ArmorType() {
			return 0;
		}
	}
}