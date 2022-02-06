using DestinyMod.Content.Items.Engrams;
using DestinyMod.Content.NPCs.Town;
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
					DestinyWorld.daysPassed = 0;
				}
				else if (item.type == ModContent.ItemType<RareEngram>())
				{
					shopInventory.FirstOrDefault(i => i.type == item.type)?.SetDefaults();
					BoughtEngramRare = true;
					DestinyWorld.daysPassed = 0;
				}
			}
		}

		public override void SaveData(TagCompound tag)
		{
			tag.Add("BoughtEngramUncommon", BoughtEngramUncommon);
			tag.Add("BoughtEngramRare", BoughtEngramRare);
			tag.Add("MotesGiven", MotesGiven);
		}

		public override void LoadData(TagCompound tag)
		{
			BoughtEngramUncommon = tag.Get<bool>("BoughtEngramUncommon");
			BoughtEngramRare = tag.Get<bool>("BoughtEngramRare");
			MotesGiven = tag.Get<int>("MotesGiven");
		}
	}
}