using DestinyMod.Common.Items.ItemTypes;
using DestinyMod.Common.ModPlayers;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DestinyMod.Content.Items.Equipables.Armor
{
	[AutoloadEquip(EquipType.Body)]
	public class Armamentarium : ExoticArmor
	{
		public override DestinyClassType ArmorClassType => DestinyClassType.Titan;

		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("50% increased throwing damage when your Super bar is charged" 
				+ "\n'For this, there is one remedy.'");
		}

		public override void DestinySetDefaults()
		{
			Item.value = Item.sellPrice(gold: 5);
			Item.rare = ItemRarityID.Yellow;
			Item.defense = 22;
		}

		public override void UpdateEquip(Player player)
		{
			if (player.GetModPlayer<SuperPlayer>().SuperChargeCurrent >= 100)
			{
				player.GetDamage(DamageClass.Throwing) += 0.5f;
			}

			base.UpdateEquip(player);
		}
	}
}