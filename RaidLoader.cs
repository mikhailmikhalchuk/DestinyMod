using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.Utilities;

namespace TheDestinyMod
{
    public static class RaidLoader
    {
        public static bool LoadingRaid = false;

        public static string RaidDataSavePath = "";

        public static string RaidDataSavePathName = "";

        public static void WriteRaid(int x, int y, int width, int height)
        {
            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + "My Games/TheDestinyMod";
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
                { "TileDatas", WriteTile(x, y, width, height) }
            };

            MemoryStream memoryStream = new MemoryStream();
            TagIO.ToStream(fileSave, memoryStream);
            byte[] array = memoryStream.ToArray();
            FileUtilities.Write(filePath, array, array.Length, false);
        }

        public static (int, int, Tile[,]) ReadRaid(string filePath)
        {
            byte[] array = FileUtilities.ReadAllBytes(filePath, false);
            TagCompound tagCompound = TagIO.FromStream(new MemoryStream(array));
            Tile[,] tiles = ReadTile(tagCompound.Get<TagCompound>("Tiles"));
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
                    if (!WorldGen.InWorld(iAdjusted, jAdjusted))
                    {
                        continue;
                    }

                    Tile tile = Framing.GetTileSafely(iAdjusted, jAdjusted);
                    tagCompound.Add(i + ", " + j, new TagCompound()
                    {
                        { "Active", tile.active() },
                        { "Type", tile.type },
                        { "FrameX", tile.frameX },
                        { "FrameY", tile.frameY },
                        { "Color", tile.color() },
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
                    tile.active(tagCompound.Get<bool>("Active"));
                    tile.type = tagCompound.Get<ushort>("Type");
                    tile.frameX = tagCompound.Get<short>("FrameX");
                    tile.frameY = tagCompound.Get<short>("FrameY");
                    tile.color(tagCompound.Get<byte>("Color"));
                    tile.liquid = tagCompound.Get<byte>("Liquid");
                    tile.wire(tagCompound.Get<bool>("Wire"));
                    tile.wire2(tagCompound.Get<bool>("Wire2"));
                    tile.wire3(tagCompound.Get<bool>("Wire3"));
                    tile.wire4(tagCompound.Get<bool>("Wire4"));
                    tile.slope(tagCompound.Get<byte>("Slope"));
                    tile.actuator(tagCompound.Get<bool>("Actuator"));
                    tile.inActive(tagCompound.Get<bool>("InActive"));
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
                            itemCompound.Add(new TagCompound()
                            {
                                { "Slot", i },
                                { "Actual Data", ItemIO.Save(item) },
                            });
                        }
                        else if (!item.IsAir)
						{
                            itemCompound.Add(new TagCompound()
                            {
                                { "Slot", i },
                                { "Type", item.type },
                                { "Stack", item.stack },
                            });
                        }
                    }
                    chestCompound.Add("Chests", chestCompound);
                    chestCompounds.Add(chestCompound);
                }
            }

            tagCompound.Add("Chests", chestCompounds);
            return tagCompound;
        }

        public static List<Chest> ReadContainers(TagCompound tagCumpound)
		{
            int width = tagCumpound.Get<int>("width");
            int height = tagCumpound.Get<int>("height");
            List<TagCompound> chestCompounds = tagCumpound.Get<List<TagCompound>>("Chests");
            foreach (TagCompound chestCompound in chestCompounds)
			{
                Chest chest = new Chest();
                chest.x = chestCompound.Get<int>("X");
                chest.y = chestCompound.Get<int>("Y");
                List<TagCompound> itemCompounds = chestCompound.Get<List<TagCompound>>("Chest");
                foreach (TagCompound itemCompound in itemCompounds)
				{

                    if (itemCompound.ContainsKey("mod"))
					{
                        ItemIO.Load(itemCompound);
					}
                    else
					{
                        int type = itemCompound.Get<int>("Type");
                        int stack = itemCompound.Get<int>("Stack");
                    }

                }
            }
        }
    }
}