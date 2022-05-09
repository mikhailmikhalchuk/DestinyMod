using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using System.Linq;
using Terraria.Chat;
using Terraria.GameContent.ItemDropRules;
using DestinyMod.Content.Items.Materials;
using DestinyMod.Common.DropConditions;
using DestinyMod.Content.Items.Engrams;
using DestinyMod.Content.Items.Misc;
using DestinyMod.Common.NPCs.Data;
using DestinyMod.Common.ModSystems;
using DestinyMod.Content.Tiles;

namespace DestinyMod.Common.GlobalNPCs
{
    public class DropsNPC : GlobalNPC
    {
        public override bool InstancePerEntity => true;

        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            if (npc.TypeName == "Zombie")
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<GunsmithMaterials>(), 10));
            }

            if (!npc.friendly && npc.damage > 0)
            {
                LeadingConditionRule ancientShard = new LeadingConditionRule(new HasAncientShard());
                ancientShard.OnSuccess(ItemDropRule.Common(ModContent.ItemType<MoteOfDark>(), 20));
                ancientShard.OnFailedConditions(ItemDropRule.Common(ModContent.ItemType<MoteOfDark>(), 50));
                npcLoot.Add(ancientShard);

                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<CommonEngram>(), 65));
            }

            if (npc.boss)
            {
                LeadingConditionRule downed = new LeadingConditionRule(new DropBasedOnDownedBossStatus());
                downed.OnSuccess(ItemDropRule.Common(ModContent.ItemType<ExoticCipher>()));

                npcLoot.Add(downed);
            }
        }

        public override void OnKill(NPC npc)
        {
            if (npc.boss)
            {
                DownedBossData downedBossIndexer = new DownedBossData(npc.type);
                if (!NPCIOSystem.DownedBoss.Any(downedBossData => downedBossData.Type == npc.type))
                {
                    NPCIOSystem.DownedBoss.Add(downedBossIndexer);
                }
            }
        }

        public override bool PreKill(NPC npc)
        {
            if (npc.type == NPCID.EyeofCthulhu && !NPC.downedBoss1)
            {
                if (Main.netMode != NetmodeID.Server)
                {
                    Main.NewText(Language.GetTextValue("Mods.DestinyMod.Common.RelicShard"), new Color(200, 200, 55));
                }
                else
                {
                    NetworkText text = NetworkText.FromLiteral(Language.GetTextValue("Mods.DestinyMod.Common.RelicShard"));
                    ChatHelper.BroadcastChatMessage(text, new Color(200, 200, 55));
                }

                for (int tileCount = 0; tileCount < (int)(Main.maxTilesX * Main.maxTilesY * 6E-05); tileCount++)
                {
                    int x = WorldGen.genRand.Next(0, Main.maxTilesX);
                    int y = WorldGen.genRand.Next((int)WorldGen.rockLayer, Main.maxTilesY);
                    WorldGen.OreRunner(x, y, WorldGen.genRand.Next(3, 8), WorldGen.genRand.Next(3, 8), (ushort)ModContent.TileType<RelicShard>());
                }
            }
            return true;
        }
    }
}