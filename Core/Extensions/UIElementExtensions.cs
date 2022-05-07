using Microsoft.Xna.Framework;
using System;
using Terraria.UI;

namespace DestinyMod.Core.Extensions
{
	public static class UIElementExtensions
	{
		public static Vector2 CalculateChildrenSize(this UIElement uiElement)
		{
			float maxWidth = 0;
			float maxHeight = 0;
			foreach (UIElement child in uiElement.Children)
            {
				float childWidth = child.Left.Pixels + child.Width.Pixels;
				float childHeight = child.Top.Pixels + child.Height.Pixels;
				maxWidth = Math.Max(maxWidth, childWidth);
				maxHeight = Math.Max(maxHeight, childHeight);
			}
			return new Vector2(maxWidth, maxHeight);
		}
	}
}