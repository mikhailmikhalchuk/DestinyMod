using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria.UI;

namespace DestinyMod.Common.UI
{
	public class UISeparator : UIElement
	{
        public Color Color;

        public override void Draw(SpriteBatch spriteBatch)
        {
            CalculatedStyle dimensions = GetDimensions();
            spriteBatch.Draw(TextureAssets.MagicPixel.Value, dimensions.ToRectangle(), Color);
        }
    }
}