using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheDestinyMod.Items
{
    public class ExoticCipher : ModItem
    {
        public override void SetDefaults() {
			item.height = 32;
			item.width = 32;
			item.maxStack = 30;
			item.rare = ItemRarityID.Yellow;
			item.value = Item.buyPrice(0, 10, 0, 0);
        }
    }
}