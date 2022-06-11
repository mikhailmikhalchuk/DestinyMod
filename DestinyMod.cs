using Terraria.ModLoader;
using Terraria.GameContent.UI;
using DestinyMod.Content.Items.Misc;

namespace DestinyMod
{
	public class DestinyMod : Mod
	{
		public static DestinyMod Instance { get; set; }

		public DestinyMod() => Instance = this;

        public override void Load()
        {
            Content.Currencies.ExoticCipher.ID = CustomCurrencyManager.RegisterCurrency(new Content.Currencies.ExoticCipher(ModContent.ItemType<ExoticCipher>(), 30L));
        }

        public override void Unload() => Instance = null;
    }
}