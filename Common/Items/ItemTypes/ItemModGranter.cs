using Terraria;
using DestinyMod.Common.ModPlayers;

namespace DestinyMod.Common.Items.ItemTypes
{
	public abstract class ItemModGranter : DestinyModItem
	{
		public int ItemModType;

        public string ItemModName;

        // TO-DO: This should remove itself from the inventory and grant the mod.

        public override bool ItemSpace(Player player) => true;

        public override bool OnPickup(Player player)
        {
            ItemDataPlayer itemDataPlayer = player.GetModPlayer<ItemDataPlayer>();
            if (itemDataPlayer.UnlockedMods.Contains(ItemModType))
            {
                return false;
            }

            itemDataPlayer.UnlockedMods.Add(ItemModType);
            return false;
        }
    }
}