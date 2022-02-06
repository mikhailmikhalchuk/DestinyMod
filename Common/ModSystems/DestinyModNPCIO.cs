using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using DestinyMod.Common.NPCs;

namespace DestinyMod.Common.ModSystems
{
	public class DestinyModNPCIO : ModSystem
	{
		public override void SaveWorldData(TagCompound tag)
		{
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