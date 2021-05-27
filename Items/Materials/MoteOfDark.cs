using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria.Localization;

namespace TheDestinyMod.Items.Materials
{
	public class MoteOfDark : ModItem
	{
		public override void SetStaticDefaults() {
			DisplayName.AddTranslation(GameCulture.Polish, "Odrobina ciemności");
			Tooltip.SetDefault("A mysterious tetrahedral object. The Drifter may like this");
			Tooltip.AddTranslation(GameCulture.Polish, "Tajemniczy czworościenny obiekt. Dryfterowi może się to spodobać");
		}

        public override void SetDefaults() {
			item.height = 44;
			item.width = 44;
			item.maxStack = 99;
			item.scale = 0.5f;
        }

        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI) {
			scale *= 0.5f;
            return true;
        }
	}
}