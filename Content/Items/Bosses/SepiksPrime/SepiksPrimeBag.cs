using DestinyMod.Common.Items.ItemTypes;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace DestinyMod.Content.Items.Bosses.SepiksPrime
{
	public class SepiksPrimeBag : TreasureBag
	{
		public override void DestinySetDefaults()
		{
			Item.rare = ItemRarityID.Orange;
		}

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
			DisplayName.SetDefault("Treasure Bag (Sepiks Prime)");

			ItemID.Sets.PreHardmodeLikeBossBag[Type] = true;
		}

        public override void OpenBossBag(Player player)
		{
			IEntitySource source = player.GetSource_OpenItem(Type);

			if (Main.rand.NextBool(7))
			{
				player.QuickSpawnItem(source, ModContent.ItemType<SepiksPrimeMask>());
			}

			switch (Main.rand.Next(4))
			{
				case 0:
					// player.QuickSpawnItem(ModContent.ItemType<Weapons.Summon.ServitorStaff>());
					break;

				default:
					player.QuickSpawnItem(source, ItemID.WaterBolt);
					break;
			}
		}
	}
}