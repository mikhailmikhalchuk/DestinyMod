using Microsoft.Xna.Framework.Input;
using Terraria.ModLoader;

namespace DestinyMod.Content.Load
{
	public sealed class HotKeys : ILoadable
    {
        public static ModKeybind ActivateSuper { get; private set; }

        public static ModKeybind ReloadWeapon { get; private set; }

        public void Load(Mod mod)
        {
            ActivateSuper = KeybindLoader.RegisterKeybind(mod, "Activate Super", Keys.U);
            ReloadWeapon = KeybindLoader.RegisterKeybind(mod, "Reload Weapon", Keys.R);
        }

        public void Unload()
        {
            ActivateSuper = null;
            ReloadWeapon = null;
        }
    }
}