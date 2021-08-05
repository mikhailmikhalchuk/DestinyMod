using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.UI;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.GameContent.Events;
using TheDestinyMod.Items.Weapons.Ranged;

namespace TheDestinyMod.NPCs.Town
{
	[AutoloadHead]
	public class Zavala : ModNPC
	{
		private bool hasClosedCeremony = false;

		public override string Texture => "TheDestinyMod/NPCs/Town/Zavala";

		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Titan Vanguard");
			DisplayName.AddTranslation(GameCulture.Polish, "Tytanowa Straż przednia");
			Main.npcFrameCount[npc.type] = 25;
			NPCID.Sets.ExtraFramesCount[npc.type] = 9;
			NPCID.Sets.AttackFrameCount[npc.type] = 4;
			NPCID.Sets.DangerDetectRange[npc.type] = 700;
			NPCID.Sets.AttackType[npc.type] = 1;
			NPCID.Sets.AttackTime[npc.type] = 30;
			NPCID.Sets.AttackAverageChance[npc.type] = 30;
			NPCID.Sets.HatOffsetY[npc.type] = 4;
		}

		public override void SetDefaults() {
			npc.townNPC = true;
			npc.friendly = true;
			npc.width = 18;
			npc.height = 40;
			npc.aiStyle = 7;
			npc.damage = 10;
			npc.defense = 15;
			npc.lifeMax = 250;
			npc.HitSound = SoundID.NPCHit1;
			npc.DeathSound = SoundID.NPCDeath1;
			npc.knockBackResist = 0.5f;
			animationType = NPCID.Guide;
		}

		public override bool CanTownNPCSpawn(int numTownNPCs, int money) {
			return NPC.downedSlimeKing;
		}

		public override string TownNPCName() {
			return "Zavala";
		}

		public override string GetChat() {
			DestinyWorld.claimedItemsGG = hasClosedCeremony = true;
			if (TheDestinyMod.guardianWinner == 1 && !hasClosedCeremony) {
				if (!DestinyWorld.claimedItemsGG) {
					Main.LocalPlayer.QuickSpawnItem(ModContent.ItemType<Items.Placeables.Furniture.TitanFlag>());
				}
				return "Through a fierce competition and defying all odds, my Titans have punched their way to victory in this year's Guardian Games. Make no mistake; the determination of both the Warlocks and Hunters were admirable. To commemorate, here is some Titan-themed decor. Stay strong, Guardian.";
			}
			else if (TheDestinyMod.guardianWinner == 2 && !hasClosedCeremony) {
				if (!DestinyWorld.claimedItemsGG) {
					Main.LocalPlayer.QuickSpawnItem(ModContent.ItemType<Items.Placeables.Furniture.HunterFlag>());
				}
				return "Through a fierce competition and defying all odds, the Hunters have stealthily taken the win in this year's Guardian Games. Make no mistake; the determination of the Hunters was admirable, but us Titans will achieve victory next year. To commemorate, here is some Hunter-themed decor. Stay strong, Guardian.";
			}
			else if (TheDestinyMod.guardianWinner == 3 && !hasClosedCeremony) {
				if (!DestinyWorld.claimedItemsGG) {
					Main.LocalPlayer.QuickSpawnItem(ModContent.ItemType<Items.Placeables.Furniture.WarlockFlag>());
				}
				return "Through a fierce competition and defying all odds, the Warlocks have outsmarted every other class in this year's Guardian Games. Make no mistake; the determination of the Warlocks was admirable, but us Titans will achieve victory next year. To commemorate, here is some Warlock-themed decor. Stay strong, Guardian.";
			}
			if (Main.rand.NextBool(100)) {
                return "Whether we wanted it or not, we've stepped into a war with the Cabal on Mars. So let's get to taking out their command, one by one. Valus Ta'aurc. From what I can gather he commands the Siege Dancers from an Imperial Land Tank outside of Rubicon. He's well protected, but with the right team, we can punch through those defenses, take this beast out, and break their grip on Freehold.";
            }
            if (NPC.AnyDanger()) {
                switch (Main.rand.Next(3)) {
                    case 0:
                        return Language.GetTextValue("Mods.TheDestinyMod.Zavala1");
                    case 1:
                        return Language.GetTextValue("Mods.TheDestinyMod.Zavala2");
                    default:
                        return Language.GetTextValue("Mods.TheDestinyMod.Zavala3");
                }
            }
			List<string> dialogue = new List<string>();
            if (NPC.downedMoonlord) {
                dialogue.Add(Language.GetTextValue("Mods.TheDestinyMod.Zavala4"));
            }
			int theDrifter = NPC.FindFirstNPC(ModContent.NPCType<Drifter>());
			if (theDrifter >= 0) {
                dialogue.Add(Language.GetTextValue("Mods.TheDestinyMod.Zavala5"));
			}
			if (BirthdayParty.PartyIsUp) {
				dialogue.Add(Language.GetTextValue("Mods.TheDestinyMod.Zavala6"));
			}
			if (Main.LocalPlayer.ZoneCorrupt || Main.LocalPlayer.ZoneCrimson) {
				dialogue.Add(Language.GetTextValue("Mods.TheDestinyMod.Zavala7"));
			}
			if (Main.LocalPlayer.ZoneHoly) {
				dialogue.Add(Language.GetTextValue("Mods.TheDestinyMod.Zavala8"));
			}
			if (TheDestinyMod.guardianGames) {
				dialogue.Add(Language.GetTextValue("Mods.TheDestinyMod.ZavalaGG"));
			}
			dialogue.Add(Language.GetTextValue("Mods.TheDestinyMod.Zavala9"));
			dialogue.Add(Language.GetTextValue("Mods.TheDestinyMod.Zavala10"));
			dialogue.Add(Language.GetTextValue("Mods.TheDestinyMod.Zavala11"));
			dialogue.Add(Language.GetTextValue("Mods.TheDestinyMod.Zavala12"));
			dialogue.Add(Language.GetTextValue("Mods.TheDestinyMod.Zavala13"));
			return Main.rand.Next(dialogue);
		}

        public override void SetChatButtons(ref string button, ref string button2) {
            button = Language.GetTextValue("LegacyInterface.28");
            button2 = Language.GetTextValue("Mods.TheDestinyMod.Bounty");
        }

        public override void OnChatButtonClicked(bool firstButton, ref bool shop) {
			if (firstButton) {
				shop = true;
            }
            else {
                DestinyPlayer player = Main.LocalPlayer.GetModPlayer<DestinyPlayer>();
				if (player.zavalaBounty == 0) {
					Main.npcChatText = Language.GetTextValue("Mods.TheDestinyMod.ZavalaBounty1");
					player.zavalaBounty = 1;
				}
				else if (player.zavalaBounty == 1 && player.zavalaEnemies == 100) {
					Main.npcChatText = Language.GetTextValue("Mods.TheDestinyMod.ZavalaBounty2");
					Main.LocalPlayer.QuickSpawnItem(ModContent.ItemType<TheThirdAxiom>());
					player.zavalaBounty = 2;
					player.zavalaEnemies = 0;
				}
				else if (player.zavalaBounty == 2) {
					Main.npcChatText = "I've got another bounty for you, Guardian. The Dungeon is an evil place, filled with servants of the Darkness.\nI need you to slay 50 Skeletons to purge this infestation.";
					player.zavalaBounty = 3;
				}
				else if (player.zavalaBounty == 3 && player.zavalaEnemies == 50) {
					Main.npcChatText = "I can feel the settling of the Darkness from here. You've done well, Guardian.";
					Main.LocalPlayer.QuickSpawnItem(ModContent.ItemType<LastWord>());
					player.zavalaBounty = 4;
					player.zavalaEnemies = 0;
				}
				else if (player.zavalaBounty == 4) {
					Main.npcChatText = "I've got nothing for you right now, Guardian.";
				}
				else {
					if (player.zavalaBounty == 1) {
						Main.npcChatText = Language.GetTextValue("Mods.TheDestinyMod.ZavalaKilled1", player.zavalaEnemies);
					}
					else if (player.zavalaBounty == 3) {
						Main.npcChatText = $"Let's see here, Guardian. You've killed {player.zavalaEnemies}/50 Skeletons.";
					}
				}
            }
        }

        public override void SetupShop(Chest shop, ref int nextSlot) {
			shop.item[nextSlot].SetDefaults(ModContent.ItemType<AceOfSpades>());
            shop.item[nextSlot].shopCustomPrice = 1000000;
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ModContent.ItemType<Items.Accessories.TitanMark>());
			shop.item[nextSlot].shopCustomPrice = 0;
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ModContent.ItemType<Items.Accessories.WarlockMark>());
			shop.item[nextSlot].shopCustomPrice = 0;
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ModContent.ItemType<Items.Accessories.HunterMark>());
			shop.item[nextSlot].shopCustomPrice = 0;
			nextSlot++;
			if (TheDestinyMod.guardianGames) {
				shop.item[nextSlot].SetDefaults(ModContent.ItemType<Items.Placeables.Podium>());
				shop.item[nextSlot].shopCustomPrice = 50000;
				nextSlot++;
				shop.item[nextSlot].SetDefaults(ModContent.ItemType<Items.Placeables.Furniture.TitanFlag>());
				shop.item[nextSlot].shopCustomPrice = 10000;
				nextSlot++;
				shop.item[nextSlot].SetDefaults(ModContent.ItemType<Items.Placeables.Furniture.HunterFlag>());
				shop.item[nextSlot].shopCustomPrice = 10000;
				nextSlot++;
				shop.item[nextSlot].SetDefaults(ModContent.ItemType<Items.Placeables.Furniture.WarlockFlag>());
				shop.item[nextSlot].shopCustomPrice = 10000;
				nextSlot++;
				shop.item[nextSlot].SetDefaults(ModContent.ItemType<Items.Dyes.GuardianGamesDye>());
				shop.item[nextSlot].shopCustomPrice = 20000;
				nextSlot++;
			}
		}

		public override bool CanGoToStatue(bool toKingStatue) {
			return true;
		}

		public override void OnGoToStatue(bool toKingStatue) {
			if (Main.netMode == NetmodeID.Server) {
				ModPacket packet = mod.GetPacket();
				packet.Write((byte)npc.whoAmI);
				packet.Send();
			}
			else {
				StatueTeleport();
			}
		}

		public void StatueTeleport() {
			for (int i = 0; i < 30; i++) {
				Vector2 position = Main.rand.NextVector2Square(-20, 21);
				if (Math.Abs(position.X) > Math.Abs(position.Y)) {
					position.X = Math.Sign(position.X) * 20;
				}
				else {
					position.Y = Math.Sign(position.Y) * 20;
				}
			}
		}

		public override void TownNPCAttackStrength(ref int damage, ref float knockback) {
			damage = 20;
			knockback = 4f;
		}

		public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown) {
			cooldown = 30;
			randExtraCooldown = 30;
		}

        public override void DrawTownAttackGun(ref float scale, ref int item, ref int closeness) {
			scale = 0.7f;
			item = ModContent.ItemType<TheThirdAxiom>();
			closeness = 20;
		}

        public override void TownNPCAttackProj(ref int projType, ref int attackDelay) {
			projType = ProjectileID.Bullet;
			attackDelay = 1;
		}

		public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset) {
			multiplier = 12f;
			randomOffset = 2f;
		}
	}
}