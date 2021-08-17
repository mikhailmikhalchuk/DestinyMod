using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheDestinyMod.Items.Armor
{
	[AutoloadEquip(EquipType.Head)]
	public class CrownOfTempests : ExoticArmor, IClassArmor
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

		public override void UpdateEquip(Player player) {
			player.GetModPlayer<DestinyPlayer>().exoticEquipped = true;
			player.accRunSpeed = 6f;
			player.moveSpeed += 0.05f;
		}

		public override void ModifyTooltips(List<TooltipLine> tooltips) {
			if (Main.LocalPlayer.GetModPlayer<DestinyPlayer>().classType != DestinyClassType.Warlock && DestinyConfig.Instance.RestrictClassItems) {
				tooltips.Add(new TooltipLine(mod, "HasClass", "You must be a Warlock to equip this") { overrideColor = new Color(255, 0, 0) });
			}
		}

		public DestinyClassType ArmorClassType() => DestinyClassType.Warlock;
	}
}