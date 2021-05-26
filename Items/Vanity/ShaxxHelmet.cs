using Terraria.ModLoader;
using Terraria.ID;

namespace TheDestinyMod.Items.Vanity
{
	[AutoloadEquip(EquipType.Head)]
	public class ShaxxHelmet : ModItem
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Shaxx's Helmet");
		}

		public override void SetDefaults() {
			item.width = 24;
			item.height = 22;
			item.rare = ItemRarityID.Blue;
			item.vanity = true;
		}

		public override bool DrawHead() {
			return false;
		}
	}
}