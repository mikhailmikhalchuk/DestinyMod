using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria.GameContent;
using Terraria.UI;

namespace DestinyMod.Content.UI.MouseText
{
    public class MouseTextElement : UIElement
    {
        public static DynamicSpriteFont MouseFont => FontAssets.MouseText.Value;

        public static float CommonOpacity => MouseTextState.CommonOpacity;

        public int WidthPixels;

        public float WidthPercentage;

        private Color? BackgroundColor_Internal = null;

        public virtual Color BackgroundColor_Default => Color.Transparent;

        public Color? BackgroundColor
        {
            get => BackgroundColor_Internal ?? BackgroundColor_Default;
            set => BackgroundColor_Internal = value == null ? BackgroundColor_Default : value.Value;
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);

            Texture2D magicPixel = TextureAssets.MagicPixel.Value;
            spriteBatch.Draw(magicPixel, GetDimensions().ToRectangle(), BackgroundColor.Value);
        }
    }
}
