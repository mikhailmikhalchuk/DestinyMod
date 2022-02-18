using DestinyMod.Common.Items;
using Terraria;
using Terraria.ModLoader;

namespace DestinyMod.Common.ModPlayers
{
	public class StatsPlayer : ModPlayer
	{
		public int ItemReuse = 0;

		public int ChannelTime = 0;

		public bool DestinyChannel = false;

		public int DestinyChannelTime = 0;

		public override void ResetEffects()
		{
			ItemReuse--;

			if (Player.channel)
			{
				ChannelTime++;
			}
			else
			{
				ChannelTime = 0;
			}

			if (DestinyChannel)
			{
				DestinyChannelTime++;
			}
			else
			{
				DestinyChannelTime = 0;
			}

			DestinyChannel = false;
		}

		public override void PostUpdate()
		{
			Item heldItem = Main.mouseItem.IsAir ? Player.inventory[Player.selectedItem] : Main.mouseItem;
			if (heldItem != null && !heldItem.IsAir && (Main.mouseLeft || Main.mouseRight) && heldItem.ModItem is DestinyModItem heldDestinyModItem && heldDestinyModItem.DestinyModChannel)
			{
				DestinyChannel = true;
			}
		}
	}
}