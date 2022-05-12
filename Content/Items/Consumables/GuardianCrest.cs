using DestinyMod.Common.Items.ItemTypes;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DestinyMod.Content.Items.Consumables
{
    public class GuardianCrest : Consumable
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Grants the ability to switch to a different class");
        }

        public override void DestinySetDefaults()
        {
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.UseSound = SoundID.Item113;
            Item.rare = ItemRarityID.Red;
            Item.value = Item.buyPrice(gold: 1);
        }

        public override bool ConsumeItem(Player player) => ModContent.GetInstance<UI.ClassChange.ClassChangeUI>().UserInterface != null;

        public override bool CanUseItem(Player player)
        {
            if (ModContent.GetInstance<UI.ClassChange.ClassChangeUI>().UserInterface.CurrentState == null)
            {
                ModContent.GetInstance<UI.ClassChange.ClassChangeUI>().UserInterface.SetState(new UI.ClassChange.ClassChangeUI());
            }
            else
            {
                ModContent.GetInstance<UI.ClassChange.ClassChangeUI>().UserInterface.SetState(null);
            }
            return true;
        }
    }
}