using Terraria;
using DestinyMod.Common.ModPlayers;

namespace DestinyMod.Common.Items.ItemTypes
{
	public interface IItemModGranter
	{
		public int ItemModType();

		public string ItemModName();
    }
}