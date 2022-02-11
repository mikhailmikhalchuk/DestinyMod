using DestinyMod.Common.Items.ItemTypes;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DestinyMod.Content.Items.Bosses.SepiksPrime
{
	public class SepiksPrimeBag : TreasureBag
	{
		public override int BossBagNPC => ModContent.NPCType<SepiksPrime>();

		public override void DestinySetDefaults() => Item.rare = ItemRarityID.Orange;

		public override void OpenBossBag(Player player)
		{
			player.TryGettingDevArmor();

			if (Main.rand.NextBool(7))
			{
				player.QuickSpawnItem(ModContent.ItemType<SepiksPrimeMask>());
			}

			switch (Main.rand.Next(4))
			{
				case 0:
					// player.QuickSpawnItem(ModContent.ItemType<Weapons.Summon.ServitorStaff>());
					break;

				default:
					player.QuickSpawnItem(ItemID.WaterBolt);
					break;
			}
		}
	}
}