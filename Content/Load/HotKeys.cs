using Microsoft.Xna.Framework.Input;
using Terraria.ModLoader;

namespace DestinyMod.Content.Load
{
	public class HotKeys : ILoadable
    {
        public static ModKeybind ActiveSuper { get; private set; }

        public void Load(Mod mod) => ActiveSuper = KeybindLoader.RegisterKeybind(mod, "Activate Super", Keys.U);

        public void Unload() => ActiveSuper = null;
    }
}