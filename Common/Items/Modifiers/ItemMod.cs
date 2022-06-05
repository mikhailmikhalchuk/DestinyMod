using Terraria;

namespace DestinyMod.Common.Items.Modifiers
{
    public abstract class ItemMod : ModifierBase
    {
        public ItemType ApplyType;

        public virtual bool CanApply(Item item) => true;
    }
}
