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
				Color GetColorBasedOnRarity(int rarity) {
                    switch (rarity) {
                        case 1:
                            return new Color(150, 150, 255);
                        case 2:
                            return new Color(150, 255, 150);
                        case 3:
                            return new Color(255, 200, 150);
                        case 4:
                            return new Color(255, 150, 150);
                        case 5:
                            return new Color(255, 150, 255);
                        case -11:
                            return new Color(255, 175, 0);
                        case -1:
                            return new Color(130, 130, 130);
                        case 6:
                            return new Color(210, 160, 255);
                        case 7:
                            return new Color(150, 255, 10);
                        case 8:
                            return new Color(255, 255, 10);
                        case 9:
                            return new Color(5, 200, 255);
                        case 10:
                            return new Color(255, 40, 100);
                        default:
                            if (rarity >= 11) {
                                return new Color(180, 40, 255);
                            }
                            break;
                    }
                    return new Color();
				}
				DestinyPlayer dPlayer = Main.LocalPlayer.DestinyPlayer();
				ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, Main.fontMouseText, $"Decrypt {_vanillaItemSlot.Item.Name}", new Vector2(slotX + 50, slotY), new Color(Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor), 0f, Vector2.Zero, Vector2.One);
				ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, Main.fontMouseText, "Potential drops:", new Vector2(slotX + 100, slotY + 30), new Color(Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor), 0f, Vector2.Zero, Vector2.One);
				TextSnippet extSnip = null;
				if (_vanillaItemSlot.Item.type == ModContent.ItemType<CommonEngram>()) {
					int start = 55;
					foreach (int item in commonLoot) {
						int hoveredSnippet = -1;
						string toParse = $"[i:{item}] {ModContent.GetModItem(item).DisplayName.GetTranslation(Language.ActiveCulture)}";
						if (!dPlayer.commonItemsDecrypted.Contains(item)) {
							toParse = "???";
						}
						List<TextSnippet> list = ChatManager.ParseMessage(toParse, dPlayer.commonItemsDecrypted.Contains(item) ? GetColorBasedOnRarity(ModContent.GetModItem(item).item.rare) : Color.White);
						ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, Main.fontMouseText, list.ToArray(), new Vector2(slotX + 100, slotY + start), 0f, Vector2.Zero, Vector2.One, out hoveredSnippet);
						start += 25;
						if (hoveredSnippet > -1) {
							extSnip = list[0];
						}
					}
				}
				else if (_vanillaItemSlot.Item.type == ModContent.ItemType<UncommonEngram>()) {
					int start = 55;
					foreach (int item in uncommonLoot) {
						int hoveredSnippet = -1;
						string toParse = $"[i:{item}] {ModContent.GetModItem(item).DisplayName.GetTranslation(Language.ActiveCulture)}";
						if (!dPlayer.uncommonItemsDecrypted.Contains(item)) {
							toParse = "???";
						}
						List<TextSnippet> list = ChatManager.ParseMessage(toParse, dPlayer.uncommonItemsDecrypted.Contains(item) ? GetColorBasedOnRarity(ModContent.GetModItem(item).item.rare) : Color.White);
						ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, Main.fontMouseText, list.ToArray(), new Vector2(slotX + 100, slotY + start), 0f, Vector2.Zero, Vector2.One, out hoveredSnippet);
						start += 25;
						if (hoveredSnippet > -1) {
							extSnip = list[0];
						}
					}
				}
				else if (_vanillaItemSlot.Item.type == ModContent.ItemType<RareEngram>()) {
					int start = 55;
					foreach (int item in rareLoot) {
						int hoveredSnippet = -1;
						string toParse = $"[i:{item}] {ModContent.GetModItem(item).DisplayName.GetTranslation(Language.ActiveCulture)}";
						if (!dPlayer.rareItemsDecrypted.Contains(item)) {
							toParse = "???";
						}
						List<TextSnippet> list = ChatManager.ParseMessage(toParse, dPlayer.rareItemsDecrypted.Contains(item) ? GetColorBasedOnRarity(ModContent.GetModItem(item).item.rare) : Color.White);
						ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, Main.fontMouseText, list.ToArray(), new Vector2(slotX + 100, slotY + start), 0f, Vector2.Zero, Vector2.One, out hoveredSnippet);
						start += 25;
						if (hoveredSnippet > -1) {
							extSnip = list[0];
						}
					}
				}
				extSnip?.OnHover();
				const int decryptX = slotX + 70;
				const int decryptY = slotY + 40;
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
					void GiveEngramItem() {
						if (_vanillaItemSlot.Item.type == ModContent.ItemType<CommonEngram>()) {
							if (Main.rand.Next(20) > commonLoot.Count) {
								Main.LocalPlayer.QuickSpawnItem(ItemID.SilverCoin, Main.rand.Next(1, 10));
							}
							else {
								int random = Main.rand.Next(commonLoot);
								Main.LocalPlayer.QuickSpawnItem(random);
								dPlayer.commonItemsDecrypted.Add(random);
							}
						}
						else if (_vanillaItemSlot.Item.type == ModContent.ItemType<UncommonEngram>()) {
							if (Main.rand.Next(20) > uncommonLoot.Count) {
								Main.LocalPlayer.QuickSpawnItem(ItemID.SilverCoin, Main.rand.Next(10, 50));
							}
							else {
								int random = Main.rand.Next(uncommonLoot);
								Main.LocalPlayer.QuickSpawnItem(random);
								dPlayer.uncommonItemsDecrypted.Add(random);
							}
						}
						else if (_vanillaItemSlot.Item.type == ModContent.ItemType<RareEngram>()) {
							if (Main.rand.Next(20) > rareLoot.Count) {
								Main.LocalPlayer.QuickSpawnItem(ItemID.GoldCoin, Main.rand.Next(1, 3));
							}
							else {
								int random = Main.rand.Next(rareLoot);
								Main.LocalPlayer.QuickSpawnItem(random);
								dPlayer.rareItemsDecrypted.Add(random);
							}
						}
						_vanillaItemSlot.Item.stack--;
					}
					if (Main.mouseLeftRelease && Main.mouseLeft) {
						while (_vanillaItemSlot.Item.stack > 0) {
							GiveEngramItem();
						}
						_vanillaItemSlot.Item.TurnToAir();
						Main.PlaySound(SoundID.Item37, -1, -1);
					}
					else if (Main.mouseRightRelease && Main.mouseRight) {
						GiveEngramItem();
						Main.PlaySound(SoundID.Item37, -1, -1);
					}
				}
				else {
					tickPlayed = false;
				}
			}
			else {
				ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, Main.fontMouseText, "Place an Engram here to decrypt it", new Vector2(slotX + 50, slotY), new Color(Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor), 0f, Vector2.Zero, Vector2.One, -1f, 2f);
			}
		}
	}
}