using TheDestinyMod.Core.Autoloading;
using Terraria;
using TheDestinyMod.UI;
using Terraria.UI;
using TheDestinyMod.Core.UI;
using Microsoft.Xna.Framework;

namespace TheDestinyMod.Content.Autoloading.Misc
{
	public class UI : IAutoloadable
    {
        public void IAutoloadable_Load(IAutoloadable createdObject)
        {
            if (Main.dedServ)
            {
                return;
            }

            TheDestinyMod mod = TheDestinyMod.Instance;

            mod.SubclassUI = new SubclassUI();
            mod.SubclassUI.Activate();
            mod.subclassInterface = new UserInterface();
            mod.subclassInterface.SetState(mod.SubclassUI);
            new UIHandler(mod.subclassInterface, "Vanilla: Inventory", "TheDestinyMod: Subclass UI", () =>
            {
                if (Main.playerInventory)
                {
                    mod.subclassInterface.Draw(Main.spriteBatch, new GameTime());
                }
                return true;
            });

            mod.RaidSelectionUI = new RaidSelectionUI("Vault of Glass", DestinyWorld.clearsVOG, NPC.downedBoss3, "Skeletron");
            mod.RaidSelectionUI.Activate();

            mod.raidInterface = new UserInterface();
            new UIHandler(mod.raidInterface, "Vanilla: Mouse Text", "TheDestinyMod: Raid Selection UI");

            mod.ClassSelectionUI = new ClassSelectionUI();
            mod.ClassSelectionUI.Activate();
            mod.classSelectionInterface = new UserInterface();

            mod.CryptarchUserInterface = new UserInterface();
            new UIHandler(mod.CryptarchUserInterface, "Vanilla: Mouse Text", "TheDestinyMod: Cryptarch UI");

            mod.SuperResourceCharge = new SuperChargeBar();
            mod.superChargeInterface = new UserInterface();
            mod.superChargeInterface.SetState(mod.SuperResourceCharge);
            new UIHandler(mod.superChargeInterface, "Vanilla: Resource Bars", "TheDestinyMod: Super Charge UI");
        }

        public void IAutoloadable_PostSetUpContent() { }

        public void IAutoloadable_Unload() { }
    }
}