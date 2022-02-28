using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DestinyMod.Common.GlobalItems
{
	public class VanillaItemAdjustments : GlobalItem
	{
		public override void SetDefaults(Item item)
		{
			switch (item.type)
			{
				case ItemID.Grenade:
				case ItemID.BouncyGrenade:
				case ItemID.StickyGrenade:
				case ItemID.PartyGirlGrenade:
					item.ammo = ItemID.Grenade;
					break;
			}
		}
	}
}