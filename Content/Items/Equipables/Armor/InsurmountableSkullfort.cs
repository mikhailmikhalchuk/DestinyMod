using DestinyMod.Common.Items.ItemTypes;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DestinyMod.Content.Items.Equipables.Armor
{
	[AutoloadEquip(EquipType.Head)]
	public class InsurmountableSkullfort : ExoticArmor
	{
		public override DestinyClassType ArmorClassType => DestinyClassType.Titan;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("An Insurmountable Skullfort");
			Tooltip.SetDefault("'BRAINVAULT Sigma-ACTIUM-IX Cranial Dreadnought (Invincible Type)'");
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