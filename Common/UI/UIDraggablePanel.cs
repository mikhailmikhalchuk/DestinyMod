using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace DestinyMod.Common.UI
{
	public class UIDraggablePanel : UIPanel
	{
		public Vector2 Offset { get; private set; }

		public bool Dragging;

		public override void MouseDown(UIMouseEvent evt)
		{
			base.MouseDown(evt);
			DragStart(evt);
		}

		public override void MouseUp(UIMouseEvent evt)
		{
			base.MouseUp(evt);
			DragEnd(evt);
		}

		private void DragStart(UIMouseEvent evt)
		{
			Offset = new Vector2(evt.MousePosition.X - Left.Pixels, evt.MousePosition.Y - Top.Pixels);
			Dragging = true;
		}

		public void DragEnd(UIMouseEvent evt)
		{
			Vector2 end = evt.MousePosition;
			Dragging = false;

			Left.Set(end.X - Offset.X, 0f);
			Top.Set(end.Y - Offset.Y, 0f);

			Recalculate();
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);

			if (ContainsPoint(Main.MouseScreen))
			{
				Main.LocalPlayer.mouseInterface = true;
			}

			if (Dragging)
			{
				Left.Set(Main.mouseX - Offset.X, 0f);
				Top.Set(Main.mouseY - Offset.Y, 0f);
				Recalculate();
			}

			var parentSpace = Parent.GetDimensions().ToRectangle();
			if (!GetDimensions().ToRectangle().Intersects(parentSpace))
			{
				Left.Pixels = Utils.Clamp(Left.Pixels, 0, parentSpace.Right - Width.Pixels);
				Top.Pixels = Utils.Clamp(Top.Pixels, 0, parentSpace.Bottom - Height.Pixels);

				Recalculate();
			}
		}
	}
}