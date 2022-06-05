using System.Collections.Generic;
using System.Linq;

namespace DestinyMod.Common.Items.Modifiers
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