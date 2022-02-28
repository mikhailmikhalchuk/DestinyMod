using Terraria.ModLoader;
using Terraria.ID;
using DestinyMod.Common.Items;

namespace DestinyMod.Content.Items.Equipables.Vanity
{
	[AutoloadEquip(EquipType.Head)]
	public class ShaxxHelmet : DestinyModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Shaxx's Helmet");
			ArmorIDs.Head.Sets.DrawHead[Item.headSlot] = false;
		}

		public override void DestinySetDefaults()
		{
			Item.rare = ItemRarityID.Blue;
			Item.vanity = true;
		}
	}
}