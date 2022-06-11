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
using DestinyMod.Content.Currencies;
using Terraria.GameContent.Bestiary;
using Terraria.DataStructures;

namespace DestinyMod.Content.NPCs.TownNPC
{
	[AutoloadHead]
	public class AgentOfNine : GenericTownNPC
	{
		public static List<NPCShopData> Shop = new List<NPCShopData>();

		public override void DestinySetStaticDefaults()
		{
			DisplayName.SetDefault("Agent of the Nine");

			NPCID.Sets.ActsLikeTownNPC[Type] = true;
			NPCID.Sets.SpawnsWithCustomName[Type] = true;
		}

		public override List<string> SetNPCNameList() => new List<string> { "Xûr" };

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

        public override void OnSpawn(IEntitySource source)
        {
			NPC.homeless = true;
			NPC.direction = Main.spawnTileX >= WorldGen.bestX ? -1 : 1;
			NPC.netUpdate = true;
			CreateNewShop();

			if (Main.netMode == NetmodeID.SinglePlayer)
			{
				Main.NewText(Language.GetTextValue("Announcement.HasArrived", NPC.FullName), 50, 125, 255);
			}
			else
			{
				ChatHelper.BroadcastChatMessage(NetworkText.FromKey("Announcement.HasArrived", NPC.GetFullNetName()), new Color(50, 125, 255));
			}
		}

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
			if (DateTime.Now.DayOfWeek == DayOfWeek.Saturday && !Main.eclipse && !Main.fastForwardTime && Main.hardMode && (Main.invasionType <= 0 || Main.invasionDelay != 0 || Main.invasionSize <= 0) && !NPC.AnyNPCs(NPC.type))
            {
				Main.NewText("e");
				return 1f;
            }

            return 0f;
        }

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
					continue;
				}

				shop.item[nextSlot].SetDefaults(shopItemData.ItemType);
				shop.item[nextSlot].shopSpecialCurrency = shopItemData.ItemCurrency;
				shop.item[nextSlot].shopCustomPrice = shopItemData.ItemPrice;
				nextSlot++;
			}
		}

		public override bool UsesPartyHat() => false;

		public override void AI()
		{
			NPC.homeless = true;
			if (DateTime.Now.DayOfWeek != DayOfWeek.Saturday && !IsNPCOnScreen(NPC.Center))
            {
				if (Main.netMode == NetmodeID.SinglePlayer)
				{
					Main.NewText(NPC.FullName + " has departed!", 50, 125, 255);
				}
				else
				{
					ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(NPC.FullName + " has departed!"), new Color(50, 125, 255));
				}

				NPC.netSkip = -1;
				NPC.life = 0;
				NPC.active = false;
			}
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
			tagCompound.Add("Shop", Shop.Select(shopData => shopData.Save()).ToList());
		}

		public override void Load(TagCompound tagCompound)
		{
			Shop = tagCompound.Get<List<TagCompound>>("Shop").Select(tag => NPCShopData.Load(tag)).ToList();
		}
	}
}