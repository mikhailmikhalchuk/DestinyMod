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
using TheDestinyMod.Items;

namespace TheDestinyMod.NPCs.Town
{
	[AutoloadHead]
	public class Cryptarch : ModNPC
	{
		public override string Texture => "TheDestinyMod/NPCs/Town/Cryptarch";

		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Cryptarch");
			DisplayName.AddTranslation(GameCulture.Polish, "Dekoder");
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
					if (item.type == ModContent.ItemType<CommonEngram>()) {
						return true;
					}
				}
			}
			return false;
		}

		public override string TownNPCName() {
			return "Master Rahool";
		}

		public override string GetChat() {
			List<string> dialogue = new List<string>();
			if (BirthdayParty.PartyIsUp) {
				dialogue.Add(Language.GetTextValue("Mods.TheDestinyMod.Cryptarch1"));
			}
			if (Main.eclipse) {
				dialogue.Add(Language.GetTextValue("Mods.TheDestinyMod.Cryptarch2"));
			}
			dialogue.Add(Language.GetTextValue("Mods.TheDestinyMod.Cryptarch3"));
			dialogue.Add(Language.GetTextValue("Mods.TheDestinyMod.Cryptarch4"));
			dialogue.Add(Language.GetTextValue("Mods.TheDestinyMod.Cryptarch5"));
			dialogue.Add(Language.GetTextValue("Mods.TheDestinyMod.Cryptarch6"));
			dialogue.Add(Language.GetTextValue("Mods.TheDestinyMod.Cryptarch7"));
			return dialogue[Main.rand.Next(dialogue.Count)];
		}

        public override void SetChatButtons(ref string button, ref string button2) {
            button = Language.GetTextValue("LegacyInterface.28");
            button2 = "Decrypt";
        }

        public override void OnChatButtonClicked(bool firstButton, ref bool shop) {
			if (firstButton) {
				shop = true;
            }
            else {
                Main.playerInventory = true;
                Main.npcChatText = "";
                ModContent.GetInstance<TheDestinyMod>().CryptarchUserInterface.SetState(new UI.CryptarchUI());
			}
        }

        public override void SetupShop(Chest shop, ref int nextSlot) {
			DestinyPlayer player = Main.LocalPlayer.GetModPlayer<DestinyPlayer>();
			if (!player.boughtCommon) {
				shop.item[nextSlot].SetDefaults(ModContent.ItemType<CommonEngram>());
            	shop.item[nextSlot].shopCustomPrice = 1000000;
			}
			if (!player.boughtUncommon && NPC.downedBoss2) {
				nextSlot++;
				shop.item[nextSlot].SetDefaults(ModContent.ItemType<UncommonEngram>());
				shop.item[nextSlot].shopCustomPrice = 1000000;
			}
			if (!player.boughtUncommon && NPC.downedMechBossAny) {
				nextSlot++;
				shop.item[nextSlot].SetDefaults(ModContent.ItemType<RareEngram>());
				shop.item[nextSlot].shopCustomPrice = 1000000;
			}
			nextSlot++;
		}

        public override bool CanGoToStatue(bool toKingStatue) => true;

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
			scale = 0.5f;
			item = ModContent.ItemType<Khvostov7G0X>();
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