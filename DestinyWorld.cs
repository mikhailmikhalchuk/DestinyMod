using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.GameContent.Generation;
using Terraria.World.Generation;
using Terraria.Localization;
using TheDestinyMod.NPCs.Town;
using TheDestinyMod.Items.Weapons.Ranged;
using TheDestinyMod.Tiles;
using TheDestinyMod.Tiles.Herbs;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;

namespace TheDestinyMod
{
    public class DestinyWorld : ModWorld
    {
        public static bool downedPrime = false;

        public static bool claimedItemsGG = false;
        public static int oraclesKilledOrder = 1;
        public static int oraclesTimesRefrained = 0;
        public static Vector2 vogPosition;

        public static int clearsVOG;
        public static int checkpointVOG;
        public static int daysPassed;

        public override void Initialize() {
            AgentOfNine.spawnTime = double.MaxValue;
            downedPrime = false;
            clearsVOG = 0;
            checkpointVOG = 0;
            daysPassed = 0;
        }

        public override TagCompound Save() {
            List<string> bossesKilled = new List<string>();
            if (downedPrime) {
                bossesKilled.Add("downedPrime");
            }
            return new TagCompound {
                {"agentOfNine", AgentOfNine.Save()},
                {"downed", bossesKilled},
                {"claimedItemsGG", claimedItemsGG},
                {"clearsVOG", clearsVOG},
                {"checkpointVOG", checkpointVOG},
                {"daysPassed", daysPassed}
            };
        }

        public override void Load(TagCompound tag) {
            AgentOfNine.Load(tag.GetCompound("agentOfNine"));
            var bossesKilled = tag.GetList<string>("downed");
            downedPrime = bossesKilled.Contains("downedPrime");
            claimedItemsGG = tag.GetBool("claimedItemsGG");
            clearsVOG = tag.GetInt("clearsVOG");
            checkpointVOG = tag.GetInt("checkpointVOG");
            daysPassed = tag.GetInt("daysPassed");
        }

        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight) {
            int genIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Jungle Temple"));
            tasks.Insert(genIndex + 1, new PassLegacy("Microphasic Datalattice", PlaceDatalattice));
            genIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Silt"));
            tasks.Insert(genIndex + 1, new PassLegacy("Spinmetal", PlaceSpinmetal));
            genIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Final Cleanup"));
            tasks.Insert(genIndex + 1, new PassLegacy("PlaceVexPortal", PlaceVexPortal));
        }

        private void PlaceDatalattice(GenerationProgress progress) {
            progress.Message = "Machinifying the world";
            int attempts = 0;
            Tile tile;
            Tile tileBelow;
            bool placeSuccessful = false;
            while (!placeSuccessful) {
                attempts++;
                int x = WorldGen.genRand.Next(20, Main.maxTilesX - 20);
                int y = WorldGen.genRand.Next(20, (int)Main.worldSurface);
                tile = Main.tile[x, y];
                tileBelow = Main.tile[x, y + 1];
                if (!tile.active() && tileBelow.nactive() && !tileBelow.halfBrick() && tileBelow.slope() == 0 && tileBelow.type == TileID.Mud) {
                    WorldGen.PlaceTile(x, y, ModContent.TileType<MicrophasicDatalattice>(), true);
                    placeSuccessful = tile.active() && tile.type == ModContent.TileType<MicrophasicDatalattice>();
                }
                if (attempts >= 30000) {
                    return;
                }
            }
        }

        private void PlaceSpinmetal(GenerationProgress progress) {
            progress.Message = "Rusting the ice";
            for (int k = 0; k < (int)(Main.maxTilesX * Main.maxTilesY * 1E-06); k++) {
                int attempts = 0;
                Tile tile;
                Tile tileBelow;
                bool placeSuccessful = false;
                while (!placeSuccessful) {
                    attempts++;
                    int x = WorldGen.genRand.Next(20, Main.maxTilesX - 20);
                    int y = WorldGen.genRand.Next(20, (int)Main.worldSurface);
                    tile = Main.tile[x, y];
                    tileBelow = Main.tile[x, y + 1];
                    if (!tile.active() && tileBelow.nactive() && !tileBelow.halfBrick() && tileBelow.slope() == 0 && (tileBelow.type == TileID.SnowBlock || tileBelow.type == TileID.IceBlock)) {
                        WorldGen.PlaceTile(x, y, ModContent.TileType<Spinmetal>(), true);
                        placeSuccessful = tile.active() && tile.type == ModContent.TileType<Spinmetal>();
                    }
                    if (attempts >= 30000) {
                        return;
                    }
                }
            }
        }


        private readonly int[,] _VoGPortalSchem = {
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1 },
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1 },
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1 },
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1 },
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1 },
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1 },
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1 },
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1 },
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1 },
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1 },
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1 },
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1 },
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1 },
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1 },
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1 },
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1 },
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1 },
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1 },
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1 },
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1 },
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1 },
            {2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2 },
        };

        private void PlaceVexPortal(GenerationProgress progress) {
            progress.Message = "Altering the fabric of time and space...";
            bool success = false;
            int attempts = 0;
            while (!success) {
                attempts++;
                if (attempts > 50000 || ModLoader.GetMod("StructureHelper") == null) {
                    success = true;
                    TheDestinyMod.Instance.Logger.Info("TheDestinyMod WorldGen: Failed to place the Vex portal");
                    continue;
                }
                progress.Set(attempts / 50000);
                int i = WorldGen.genRand.Next(300, Main.maxTilesX - 300);
                int j = WorldGen.genRand.Next((int)Main.worldSurface + 100, Main.maxTilesY - 250);
                if (i <= Main.maxTilesX / 2 - 50 || i >= Main.maxTilesX / 2 + 50) {
                    bool placementOK = true;
                    for (int l = i; l < i + _VoGPortalSchem.GetLength(0); l++) {
                        for (int m = j; m < j + _VoGPortalSchem.GetLength(1); m++) {
                            if (Main.tile[l, m].active()) {
                                int type = Main.tile[l, m].type;
                                if (type == TileID.BlueDungeonBrick || type == TileID.GreenDungeonBrick || type == TileID.PinkDungeonBrick || type == TileID.LihzahrdBrick) {
                                    placementOK = false;
                                }
                            }
                        }
                    }
                    if (placementOK) {
                        if (!WorldGen.SolidTile(i, j + 1) || !Main.tile[i, j].active() || Main.tile[i, j].type != TileID.JungleGrass) {
                            placementOK = false;
                        }
                        if (placementOK) {
                            TheDestinyMod.Instance.Logger.Info($"X: {i * 16} | Y: {j * 16}");
                            success = DestinyHelper.StructureHelperGenerateStructure(new Terraria.DataStructures.Point16(i / 16, j / 16), "VoGPortal");
                        }
                    }
                }
            }
        }

        public override void PostUpdate() {
            if (Main.rand.NextBool(15000)) {
                int attempts = 0;
                Tile tile;
                Tile tileBelow;
                bool placeSuccessful = false;
                while (!placeSuccessful) {
                    attempts++;
                    int x = WorldGen.genRand.Next(20, Main.maxTilesX - 20);
                    int y = WorldGen.genRand.Next(20, (int)Main.worldSurface);
                    tile = Main.tile[x, y];
                    tileBelow = Main.tile[x, y + 1];
                    if (!tile.active() && tileBelow.nactive() && !tileBelow.halfBrick() && tileBelow.slope() == 0 && (tileBelow.type == TileID.Grass || tileBelow.type == TileID.SnowBlock || tileBelow.type == TileID.IceBlock)) {
                        WorldGen.PlaceTile(x, y, ModContent.TileType<Spinmetal>(), true);
                        placeSuccessful = tile.active() && tile.type == ModContent.TileType<Spinmetal>();
                    }
                    if (attempts >= 30000) {
                        return;
                    }
                }
            }
            if (Main.rand.NextBool(35000)) {
                int attempts = 0;
                Tile tile;
                Tile tileBelow;
                bool placeSuccessful = false;
                while (!placeSuccessful) {
                    attempts++;
                    int x = WorldGen.genRand.Next(20, Main.maxTilesX - 20);
                    int y = WorldGen.genRand.Next(20, (int)Main.worldSurface);
                    tile = Main.tile[x, y];
                    tileBelow = Main.tile[x, y + 1];
                    if (!tile.active() && tileBelow.nactive() && !tileBelow.halfBrick() && tileBelow.slope() == 0 && tileBelow.type == TileID.JungleGrass) {
                        WorldGen.PlaceTile(x, y, ModContent.TileType<MicrophasicDatalattice>(), true);
                        placeSuccessful = tile.active() && tile.type == ModContent.TileType<MicrophasicDatalattice>();
                    }
                    if (attempts >= 30000) {
                        return;
                    }
                }
            }
        }

        public override void PreUpdate() {
            AgentOfNine.UpdateTravelingMerchant();
            DestinyPlayer player = Main.LocalPlayer.DestinyPlayer();
            if (Main.dayTime && Main.time == 0) {
                player.boughtCommon = false;
                daysPassed++;
            }
            if (daysPassed == 2) {
                player.boughtUncommon = false;
                daysPassed = 0;
            }
            if (daysPassed == 4) {
                player.boughtRare = false;
                daysPassed = 0;
            }
        }

        public override void NetSend(BinaryWriter writer) {
            var flags = new BitsByte
            {
                [0] = downedPrime
            };
            writer.Write(flags);
        }

        public override void NetReceive(BinaryReader reader) {
            BitsByte flags = reader.ReadByte();
			downedPrime = flags[0];
        }
    }
}