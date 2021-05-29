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
using TheDestinyMod.Projectiles.Ammo;
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
			for (int k = 0; k < 255; k++) {
				Player player = Main.player[k];
				if (!player.active) {
					continue;
				}
				foreach (Item item in player.inventory) {
					if (item.type == ModContent.ItemType<MoteOfDark>() && item.stack >= 5) {
						return true;
					}
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
			string gender = Main.LocalPlayer.Male ? "brother" : "sister";
			List<string> dialogue = new List<string>();
			if (taxCollector >= 0) {
				int zavala = NPC.FindFirstNPC(ModContent.NPCType<Zavala>());
				if (zavala >= 0) {
					dialogue.Add($"Gah, what's with {Main.npc[taxCollector].GivenName}, {gender}? He reminds me of ol' Zavala back at the Tower...what do you mean Zavala is here?!");
				}
				else {
					dialogue.Add($"Gah, what's with {Main.npc[taxCollector].GivenName}, {gender}? He reminds me of ol' Zavala back at the Tower, always killing all the fun...");
				}
			}
			if (xur >= 0) {
				dialogue.Add($"Oh, no, no, no, that CREEP is here again. When will him and these...\"Nine\" stop botherin' us, {gender}?");
			}
			if (BirthdayParty.PartyIsUp && Main.rand.NextBool(6)) {
				dialogue.Add($"Hey, {gender}, wanna top off this party with some Gambit?");
			}
			if (Main.eclipse) {
				dialogue.Add("What happened? Did the Taken Take the sun or somethin'?!");
			}
			if (Main.hardMode) {
				dialogue.Add("You think 'cause you released some fancy spirits, you're too good for me? Come on. Drifter needs his Motes.");
				dialogue.Add("Light isn’t the only source of power out here.");
				dialogue.Add("Light, Dark... Let me tell you, the only thing that matters is the hand holding the gun.");
			}
			dialogue.Add("How you livin'?");
			dialogue.Add($"Get me those Motes and I'll make you rich, {gender}, I promise.");
			dialogue.Add("Call me Drifter.");
			dialogue.Add("Ready to bang knuckles?");
			dialogue.Add("Transmat firing!");
			dialogue.Add("Let's be bad guys.");
			dialogue.Add("Motes of Light have always been a thing. Motes of Dark? I had to make 'em. One day, I may have to answer for that.");
			dialogue.Add("I see you lookin' at me like I'm nuts. You think all this is for nothing? That I do this cause I like it? You don't know the half of it.");
			dialogue.Add("Ah, all the stars in heaven! I am so... hungry.");
			return dialogue[Main.rand.Next(dialogue.Count)];
		}

        public override void SetChatButtons(ref string button, ref string button2) {
			button = Language.GetTextValue("Mods.TheDestinyMod.GiveMotes");
            button2 = Language.GetTextValue("Mods.TheDestinyMod.CheckMotes");
		}

        public override void OnChatButtonClicked(bool firstButton, ref bool shop) {
            DestinyPlayer player = Main.LocalPlayer.GetModPlayer<DestinyPlayer>();
			string gender = Main.LocalPlayer.Male ? "brother" : "sister";
			if (firstButton) {
				if (Main.LocalPlayer.HasItem(ModContent.ItemType<MoteOfDark>())) {
					if (Main.LocalPlayer.CountItem(ModContent.ItemType<MoteOfDark>()) < 25) {
						Main.npcChatText = $"Thank you, {gender}. I'll do something real special with these Motes, trust.";
					}
					else if (Main.LocalPlayer.CountItem(ModContent.ItemType<MoteOfDark>()) > 25 && Main.LocalPlayer.CountItem(ModContent.ItemType<MoteOfDark>()) < 50) {
						Main.npcChatText = "Ooh, that's quite the haul you have there, thank you very much. I'ma do something real special with these Motes, somethin' that'll make you shiver.";
					}
					else {
						switch (Main.rand.Next(3)) {
							case 1:
								Main.npcChatText = $"Thanks for the Motes, {gender}.";
								break;
							case 2:
								Main.npcChatText = "Nice work. The line between Light and Dark's gettin' thinner every day. Keep walking it.";
								break;
							default:
								Main.npcChatText = "Keep choosing the winning side, kid, and you'll keep on winning. Simple as that.";
								break;
						}
					}
					foreach (Item item in Main.LocalPlayer.inventory) {
						if (item.type == ModContent.ItemType<MoteOfDark>()) {
							player.motesGiven += Main.LocalPlayer.CountItem(ModContent.ItemType<MoteOfDark>());
							Main.LocalPlayer.inventory[Main.LocalPlayer.FindItem(ModContent.ItemType<MoteOfDark>())].TurnToAir();
						}
					}
					Main.PlaySound(SoundID.Grab);
					if (player.motesGiven > 10 && player.drifterRewards == 0) {
						player.drifterRewards = 1;
						Main.LocalPlayer.QuickSpawnItem(ItemID.GoldCoin, 5);
						Main.npcChatText = $"Hey {(Main.LocalPlayer.Male ? "brother" : "sister")}, thanks for the Motes. I said I'd make you rich, and I intend to keep that promise, unlike some others... Here, take this.";
					}
					if (player.motesGiven > 20 && player.drifterRewards < 2) {
						player.drifterRewards = 2;
						Main.LocalPlayer.QuickSpawnItem(ModContent.ItemType<ImpactShard>());
						Main.npcChatText = $"Motes? Motes! Speaking of Motes, I've gotta way for you to get more, faster. This is for you, {gender}.";
					}
					if (player.motesGiven > 30 && player.drifterRewards < 3) {
						player.drifterRewards = 3;
						Main.LocalPlayer.QuickSpawnItem(ModContent.ItemType<BottomDollar>());
						Main.npcChatText = $"I'll never get sick of Motes. Never. Anyways, here's your reward.";
					}
					if (player.motesGiven > 40 && player.drifterRewards < 4) {
						player.drifterRewards = 4;
						Main.LocalPlayer.QuickSpawnItem(ModContent.ItemType<GambitDye>(), 3);
						Main.npcChatText = $"Mmm, Motes...ooh, I think you're gonna like this, {gender}. Free merchandise, on the house!";
					}
					if (player.motesGiven > 50 && player.drifterRewards < 5) {
						player.drifterRewards = 5;
						Main.LocalPlayer.QuickSpawnItem(ModContent.ItemType<GunsmithMaterials>(), 100);
						Main.npcChatText = $"Thanks for the Motes! By the way, I found these parts lying around. Maybe you could put 'em to good use, {gender}?";
					}
					if (player.motesGiven > 60 && player.drifterRewards < 6) {
						player.drifterRewards = 6;
						Main.LocalPlayer.QuickSpawnItem(ModContent.ItemType<Ghost>());
						Main.npcChatText = $"These are some nice Motes...oh yeah, I found this...thing, on the ground. Not sure what it is, but it's yours now, {gender}.";
					}
					if (player.motesGiven > 70 && player.drifterRewards < 7) {
						player.drifterRewards = 7;
						Main.LocalPlayer.QuickSpawnItem(ModContent.ItemType<TrinarySystem>());
						Main.npcChatText = $"Motes, {gender}, Motes! Gotta unique weapon I hand-crafted just for you. Take care of it.";
					}
					return;
				}
                else {
					switch (Main.rand.Next(3)) {
						case 1:
							Main.npcChatText = $"{gender}, you gotta have those Motes on you! Come back when you got some.";
							return;
						case 2:
							Main.npcChatText = "Thanks for the...huh? You don't have any Motes for me to unload off 'ya!";
							return;
						default:
							Main.npcChatText = $"Hey, you gotta have Motes to deposit! You tryna cheat me? Just kidding, {gender}.";
							return;
					}
                }
			}
            else {
				if (player.motesGiven == 0) {
					Main.npcChatText = $"Aw man, {gender}, you haven't deposited any Motes yet!";
				}
				else if (player.motesGiven > 200) {
					Main.npcChatText = $"Woo, you've deposited {player.motesGiven} Motes! This is one heckuva collection, {gender}.";
				}
                else if (player.motesGiven > 100) {
					Main.npcChatText = $"You've deposited {player.motesGiven} Motes so far, {gender}. Not bad.";
				}
				else {
					Main.npcChatText = $"You've deposited a total of {player.motesGiven} Motes, {gender}.";
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