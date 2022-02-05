using DestinyMod.Common.Items;
using Terraria;
using Terraria.ID;

namespace DestinyMod.Content.Items.Misc
{
    public class ExoticCipher : DestinyModItem
    {
        public override void SetStaticDefaults() => Tooltip.SetDefault("Historial data preserved as luminous matter"
                + "\nA certain agent might like this item");

        public override void DestinySetDefaults()
        {
            Item.maxStack = 30;
            Item.rare = ItemRarityID.Yellow;
            Item.value = Item.buyPrice(gold: 10);
        }
    }
}