using Microsoft.Xna.Framework;

namespace DestinyMod.Core.Extensions
{
	public static class RectangleExtensions
	{
        public static void Inflate(this Rectangle rectangle, int horizontalValue, int verticalValue)
        {
            rectangle.X -= horizontalValue;
            rectangle.Y -= verticalValue;
            rectangle.Width += horizontalValue * 2;
            rectangle.Height += verticalValue * 2;
        }
    }
}