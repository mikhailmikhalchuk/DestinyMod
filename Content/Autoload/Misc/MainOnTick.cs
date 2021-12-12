using TheDestinyMod.Core.Autoloading;
using Terraria;

namespace TheDestinyMod.Content.Autoloading.Misc
{
    public class MainOnTick : IAutoloadable
    {
        public void IAutoloadable_Load(IAutoloadable createdObject)
        {
            if (Main.dedServ)
            {
                return;
            }

            Main.OnTick += Main_OnTick;
        }

        public void IAutoloadable_PostSetUpContent() { }

        public void IAutoloadable_Unload()
		{
            if (Main.dedServ)
            {
                return;
            }

            Main.OnTick -= Main_OnTick;
        }

        private void Main_OnTick()
        {
            TheDestinyMod mod = TheDestinyMod.Instance;

            void SetUI()
            {
                mod.classSelectionInterface.SetState(mod.ClassSelectionUI);
                Main.menuMode = 888;
                Main.MenuUI.SetState(mod.ClassSelectionUI);
                TheDestinyMod.classSelecting = true;
            }

            if (Main.menuMode == 1)
            {
                TheDestinyMod.classSelecting = false;
                if (mod.wasJustCreating)
                {
                    SetUI();
                }
            }

            if (Main.menuMode == 2 && !TheDestinyMod.classSelecting)
            {
                SetUI();
            }

            mod.wasJustCreating = Main.menuMode == 2;
        }
    }
}