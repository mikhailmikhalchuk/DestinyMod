using Microsoft.Xna.Framework;

namespace DestinyMod.Core.Utils
{
	public static class RectangleUtils
	{
		public static Rectangle RectangleFromCenter(int x, int y, int width, int height) => new Rectangle(x - width / 2, y - height / 2, width, height);
	}
}