using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameInput;
using Terraria.UI;

namespace TheDestinyMod.UI
{
	internal class VanillaItemSlotWrapper : UIElement
	{
		internal Item Item;
		internal Func<Item, bool> ValidItemFunc;
		private readonly int _context;
		private readonly float _scale;
		private readonly int _handlerContext;

		public VanillaItemSlotWrapper(int context = ItemSlot.Context.BankItem, int handlerContext = ItemSlot.Context.BankItem, float scale = 1f) {
			_context = context;
			_handlerContext = handlerContext;
			_scale = scale;
			Item = new Item();
			Item.SetDefaults(0);

			Width.Set(Main.inventoryBack9Texture.Width * scale, 0f);
			Height.Set(Main.inventoryBack9Texture.Height * scale, 0f);
		}

		protected override void DrawSelf(SpriteBatch spriteBatch) {
			float oldScale = Main.inventoryScale;
			Main.inventoryScale = _scale;
			Rectangle rectangle = GetDimensions().ToRectangle();

			if (ContainsPoint(Main.MouseScreen) && !PlayerInput.IgnoreMouseInterface) {
				Main.LocalPlayer.mouseInterface = true;
				if (ValidItemFunc == null || ValidItemFunc(Main.mouseItem)) {
					ItemSlot.Handle(ref Item, _handlerContext);
				}
			}
			ItemSlot.Draw(spriteBatch, ref Item, _context, rectangle.TopLeft());
			Main.inventoryScale = oldScale;
		}
	}
}