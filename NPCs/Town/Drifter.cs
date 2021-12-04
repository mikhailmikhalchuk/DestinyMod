using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.GameContent.Events;
using Terraria.GameContent.Biomes;
using Terraria.Localization;
using Terraria.ModLoader;
using TheDestinyMod.Items.Materials;
using TheDestinyMod.Items.Dyes;
using TheDestinyMod.Items.Pets;
using TheDestinyMod.Items.Weapons.Ranged;
using TheDestinyMod.Items.Potions;

namespace TheDestinyMod.NPCs.Town
{
	[AutoloadHead]
	public class Drifter : ModNPC
	{
		public override string Texture => "TheDestinyMod/NPCs/Town/Drifter";

		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Dredgen");
			Main.npcFrameCount[npc.type] = 26;
			NPCID.Sets.AttackFrameCount[npc.type] = 4;
			NPCID.Sets.ExtraFramesCount[npc.type] = 10;
			NPCID.Sets.DangerDetectRange[npc.type] = 700;
			NPCID.Sets.AttackType[npc.type] = 1;
			NPCID.Sets.AttackTime[npc.type] = 30;
			NPCID.Sets.AttackAverageChance[npc.type] = 30;
			NPCID.Sets.HatOffsetY[npc.type] = 8;
		}

		public override void SetDefaults() {
			npc.townNPC = true;
			npc.friendly = true;
			npc.width = 26;
			npc.height = 46;
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
			for (int k = 0; k < Main.maxPlayers; k++) {
				Player player = Main.player[k];
				if (!player.active) {
					continue;
				}
				if (player.CountItem(ModContent.ItemType<MoteOfDark>()) >= 5) {
					return true;
				}
			}
			return false;
		}

		public override string TownNPCName() {
			return "The Drifter";
		}

		public override string GetChat() {
			int taxCollector = NPC.FindFirstNPC(NPCID.TaxCollector);
			int xur = NPC.FindFirstNPC(ModContent.NPCType<AgentOfNine>());
			string gender = Main.LocalPlayer.Male ? Language.GetTextValue("Mods.TheDestinyMod.Brother") : Language.GetTextValue("Mods.TheDestinyMod.Sister");
			List<string> dialogue = new List<string>();
			if (taxCollector >= 0) {
				int zavala = NPC.FindFirstNPC(ModContent.NPCType<Zavala>());
				if (zavala >= 0) {
					dialogue.Add(Language.GetTextValue("Mods.TheDestinyMod.Drifter16", Main.npc[taxCollector].GivenName, gender));
				}
				else {
					dialogue.Add(Language.GetTextValue("Mods.TheDestinyMod.Drifter17", Main.npc[taxCollector].GivenName, gender));
				}
			}
			if (xur >= 0) {
				dialogue.Add(Language.GetTextValue("Mods.TheDestinyMod.Drifter15", gender));
			}
			if (BirthdayParty.PartyIsUp && Main.rand.NextBool(6)) {
				dialogue.Add(Language.GetTextValue("Mods.TheDestinyMod.Drifter14", gender));
			}
			if (Main.eclipse) {
				dialogue.Add(Language.GetTextValue("Mods.TheDestinyMod.Drifter13"));
			}
			if (Main.hardMode) {
				dialogue.Add(Language.GetTextValue("Mods.TheDestinyMod.Drifter10"));
				dialogue.Add(Language.GetTextValue("Mods.TheDestinyMod.Drifter11"));
				dialogue.Add(Language.GetTextValue("Mods.TheDestinyMod.Drifter12"));
			}
			if (Main.invasionType > 0 && Main.invasionDelay == 0 && Main.invasionSize > 0) {
				dialogue.Add(Language.GetTextValue("Mods.TheDestinyMod.Drifter18"));
			}
			dialogue.Add(Language.GetTextValue("Mods.TheDestinyMod.Drifter1"));
			dialogue.Add(Language.GetTextValue("Mods.TheDestinyMod.Drifter2", gender));
			dialogue.Add(Language.GetTextValue("Mods.TheDestinyMod.Drifter3"));
			dialogue.Add(Language.GetTextValue("Mods.TheDestinyMod.Drifter4"));
			dialogue.Add(Language.GetTextValue("Mods.TheDestinyMod.Drifter5"));
			dialogue.Add(Language.GetTextValue("Mods.TheDestinyMod.Drifter6"));
			dialogue.Add(Language.GetTextValue("Mods.TheDestinyMod.Drifter7"));
			dialogue.Add(Language.GetTextValue("Mods.TheDestinyMod.Drifter8"));
			dialogue.Add(Language.GetTextValue("Mods.TheDestinyMod.Drifter9"));
			return Main.rand.Next(dialogue);
		}

        public override void SetChatButtons(ref string button, ref string button2) {
			button = Language.GetTextValue("Mods.TheDestinyMod.GiveMotes");
            button2 = Language.GetTextValue("Mods.TheDestinyMod.CheckMotes");
		}

        public override void OnChatButtonClicked(bool firstButton, ref bool shop) {
            DestinyPlayer player = Main.LocalPlayer.GetModPlayer<DestinyPlayer>();
			string gender = Main.LocalPlayer.Male ? Language.GetTextValue("Mods.TheDestinyMod.Brother") : Language.GetTextValue("Mods.TheDestinyMod.Sister");
			if (firstButton) {
				if (Main.LocalPlayer.HasItem(ModContent.ItemType<MoteOfDark>())) {
					if (Main.LocalPlayer.CountItem(ModContent.ItemType<MoteOfDark>()) < 25) {
						Main.npcChatText = Language.GetTextValue("Mods.TheDestinyMod.DrifterMotes1", gender);
					}
					else if (Main.LocalPlayer.CountItem(ModContent.ItemType<MoteOfDark>()) > 25 && Main.LocalPlayer.CountItem(ModContent.ItemType<MoteOfDark>()) < 50) {
						Main.npcChatText = Language.GetTextValue("Mods.TheDestinyMod.DrifterMotes2");
					}
					else {
						switch (Main.rand.Next(3)) {
							case 1:
								Main.npcChatText = Language.GetTextValue("Mods.TheDestinyMod.DrifterMotes3", gender);
								break;
							case 2:
								Main.npcChatText = Language.GetTextValue("Mods.TheDestinyMod.DrifterMotes4");
								break;
							default:
								Main.npcChatText = Language.GetTextValue("Mods.TheDestinyMod.DrifterMotes5");
								break;
						}
					}
					player.motesGiven += Main.LocalPlayer.CountItem(ModContent.ItemType<MoteOfDark>());
					foreach (Item item in Main.LocalPlayer.inventory) {
						if (item.type == ModContent.ItemType<MoteOfDark>()) {
							item.TurnToAir();
						}
					}
					Main.PlaySound(SoundID.Grab);
					if (player.motesGiven > 10 && player.drifterRewards == 0) {
						player.drifterRewards = 1;
						Main.LocalPlayer.QuickSpawnItem(ItemID.GoldCoin, 5);
						Main.npcChatText = Language.GetTextValue("Mods.TheDestinyMod.DrifterMotes6", gender);
					}
					if (player.motesGiven > 20 && player.drifterRewards < 2) {
						player.drifterRewards = 2;
						Main.LocalPlayer.QuickSpawnItem(ModContent.ItemType<ImpactShard>());
						Main.npcChatText = Language.GetTextValue("Mods.TheDestinyMod.DrifterMotes7", gender);
					}
					if (player.motesGiven > 30 && player.drifterRewards < 3) {
						player.drifterRewards = 3;
						Main.LocalPlayer.QuickSpawnItem(ModContent.ItemType<BottomDollar>());
						Main.npcChatText = Language.GetTextValue("Mods.TheDestinyMod.DrifterMotes8");
					}
					if (player.motesGiven > 40 && player.drifterRewards < 4) {
						player.drifterRewards = 4;
						Main.LocalPlayer.QuickSpawnItem(ModContent.ItemType<GambitDye>(), 3);
						Main.npcChatText = Language.GetTextValue("Mods.TheDestinyMod.DrifterMotes9", gender);
					}
					if (player.motesGiven > 50 && player.drifterRewards < 5) {
						player.drifterRewards = 5;
						Main.LocalPlayer.QuickSpawnItem(ModContent.ItemType<GunsmithMaterials>(), 100);
						Main.npcChatText = Language.GetTextValue("Mods.TheDestinyMod.DrifterMotes10", gender);
					}
					if (player.motesGiven > 60 && player.drifterRewards < 6) {
						player.drifterRewards = 6;
						Main.LocalPlayer.QuickSpawnItem(ModContent.ItemType<Ghost>());
						Main.npcChatText = Language.GetTextValue("Mods.TheDestinyMod.DrifterMotes11", gender);
					}
					if (player.motesGiven > 70 && player.drifterRewards < 7) {
						player.drifterRewards = 7;
						Main.LocalPlayer.QuickSpawnItem(ModContent.ItemType<TrinarySystem>());
						Main.npcChatText = Language.GetTextValue("Mods.TheDestinyMod.DrifterMotes12", gender);
					}
					return;
				}
                else {
					switch (Main.rand.Next(3)) {
						case 1:
							Main.npcChatText = Language.GetTextValue("Mods.TheDestinyMod.DrifterMotes13", char.ToUpper(gender[0]) + gender.Substring(1));
							return;
						case 2:
							Main.npcChatText = Language.GetTextValue("Mods.TheDestinyMod.DrifterMotes14");
							return;
						default:
							Main.npcChatText = Language.GetTextValue("Mods.TheDestinyMod.DrifterMotes15", gender);
							return;
					}
                }
			}
            else {
				if (player.motesGiven == 0) {
					Main.npcChatText = Language.GetTextValue("Mods.TheDestinyMod.DrifterMotes16", gender);
				}
				else if (player.motesGiven > 200) {
					Main.npcChatText = Language.GetTextValue("Mods.TheDestinyMod.DrifterMotes17", player.motesGiven, gender);
				}
                else if (player.motesGiven > 100) {
					Main.npcChatText = Language.GetTextValue("Mods.TheDestinyMod.DrifterMotes18", player.motesGiven, gender);
				}
				else {
					Main.npcChatText = Language.GetTextValue("Mods.TheDestinyMod.DrifterMotes19", player.motesGiven, gender);
				}
				return;
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
			if (Main.hardMode) {
				damage = 40;
			}
			if (NPC.downedMechBossAny) {
				damage = 60;
			}
		}

		public override void DrawTownAttackGun(ref float scale, ref int item, ref int closeness) {
			scale = 0.5f;
			item = ModContent.ItemType<BottomDollar>();
			closeness = 20;
		}

		public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown) {
			cooldown = 30;
			randExtraCooldown = 30;
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