using Terraria.UI;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Microsoft.Xna.Framework;
using Terraria;
using System;

namespace DestinyMod.Common.UI
{
    public class UIImageWithBackground : UIElement
    {
        public Texture2D Background { get; }

        public Color BackgroundColor;

        public int ScaleSize = 50;

        public Asset<Texture2D> Image;

        public Color ImageColor;

        public UIImageWithBackground(Texture2D background, Asset<Texture2D> image = null, int scaleSize = 50)
        {
            Background = background;
            Image = image;
            ScaleSize = scaleSize;
            BackgroundColor = Color.White;
            ImageColor = Color.White;
            Width.Pixels = Background.Width;
            Height.Pixels = Background.Height;
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            CalculatedStyle dimensions = GetDimensions();
            spriteBatch.Draw(Background, dimensions.Position(), BackgroundColor);

            if (Image != null)
            {
                Texture2D image = Image.Value;
                float largerSide = Math.Max(image.Width, image.Height);
                float drawnScale = largerSide / ScaleSize;
                spriteBatch.Draw(image, dimensions.Center(), null, ImageColor, 0f, image.Size() / 2, drawnScale, SpriteEffects.None, 0f);
            }
        }
    }
}