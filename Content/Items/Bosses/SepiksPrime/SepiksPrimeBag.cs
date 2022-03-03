using DestinyMod.Common.Items.ItemTypes;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DestinyMod.Content.Items.Bosses.SepiksPrime
{
	public class SepiksPrimeBag : TreasureBag
	{
		public override void DestinySetDefaults() => Item.rare = ItemRarityID.Orange;

		public override void OpenBossBag(Player player)
		{
			player.TryGettingDevArmor(player.GetItemSource_OpenItem(Type));

			if (Main.rand.NextBool(7))
			{
				player.QuickSpawnItem(player.GetItemSource_OpenItem(ModContent.ItemType<SepiksPrimeMask>()), ModContent.ItemType<SepiksPrimeMask>());
			}

			switch (Main.rand.Next(4))
			{
				case 0:
					// player.QuickSpawnItem(ModContent.ItemType<Weapons.Summon.ServitorStaff>());
					break;

				default:
					player.QuickSpawnItem(player.GetItemSource_OpenItem(ItemID.WaterBolt), ItemID.WaterBolt);
					break;
			}
		}
	}
}