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

namespace TheDestinyMod.UI
{
	internal class CryptarchUI : UIState
	{
		public static VanillaItemSlotWrapper _vanillaItemSlot;

		public override void OnInitialize() {
			_vanillaItemSlot = new VanillaItemSlotWrapper(ItemSlot.Context.BankItem, 0.85f) {
				Left = { Pixels = 50 },
				Top = { Pixels = 270 },
				ValidItemFunc = item => item.IsAir || !item.IsAir && (item.type == ModContent.ItemType<RareEngram>() || item.type == ModContent.ItemType<CommonEngram>() || item.type == ModContent.ItemType<LegendaryEngram>() || item.type == ModContent.ItemType<ExoticEngram>() || item.type == ModContent.ItemType<UncommonEngram>())
			};
			Append(_vanillaItemSlot);
		}

		public override void OnDeactivate() {
			if (!_vanillaItemSlot.Item.IsAir) {
				Main.LocalPlayer.QuickSpawnClonedItem(_vanillaItemSlot.Item, _vanillaItemSlot.Item.stack);
				_vanillaItemSlot.Item.TurnToAir();
			}
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
				ItemSlot.DrawSavings(Main.spriteBatch, slotX + 130, Main.instance.invBottom, true);
				ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, Main.fontMouseText, $"Decrypt {_vanillaItemSlot.Item.Name}", new Vector2(slotX + 50, slotY), new Color(Main.mouseTextColor, (byte)Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor), 0f, Vector2.Zero, Vector2.One, -1f, 2f);
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
								switch (Main.rand.Next(20)) {
									case 0:
										Main.LocalPlayer.QuickSpawnItem(ModContent.ItemType<Items.Weapons.Ranged.HakkeAutoRifle>());
										break;
									case 1:
										Main.LocalPlayer.QuickSpawnItem(ModContent.ItemType<Items.Weapons.Ranged.HakkePulseRifle>());
										break;
									case 2:
										Main.LocalPlayer.QuickSpawnItem(ModContent.ItemType<Items.Weapons.Ranged.HakkeScoutRifle>());
										break;
									case 3:
										Main.LocalPlayer.QuickSpawnItem(ModContent.ItemType<Items.Weapons.Ranged.HakkeShotgun>());
										break;
									case 4:
										Main.LocalPlayer.QuickSpawnItem(ModContent.ItemType<Items.Weapons.Ranged.HakkeSidearm>());
										break;
									case 5:
										Main.LocalPlayer.QuickSpawnItem(ModContent.ItemType<Items.Weapons.Ranged.HakkeSniper>());
										break;
									case 6:
										Main.LocalPlayer.QuickSpawnItem(ModContent.ItemType<Items.Weapons.Ranged.HakkeRocketLauncher>());
										break;
									case 7:
										Main.LocalPlayer.QuickSpawnItem(ModContent.ItemType<Items.Weapons.Ranged.HakkeGrenadeLauncher>());
										break;
									case 8:
										Main.LocalPlayer.QuickSpawnItem(ModContent.ItemType<Items.Weapons.Ranged.HakkeHandcannon>());
										break;
									default:
										Main.LocalPlayer.QuickSpawnItem(ItemID.SilverCoin, Main.rand.Next(1, 10));
										break;
								}
							}
							else if (_vanillaItemSlot.Item.type == ModContent.ItemType<UncommonEngram>()) {
								switch (Main.rand.Next(20)) {
									case 0:
										Main.LocalPlayer.QuickSpawnItem(ModContent.ItemType<Items.Weapons.Ranged.OmolonAutoRifle>());
										break;
									case 1:
										Main.LocalPlayer.QuickSpawnItem(ModContent.ItemType<Items.Weapons.Ranged.OmolonPulseRifle>());
										break;
									case 2:
										Main.LocalPlayer.QuickSpawnItem(ModContent.ItemType<Items.Weapons.Ranged.OmolonScoutRifle>());
										break;
									case 3:
										Main.LocalPlayer.QuickSpawnItem(ModContent.ItemType<Items.Weapons.Ranged.OmolonShotgun>());
										break;
									case 4:
										Main.LocalPlayer.QuickSpawnItem(ModContent.ItemType<Items.Weapons.Ranged.OmolonSidearm>());
										break;
									case 5:
										Main.LocalPlayer.QuickSpawnItem(ModContent.ItemType<Items.Weapons.Ranged.OmolonSniper>());
										break;
									case 6:
										Main.LocalPlayer.QuickSpawnItem(ModContent.ItemType<Items.Weapons.Ranged.OmolonRocketLauncher>());
										break;
									case 7:
										Main.LocalPlayer.QuickSpawnItem(ModContent.ItemType<Items.Weapons.Ranged.OmolonGrenadeLauncher>());
										break;
									case 8:
										Main.LocalPlayer.QuickSpawnItem(ModContent.ItemType<Items.Weapons.Ranged.OmolonHandcannon>());
										break;
									default:
										Main.LocalPlayer.QuickSpawnItem(ItemID.SilverCoin, Main.rand.Next(10, 50));
										break;
								}
							}
							else if (_vanillaItemSlot.Item.type == ModContent.ItemType<RareEngram>()) {
								switch (Main.rand.Next(20)) {
									case 0:
										Main.LocalPlayer.QuickSpawnItem(ModContent.ItemType<Items.Weapons.Ranged.SurosAutoRifle>());
										break;
									case 1:
										Main.LocalPlayer.QuickSpawnItem(ModContent.ItemType<Items.Weapons.Ranged.SurosPulseRifle>());
										break;
									case 2:
										Main.LocalPlayer.QuickSpawnItem(ModContent.ItemType<Items.Weapons.Ranged.SurosScoutRifle>());
										break;
									case 3:
										Main.LocalPlayer.QuickSpawnItem(ModContent.ItemType<Items.Weapons.Ranged.SurosShotgun>());
										break;
									case 4:
										Main.LocalPlayer.QuickSpawnItem(ModContent.ItemType<Items.Weapons.Ranged.SurosSidearm>());
										break;
									case 5:
										Main.LocalPlayer.QuickSpawnItem(ModContent.ItemType<Items.Weapons.Ranged.SurosSniper>());
										break;
									case 6:
										Main.LocalPlayer.QuickSpawnItem(ModContent.ItemType<Items.Weapons.Ranged.SurosRocketLauncher>());
										break;
									case 7:
										Main.LocalPlayer.QuickSpawnItem(ModContent.ItemType<Items.Weapons.Ranged.SurosGrenadeLauncher>());
										break;
									case 8:
										Main.LocalPlayer.QuickSpawnItem(ModContent.ItemType<Items.Weapons.Ranged.SurosHandcannon>());
										break;
									default:
										Main.LocalPlayer.QuickSpawnItem(ItemID.GoldCoin, Main.rand.Next(1, 3));
										break;
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