using DestinyMod.Common.Items;
using DestinyMod.Common.Items.PerksAndMods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace DestinyMod.Common.GlobalItems
{
    public class ItemModItem : GlobalItem
    {
        public IList<ItemMod> ItemMods;

        public IList<ItemPerk> ItemPerks;

        public ItemPerkPool PerkPool;

        public override bool InstancePerEntity => true;

        public override GlobalItem Clone(Item item, Item itemClone)
        {
            return base.Clone(item, itemClone);
        }

        public override void SetDefaults(Item item)
        {

        }

        public void ImplementItemModsAndPerks()
        {

        }
    }
}
