using DestinyMod.Content.Items.Engrams;
using DestinyMod.Content.NPCs.TownNPC;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace DestinyMod.Common.ModPlayers
{
	public class NPCPlayer : ModPlayer
	{
		public List<int> DecryptedItems;

		public int MotesGiven;

		public int ZavalaBountyProgress;

		public int ZavalaEnemies;

		public bool SpottedGorgon;

		public int SpottedGorgonTimer;

        public override void UpdateDead()
        {
            SpottedGorgon = false;
			SpottedGorgonTimer = 0;
        }

        public override void ModifyScreenPosition()
		{
			if (SpottedGorgon)
			{
				Main.screenPosition.X += Main.rand.NextFloat(0, SpottedGorgonTimer / 300);
				SpottedGorgonTimer += 4;
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
			/*tag.Add("DecryptedItems", DecryptedItems?.Select(type =>
			{
				Item item = new Item();
				item.SetDefaults(type);
				return ItemIO.Save(item);
			}).ToList());*/ // This breaky for some reason idk, I didn't feel like fixing it
			tag.Add("MotesGiven", MotesGiven);
			tag.Add("ZavalaBountyProgress", ZavalaBountyProgress);
			tag.Add("ZavalaEnemies", ZavalaEnemies);
		}

		public override void LoadData(TagCompound tag)
		{
			DecryptedItems = tag.Get<List<Item>>("DecryptedItems").Select(item => item.type).ToList();
			MotesGiven = tag.Get<int>("MotesGiven");
			ZavalaBountyProgress = tag.Get<int>("ZavalaBountyProgress");
			ZavalaEnemies = tag.Get<int>("ZavalaEnemies");
		}
	}
}