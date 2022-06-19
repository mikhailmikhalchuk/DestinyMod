using Terraria.UI;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using System;
using Terraria;
using Terraria.ID;
using Terraria.Graphics.Shaders;
using ReLogic.Graphics;
using DestinyMod.Common.GlobalItems;
using Terraria.ModLoader;
using DestinyMod.Content.UI.ItemDetails;
using DestinyMod.Content.Items.Equipables.Dyes;
using DestinyMod.Common.ModPlayers;

namespace DestinyMod.Content.UI.GameplayInformation
{
	public class AmmoDisplay : UIElement
	{
		public bool Visible;

		public Texture2D WeaponIcon;

		public bool ApplyShader;

		public string MagazineCount;

		public string AmmoCount;

		public DynamicSpriteFont Font => FontAssets.MouseText.Value;

		public AmmoDisplay()
        {
			Width.Pixels = 160;
			Height.Pixels = 60;
        }

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);

			Player player = Main.LocalPlayer;
			Item heldItem = Main.mouseItem.IsAir ? player.HeldItem : Main.mouseItem;
			if (heldItem == null || heldItem.IsAir || !heldItem.TryGetGlobalItem(out ItemDataItem itemDataItem) || itemDataItem.MagazineCapacity < 0 || itemDataItem.Magazine == null)
            {
				Visible = false;
				return;
            }

			Visible = true;
			WeaponIcon = TextureAssets.Item[heldItem.type].Value;
			ApplyShader = true;
			MagazineCount = itemDataItem.Magazine.Count.ToString();
			int ammoCount = 0;
			foreach (Item superAmmosition in player.inventory)
			{
				if (superAmmosition.stack > 0 && ItemLoader.CanChooseAmmo(heldItem, superAmmosition, player))
				{
					ammoCount += superAmmosition.stack;

					if (ammoCount >= 10000)
                    {
						break;
                    }
				}
			}
			AmmoCount = ammoCount >= 10000 ? "9999+" : ammoCount.ToString();
		}

		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			if (!Visible)
			{
				return;
			}

			base.DrawSelf(spriteBatch);
			CalculatedStyle dimensions = GetDimensions();

			Texture2D magicPixel = TextureAssets.MagicPixel.Value;

			// Idk the actual in-game color values so
			spriteBatch.Draw(magicPixel, dimensions.ToRectangle(), new Color(200, 220, 230) * 0.2f);

			int ammoBorderSize = 1;
			Color ammoBorderColor = new Color(135, 142, 142);
			int borderSize = 4;
			Rectangle ammoBorderDestRect = new Rectangle((int)(dimensions.X + dimensions.Width * 0.7f), (int)dimensions.Y, ammoBorderSize, (int)dimensions.Height - borderSize);
			spriteBatch.Draw(magicPixel, ammoBorderDestRect, ammoBorderColor);
			Color borderColor = new Color(242, 244, 244);
			Rectangle bottomBorderDestRect = new Rectangle((int)dimensions.X, (int)(dimensions.Y + dimensions.Height - borderSize), (int)dimensions.Width, borderSize);
			spriteBatch.Draw(magicPixel, bottomBorderDestRect, borderColor);
			Rectangle rightBorderDestRect = new Rectangle((int)(dimensions.X + dimensions.Width - borderSize), (int)dimensions.Y, borderSize, (int)dimensions.Height);
			spriteBatch.Draw(magicPixel, rightBorderDestRect, borderColor);

			int weaponIconBorderSize = 6;
			Rectangle weaponIconFrame = new Rectangle((int)dimensions.X + weaponIconBorderSize, (int)dimensions.Y + weaponIconBorderSize, (int)Width.Pixels / 2, (int)dimensions.Height - 2 * weaponIconBorderSize - borderSize);
			bool isXGreaterThanY = WeaponIcon.Width > WeaponIcon.Height;
			int greaterDimension = Math.Max(WeaponIcon.Width, WeaponIcon.Height);
			float scaleRatio = isXGreaterThanY ? weaponIconFrame.Width / greaterDimension : weaponIconFrame.Height / greaterDimension;
			int destWidth = (int)(WeaponIcon.Width * scaleRatio);
			int destHeight = (int)(WeaponIcon.Height * scaleRatio);
			int destX = (int)(weaponIconFrame.X + (weaponIconFrame.Width - destWidth) / 2f);
			int destY = (int)(weaponIconFrame.Y + (weaponIconFrame.Height - destHeight) / 2f);
			Rectangle destinationRect = new Rectangle(destX, destY, destWidth, destHeight);
			DrawData itemDisplay = new DrawData(WeaponIcon, destinationRect, Color.White);

			if (ApplyShader)
			{
				SamplerState anisotropicClamp = SamplerState.AnisotropicClamp;
				spriteBatch.End();
				spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.UIScaleMatrix);
				GameShaders.Armor.GetShaderFromItemId(ModContent.ItemType<MysteriousDye>()).Apply(null, itemDisplay);
				itemDisplay.effect = SpriteEffects.FlipHorizontally;
				itemDisplay.Draw(spriteBatch);
				spriteBatch.End();
				spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, anisotropicClamp, DepthStencilState.None, ItemDetailsState.OverflowHiddenRasterizerState, null, Main.UIScaleMatrix);
			}
			else
            {
				itemDisplay.Draw(spriteBatch);
			}

			string magText = MagazineCount;
			float heightPosHalved = dimensions.Y + ammoBorderDestRect.Height / 2;
			float magTextScale = 1f;
			Vector2 magCountSize = Font.MeasureString(magText) * magTextScale;
			Vector2 magTextPosition = new Vector2(ammoBorderDestRect.X - borderSize - magCountSize.X, heightPosHalved + 3); // Looks weird without magic number. Dunno why and can't be bothered to investigate.
			spriteBatch.DrawString(Font, magText, magTextPosition, Color.White, 0f, new Vector2(0, magCountSize.Y / 2), magTextScale, SpriteEffects.None, 0f);

			string ammoText = AmmoCount;
			float ammoTextScale = 0.66f;
			Vector2 ammoTextSize = Font.MeasureString(ammoText) * ammoTextScale;
			Vector2 ammoTextPosition = new Vector2(rightBorderDestRect.X - borderSize * 2 - ammoTextSize.X, heightPosHalved - 1); // Same thing here
			spriteBatch.DrawString(Font, ammoText, ammoTextPosition, Color.White, 0f, new Vector2(0, ammoTextSize.Y / 2), ammoTextScale, SpriteEffects.None, 0f);

			// Simple reload progress bar until / unless you want to do dreaded reload animations
			ItemDataPlayer itemDataPlayer = Main.LocalPlayer.GetModPlayer<ItemDataPlayer>();
			if (itemDataPlayer.ReloadTimer > 0)
			{
				float reloadProgress = Utils.Clamp((itemDataPlayer.ReloadTime - itemDataPlayer.ReloadTimer) / (float)itemDataPlayer.ReloadTime, 0f, 1f);
				Rectangle reloadProgressBarDestRect = new Rectangle(bottomBorderDestRect.X, bottomBorderDestRect.Y, (int)(bottomBorderDestRect.Width * reloadProgress), bottomBorderDestRect.Height);
				Color reloadProgressBarColor = ammoBorderColor * Math.Clamp((float)Math.Abs(Math.Sin(Main.GameUpdateCount / 10f) * 0.66f), 0.4f, 1f);
				spriteBatch.Draw(magicPixel, reloadProgressBarDestRect, reloadProgressBarColor);
			}
		}
	}
}