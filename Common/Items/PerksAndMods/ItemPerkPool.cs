using System.Collections.Generic;
using System.Linq;

namespace DestinyMod.Common.Items.PerksAndMods
{
    public class ItemPerkPool
    {
        public string TypeName { get; }

        public List<ItemPerk> Perks { get; }

        public ItemPerkPool(string typeName, params ItemPerk[] perks)
        {
            TypeName = typeName;
            Perks = perks.ToList();
            //Perks.Sort();
        }
    }
}