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
		public static bool LoadingRaid = false;

		public static string RaidDataSavePath = "";

		public static string RaidDataSavePathName = "";

        //chest saving and loading is low priority
        //npc saving and loading is not necessary
        //don't know what modded tile data entails
		public static void WriteRaid(int x, int y, int width, int height)
        {
            Tile[,] tiles = new Tile[width, height];
            for (int i = 0; i < tiles.GetLength(0); i++)
            {
                for (int j = 0; j < tiles.GetLength(1); j++)
                {
                    if (!WorldGen.InWorld(i, j))
                    {
                        continue;
                    }

                    tiles[i, j] = Framing.GetTileSafely(x + i, y + j);
                }
            }

            //mod.GetFileStream("Structures/etc")
            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + "My Games/Terraria/ModLoader/Mod Sources/TheDestinyMod/Structures/TemplarsWell";
            string directory = filePath.Substring(0, filePath.LastIndexOf("/"));
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            using (var writer = new BinaryWriter(File.Open(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite)))
            {
                writer.Write(x);
                writer.Write(y);
                writer.Write(tiles.GetLength(0));
                writer.Write(tiles.GetLength(1));
                for (int i = 0; i < tiles.GetLength(0); i++)
                {
                    for (int j = 0; j < tiles.GetLength(1); j++)
                    {
                        WriteTile(tiles[i, j], writer);
                    }
                }
                writer.Close();
            }
        }

        public static (int, int, Tile[,]) ReadRaid(string filePath)
        {
            using (var reader = new BinaryReader(TheDestinyMod.Instance.GetFileStream(filePath)))
            {
                int xR = reader.ReadInt32();
                int yR = reader.ReadInt32();
                int xLength = reader.ReadInt32();
                int yLength = reader.ReadInt32();

                Tile[,] tiles = new Tile[xLength, yLength];
                for (int i = 0; i < xLength; i++)
                {
                    for (int j = 0; j < yLength; j++)
                    {
                        tiles[i, j] = ReadTile(reader);
                    }
                }
                reader.Close();
                return (xR, yR, tiles);
            }
        }

        public static void WriteTile(Tile tile, BinaryWriter binaryWriter)
		{
			binaryWriter.Write(tile.active());
            binaryWriter.Write(tile.type);
            binaryWriter.Write(tile.wall);
            binaryWriter.Write(tile.frameX);
            binaryWriter.Write(tile.frameY);
            binaryWriter.Write(tile.bTileHeader);
            binaryWriter.Write(tile.bTileHeader2);
            binaryWriter.Write(tile.bTileHeader3);
            binaryWriter.Write(tile.sTileHeader);
            binaryWriter.Write(tile.color());
            binaryWriter.Write(tile.liquid);
            binaryWriter.Write(tile.wire());
            binaryWriter.Write(tile.wire2());
            binaryWriter.Write(tile.wire3());
            binaryWriter.Write(tile.wire4());
            binaryWriter.Write(tile.slope());
            binaryWriter.Write(tile.actuator());
            binaryWriter.Write(tile.inActive());
        }

        public static Tile ReadTile(BinaryReader binaryReader)
        {
            Tile tile = new Tile();
            tile.active(binaryReader.ReadBoolean());
            tile.type = binaryReader.ReadUInt16();
            tile.wall = binaryReader.ReadUInt16();
            tile.frameX = binaryReader.ReadInt16();
            tile.frameY = binaryReader.ReadInt16();
            tile.bTileHeader = binaryReader.ReadByte();
            tile.bTileHeader2 = binaryReader.ReadByte();
            tile.bTileHeader3 = binaryReader.ReadByte();
            tile.sTileHeader = binaryReader.ReadUInt16();
            tile.color(binaryReader.ReadByte());
            tile.liquid = binaryReader.ReadByte();
            tile.wire(binaryReader.ReadBoolean());
            tile.wire2(binaryReader.ReadBoolean());
            tile.wire3(binaryReader.ReadBoolean());
            tile.wire4(binaryReader.ReadBoolean());
            tile.slope(binaryReader.ReadByte());
            tile.actuator(binaryReader.ReadBoolean());
            tile.inActive(binaryReader.ReadBoolean());
            return tile;
        }
    }
}