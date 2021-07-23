using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.DataStructures;

namespace TheDestinyMod.Items
{
	public class UncommonEngram : ModItem
	{
		public override void SetStaticDefaults() {
			DisplayName.AddTranslation(GameCulture.Polish, "Niepospolity Engram");
			Tooltip.SetDefault("A highly advanced, encrypted storage unit\nA cryptarch could probably break its encryption for you");
			Tooltip.AddTranslation(GameCulture.Polish, "Wysoce zaawansowana zaszyfrowana jednostka przechowywania. Dekoder może prawdopodobnie złamać szyfrowania dla Ciebie");
			Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(8, 12));
		}

        public override void SetDefaults() {
			item.height = 40;
			item.width = 38;
			item.maxStack = 99;
			item.rare = ItemRarityID.Green;
			item.value = Item.buyPrice(0, 0, 10, 0);
			item.scale = 0.5f;
        }

        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI) {
			scale *= 0.5f;
            return true;
        }

		public override void PostUpdate() {
            Lighting.AddLight(item.Center, Color.LightGreen.ToVector3() * 0.55f * Main.essScale);
        }

		public override bool CanBurnInLava() => true;
	}
}