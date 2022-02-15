using ReLogic.Content;
using ReLogic.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace DestinyMod.Content.Load
{
    public class FontLoading : ILoadable
    {
        public static DynamicSpriteFont FuturaBold { get; private set; }

        public static DynamicSpriteFont FuturaBook { get; private set; }

        public void Load(Mod mod)
		{
            if (Main.dedServ)
            {
                return;
            }

            FuturaBold = ModContent.RequestIfExists("Assets/Fonts/FuturaBold", out Asset<DynamicSpriteFont> futuraBold) 
                ? futuraBold.Value : FontAssets.MouseText.Value;

            FuturaBook = ModContent.RequestIfExists("Assets/Fonts/FuturaBook", out Asset<DynamicSpriteFont> futuraBook)
                ? futuraBook.Value : FontAssets.MouseText.Value;
        }

		public void Unload()
		{
            FuturaBold = null;
            FuturaBook = null;
        }
	}
}