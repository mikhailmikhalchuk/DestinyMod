using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using TheDestinyMod.Items;
using TheDestinyMod.Items.Materials;
using TheDestinyMod.Tiles;
using TheDestinyMod.NPCs;

namespace TheDestinyMod.NPCs
{
    public class DestinyGlobalNPC : GlobalNPC
    {
        public override void NPCLoot(NPC npc) {
            DestinyPlayer player = Main.LocalPlayer.GetModPlayer<DestinyPlayer>();
            if (npc.TypeName == "Zombie") {
                if (Main.rand.NextBool(10)) {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<GunsmithMaterials>());
                }
            }
            if (npc.friendly == false && npc.damage > 0) {
                if (Main.rand.NextBool(50) && !player.ancientShard || Main.rand.NextBool(25) && player.ancientShard) {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<MoteOfDark>());
                }
                if (Main.rand.NextBool(65)) {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<CommonEngram>());
                }
                if (Main.rand.NextBool(25) && TheDestinyMod.guardianGames && (player.titan || player.warlock || player.hunter)) {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<Laurel>());
                }
            }
        }

        public override bool PreNPCLoot(NPC npc) {
            if (npc.type == NPCID.EyeofCthulhu && !NPC.downedBoss1) {
                if (Main.netMode != NetmodeID.Server) {
                    Main.NewText(Language.GetTextValue("Mods.TheDestinyMod.RelicShard"), new Color(200, 200, 55), false);
                }
                else {
                    NetworkText text = NetworkText.FromLiteral(Language.GetTextValue("Mods.TheDestinyMod.RelicShard"));
				    NetMessage.BroadcastChatMessage(text, new Color(200, 200, 55));
                }
                for (int k = 0; k < (int)(Main.maxTilesX * Main.maxTilesY * 6E-05); k++) {
                    int x = WorldGen.genRand.Next(0, Main.maxTilesX);
                    int y = WorldGen.genRand.Next((int)WorldGen.rockLayer, Main.maxTilesY);
                    WorldGen.OreRunner(x, y, WorldGen.genRand.Next(3, 8), WorldGen.genRand.Next(3, 8), (ushort)ModContent.TileType<RelicShard>());
                }
            }
            if (npc.TypeName == "Wall of Flesh" && !Main.hardMode) {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<Items.ExoticCipher>());
            }
            return true;
        }

        public override void DrawEffects(NPC npc, ref Color drawColor) {
            if (npc.HasBuff(ModContent.BuffType<Buffs.Debuffs.Judgment>())) {
                drawColor = Color.Yellow;
                if (Main.rand.NextBool(10)) {
                    int dust = Dust.NewDust(npc.position, npc.width, npc.height, 133);
                    Main.dust[dust].noGravity = true;
                }
            }
        }

        public override void EditSpawnRate(Player player, ref int spawnRate, ref int maxSpawns) {
            if (TheDestinyMod.currentSubworldID != string.Empty) {
                spawnRate = 0;
                maxSpawns = 0;
            }
        }

        public override void UpdateLifeRegen(NPC npc, ref int damage) {
            if (npc.HasBuff(ModContent.BuffType<Buffs.Debuffs.Conducted>())) {
                if (npc.lifeRegen > 0) {
                    npc.lifeRegen = 0;
                }
                npc.lifeRegen -= 15;
                damage = 2;
            }
        }
    }
}