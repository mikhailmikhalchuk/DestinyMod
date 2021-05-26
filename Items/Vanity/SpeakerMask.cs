using Terraria.ModLoader;
using Terraria.ID;

namespace TheDestinyMod.Items.Vanity
{
	[AutoloadEquip(EquipType.Head)]
	public class SpeakerMask : ModItem
	{
        public override void SetStaticDefaults() {
			DisplayName.SetDefault("Speaker's Mask");
        }

        public override void SetDefaults() {
			item.width = 18;
			item.height = 22;
			item.rare = ItemRarityID.Blue;
			item.vanity = true;
		}

		public override bool DrawHead() {
			return false;
		}
	}
}