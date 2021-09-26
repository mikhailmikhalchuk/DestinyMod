using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using TheDestinyMod.Items.Materials;
using TheDestinyMod.Items.Weapons.Ranged;

namespace TheDestinyMod.NPCs.Town
{
	[AutoloadHead]
	public class AgentOfNine : ModNPC
	{
		public static double spawnTime = double.MaxValue;
		
		public static List<Item> shopItems = new List<Item>();

		public static List<int> itemPrices = new List<int>();
		
		public static List<int> itemCurrency = new List<int>();

		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Agent of the Nine");
			DisplayName.AddTranslation(GameCulture.Polish, "Agent Dziewiątki");
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
			npc.width = 20;
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

		public static void UpdateTravelingMerchant() {
			NPC agentOfNine = Main.npc.FirstOrDefault(npc => npc.type == ModContent.NPCType<AgentOfNine>() && npc.active);
			if (agentOfNine != null && (Main.dayTime || Main.time >= 32400) && !IsNpcOnscreen(agentOfNine.Center)) {
				if (Main.netMode == NetmodeID.SinglePlayer) Main.NewText(agentOfNine.FullName + " has departed!", 50, 125, 255);
				else NetMessage.BroadcastChatMessage(NetworkText.FromLiteral(agentOfNine.FullName + " has departed!"), new Color(50, 125, 255));
				agentOfNine.active = false;
				agentOfNine.netSkip = -1;
				agentOfNine.life = 0;
				agentOfNine = null;
			}
			if (!Main.dayTime && Main.time == 0) {
				if (agentOfNine == null && Main.rand.NextBool(10)) {
					spawnTime = GetRandomSpawnTime(5400, 8100);
				}
				else {
					spawnTime = double.MaxValue;
				}
			}
			if (agentOfNine == null && CanSpawnNow()) {
				int newAgentOfNine = NPC.NewNPC(Main.spawnTileX * 16, Main.spawnTileY * 16, ModContent.NPCType<AgentOfNine>(), 1);
				agentOfNine = Main.npc[newAgentOfNine];
				agentOfNine.homeless = true;
				agentOfNine.direction = Main.spawnTileX >= WorldGen.bestX ? -1 : 1;
				agentOfNine.netUpdate = true;
				shopItems = CreateNewShop();
				spawnTime = double.MaxValue;
				if (Main.netMode == NetmodeID.SinglePlayer) Main.NewText(Language.GetTextValue("Announcement.HasArrived", agentOfNine.FullName), 50, 125, 255);
				else NetMessage.BroadcastChatMessage(NetworkText.FromKey("Announcement.HasArrived", agentOfNine.GetFullNetName()), new Color(50, 125, 255));
			}
		}

		private static bool CanSpawnNow() {
			if (Main.eclipse || Main.fastForwardTime || !Main.hardMode || Main.invasionType > 0 && Main.invasionDelay == 0 && Main.invasionSize > 0)
				return false;

			return !Main.dayTime && Main.time >= spawnTime && Main.time < 32400;
		}

		private static bool IsNpcOnscreen(Vector2 center) {
			int w = NPC.sWidth + NPC.safeRangeX * 2;
			int h = NPC.sHeight + NPC.safeRangeY * 2;
			Rectangle npcScreenRect = new Rectangle((int)center.X - w / 2, (int)center.Y - h / 2, w, h);
			foreach (Player player in Main.player) {
				if (player.active && player.getRect().Intersects(npcScreenRect)) return true;
			}
			return false;
		}
		
		public static double GetRandomSpawnTime(double minTime, double maxTime) {
			return (maxTime - minTime) * Main.rand.NextDouble() + minTime;
		}

		public static List<Item> CreateNewShop() {
			var itemIds = new List<int>();
			itemCurrency.Clear();
			itemPrices.Clear();
			switch (Main.rand.Next(2)) {
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
			foreach (int itemId in itemIds) {
				Item item = new Item();
				item.SetDefaults(itemId);
				items.Add(item);
				TheDestinyMod.Instance.Logger.Debug($"Item ID: {itemId}");
			}
			return items;
		}

		public static TagCompound Save() {
			return new TagCompound {
                {"spawnTime", spawnTime},
				{"shopItems", shopItems},
				{"itemPrices", itemPrices},
				{"itemCurrency", itemCurrency}
			};
		}

		public static void Load(TagCompound tag) {
			spawnTime = tag.GetDouble("spawnTime");
			shopItems = tag.Get<List<Item>>("shopItems");
            itemPrices = tag.Get<List<int>>("itemPrices");
			itemCurrency = tag.Get<List<int>>("itemCurrency");
		}

		public override bool CanTownNPCSpawn(int numTownNPCs, int money) {
			return false;
		}

		public override string TownNPCName() {
			return "Xûr";
		}

		public override string GetChat() {
			List<string> dialogue = new List<string>();
			if (Main.LocalPlayer.ZoneHoly) {
				dialogue.Add(Language.GetTextValue("Mods.TheDestinyMod.AgentOfNine11"));
			}
			dialogue.Add(Language.GetTextValue("Mods.TheDestinyMod.AgentOfNine1"));
			dialogue.Add(Language.GetTextValue("Mods.TheDestinyMod.AgentOfNine2"));
			dialogue.Add(Language.GetTextValue("Mods.TheDestinyMod.AgentOfNine3"));
			dialogue.Add(Language.GetTextValue("Mods.TheDestinyMod.AgentOfNine4"));
			dialogue.Add(Language.GetTextValue("Mods.TheDestinyMod.AgentOfNine5"));
			dialogue.Add(Language.GetTextValue("Mods.TheDestinyMod.AgentOfNine6"));
			dialogue.Add(Language.GetTextValue("Mods.TheDestinyMod.AgentOfNine7"));
			dialogue.Add(Language.GetTextValue("Mods.TheDestinyMod.AgentOfNine8"));
			dialogue.Add(Language.GetTextValue("Mods.TheDestinyMod.AgentOfNine9"));
			dialogue.Add(Language.GetTextValue("Mods.TheDestinyMod.AgentOfNine10"));
			return Main.rand.Next(dialogue);
		}

		public override void SetChatButtons(ref string button, ref string button2) {
			button = Language.GetTextValue("LegacyInterface.28");
		}

		public override void OnChatButtonClicked(bool firstButton, ref bool shop) {
			if (firstButton) {
				shop = true;
			}
		}

		public override void SetupShop(Chest shop, ref int nextSlot) {
			foreach (Item item in shopItems) {
				if (item == null || item.type == ItemID.None) {
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

        public override bool UsesPartyHat() {
            return false;
        }

		public override void AI() {
			npc.homeless = true;
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
			item = ModContent.ItemType<UniversalRemote>();
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