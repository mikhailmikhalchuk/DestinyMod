using DestinyMod.Common.Items.ItemTypes;
using Terraria;
using Terraria.ID;

namespace DestinyMod.Content.Items.Consumables
{
    public class GuardianCrest : Consumable
    {
        public override void SetStaticDefaults() => Tooltip.SetDefault("Grants the ability to switch to a different class");

        public override void DestinySetDefaults()
        {
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.UseSound = SoundID.Item3;
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.buyPrice(gold: 1);
        }
    }
}