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

        public override GlobalItem Clone(Item item, Item itemClone)
        {
            return base.Clone(item, itemClone);
        }

        public override void SetDefaults(Item item)
		{
			if (item.ModItem is DestinyModItem destinyModItem)
			{
				ItemReuse = destinyModItem.DestinyModReuseDelay;
			}
		}

		public override void UseAnimation(Item item, Player player)
		{
			if (item.useAnimation == 1)
			{
				player.GetModPlayer<StatsPlayer>().ItemReuse += ItemReuse;
			}
		}

		public override bool CanUseItem(Item item, Player player) => player.GetModPlayer<StatsPlayer>().ItemReuse <= 0;
	}
}