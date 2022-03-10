using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using DestinyMod.Common.Items.ItemTypes;

namespace DestinyMod.Content.Items.Equipables.Armor
{
	[AutoloadEquip(EquipType.Head)]
	public class HelmOfSaintXIV : ExoticArmor
	{
		public override DestinyClassType ArmorClassType => DestinyClassType.Titan;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Helm of Saint-14");
			Tooltip.SetDefault("'He walked out into the demon light. But at the end he was brighter.'");
		}

		public override void DestinySetDefaults()
		{
			Item.rare = ItemRarityID.Yellow;
			Item.value = Item.sellPrice(gold: 1);
			Item.defense = 20;
		}

		public override void UpdateEquip(Player player)
		{
			player.accRunSpeed = 6f;
			player.moveSpeed += 0.05f;
			base.UpdateEquip(player);
		}
	}
}