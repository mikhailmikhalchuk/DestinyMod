using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using DestinyMod.Common.NPCs;
using DestinyMod.Common.NPCs.Data;
using System.Collections.Generic;
using System.Linq;

namespace DestinyMod.Common.ModSystems
{
	public sealed class NPCIOSystem : ModSystem
	{
		public static List<DownedBossData> DownedBoss = new List<DownedBossData>();

		public override void SaveWorldData(TagCompound tag)
		{
			tag.Add("DownedBoss", DownedBoss.Select(bossData => bossData.Save()).ToList());

			foreach (ModNPC npc in DestinyMod.Instance.GetContent<ModNPC>())
			{
				if (npc is not DestinyModNPC destinyModNPC)
				{
					continue;
				}

				TagCompound npcCompound = new TagCompound();
				destinyModNPC.Save(npcCompound);
				tag.Add(npc.Name, npcCompound);
			}
		}

		public override void LoadWorldData(TagCompound tag)
		{
			List<TagCompound> downedBossSaved = tag.Get<List<TagCompound>>("DownedBoss");
			DownedBoss = downedBossSaved.Select(savedData => DownedBossData.Load(savedData)).ToList();

			foreach (ModNPC npc in DestinyMod.Instance.GetContent<ModNPC>())
			{
				if (npc is not DestinyModNPC destinyModNPC)
				{
					continue;
				}

				if (tag.ContainsKey(npc.Name))
				{
					destinyModNPC.Load(tag.Get<TagCompound>(npc.Name));
				}
			}
		}
    }
}