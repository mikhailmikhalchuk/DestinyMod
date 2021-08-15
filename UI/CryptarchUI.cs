using TheDestinyMod.NPCs.Town;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.UI.Chat;
using TheDestinyMod.Items;
using TheDestinyMod.Items.Weapons.Ranged;
using System.Collections.Generic;

namespace TheDestinyMod.UI
{
	internal class CryptarchUI : UIState
	{
		public static VanillaItemSlotWrapper _vanillaItemSlot;

		public override void OnInitialize() {
			_vanillaItemSlot = new VanillaItemSlotWrapper(ItemSlot.Context.PrefixItem, ItemSlot.Context.BankItem, 0.85f) {
				Left = { Pixels = 50 },
				Top = { Pixels = 270 },
				ValidItemFunc = item => item.IsAir || !item.IsAir && (item.type == ModContent.ItemType<RareEngram>() || item.type == ModContent.ItemType<CommonEngram>() || item.type == ModContent.ItemType<LegendaryEngram>() || item.type == ModContent.ItemType<ExoticEngram>() || item.type == ModContent.ItemType<UncommonEngram>())
			};
			Append(_vanillaItemSlot);
		}
		
		public override void Update(GameTime gameTime) {
			base.Update(gameTime);
			if (Main.LocalPlayer.talkNPC == -1 || Main.npc[Main.LocalPlayer.talkNPC].type != ModContent.NPCType<Cryptarch>()) {
				ModContent.GetInstance<TheDestinyMod>().CryptarchUserInterface.SetState(null);
			}
		}

		private bool tickPlayed;
		protected override void DrawSelf(SpriteBatch spriteBatch) {
			base.DrawSelf(spriteBatch);

			Main.HidePlayerCraftingMenu = true;

			const int slotX = 50;
			const int slotY = 270;
			if (!_vanillaItemSlot.Item.IsAir) {
				ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, Main.fontMouseText, $"Decrypt {_vanillaItemSlot.Item.Name}", new Vector2(slotX + 50, slotY), new Color(Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor), 0f, Vector2.Zero, Vector2.One, -1f, 2f);
				int decryptX = slotX + 70;
				int decryptY = slotY + 40;
				bool hoveringOverDecryptButton = Main.mouseX > decryptX - 15 && Main.mouseX < decryptX + 15 && Main.mouseY > decryptY - 15 && Main.mouseY < decryptY + 15 && !PlayerInput.IgnoreMouseInterface;
				Texture2D decryptTexture = Main.reforgeTexture[hoveringOverDecryptButton ? 1 : 0];
				Main.spriteBatch.Draw(decryptTexture, new Vector2(decryptX, decryptY), null, Color.White, 0f, decryptTexture.Size() / 2f, 0.8f, SpriteEffects.None, 0f);
				if (hoveringOverDecryptButton) {
					Main.hoverItemName = "Decrypt";
					if (!tickPlayed) {
						Main.PlaySound(SoundID.MenuTick, -1, -1, 1, 1f, 0f);
					}
					tickPlayed = true;
					Main.LocalPlayer.mouseInterface = true;
					if (Main.mouseLeftRelease && Main.mouseLeft) {
						while (_vanillaItemSlot.Item.stack > 0) {
							if (_vanillaItemSlot.Item.type == ModContent.ItemType<CommonEngram>()) {
								List<int> commonLoot = new List<int>
								{
									ModContent.ItemType<HakkeAutoRifle>(),
									ModContent.ItemType<HakkePulseRifle>(),
									ModContent.ItemType<HakkeScoutRifle>(),
									ModContent.ItemType<HakkeShotgun>(),
									ModContent.ItemType<HakkeSidearm>(),
									ModContent.ItemType<HakkeSniper>(),
									ModContent.ItemType<HakkeRocketLauncher>(),
									ModContent.ItemType<HakkeGrenadeLauncher>(),
									ModContent.ItemType<HakkeHandcannon>()
								};
								if (Main.rand.Next(20) > commonLoot.Count) {
									Main.LocalPlayer.QuickSpawnItem(ItemID.SilverCoin, Main.rand.Next(1, 10));
								}
								else {
									Main.LocalPlayer.QuickSpawnItem(Main.rand.Next(commonLoot));
								}
								switch (Main.rand.Next(20)) {
									default:
										
										break;
								}
							}
							else if (_vanillaItemSlot.Item.type == ModContent.ItemType<UncommonEngram>()) {
								List<int> uncommonLoot = new List<int>
								{
									ModContent.ItemType<OmolonAutoRifle>(),
									ModContent.ItemType<OmolonPulseRifle>(),
									ModContent.ItemType<OmolonScoutRifle>(),
									ModContent.ItemType<OmolonShotgun>(),
									ModContent.ItemType<OmolonSidearm>(),
									ModContent.ItemType<OmolonSniper>(),
									ModContent.ItemType<OmolonRocketLauncher>(),
									ModContent.ItemType<OmolonGrenadeLauncher>(),
									ModContent.ItemType<OmolonHandcannon>()
								};
								if (Main.rand.Next(20) > uncommonLoot.Count) {
									Main.LocalPlayer.QuickSpawnItem(ItemID.SilverCoin, Main.rand.Next(10, 50));
								}
								else {
									Main.LocalPlayer.QuickSpawnItem(Main.rand.Next(uncommonLoot));
								}
							}
							else if (_vanillaItemSlot.Item.type == ModContent.ItemType<RareEngram>()) {
								List<int> rareLoot = new List<int>
								{
									ModContent.ItemType<SurosAutoRifle>(),
									ModContent.ItemType<SurosPulseRifle>(),
									ModContent.ItemType<SurosScoutRifle>(),
									ModContent.ItemType<SurosShotgun>(),
									ModContent.ItemType<SurosSidearm>(),
									ModContent.ItemType<SurosSniper>(),
									ModContent.ItemType<SurosRocketLauncher>(),
									ModContent.ItemType<SurosGrenadeLauncher>(),
									ModContent.ItemType<SurosHandcannon>()
								};
								if (Main.rand.Next(20) > rareLoot.Count) {
									Main.LocalPlayer.QuickSpawnItem(ItemID.GoldCoin, Main.rand.Next(1, 3));
								}
								else {
									Main.LocalPlayer.QuickSpawnItem(Main.rand.Next(rareLoot));
								}
							}
							_vanillaItemSlot.Item.stack--;
						}
						_vanillaItemSlot.Item.TurnToAir();
						Main.PlaySound(SoundID.Item37, -1, -1);
					}
				}
				else {
					tickPlayed = false;
				}
			}
			else {
				string message = "Place an Engram here to decrypt it";
				ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, Main.fontMouseText, message, new Vector2(slotX + 50, slotY), new Color(Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor), 0f, Vector2.Zero, Vector2.One, -1f, 2f);
			}
		}
	}
}