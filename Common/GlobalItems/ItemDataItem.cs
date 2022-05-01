using DestinyMod.Common.Items;
using DestinyMod.Common.Items.PerksAndMods;
using DestinyMod.Content.Items.Mods;
using DestinyMod.Content.Items.Weapons.Ranged.Hakke;
using DestinyMod.Content.UI.ItemDetails;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace DestinyMod.Common.GlobalItems
{
    public class ItemDataItem : GlobalItem
    {
        public int LightLevel;

        public IList<ItemPerk> ActivePerks;

        public IList<ItemMod> ItemMods;

        public override bool InstancePerEntity => true;

        public override GlobalItem Clone(Item item, Item itemClone)
        {
            return base.Clone(item, itemClone);
        }

        public override void SetDefaults(Item item)
        {
            if (ItemData.ItemDatasByID != null && ItemData.ItemDatasByID.TryGetValue(item.type, out ItemData itemData))
            {
                if (LightLevel < itemData.DefaultLightLevel)
                {
                    LightLevel = itemData.DefaultLightLevel;
                }

                if (itemData.InterpretLightLevel == null)
                {
                    float defaultDamageMultiplier = (float)(Math.Pow(itemData.DefaultLightLevel - 1350, 2) / 500f) + 1f;
                    float currentDamageMultiplier = (float)(Math.Pow(LightLevel - 1350, 2) / 500f) + 1f;
                    item.damage = (int)(item.damage * (currentDamageMultiplier / defaultDamageMultiplier));
                }
                else
                {
                    itemData.InterpretLightLevel(LightLevel);
                }
            }
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (LightLevel < ItemData.MinimumLightLevel)
            {
                return;
            }

            TooltipLine lightLevelTooltip = new TooltipLine(DestinyMod.Instance, "LightLevel", "Power: " + LightLevel.ToString()); // To Do: Fancy icons when different classes get implemented
            TooltipLine nameTooltip = tooltips.FirstOrDefault(tooltip => tooltip.Mod == "Terraria" && tooltip.Name == "ItemName");

            if (nameTooltip != null)
            {
                int nameIndex = tooltips.IndexOf(nameTooltip);
                tooltips.Insert(nameIndex + 1, lightLevelTooltip);
            }
            else
            {
                tooltips.Add(lightLevelTooltip);
            }
        }

        // death
        public override void PostDrawInInventory(Item item, SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Rectangle drawHitbox = new Rectangle((int)position.X, (int)position.Y, (int)(frame.Width * scale), (int)(frame.Height * scale));
            // spriteBatch.Draw(Terraria.GameContent.TextureAssets.MagicPixel.Value, drawHitbox, Color.Red * 0.5f);
            if (drawHitbox.Contains(Main.MouseScreen.ToPoint()))
            {
                if (Main.keyState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.N))
                {
                    ModContent.GetInstance<ItemDetailsState>().UserInterface.SetState(new ItemDetailsState(item));
                    Main.playerInventory = false;
                }
            }
        }

        public override void SaveData(Item item, TagCompound tag)
        {
            tag.Add("LightLevel", LightLevel);

            if (ActivePerks != null)
            {
                tag.Add("ItemPerks", ActivePerks.Select(itemPerk => itemPerk == null ? "Null" : itemPerk.Name).ToList());
            }

            if (ItemMods != null && ItemMods.Count > 0)
            {
                tag.Add("ItemMods", ItemMods.Select(mod => mod == null ? ModContent.GetInstance<NullMod>().Name : mod.Name).ToList());
            }
        }

        public override void LoadData(Item item, TagCompound tag)
        {
            LightLevel = tag.Get<int>("LightLevel");

            if (tag.ContainsKey("ItemPerks"))
            {
                ActivePerks = new List<ItemPerk>();
                List<string> savedPerks = tag.Get<List<string>>("ItemPerks");
                foreach (string perk in savedPerks)
                {
                    if (perk == "Null")
                    {
                        ActivePerks.Add(null);
                        continue;
                    }

                    if (ModAndPerkLoader.ItemPerksByName.TryGetValue(perk, out ItemPerk itemPerk))
                    {
                        ActivePerks.Add(itemPerk);
                    }
                    else
                    {
                        ActivePerks.Add(null);
                    }
                }
            }

            if (tag.ContainsKey("ItemMods"))
            {
                ItemMods = new List<ItemMod>();
                List<string> itemModsSaved = tag.Get<List<string>>("ItemMods");
                foreach (string modName in itemModsSaved)
                {
                    if (modName == "Null")
                    {
                        ItemMods.Add(ModContent.GetInstance<NullMod>());
                        continue;
                    }

                    if (ModAndPerkLoader.ItemModsByName.TryGetValue(modName, out ItemMod itemMod))
                    {
                        ItemMods.Add(itemMod);
                    }
                    else
                    {
                        ItemMods.Add(ModContent.GetInstance<NullMod>());
                    }
                }
            }

            SetDefaults(item);
        }
    }

    public class TestLightLevelCommand : ModCommand
    {
        public override string Command => "TLL";

        public override CommandType Type => CommandType.Chat;

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            if (args.Length != 1)
            {
                Main.NewText("Arg length too short");
                return;
            }

            if (!int.TryParse(args[0], out int lightLevel))
            {
                Main.NewText("Arg 1 not int");
                return;
            }

            if (ItemData.ItemDatasByID.TryGetValue(ModContent.ItemType<HakkeAutoRifle>(), out ItemData itemData))
            {
                itemData.GenerateItem(caller.Player, new EntitySource_WorldEvent(), lightLevel);
            }
        }
    }
}
