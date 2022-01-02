using Terraria.ModLoader;
using Terraria.ID;
using DestinyMod.Common.Items;

namespace DestinyMod.Content.Items.Bossess.SepkisPrime
{
	[AutoloadEquip(EquipType.Head)]
	public class SepiksPrimeMask : DestinyModItem
	{
		public override void SetStaticDefaults() => ArmorIDs.Head.Sets.DrawHead[Item.headSlot] = false;

		public override void DestinySetDefaults()
		{
			Item.rare = ItemRarityID.Blue;
			Item.vanity = true;
		}
	}
}