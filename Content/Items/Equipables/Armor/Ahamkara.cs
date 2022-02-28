using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using DestinyMod.Common.Items.ItemTypes;

namespace DestinyMod.Content.Items.Equipables.Armor
{
	[AutoloadEquip(EquipType.Head)]
	public class Ahamkara : ExoticArmor
	{
		public override DestinyClassType ArmorClassType => DestinyClassType.Warlock;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Skull of Dire Ahamkara");
			Tooltip.SetDefault("Increases movement speed by 10%"
				+ "\n\"Reality is of the finest flesh, oh bearer mine. And are you not...hungry?\"");
		}

		public override void DestinySetDefaults()
		{
			Item.rare = ItemRarityID.Yellow;
			Item.value = Item.sellPrice(gold: 1);
			Item.defense = 20;
		}

		public override void UpdateEquip(Player player)
		{
			player.moveSpeed += 0.1f;
			base.UpdateEquip(player);
		}
	}
}