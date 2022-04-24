using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.UI;

namespace DestinyMod.Common.UI
{
	public class DisplayItemSlot : UIElement
	{
		public Item Item;

		public readonly int Context;

		public readonly float Scale;

		public DisplayItemSlot(int itemType, int context = ItemSlot.Context.BankItem, float scale = 1f)
		{
			Item = new Item();
			Item.SetDefaults(itemType);

			Context = context;
			Scale = scale;

			Width.Set(TextureAssets.InventoryBack9.Value.Width * scale, 0f);
			Height.Set(TextureAssets.InventoryBack9.Value.Height * scale, 0f);
		}

		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			float oldScale = Main.inventoryScale;
			Main.inventoryScale = Scale;
			ItemSlot.Draw(spriteBatch, ref Item, Context, GetDimensions().Position());
			Main.inventoryScale = oldScale;
		}
	}
}