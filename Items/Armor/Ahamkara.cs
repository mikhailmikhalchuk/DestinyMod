using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheDestinyMod.Items.Armor
{
	[AutoloadEquip(EquipType.Head)]
	public class Ahamkara : ExoticArmor, IClassArmor
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
			item.defense = 20;
		}

        public override void UpdateEquip(Player player) {
			player.GetModPlayer<DestinyPlayer>().exoticEquipped = true;
			player.moveSpeed += 0.1f;
		}

        public override void ModifyTooltips(List<TooltipLine> tooltips) {
			if (!Main.LocalPlayer.GetModPlayer<DestinyPlayer>().warlock && DestinyConfig.Instance.restrictClassItems) {
				tooltips.Add(new TooltipLine(mod, "HasClass", "You must be a Warlock to equip this") { overrideColor = new Color(255, 0, 0) });
			}
		}

		public int ArmorType() {
			return 2;
		}
	}
}