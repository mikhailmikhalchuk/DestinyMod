using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria.UI;
using ReLogic.Graphics;
using Terraria;

namespace DestinyMod.Content.UI.MouseText
{
	/// <summary>Used for mouseover info item requirements, such as upgrade module requirements on infusion slot.</summary>
	public class MouseText_RequiredItem : MouseTextElement
	{
		public Texture2D RequiredItemTexture { get; }

		public string RequiredItemName { get; }

		public Vector2 RequiredItemNameDimensions { get; }

		public int RequiredItemType { get; }

		public int RequiredItemStack { get; }

		public override Color BackgroundColor_Default => new Color(20, 20, 20) * CommonOpacity;

		public MouseText_RequiredItem(int itemType, int itemStack)
		{
			Main.instance.LoadItem(itemType);
			RequiredItemTexture = TextureAssets.Item[itemType].Value;

			Item tempItem = new Item();
			tempItem.SetDefaults(itemType);
			RequiredItemName = tempItem.HoverName;
			RequiredItemNameDimensions = MouseFont.MeasureString(RequiredItemName);

			RequiredItemType = itemType;
			RequiredItemStack = itemStack;
			WidthPercentage = 1f;
			Height.Pixels = 48;
		}

		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			base.DrawSelf(spriteBatch);

			CalculatedStyle dimensions = GetDimensions();

			float heightScale = 32f / RequiredItemTexture.Height;
			Rectangle itemTextureDestRect = new Rectangle((int)dimensions.X + 8, (int)dimensions.Y + 8, (int)(RequiredItemTexture.Width * heightScale), 32);
			spriteBatch.Draw(RequiredItemTexture, itemTextureDestRect, null, Color.Wheat, 0f, Vector2.Zero, SpriteEffects.None, 0f);

			Vector2 itemNamePosition = new Vector2(itemTextureDestRect.Right + 8, dimensions.Center().Y);
			spriteBatch.DrawString(MouseFont, RequiredItemName, itemNamePosition, Color.White, 0f, new Vector2(0, RequiredItemNameDimensions.Y / 2), 1f, SpriteEffects.None, 0f);

			int playerItemCount = Main.LocalPlayer.CountItem(RequiredItemType);
			string itemCountText = playerItemCount + " / " + RequiredItemStack;
			Vector2 itemCountTextSize = MouseFont.MeasureString(itemCountText);
			Vector2 itemCountPosition = new Vector2(dimensions.X + dimensions.Width - 8, dimensions.Center().Y);
			Color requiredItemColor = playerItemCount >= RequiredItemStack ? Color.Green : Color.Red;
			spriteBatch.DrawString(MouseFont, itemCountText, itemCountPosition, requiredItemColor, 0f, new Vector2(itemCountTextSize.X, itemCountTextSize.Y / 2), 1f, SpriteEffects.None, 0f);
		}
	}
}
