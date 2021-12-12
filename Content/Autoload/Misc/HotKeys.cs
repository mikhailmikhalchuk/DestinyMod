using TheDestinyMod.Core.Autoloading;

namespace TheDestinyMod.Content.Autoloading.Misc
{
    public class HotKeys : IAutoloadable
    {
        public void IAutoloadable_Load(IAutoloadable createdObject) => TheDestinyMod.activateSuper = TheDestinyMod.Instance.RegisterHotKey("Activate Super", "U");

        public void IAutoloadable_PostSetUpContent() { }

        public void IAutoloadable_Unload() => TheDestinyMod.activateSuper = null;
    }
}