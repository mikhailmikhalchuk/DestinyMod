using Terraria;

namespace DestinyMod.Common.Items.PerksAndMods
{
    public abstract class ItemMod : ModAndPerkBase
    {
        public ItemType ApplyType;

        public virtual bool CanApply(Item item) => true;
    }
}
