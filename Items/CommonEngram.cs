using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terraria.Localization;

namespace TheDestinyMod.Items
{
	public class CommonEngram : ModItem
	{
		public override void SetStaticDefaults() {
			DisplayName.AddTranslation(GameCulture.Polish, "Pospolity Engram");
			Tooltip.SetDefault("A highly advanced, encrypted storage unit\nA cryptarch could probably break its encryption for you");
			Tooltip.AddTranslation(GameCulture.Polish, "Wysoce zaawansowana zaszyfrowana jednostka przechowywania. Dekoder może prawdopodobnie złamać szyfrowania dla Ciebie");
			Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(8, 12));
		}

        public override void SetDefaults() {
			item.height = 40;
			item.width = 38;
			item.maxStack = 99;
			item.value = Item.buyPrice(0, 0, 1, 0);
			item.scale = 0.5f;
        }

        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI) {
			scale *= 0.5f;
            return true;
        }

		public override void PostUpdate() {
            Lighting.AddLight(item.Center, Color.WhiteSmoke.ToVector3() * 0.55f * Main.essScale);
        }

		public override bool CanBurnInLava() => true;
	}
}