using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.GameContent.Events;
using DestinyMod.Common.NPCs.NPCTypes;
using DestinyMod.Content.Items.Weapons.Ranged;
using DestinyMod.Content.Items.Placeables.Furniture;
using DestinyMod.Content.Items.Equipables.Dyes;
using DestinyMod.Content.Items.Placeables;
using DestinyMod.Common.ModSystems;
using DestinyMod.Common.ModPlayers;

namespace DestinyMod.Content.NPCs.Town
{
	[AutoloadHead]
	public class Zavala : GenericTownNPC
	{
		private bool hasClosedCeremony = false;

		public override void DestinySetStaticDefaults()
		{
			DisplayName.SetDefault("Titan Vanguard");
			Main.npcFrameCount[NPC.type] = 25;
			NPCID.Sets.ExtraFramesCount[NPC.type] = 9;
			NPCID.Sets.AttackFrameCount[NPC.type] = 4;
		}

		public override void DestinySetDefaults()
		{
			NPC.width = 18;
			NPC.height = 40;
		}

		public override bool CanTownNPCSpawn(int numTownNPCs, int money) => NPC.downedSlimeKing;

		public override string TownNPCName() => "Zavala";

		public override string GetChat()
		{
			DestinyWorld.claimedItemsGG = hasClosedCeremony = true;
			if (TheDestinyMod.guardianWinner == 1 && !hasClosedCeremony)
			{
				if (!DestinyWorld.claimedItemsGG)
				{
					Main.LocalPlayer.QuickSpawnItem(ModContent.ItemType<Items.Placeables.Furniture.TitanFlag>());
				}
				return Language.GetTextValue("Mods.TheDestinyMod.Zavala.GuardianGamesTitanWin");
			}
			else if (TheDestinyMod.guardianWinner == 2 && !hasClosedCeremony)
			{
				if (!DestinyWorld.claimedItemsGG)
				{
					Main.LocalPlayer.QuickSpawnItem(ModContent.ItemType<Items.Placeables.Furniture.HunterFlag>());
				}
				return Language.GetTextValue("Mods.TheDestinyMod.Zavala.GuardianGamesHunterWin");
			}
			else if (TheDestinyMod.guardianWinner == 3 && !hasClosedCeremony)
			{
				if (!DestinyWorld.claimedItemsGG)
				{
					Main.LocalPlayer.QuickSpawnItem(ModContent.ItemType<WarlockFlag>());
				}
				return Language.GetTextValue("Mods.TheDestinyMod.Zavala.GuardianGamesWarlockWin");
			}

			if (Main.rand.NextBool(100))
			{
				return "Whether we wanted it or not, we've stepped into a war with the Cabal on Mars. So let's get to taking out their command, one by one. Valus Ta'aurc. From what I can gather he commands the Siege Dancers from an Imperial Land Tank outside of Rubicon. He's well protected, but with the right team, we can punch through those defenses, take this beast out, and break their grip on Freehold.";
			}

			if (NPC.AnyDanger())
			{
				return Language.GetTextValue("Mods.TheDestinyMod.Zavala.Boss" + Main.rand.Next(1, 4));
			}

			List<string> dialogue = new List<string>();
			if (NPC.downedMoonlord)
			{
				dialogue.Add(Language.GetTextValue("Mods.TheDestinyMod.Zavala.AfterML"));
			}

			if (NPC.FindFirstNPC(ModContent.NPCType<Drifter>()) >= 0)
			{
				dialogue.Add(Language.GetTextValue("Mods.TheDestinyMod.Zavala.Chatter_1"));
			}

			if (BirthdayParty.PartyIsUp)
			{
				dialogue.Add(Language.GetTextValue("Mods.TheDestinyMod.Zavala.Party"));
			}

			if (Main.LocalPlayer.ZoneCorrupt || Main.LocalPlayer.ZoneCrimson)
			{
				dialogue.Add(Language.GetTextValue("Mods.TheDestinyMod.Zavala.Evil"));
			}

			if (Main.LocalPlayer.ZoneHallow)
			{
				dialogue.Add(Language.GetTextValue("Mods.TheDestinyMod.Zavala.Hallow"));
			}

			if (GuardianGames.Active)
			{
				dialogue.Add(Language.GetTextValue("Mods.TheDestinyMod.Zavala.GuardianGames"));
			}

			for (int dialogueCount = 2; dialogueCount < 6; dialogueCount++)
			{
				dialogue.Add(Language.GetTextValue("Mods.TheDestinyMod.Zavala.Chatter_" + dialogueCount));
			}
			return Main.rand.Next(dialogue);
		}

		public override void SetChatButtons(ref string button, ref string button2)
		{
			button = Language.GetTextValue("LegacyInterface.28");
			button2 = Language.GetTextValue("Mods.TheDestinyMod.Common.Bounty");
		}

		public override void OnChatButtonClicked(bool firstButton, ref bool shop)
		{
			if (firstButton)
			{
				shop = true;
			}
			else
			{
				NPCPlayer player = Main.LocalPlayer.GetModPlayer<NPCPlayer>();
				switch (player.ZavalaBountyProgress)
				{
					case 0:
						Main.npcChatText = Language.GetTextValue("Mods.TheDestinyMod.ZavalaBounty1");
						player.ZavalaBountyProgress = 1;
						break;

					case 1:
						if (player.ZavalaEnemies == 100)
						{
							Main.npcChatText = Language.GetTextValue("Mods.TheDestinyMod.Zavala.BountyComplete1");
							Main.LocalPlayer.QuickSpawnItem(ModContent.ItemType<TheThirdAxiom>());
							player.ZavalaBountyProgress = 2;
							player.ZavalaEnemies = 0;
						}
						else
						{
							Main.npcChatText = Language.GetTextValue("Mods.TheDestinyMod.Zavala.BountyProgress1", player.ZavalaEnemies);
						}
						break;

					case 2:
						Main.npcChatText = Language.GetTextValue("Mods.TheDestinyMod.Zavala.BountyRequisition2");
						player.ZavalaBountyProgress = 3;
						break;

					case 3:
						if (player.ZavalaEnemies == 50)
						{
							Main.npcChatText = Language.GetTextValue("Mods.TheDestinyMod.Zavala.BountyComplete2");
							Main.LocalPlayer.QuickSpawnItem(ModContent.ItemType<LastWord>());
							player.ZavalaBountyProgress = 4;
							player.ZavalaEnemies = 0;
						}
						else
						{
							Main.npcChatText = Language.GetTextValue("Mods.TheDestinyMod.Zavala.BountyProgress2", player.ZavalaBountyProgress);
						}
						break;

					case 4:
					default:
						Main.npcChatText = "I've got nothing for you right now, Guardian.";
						break;
				}
			}
		}

		public override void SetupShop(Chest shop, ref int nextSlot)
		{
			shop.item[nextSlot].SetDefaults(ModContent.ItemType<AceOfSpades>());
			shop.item[nextSlot].shopCustomPrice = 1000000;
			nextSlot++;
			if (GuardianGames.Active)
			{
				shop.item[nextSlot].SetDefaults(ModContent.ItemType<Podium>());
				shop.item[nextSlot].shopCustomPrice = 50000;
				nextSlot++;
				shop.item[nextSlot].SetDefaults(ModContent.ItemType<TitanFlag>());
				shop.item[nextSlot].shopCustomPrice = 10000;
				nextSlot++;
				shop.item[nextSlot].SetDefaults(ModContent.ItemType<HunterFlag>());
				shop.item[nextSlot].shopCustomPrice = 10000;
				nextSlot++;
				shop.item[nextSlot].SetDefaults(ModContent.ItemType<WarlockFlag>());
				shop.item[nextSlot].shopCustomPrice = 10000;
				nextSlot++;
				shop.item[nextSlot].SetDefaults(ModContent.ItemType<GuardianGamesDye>());
				shop.item[nextSlot].shopCustomPrice = 20000;
				nextSlot++;
			}
		}

		public override void DrawTownAttackGun(ref float scale, ref int item, ref int closeness)
		{
			scale = 0.7f;
			item = ModContent.ItemType<TheThirdAxiom>();
			closeness = 20;
		}
	}
}