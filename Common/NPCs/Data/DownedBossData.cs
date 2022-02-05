using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace DestinyMod.Common.NPCs.Data
{
    public struct DownedBossData
    {
        public int Type;

        public string ModName;

        public string NPCName;

        public DownedBossData(int type)
        {
            Type = type;
            ModNPC modNPC = ModContent.GetModNPC(type);
            if (modNPC == null)
            {
                ModName = string.Empty;
                NPCName = string.Empty;
            }
            else
            {
                ModName = modNPC.Mod.Name;
                NPCName = modNPC.Name;
            }
        }

        public DownedBossData(int type, string modName, string npcName)
        {
            Type = type;
            ModName = modName;
            NPCName = npcName;
        }

        public TagCompound Save()
        {
            return new TagCompound()
            {
                { "Type", Type },
                { "ModName", ModName },
                { "NPCName", NPCName },
            };
        }

        public static DownedBossData Load(TagCompound tagCompound)
        {
            int type = tagCompound.Get<int>("Type");
            string modName = tagCompound.Get<string>("ModName");
            string npcName = tagCompound.Get<string>("NPCName");
            if (modName == string.Empty)
            {
                return new DownedBossData(type, modName, npcName);
            }

            Mod mod = ModLoader.GetMod(modName);
            if (mod == null)
            {
                return new DownedBossData(-1, modName, npcName);
            }

            ModNPC modNPC = ModContent.GetModNPC(type);
            if (modNPC == null)
            {
                return new DownedBossData(-1, modName, npcName);
            }

            return new DownedBossData(modNPC.NPC.type, modName, npcName);
        }
    }
}