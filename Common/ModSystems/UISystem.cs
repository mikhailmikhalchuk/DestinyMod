using Terraria;
using Terraria.ModLoader;

namespace DestinyMod.Common.ModSystems
{
    public sealed class UISystem : ModSystem
    {
        public override void OnWorldLoad()
        {
            if (!Main.dedServ)
            {
                ModContent.GetInstance<Content.UI.SuperCharge.SuperChargeUI>().UserInterface.SetState(new Content.UI.SuperCharge.SuperChargeUI());
            }
        }

        public override void OnWorldUnload()
        {
            if (!Main.dedServ)
            {
                ModContent.GetInstance<Content.UI.SuperCharge.SuperChargeUI>().UserInterface.SetState(null);
            }
        }
    }
}
