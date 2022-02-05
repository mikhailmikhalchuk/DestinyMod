using DestinyMod.Common.Items.ItemTypes;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DestinyMod.Content.Items.Armor
{
	[AutoloadEquip(EquipType.Head)]
	public class CrownOfTempests : ExoticArmor
	{
		public override DestinyClassType ArmorClassType => DestinyClassType.Warlock;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Crown of Tempests");
			Tooltip.SetDefault("Mighty are they of the stormcloud thrones, and quick to anger");
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