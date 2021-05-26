using Terraria.ModLoader;
using Terraria.ID;

namespace TheDestinyMod.Items.Vanity
{
	[AutoloadEquip(EquipType.Head)]
	public class SepiksPrimeMask : ModItem
	{
		public override void SetDefaults() {
			item.width = 34;
			item.height = 28;
			item.rare = ItemRarityID.Blue;
			item.vanity = true;
		}

		public override bool DrawHead() {
			return false;
		}
	}
}