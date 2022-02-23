using DestinyMod.Common.Items;
using DestinyMod.Common.ModPlayers;
using Terraria;
using Terraria.ModLoader;

namespace DestinyMod.Common.GlobalItems
{
	public class ReuseDelayItem : GlobalItem
	{
		public int ItemReuse = 0;

        public override bool InstancePerEntity => true;

		public override void UseAnimation(Item item, Player player)
		{
			int itemReuse = 0;
			if (item.ModItem is DestinyModItem destinyModItem)
			{
				itemReuse = destinyModItem.DestinyModReuseDelay;
			}
			player.GetModPlayer<StatsPlayer>().ItemReuse = ItemReuse + itemReuse + player.itemAnimation + 1;
		}

		public override bool CanUseItem(Item item, Player player) => player.GetModPlayer<StatsPlayer>().ItemReuse <= 0;
	}
}