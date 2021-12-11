using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace TheDestinyMod.Items.Armor
{
	[AutoloadEquip(EquipType.Head)]
	public class HelmOfSaintXIV : ExoticArmor, IClassArmor
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
			item.defense = 20;
		}

		public override void UpdateEquip(Player player) {
			player.DestinyPlayer().exoticEquipped = true;
			player.accRunSpeed = 6f; // The player's maximum run speed with accessories
			player.moveSpeed += 0.05f; // The acceleration multiplier of the player's movement speed
		}

		public override void ModifyTooltips(List<TooltipLine> tooltips) => tooltips.Add(DestinyHelper.GetRestrictedClassTooltip(DestinyClassType.Titan));

		public DestinyClassType ArmorClassType() => DestinyClassType.Titan;
	}
}