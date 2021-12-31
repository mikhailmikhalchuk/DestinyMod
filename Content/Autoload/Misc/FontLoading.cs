using TheDestinyMod.Core.Autoloading;
using Terraria;
using Terraria.ModLoader;

namespace TheDestinyMod.Content.Autoloading.Misc
{
	public class FontLoading : IAutoloadable
    {
        public void IAutoloadable_Load(IAutoloadable createdObject)
        {
            if (Main.dedServ)
            {
                return;
            }

            TheDestinyMod mod = TheDestinyMod.Instance;

            TheDestinyMod.fontFuturaBold = Main.fontMouseText;
            TheDestinyMod.fontFuturaBook = Main.fontMouseText;

            if (mod.FontExists("Fonts/FuturaBold"))
                TheDestinyMod.fontFuturaBold = mod.GetFont("Fonts/FuturaBold");

            if (mod.FontExists("Fonts/FuturaBook"))
                TheDestinyMod.fontFuturaBook = mod.GetFont("Fonts/FuturaBook");

            TheDestinyMod.SubworldLibrary = ModLoader.GetMod("SubworldLibrary");
        }

        public void IAutoloadable_PostSetUpContent() { }

        public void IAutoloadable_Unload() { }
    }
}