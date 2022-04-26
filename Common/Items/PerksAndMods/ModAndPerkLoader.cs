using System;
using System.Collections.Generic;
using Terraria.ModLoader;

namespace DestinyMod.Common.Items.PerksAndMods
{
    public class ModAndPerkLoader : ILoadable
    {
        public static IDictionary<string, ItemPerk> ItemPerksByName { get; private set; }

        public static IDictionary<string, ItemMod> ItemModsByName { get; private set; }

        public void Load(Mod mod)
        {
            ItemModsByName = new Dictionary<string, ItemMod>();

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
                    itemMod.Name = internalName;
                    itemMod.SetDefaults();
                    ItemModsByName.Add(itemMod.Name, itemMod);
                    ContentInstance.Register(itemMod);
                }

                if (type.IsSubclassOf(typeof(ItemPerk)))
                {
                    ItemPerk itemPerk = Activator.CreateInstance(type) as ItemPerk;
                    string internalName = type.Name;
                    itemPerk.Load(ref internalName);
                    itemPerk.Name = internalName;
                    itemPerk.SetDefaults();
                    ItemPerksByName.Add(itemPerk.Name, itemPerk);
                    ContentInstance.Register(itemPerk);
                }
            }
        }

        public void Unload()
        {
            ItemModsByName?.Clear();
            ItemPerksByName?.Clear();
        }
    }
}
