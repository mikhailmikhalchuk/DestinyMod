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

namespace TheDestinyMod
{
    public class DestinyWorld : ModWorld
    {
        public static bool downedPrime = false;
        public static bool claimedItemsGG = false;
        public static int oraclesKilledOrder = 1;
        public static int oraclesTimesRefrained = 0;

        public override void Initialize() {
            AgentOfNine.spawnTime = double.MaxValue;
        }

        public override TagCompound Save() {
            List<string> bossesKilled = new List<string>();
            if (downedPrime) {
                bossesKilled.Add("downedPrime");
            }
            return new TagCompound {
                {"agentOfNine", AgentOfNine.Save()},
                {"downed", bossesKilled},
                {"claimedItemsGG", claimedItemsGG}
            };
        }

        public override void Load(TagCompound tag) {
            AgentOfNine.Load(tag.GetCompound("agentOfNine"));
            var bossesKilled = tag.GetList<string>("downed");
            downedPrime = bossesKilled.Contains("downedPrime");
            claimedItemsGG = tag.GetBool("claimedItemsGG");
        }

        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight) {
            TheDestinyMod.Logger.Info(tasks);
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
            for (int k = 0; k < (int)((Main.maxTilesX * Main.maxTilesY) * 1E-06); k++) {
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
                if (attempts > 50000) {
                    success = true;
                    TheDestinyMod.Logger.Info("Failed!");
                    continue;
                }
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
                        if (!WorldGen.SolidTile(i, j + 1) || Main.tile[i, j].active() || Main.tile[i, j].type != TileID.JungleGrass) {
                            placementOK = false;
                        }
                        if (placementOK) {
                            for (int y = 0; y < _VoGPortalSchem.GetLength(0); y++) {
                                for (int x = 0; x < _VoGPortalSchem.GetLength(1); x++) {
                                    int k = i - 3 + x;
                                    int l = j - 6 + y;
                                    if (WorldGen.InWorld(k, l, 30)) {
                                        Tile tile = Framing.GetTileSafely(k, l);
                                        switch (_VoGPortalSchem[y, x]) {
                                            case 0:
                                                WorldGen.KillTile(k, l);
                                                break;
                                            case 1:
                                                tile.type = TileID.TeamBlockGreen;
                                                tile.active(true);
                                                break;
                                            case 2:
                                                tile.type = TileID.Grass;
                                                tile.active(true);
                                                break;
                                        }
                                        TheDestinyMod.Logger.Info($"X: {k * 16} | Y: {l * 16}");
                                        success = true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public override void PostUpdate() {
            if (Main.rand.NextBool(25000)) {
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
            if (Main.rand.NextBool(50000)) {
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
            DestinyPlayer player = Main.LocalPlayer.GetModPlayer<DestinyPlayer>();
            if (Main.dayTime && Main.time == 0) {
                player.boughtCommon = false;
            }
        }

        public override void NetSend(BinaryWriter writer) {
            var flags = new BitsByte();
			flags[0] = downedPrime;
			writer.Write(flags);
        }

        public override void NetReceive(BinaryReader reader) {
            BitsByte flags = reader.ReadByte();
			downedPrime = flags[0];
        }
    }
}