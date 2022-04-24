using System;
using System.Collections.Generic;
using Terraria.ModLoader;

namespace DestinyMod.Common.Items.PerksAndMods
{
    public class ItemPerkLoader : ILoadable
    {
        public static IDictionary<string, ItemPerk> ItemPerksByName { get; private set; }

        public void Load(Mod mod)
        {
            ItemPerksByName = new Dictionary<string, ItemPerk>();

            foreach (Type type in mod.Code.GetTypes())
            {
                if (!type.IsAbstract && type.GetConstructor(Array.Empty<Type>()) != null)
                {
                    if (type.IsSubclassOf(typeof(ItemPerk)))
                    {
                        ItemPerk itemPerk = Activator.CreateInstance(type) as ItemPerk;
                        string itemPerkInternalName = type.Name;
                        itemPerk.Load(ref itemPerkInternalName);
                        itemPerk.Name = itemPerkInternalName;
                        itemPerk.SetDefaults();
                        ItemPerksByName.Add(itemPerk.Name, itemPerk);
                        ContentInstance.Register(itemPerk);
                    }
                }
            }
        }

        public void Unload() => ItemPerksByName?.Clear();
    }
}
