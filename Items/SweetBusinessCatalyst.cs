using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace TheDestinyMod.Items
{
    public class SweetBusinessCatalyst : ModItem
    {
        public override void SetStaticDefaults() {
            Tooltip.SetDefault("Right click Sweet Business while holding this to apply the catalyst");
        }

        public override void SetDefaults() {
            item.width = 50;
            item.height = 50;
            item.rare = ItemRarityID.Yellow;
            item.scale = 0.5f;
        }

        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI) {
            scale *= 0.5f;
            return true;
        }
    }
}