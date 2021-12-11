using Terraria.GameContent.UI;
using Terraria.ModLoader;
using TheDestinyMod.Core.Autoloading;

namespace TheDestinyMod.Content.Autoloading.Misc
{
    public class CustomCurrencies : IAutoloadable
    {
        public void IAutoloadable_Load(IAutoloadable createdObject) => TheDestinyMod.CipherCustomCurrencyId = CustomCurrencyManager.RegisterCurrency(new ExoticCipher(ModContent.ItemType<Items.ExoticCipher>(), 30L));

        public void IAutoloadable_PostSetUpContent() { }

        public void IAutoloadable_Unload() { }
    }
}