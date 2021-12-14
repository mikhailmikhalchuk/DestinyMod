using System;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheDestinyMod.Items;
using TheDestinyMod.NPCs;
using TheDestinyMod.Projectiles;

namespace TheDestinyMod
{
    public static class RaidLoader
    {
        public static void WriteRaid(int x, int y, int width, int height) {
            int[,] tiles = new int[x + width, y + height];
            for (int i = x; i < tiles.GetLength(0); i++) {
                for (int j = y; j < tiles.GetLength(1); j++) {
                    tiles[i, j] = Main.tile[i / 16, j / 16].type;
                }
            }
            string filePath = @"C:/Users/Cuno/Documents/My Games/TheDestinyMod";
            string directory = filePath.Substring(0, filePath.LastIndexOf("/"));
            if (!Directory.Exists(directory)) {
                Directory.CreateDirectory(directory);
            }

            using (var writer = new BinaryWriter(File.Open(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite))) {
                writer.Write(x);
                writer.Write(y);
                writer.Write(tiles.GetLength(0));
                writer.Write(tiles.GetLength(1));
                for (int i = x; i < tiles.GetLength(0); i++) {
                    for (int j = y; j < tiles.GetLength(1); j++) {
                        writer.Write(tiles[i, j]);
                    }
                }
                writer.Close();
            }
        }

        public static (int, int, int[,]) ReadRaid(int x, int y, string filePath) {
            using (var reader = new BinaryReader(File.Open(filePath, FileMode.Open, FileAccess.Read))) {
                int xR = reader.ReadInt32();
                int yR = reader.ReadInt32();
                int xLength = reader.ReadInt32();
                int yLength = reader.ReadInt32();

                int[,] tiles = new int[xR + xLength, yR + yLength];
                for (int i = xR; i < tiles.GetLength(0); i++) {
                    for (int j = yR; j < tiles.GetLength(1); j++) {
                        tiles[i, j] = reader.ReadInt32();
                    }
                }
                reader.Close();

                return (xR, yR, tiles);
            }
        }
    }
}