using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.Utilities;
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
		public static void WriteRaid(int x, int y, int width, int height, string fileName)
        {
            //mod.GetFileStream("Structures/etc")
            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + "My Games/Terraria/ModLoader/Mod Sources/TheDestinyMod/Structures/" + fileName;
            string directory = filePath.Substring(0, filePath.LastIndexOf("/"));
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            TagCompound fileSave = new TagCompound()
            {
                { "X", x },
                { "Y", y },
                { "Width", width },
                { "Height", height },
                { "TileData", WriteTile(x, y, width, height) },
                { "ChestData", WriteContainers(x, y, width, height) },
            };

            using (MemoryStream memoryStream = new MemoryStream())
            {
                TagIO.ToStream(fileSave, memoryStream);
                byte[] array = memoryStream.ToArray();
                FileUtilities.Write(filePath, array, array.Length, false);
            }
        }

        public static (int x, int y, Tile[,] tileData, List<Chest> chestData) ReadRaid(string filePath)
        {
            using (Stream array = TheDestinyMod.Instance.GetFileStream(filePath)) {
                TagCompound tagCompound = TagIO.FromStream(array);
                int x = tagCompound.Get<int>("X");
                int y = tagCompound.Get<int>("Y");
                Tile[,] tiles = ReadTile(tagCompound.Get<TagCompound>("TileData"));
                List<Chest> chests = ReadContainers(tagCompound.Get<TagCompound>("ChestData"));
                return (x, y, tiles, chests);
            }
        }

        public static TagCompound WriteTile(int x, int y, int width, int height)
        {
            TagCompound tagCompound = new TagCompound
            {
                { "width", width },
                { "height", height },
            };

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    int iAdjusted = x + i;
                    int jAdjusted = y + j;
                    if (!WorldGen.InWorld(iAdjusted, jAdjusted)) //iAdjusted some bitches
                    {
                        continue;
                    }

                    Tile tile = Framing.GetTileSafely(iAdjusted, jAdjusted);
                    tagCompound.Add(i + ", " + j, new TagCompound()
                    {
                        { "Active", tile.active() },
                        { "Type", tile.type },
                        { "Wall", tile.wall },
                        { "Header1", tile.bTileHeader },
                        { "Header2", tile.bTileHeader2 },
                        { "Header3", tile.bTileHeader3 },
                        { "Header4", tile.sTileHeader },
                        { "FrameX", tile.frameX },
                        { "FrameY", tile.frameY },
                        { "WallFrameX", tile.wallFrameX() },
                        { "WallFrameY", tile.wallFrameY() },
                        { "WallFrameNumber", tile.wallFrameNumber() },
                        { "Color", tile.color() },
                        { "WallColor", tile.wallColor() },
                        { "Liquid", tile.liquid },
                        { "Wire", tile.wire() },
                        { "Wire2", tile.wire2() },
                        { "Wire3", tile.wire3() },
                        { "Wire4", tile.wire4() },
                        { "Slope", tile.slope() },
                        { "Actuator", tile.actuator() },
                        { "InActive", tile.inActive() },
                    });
                }
            }

            return tagCompound;
        }

        public static Tile[,] ReadTile(TagCompound tagCompound)
        {
            int width = tagCompound.Get<int>("width");
            int height = tagCompound.Get<int>("height");
            Tile[,] tiles = new Tile[width, height];
            for (int i = 0; i < width; i++)
			      {
                for (int j = 0; j < height; j++)
                {
                    TagCompound tileCompound = tagCompound.Get<TagCompound>(i + ", " + j);
                    Tile tile = new Tile();
                    tile.active(tileCompound.Get<bool>("Active"));
                    tile.type = tileCompound.Get<ushort>("Type");
                    tile.wall = tileCompound.Get<ushort>("Wall");
                    tile.bTileHeader = tileCompound.Get<byte>("Header1");
                    tile.bTileHeader2 = tileCompound.Get<byte>("Header2");
                    tile.bTileHeader3 = tileCompound.Get<byte>("Header3");
                    tile.sTileHeader = tileCompound.Get<ushort>("Header4");
                    tile.frameX = tileCompound.Get<short>("FrameX");
                    tile.frameY = tileCompound.Get<short>("FrameY");
                    tile.wallFrameX(tileCompound.Get<int>("WallFrameX"));
                    tile.wallFrameY(tileCompound.Get<int>("WallFrameY"));
                    tile.wallFrameNumber(tileCompound.Get<byte>("WallFrameNumber"));
                    tile.color(tileCompound.Get<byte>("Color"));
                    tile.wallColor(tileCompound.Get<byte>("WallColor"));
                    tile.liquid = tileCompound.Get<byte>("Liquid");
                    tile.wire(tileCompound.Get<bool>("Wire"));
                    tile.wire2(tileCompound.Get<bool>("Wire2"));
                    tile.wire3(tileCompound.Get<bool>("Wire3"));
                    tile.wire4(tileCompound.Get<bool>("Wire4"));
                    tile.slope(tileCompound.Get<byte>("Slope"));
                    tile.actuator(tileCompound.Get<bool>("Actuator"));
                    tile.inActive(tileCompound.Get<bool>("InActive"));
                    tiles[i, j] = tile;
                }
            }
            return tiles;
        }

        public static TagCompound WriteContainers(int x, int y, int width, int height)
        {
            TagCompound tagCompound = new TagCompound
            {
                { "width", width },
                { "height", height },
            };

            List<TagCompound> chestCompounds = new List<TagCompound>();
            for (int i = 0; i < 1000; i++)
            {
                Chest chest = Main.chest[i];
                if (chest != null && chest.x >= x && chest.x < x + width && chest.y >= y && chest.y < y + height)
                {
                    TagCompound chestCompound = new TagCompound()
                    {
                        { "X", chest.x - x },
                        { "Y", chest.y - y },
                    };

                    List<TagCompound> itemCompound = new List<TagCompound>();
                    for (int itemSlot = 0; itemSlot < chest.item.Length; itemSlot++)
					          {
                        Item item = chest.item[itemSlot];
                        if (ItemLoader.NeedsModSaving(item))
                        {
                            itemCompound.Add(ItemIO.Save(item));
                        }
                        else
						            {
                            itemCompound.Add(new TagCompound()
                            {
                                { "Slot", itemSlot },
                                { "Type", item.type },
                                { "Stack", item.stack },
                            });
                        }
                    }
                    chestCompound.Add("Items", itemCompound);
                    chestCompounds.Add(chestCompound);
                }
            }

            tagCompound.Add("Chests", chestCompounds);
            return tagCompound;
        }

        public static List<Chest> ReadContainers(TagCompound tagCumpound) {
            List<Chest> output = new List<Chest>();
            int width = tagCumpound.Get<int>("width");
            int height = tagCumpound.Get<int>("height");
            List<TagCompound> chestCompounds = tagCumpound.Get<List<TagCompound>>("Chests");
            foreach (TagCompound chestCompound in chestCompounds) {
                Chest chest = new Chest
                {
                    x = chestCompound.Get<int>("X"),
                    y = chestCompound.Get<int>("Y")
                };

                List<TagCompound> itemCompounds = chestCompound.Get<List<TagCompound>>("Items");
                int chestIndex = 0;
                foreach (TagCompound itemCompound in itemCompounds) {
                    if (itemCompound.ContainsKey("mod")) {
                        chest.item[chestIndex] = ItemIO.Load(itemCompound);
                    }
                    else {
                        int type = itemCompound.Get<int>("Type");
                        int stack = itemCompound.Get<int>("Stack");
                        Item item = new Item();
                        item.SetDefaults(type);
                        item.stack = stack;
                        chest.item[chestIndex] = item;
                    }

                    chestIndex++;
                }

                output.Add(chest);
            }

            return output;
        }
    }
}