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

namespace TheDestinyMod.NPCs.Town
{
	[AutoloadHead]
	public class AgentOfNine : ModNPC
	{
		public static double spawnTime = double.MaxValue;

		public static List<Item> shopItems = new List<Item>();

		public static List<int> itemPrices = new List<int>();

		public static List<int> itemCurrency = new List<int>();

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Agent of the Nine");
			Main.npcFrameCount[NPC.type] = 26;
			NPCID.Sets.AttackFrameCount[NPC.type] = 4;
			NPCID.Sets.ExtraFramesCount[NPC.type] = 10;
			NPCID.Sets.DangerDetectRange[NPC.type] = 700;
			NPCID.Sets.AttackType[NPC.type] = 1;
			NPCID.Sets.AttackTime[NPC.type] = 30;
			NPCID.Sets.AttackAverageChance[NPC.type] = 30;
			NPCID.Sets.HatOffsetY[NPC.type] = 8;
		}

		public override void SetDefaults()
		{
			NPC.townNPC = true;
			NPC.friendly = true;
			NPC.width = 20;
			NPC.height = 46;
			NPC.aiStyle = 7;
			NPC.damage = 10;
			NPC.defense = 15;
			NPC.lifeMax = 250;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			NPC.knockBackResist = 0.5f;
			AnimationType = NPCID.Guide;
		}

		public static void UpdateTravelingMerchant()
		{
			NPC agentOfNine = Main.npc.FirstOrDefault(npc => npc.type == ModContent.NPCType<AgentOfNine>() && npc.active);
			DateTime now = DateTime.Now;
			DayOfWeek day = now.DayOfWeek;
			if (agentOfNine != null && day != DayOfWeek.Friday && !IsNpcOnscreen(agentOfNine.Center))
			{
				if (Main.netMode == NetmodeID.SinglePlayer) Main.NewText(agentOfNine.FullName + " has departed!", 50, 125, 255);
				else ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(agentOfNine.FullName + " has departed!"), new Color(50, 125, 255));
				// agentOfNine.active = false;
				agentOfNine.netSkip = -1;
				agentOfNine.life = 0;
				agentOfNine = null;
			}
			if (!Main.dayTime && Main.time == 0)
			{
				if (agentOfNine == null && Main.rand.NextBool(10))
				{
					spawnTime = GetRandomSpawnTime(5400, 8100);
				}
				else
				{
					spawnTime = double.MaxValue;
				}
			}
			if (agentOfNine == null && CanSpawnNow())
			{
				int newAgentOfNine = NPC.NewNPC(Main.spawnTileX * 16, Main.spawnTileY * 16, ModContent.NPCType<AgentOfNine>(), 1);
				agentOfNine = Main.npc[newAgentOfNine];
				agentOfNine.homeless = true;
				agentOfNine.direction = Main.spawnTileX >= WorldGen.bestX ? -1 : 1;
				agentOfNine.netUpdate = true;
				shopItems = CreateNewShop();
				spawnTime = double.MaxValue;
				if (Main.netMode == NetmodeID.SinglePlayer) Main.NewText(Language.GetTextValue("Announcement.HasArrived", agentOfNine.FullName), 50, 125, 255);
				else ChatHelper.BroadcastChatMessage(NetworkText.FromKey("Announcement.HasArrived", agentOfNine.GetFullNetName()), new Color(50, 125, 255));
			}
		}

		private static bool CanSpawnNow()
		{
			DateTime now = DateTime.Now;
			DayOfWeek day = now.DayOfWeek;
			if (Main.eclipse || Main.fastForwardTime || !Main.hardMode || Main.invasionType > 0 && Main.invasionDelay == 0 && Main.invasionSize > 0)
				return false;

			return day == DayOfWeek.Friday;
		}

		private static bool IsNpcOnscreen(Vector2 center)
		{
			int w = NPC.sWidth + NPC.safeRangeX * 2;
			int h = NPC.sHeight + NPC.safeRangeY * 2;
			Rectangle npcScreenRect = new Rectangle((int)center.X - w / 2, (int)center.Y - h / 2, w, h);
			foreach (Player player in Main.player)
			{
				if (player.active && player.getRect().Intersects(npcScreenRect))
					return true;
			}
			return false;
		}

		public static double GetRandomSpawnTime(double minTime, double maxTime)
		{
			return (maxTime - minTime) * Main.rand.NextDouble() + minTime;
		}

		public static List<Item> CreateNewShop()
		{
			var itemIds = new List<int>();
			itemCurrency.Clear();
			itemPrices.Clear();
			switch (Main.rand.Next(2))
			{
				case 0:
					itemIds.Add(ModContent.ItemType<BorealisRanged>());
					itemPrices.Add(3);
					itemCurrency.Add(TheDestinyMod.CipherCustomCurrencyId);
					break;
				case 1:
					itemIds.Add(ItemID.MythrilAnvil);
					itemPrices.Add(10000);
					itemCurrency.Add(TheDestinyMod.CipherCustomCurrencyId);
					break;
				default:
					itemIds.Add(ModContent.ItemType<SweetBusiness>());
					itemPrices.Add(1);
					itemCurrency.Add(TheDestinyMod.CipherCustomCurrencyId);
					break;
			}
			TheDestinyMod.Instance.Logger.Debug($"Selected Weapon: {itemIds[0]}");
			var items = new List<Item>();
			foreach (int itemId in itemIds)
			{
				Item item = new Item();
				item.SetDefaults(itemId);
				items.Add(item);
				TheDestinyMod.Instance.Logger.Debug($"Item ID: {itemId}");
			}
			return items;
		}

		public static TagCompound Save()
		{
			return new TagCompound 
			{
				{"spawnTime", spawnTime},
				{"shopItems", shopItems},
				{"itemPrices", itemPrices},
				{"itemCurrency", itemCurrency}
			};
		}

		public static void Load(TagCompound tag)
		{
			spawnTime = tag.GetDouble("spawnTime");
			shopItems = tag.Get<List<Item>>("shopItems");
			itemPrices = tag.Get<List<int>>("itemPrices");
			itemCurrency = tag.Get<List<int>>("itemCurrency");
		}

		public override bool CanTownNPCSpawn(int numTownNPCs, int money) => false;

		public override string TownNPCName() => "Xûr";

		public override string GetChat()
		{
			if (Main.LocalPlayer.ZoneHallow && Main.rand.NextBool(10))
			{
				return Language.GetTextValue("Mods.TheDestinyMod.AgentOfNine.Hallow");
			}

			return Language.GetTextValue("Mods.TheDestinyMod.AgentOfNine.Chatter_" + Main.rand.Next(1, 11));
		}

		public override void SetChatButtons(ref string button, ref string button2) => button = Language.GetTextValue("LegacyInterface.28");

		public override void OnChatButtonClicked(bool firstButton, ref bool shop)
		{
			if (firstButton)
			{
				shop = true;
			}
		}

		public override void SetupShop(Chest shop, ref int nextSlot)
		{
			foreach (Item item in shopItems)
			{
				if (item == null || item.type == ItemID.None)
				{
					TheDestinyMod.Instance.Logger.Debug("The item just checked in SetupShop was either null or had type 0");
					continue;
				}
				shop.item[nextSlot].SetDefaults(item.type);
				shop.item[nextSlot].shopCustomPrice = itemPrices[nextSlot];
				shop.item[nextSlot].shopSpecialCurrency = itemCurrency[nextSlot];
				TheDestinyMod.Instance.Logger.Debug($"The item just checked in SetupShop was just added: {shop.item[nextSlot].Name}");
				nextSlot++;
			}
		}

		public override bool UsesPartyHat() => false;

		public override void AI() => NPC.homeless = true;

		public override void TownNPCAttackStrength(ref int damage, ref float knockback)
		{
			damage = 20;
			knockback = 4f;
		}

		public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
		{
			cooldown = 30;
			randExtraCooldown = 30;
		}

		public override void DrawTownAttackGun(ref float scale, ref int item, ref int closeness)
		{
			scale = 0.5f;
			item = ModContent.ItemType<UniversalRemote>();
			closeness = 20;
		}

		public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
		{
			projType = ProjectileID.Bullet;
			attackDelay = 1;
		}

		public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
		{
			multiplier = 12f;
			randomOffset = 2f;
		}
	}
}