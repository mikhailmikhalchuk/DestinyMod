using Terraria.ModLoader;

namespace DestinyMod.Common.ModSystems
{
    public sealed class UISystem : ModSystem
    {
        public override void OnWorldLoad()
        {
            ModContent.GetInstance<Content.UI.SuperCharge.SuperChargeUI>().UserInterface.SetState(new Content.UI.SuperCharge.SuperChargeUI());
        }

        public override void OnWorldUnload()
        {
            ModContent.GetInstance<Content.UI.SuperCharge.SuperChargeUI>().UserInterface.SetState(null);
        }
    }
}
