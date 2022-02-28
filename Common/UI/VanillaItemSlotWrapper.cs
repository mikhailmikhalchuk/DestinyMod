using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.GameInput;
using Terraria.UI;

namespace DestinyMod.Common.UI
{
	public class VanillaItemSlotWrapper : UIElement
	{
		public Item Item;

		public Func<Item, bool> ValidItemFunc;

		public readonly int Context;

		public readonly float Scale;

		public readonly int HandlerContext;

		public VanillaItemSlotWrapper(int context = ItemSlot.Context.BankItem, int handlerContext = ItemSlot.Context.BankItem, float scale = 1f)
		{
			Item = new Item();
			Item.SetDefaults();

			Context = context;
			HandlerContext = handlerContext;
			Scale = scale;

			Width.Set(TextureAssets.InventoryBack9.Value.Width * scale, 0f);
			Height.Set(TextureAssets.InventoryBack9.Value.Height * scale, 0f);
		}

		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			float oldScale = Main.inventoryScale;
			Main.inventoryScale = Scale;

			if (!PlayerInput.IgnoreMouseInterface && ContainsPoint(Main.MouseScreen))
			{
				Main.LocalPlayer.mouseInterface = true;

				if (ValidItemFunc == null || ValidItemFunc(Main.mouseItem))
				{
					ItemSlot.Handle(ref Item, HandlerContext);
				}
			}

			ItemSlot.Draw(spriteBatch, ref Item, Context, GetDimensions().Position());
			Main.inventoryScale = oldScale;
		}
	}
}