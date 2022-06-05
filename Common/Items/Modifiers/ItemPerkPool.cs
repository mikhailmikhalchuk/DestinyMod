using System.Collections.Generic;
using System.Linq;

namespace DestinyMod.Common.Items.Modifiers
{
    /// <summary>Defines item perk pools (columns).</summary>
    public class ItemPerkPool
    {
        /// <summary>
        /// The name of the perk pool.
        /// </summary>
        public string TypeName { get; }

        /// <summary>
        /// The list of perks this pool contains.
        /// </summary>
        public List<ItemPerk> Perks { get; }

        public ItemPerkPool(string typeName, params ItemPerk[] perks)
        {
            TypeName = typeName;
            Perks = perks.ToList();
            //Perks.Sort();
        }
    }
}