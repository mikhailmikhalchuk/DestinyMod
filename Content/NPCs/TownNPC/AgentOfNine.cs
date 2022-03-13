using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using System;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.Chat;
using DestinyMod.Content.Items.Weapons.Ranged;
using DestinyMod.Common.NPCs.Data;
using DestinyMod.Common.NPCs.NPCTypes;
using DestinyMod.Content.Currency;
using Terraria.GameContent.Bestiary;

namespace DestinyMod.Content.NPCs.TownNPC
{
	[AutoloadHead]
	public class AgentOfNine : GenericTownNPC
	{
		public static double SpawnTime = double.MaxValue;

		public static List<NPCShopData> Shop = new List<NPCShopData>();

		public override void DestinySetStaticDefaults()
		{
			DisplayName.SetDefault("Agent of the Nine");

			NPCID.Sets.ActsLikeTownNPC[Type] = true;
			NPCID.Sets.SpawnsWithCustomName[Type] = true;
		}

		public override string TownNPCName() => "Xûr";

		public override void DestinySetDefaults()
		{
			NPC.width = 20;
			NPC.height = 46;
		}

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,

				new FlavorTextBestiaryInfoElement("Mods.DestinyMod.Bestiary.AgentOfNine")
			});
		}

        public override bool CanTownNPCSpawn(int numTownNPCs, int money) => false;

		public static void UpdateTravelingMerchant()
		{
			NPC agentOfNine = null;
			for (int npcCount = 0; npcCount < Main.maxNPCs; npcCount++)
			{
				NPC allNPC = Main.npc[npcCount];
				if (allNPC.active && allNPC.type == ModContent.NPCType<AgentOfNine>())
				{
					agentOfNine = allNPC;
					break;
				}
			}

			DateTime now = DateTime.Now;
			DayOfWeek day = now.DayOfWeek;
			if (agentOfNine != null && day != DayOfWeek.Friday && !IsNPCOnScreen(agentOfNine.Center))
			{
				if (Main.netMode == NetmodeID.SinglePlayer)
				{
					Main.NewText(agentOfNine.FullName + " has departed!", 50, 125, 255);
				}
				else
				{
					ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(agentOfNine.FullName + " has departed!"), new Color(50, 125, 255));
				}
				// agentOfNine.active = false;
				agentOfNine.netSkip = -1;
				agentOfNine.life = 0;
				agentOfNine = null;
			}

			if (!Main.dayTime && Main.time == 0)
			{
				SpawnTime = (agentOfNine == null && Main.rand.NextBool(10)) ? GetRandomSpawnTime(5400, 8100) : double.MaxValue;
			}

			if (agentOfNine == null && CanSpawnNow())
			{
				int newAgentOfNine = NPC.NewNPC(NPC.GetSpawnSourceForTownSpawn(), Main.spawnTileX * 16, Main.spawnTileY * 16, ModContent.NPCType<AgentOfNine>(), 1);
				agentOfNine = Main.npc[newAgentOfNine];
				agentOfNine.homeless = true;
				agentOfNine.direction = Main.spawnTileX >= WorldGen.bestX ? -1 : 1;
				agentOfNine.netUpdate = true;
				CreateNewShop();
				SpawnTime = double.MaxValue;

				if (Main.netMode == NetmodeID.SinglePlayer)
				{
					Main.NewText(Language.GetTextValue("Announcement.HasArrived", agentOfNine.FullName), 50, 125, 255);
				}
				else
				{
					ChatHelper.BroadcastChatMessage(NetworkText.FromKey("Announcement.HasArrived", agentOfNine.GetFullNetName()), new Color(50, 125, 255));
				}
			}
		}

		private static bool CanSpawnNow() => DateTime.Now.DayOfWeek == DayOfWeek.Friday 
			&& !Main.eclipse && !Main.fastForwardTime && Main.hardMode 
			&& (Main.invasionType <= 0 || Main.invasionDelay != 0 || Main.invasionSize <= 0);

		private static bool IsNPCOnScreen(Vector2 center)
		{
			int width = NPC.sWidth + NPC.safeRangeX * 2;
			int height = NPC.sHeight + NPC.safeRangeY * 2;
			Rectangle npcScreenRect = new Rectangle((int)center.X - width / 2, (int)center.Y - height / 2, width, height);

			for (int playerCount = 0; playerCount < Main.maxPlayers; playerCount++)
			{
				Player player = Main.player[playerCount];
				if (player.active && player.Hitbox.Intersects(npcScreenRect))
				{
					return true;
				}
			}
			return false;
		}

		public static double GetRandomSpawnTime(double minTime, double maxTime) => (maxTime - minTime) * Main.rand.NextDouble() + minTime;

		public static void CreateNewShop()
		{
			NPCShopData shopData = new NPCShopData();
			switch (Main.rand.Next(3))
			{
				case 0:
					shopData.ItemType = ModContent.ItemType<BorealisRanged>();
					shopData.ItemCurrency = ExoticCipher.ID;
					shopData.ItemPrice = 3;
					break;

				case 1:
					shopData.ItemType = ItemID.MythrilAnvil;
					shopData.ItemCurrency = ExoticCipher.ID;
					shopData.ItemPrice = 10000;
					break;

				default:
					shopData.ItemType = ModContent.ItemType<SweetBusiness>();
					shopData.ItemCurrency = ExoticCipher.ID;
					shopData.ItemPrice = 1;
					break;
			}
			Shop.Add(shopData);
			DestinyMod.Instance.Logger.Debug($"Selected Weapon: {Shop[0]}");
		}

		public override void SetChatButtons(ref string button, ref string button2)
		{
			button = Language.GetTextValue("LegacyInterface.28");
		}

		public override void OnChatButtonClicked(bool firstButton, ref bool shop)
		{
			if (firstButton)
			{
				shop = true;
			}
		}

		public override string GetChat()
		{
			if (Main.LocalPlayer.ZoneHallow && Main.rand.NextBool(10))
			{
				return Language.GetTextValue("Mods.DestinyMod.AgentOfNine.Hallow");
			}

			return Language.GetTextValue("Mods.DestinyMod.AgentOfNine.Chatter_" + Main.rand.Next(1, 11));
		}

		public override void SetupShop(Chest shop, ref int nextSlot)
		{
			foreach (NPCShopData shopItemData in Shop)
			{
				if (shopItemData.ItemType == ItemID.None)
				{
					DestinyMod.Instance.Logger.Debug("The item just checked in SetupShop was either null or had type 0");
					continue;
				}

				shop.item[nextSlot].SetDefaults(shopItemData.ItemType);
				shop.item[nextSlot].shopSpecialCurrency = shopItemData.ItemCurrency;
				shop.item[nextSlot].shopCustomPrice = shopItemData.ItemPrice;
				DestinyMod.Instance.Logger.Debug($"The item just checked in SetupShop was just added: {shop.item[nextSlot].Name}");
				nextSlot++;
			}
		}

		public override bool UsesPartyHat() => false;

		public override void AI()
		{
			NPC.homeless = true;
		}

		public override bool CanGoToStatue(bool toKingStatue) => false;

		public override void DrawTownAttackGun(ref float scale, ref int item, ref int closeness)
		{
			scale = 0.5f;
			item = ModContent.ItemType<UniversalRemote>();
			closeness = 20;
		}

		public override void Save(TagCompound tagCompound)
		{
			tagCompound.Add("SpawnTime", SpawnTime);
			tagCompound.Add("Shop", Shop.Select(shopData => shopData.Save()).ToList());
		}

		public override void Load(TagCompound tagCompound)
		{
			SpawnTime = tagCompound.GetDouble("SpawnTime");
			Shop = tagCompound.Get<List<TagCompound>>("Shop").Select(tag => NPCShopData.Load(tag)).ToList();
		}
	}
}