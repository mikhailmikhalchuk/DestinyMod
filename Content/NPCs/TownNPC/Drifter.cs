using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.GameContent.Events;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Audio;
using DestinyMod.Content.Items.Materials;
using DestinyMod.Common.NPCs.NPCTypes;
using DestinyMod.Content.Items.Weapons.Ranged;
using DestinyMod.Common.ModPlayers;
using DestinyMod.Content.Items.Equipables.Pets;
using DestinyMod.Content.Items.Consumables.Potions;
using DestinyMod.Content.Items.Equipables.Dyes;
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.Personalities;

namespace DestinyMod.Content.NPCs.TownNPC
{
	[AutoloadHead]
	public class Drifter : GenericTownNPC
	{
		public override void DestinySetStaticDefaults()
		{
			DisplayName.SetDefault("Dredgen");

			NPC.Happiness
				.SetNPCAffection(ModContent.NPCType<Zavala>(), AffectionLevel.Hate)
				.SetNPCAffection(NPCID.Pirate, AffectionLevel.Like)
			;
		}

		public override List<string> SetNPCNameList() => new List<string> { "The Drifter" };

		public override void DestinySetDefaults()
		{
			NPC.width = 26;
			NPC.height = 46;
		}

		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,

				new FlavorTextBestiaryInfoElement("Mods.DestinyMod.Bestiary.Drifter")
			});
		}

		public override bool CanTownNPCSpawn(int numTownNPCs, int money)
		{
			for (int playerCount = 0; playerCount < Main.maxPlayers; playerCount++)
			{
				Player player = Main.player[playerCount];
				if (!player.active || player.CountItem(ModContent.ItemType<MoteOfDark>()) < 5)
				{
					continue;
				}

				return true;
			}
			return false;
		}

		public override void SetChatButtons(ref string button, ref string button2)
		{
			button = Language.GetTextValue("Mods.DestinyMod.Common.GiveMotes");
			button2 = Language.GetTextValue("Mods.DestinyMod.Common.CheckMotes");
		}

		public override void OnChatButtonClicked(bool firstButton, ref bool shop)
		{
			Player player = Main.LocalPlayer;
			NPCPlayer npcPlayer = player.GetModPlayer<NPCPlayer>();
			string gender = player.Male ? Language.GetTextValue("Mods.DestinyMod.Common.Brother") : Language.GetTextValue("Mods.DestinyMod.Common.Sister");
			if (firstButton)
			{
				if (player.HasItem(ModContent.ItemType<MoteOfDark>()))
				{
					int playerMoteCount = player.CountItem(ModContent.ItemType<MoteOfDark>());
					if (playerMoteCount < 25)
					{
						Main.npcChatText = Language.GetTextValue("Mods.DestinyMod.Drifter.Motes1", gender);
					}
					else if (playerMoteCount > 25 && playerMoteCount < 50)
					{
						Main.npcChatText = Language.GetTextValue("Mods.DestinyMod.Drifter.Motes2");
					}
					else
					{
						Main.npcChatText = Language.GetTextValue("Mods.DestinyMod.Drifter.Motes" + Main.rand.Next(3, 6), gender);
					}

					int oldMoteCount = npcPlayer.MotesGiven;
					npcPlayer.MotesGiven += playerMoteCount;
					foreach (Item item in player.inventory)
					{
						if (item.type == ModContent.ItemType<MoteOfDark>())
						{
							item.TurnToAir();
						}
					}

					SoundEngine.PlaySound(SoundID.Grab);
					int checkImpact = (npcPlayer.MotesGiven - oldMoteCount) / 10;
					int checkMoney = (npcPlayer.MotesGiven - oldMoteCount) / 25;
					EntitySource_Gift source = new EntitySource_Gift(NPC);
					if (checkImpact >= 1 && npcPlayer.MotesGiven > 20)
					{
						player.QuickSpawnItem(source, ModContent.ItemType<ImpactShard>(), checkImpact);
						Main.npcChatText = Language.GetTextValue("Mods.DestinyMod.Drifter.MotesRepeatable" + Main.rand.Next(1, 3), gender);
					}

					if (checkMoney >= 1 && npcPlayer.MotesGiven > 20)
					{
						player.QuickSpawnItem(source, ItemID.GoldCoin, checkImpact * 20);
						Main.npcChatText = Language.GetTextValue("Mods.DestinyMod.Drifter.MoneyRepeatable" + Main.rand.Next(1, 3), gender);
					}

					if (npcPlayer.MotesGiven > 10 && oldMoteCount < 10)
					{
						player.QuickSpawnItem(source, ItemID.GoldCoin, 5);
						Main.npcChatText = Language.GetTextValue("Mods.DestinyMod.Drifter.MotesReward1", gender);
					}

					if (npcPlayer.MotesGiven > 20 && oldMoteCount < 20)
					{
						player.QuickSpawnItem(source, ModContent.ItemType<ImpactShard>(), 5);
						Main.npcChatText = Language.GetTextValue("Mods.DestinyMod.Drifter.MotesReward2", gender);
					}

					if (npcPlayer.MotesGiven > 30 && oldMoteCount < 30)
					{
						player.QuickSpawnItem(source, ModContent.ItemType<BottomDollar>());
						Main.npcChatText = Language.GetTextValue("Mods.DestinyMod.Drifter.MotesReward3");
					}

					if (npcPlayer.MotesGiven > 40 && oldMoteCount < 40)
					{
						player.QuickSpawnItem(source, ModContent.ItemType<GambitDye>(), 3);
						Main.npcChatText = Language.GetTextValue("Mods.DestinyMod.Drifter.MotesReward4", gender);
					}

					if (npcPlayer.MotesGiven > 50 && oldMoteCount < 50)
					{
						player.QuickSpawnItem(source, ModContent.ItemType<GunsmithMaterials>(), 100);
						Main.npcChatText = Language.GetTextValue("Mods.DestinyMod.Drifter.MotesReward5", gender);
					}

					if (npcPlayer.MotesGiven > 60 && oldMoteCount < 60)
					{
						player.QuickSpawnItem(source, ModContent.ItemType<Ghost>());
						Main.npcChatText = Language.GetTextValue("Mods.DestinyMod.Drifter.MotesReward6", gender);
					}

					if (npcPlayer.MotesGiven > 70 && oldMoteCount < 70)
					{
						player.QuickSpawnItem(source, ModContent.ItemType<TrinarySystem>());
						Main.npcChatText = Language.GetTextValue("Mods.DestinyMod.Drifter.MotesReward7", gender);
					}

					if (npcPlayer.MotesGiven > 100 && oldMoteCount < 100)
					{
						player.QuickSpawnItem(source, ModContent.ItemType<BorrowedTime>());
						Main.npcChatText = Language.GetTextValue("Mods.DestinyMod.Drifter.MotesReward8", gender);
					}
					return;
				}
				else
				{
					switch (Main.rand.Next(3))
					{
						case 1:
							Main.npcChatText = Language.GetTextValue("Mods.DestinyMod.Drifter.NoMotes1", char.ToUpper(gender[0]) + gender.Substring(1));
							break; ;

						case 2:
							Main.npcChatText = Language.GetTextValue("Mods.DestinyMod.Drifter.NoMotes2");
							break;

						default:
							Main.npcChatText = Language.GetTextValue("Mods.DestinyMod.Drifter.NoMotes3", gender);
							break;
					}
				}

				return;
			}

			if (npcPlayer.MotesGiven <= 0)
			{
				Main.npcChatText = Language.GetTextValue("Mods.DestinyMod.Drifter.CheckMotes1", gender);
			}
			else if (npcPlayer.MotesGiven < 100)
			{
				Main.npcChatText = Language.GetTextValue("Mods.DestinyMod.Drifter.CheckMotes4", npcPlayer.MotesGiven, gender);
			}
			else if (npcPlayer.MotesGiven < 200)
			{
				Main.npcChatText = Language.GetTextValue("Mods.DestinyMod.Drifter.CheckMotes3", npcPlayer.MotesGiven, gender);
			}
			else
			{
				Main.npcChatText = Language.GetTextValue("Mods.DestinyMod.Drifter.CheckMotes2", npcPlayer.MotesGiven, gender);
			}
		}

		public override string GetChat()
		{
			int taxCollector = NPC.FindFirstNPC(NPCID.TaxCollector);
			bool xur = NPC.AnyNPCs(ModContent.NPCType<AgentOfNine>());
			string gender = Main.LocalPlayer.Male ? Language.GetTextValue("Mods.DestinyMod.Common.Brother") : Language.GetTextValue("Mods.DestinyMod.Common.Sister");
			List<string> dialogue = new List<string>();
			if (taxCollector >= 0)
			{
				string taxCollectorName = Main.npc[taxCollector].GivenName;
				dialogue.Add(Language.GetTextValue("Mods.DestinyMod.Drifter.Chatter_" + (NPC.AnyNPCs(ModContent.NPCType<Zavala>()) ? 14 : 15), taxCollectorName, gender));
			}

			if (xur)
			{
				dialogue.Add(Language.GetTextValue("Mods.DestinyMod.Drifter.Chatter_13", gender));
			}

			if (BirthdayParty.PartyIsUp && Main.rand.NextBool(6))
			{
				dialogue.Add(Language.GetTextValue("Mods.DestinyMod.Drifter.Party", gender));
			}

			if (Main.eclipse)
			{
				dialogue.Add(Language.GetTextValue("Mods.DestinyMod.Drifter.Eclipse"));
			}

			if (Main.hardMode)
			{
				dialogue.Add(Language.GetTextValue("Mods.DestinyMod.Drifter.Chatter_10"));
				dialogue.Add(Language.GetTextValue("Mods.DestinyMod.Drifter.Chatter_11"));
				dialogue.Add(Language.GetTextValue("Mods.DestinyMod.Drifter.Chatter_12"));
			}

			if (Main.invasionType > 0 && Main.invasionDelay == 0 && Main.invasionSize > 0)
			{
				dialogue.Add(Language.GetTextValue("Mods.DestinyMod.Drifter.Invasion"));
			}

			for (int dialogueCount = 1; dialogueCount < 9; dialogueCount++)
			{
				if (dialogueCount == 2)
				{
					dialogue.Add(Language.GetTextValue("Mods.DestinyMod.Drifter.Chatter_2", gender));
					continue;
				}

				dialogue.Add(Language.GetTextValue("Mods.DestinyMod.Drifter.Chatter_" + dialogueCount));
			}

			return Main.rand.Next(dialogue);
		}

		public override void TownNPCAttackStrength(ref int damage, ref float knockback)
		{
			damage = 20;
			knockback = 4f;

			if (Main.hardMode)
			{
				damage = 40;
			}

			if (NPC.downedMechBossAny)
			{
				damage = 60;
			}
		}

		public override void DrawTownAttackGun(ref float scale, ref int item, ref int closeness)
		{
			scale = 0.5f;
			item = ModContent.ItemType<BottomDollar>();
			closeness = 20;
		}
	}
}