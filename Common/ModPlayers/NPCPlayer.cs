using DestinyMod.Content.Items.Engrams;
using DestinyMod.Content.NPCs.TownNPC;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace DestinyMod.Common.ModPlayers
{
	public class NPCPlayer : ModPlayer
	{
		public bool BoughtEngramCommon;

		public bool BoughtEngramUncommon;

		public bool BoughtEngramRare;

		public List<int> DecryptedItems;

		public int MotesGiven;

		public int ZavalaBountyProgress;

		public int ZavalaEnemies;

		public bool SpottedGorgon;

		public int SpottedGorgonTimer;

		public override void ResetEffects()
		{
			BoughtEngramCommon = false;
		}

		public override void ModifyScreenPosition()
		{
			if (SpottedGorgon)
			{
				Main.screenPosition.X += Main.rand.NextFloat(0, SpottedGorgonTimer / 300);
				SpottedGorgonTimer++;
			}
		}

		public override void PostBuyItem(NPC vendor, Item[] shopInventory, Item item)
		{
			if (vendor.type == ModContent.NPCType<Cryptarch>())
			{
				if (item.type == ModContent.ItemType<CommonEngram>())
				{
					shopInventory.FirstOrDefault(i => i.type == item.type)?.TurnToAir();
					BoughtEngramCommon = true;
				}
				else if (item.type == ModContent.ItemType<UncommonEngram>())
				{
					shopInventory.FirstOrDefault(i => i.type == item.type)?.TurnToAir();
					BoughtEngramUncommon = true;
					//DestinyWorld.daysPassed = 0;
				}
				else if (item.type == ModContent.ItemType<RareEngram>())
				{
					shopInventory.FirstOrDefault(i => i.type == item.type)?.SetDefaults();
					BoughtEngramRare = true;
					//DestinyWorld.daysPassed = 0;
				}
			}
		}

		public override void PostSellItem(NPC vendor, Item[] shopInventory, Item item)
		{
			if (vendor.type == ModContent.NPCType<Cryptarch>())
			{
				if (item.type == ModContent.ItemType<CommonEngram>())
				{
					BoughtEngramCommon = false;
				}
				else if (item.type == ModContent.ItemType<UncommonEngram>())
				{
					BoughtEngramUncommon = false;
				}
				else if (item.type == ModContent.ItemType<RareEngram>())
				{
					BoughtEngramRare = false;
				}
			}
		}

		public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
		{
			if (target.TypeName == "Zombie" && ZavalaBountyProgress == 1 && ZavalaEnemies < 100 && target.life <= 0)
			{
				ZavalaEnemies++;
			}

			if (target.TypeName == "Skeleton" && ZavalaBountyProgress == 3 && ZavalaEnemies < 50 && target.life <= 0)
			{
				ZavalaEnemies++;
			}
		}

		public override void SaveData(TagCompound tag)
		{
			tag.Add("BoughtEngramUncommon", BoughtEngramUncommon);
			tag.Add("BoughtEngramRare", BoughtEngramRare);
			tag.Add("DecryptedItems", DecryptedItems?.Select(type =>
			{
				Item item = new Item();
				item.SetDefaults(type);
				return ItemIO.Save(item);
			}).ToList());
			tag.Add("MotesGiven", MotesGiven);
			tag.Add("ZavalaBountyProgress", ZavalaBountyProgress);
			tag.Add("ZavalaEnemies", ZavalaEnemies);
		}

		public override void LoadData(TagCompound tag)
		{
			BoughtEngramUncommon = tag.Get<bool>("BoughtEngramUncommon");
			BoughtEngramRare = tag.Get<bool>("BoughtEngramRare");
			DecryptedItems = tag.Get<List<Item>>("DecryptedItems").Select(item => item.type).ToList();
			MotesGiven = tag.Get<int>("MotesGiven");
			ZavalaBountyProgress = tag.Get<int>("ZavalaBountyProgress");
			ZavalaEnemies = tag.Get<int>("ZavalaEnemies");
		}
	}
}