using System;
using System.Collections.Generic;
using Terraria.ModLoader;

namespace DestinyMod.Common.Items.PerksAndMods
{
    public class ModAndPerkLoader : ILoadable
    {
        public static int PerkTypeReserver;

        public static int ModTypeReserver;

        public IList<ItemPerk> ItemPerks { get; private set; }

        public IList<ItemMod> ItemMods { get; private set; }

        public static IDictionary<string, ItemPerk> ItemPerksByName { get; private set; }

        public static IDictionary<string, ItemMod> ItemModsByName { get; private set; }

        public void Load(Mod mod)
        {
            ModTypeReserver = 0;
            ItemMods = new List<ItemMod>();
            ItemModsByName = new Dictionary<string, ItemMod>();

            PerkTypeReserver = 0;
            ItemPerks = new List<ItemPerk>();
            ItemPerksByName = new Dictionary<string, ItemPerk>();

            foreach (Type type in mod.Code.GetTypes())
            {
                if (type.IsAbstract || type.GetConstructor(Array.Empty<Type>()) == null)
                {
                    continue;
                }

                if (type.IsSubclassOf(typeof(ItemMod)))
                {
                    ItemMod itemMod = Activator.CreateInstance(type) as ItemMod;
                    string internalName = type.Name;
                    itemMod.Load(ref internalName);
                    itemMod.Type = ++ModTypeReserver;
                    itemMod.Name = internalName;
                    itemMod.SetDefaults();
                    ItemMods.Add(itemMod);
                    ItemModsByName.Add(itemMod.Name, itemMod);
                    ContentInstance.Register(itemMod);
                }

                if (type.IsSubclassOf(typeof(ItemPerk)))
                {
                    ItemPerk itemPerk = Activator.CreateInstance(type) as ItemPerk;
                    string internalName = type.Name;
                    itemPerk.Load(ref internalName);
                    itemPerk.Type = ++PerkTypeReserver;
                    itemPerk.Name = internalName;
                    itemPerk.SetDefaults();
                    ItemPerks.Add(itemPerk);
                    ItemPerksByName.Add(itemPerk.Name, itemPerk);
                    ContentInstance.Register(itemPerk);
                }
            }
        }

        public void Unload()
        {
            ItemMods?.Clear();
            ItemModsByName?.Clear();
            ItemPerks?.Clear();
            ItemPerksByName?.Clear();
        }
    }
}
