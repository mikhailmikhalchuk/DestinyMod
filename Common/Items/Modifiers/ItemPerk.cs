using Terraria;

namespace DestinyMod.Common.Items.Modifiers
{
    public abstract class ItemPerk : ModifierBase
    {
        public const int TextureSize = 34;

        public Item SocketedItem;

        public static ItemPerk GetInstance(int type) => ModAndPerkLoader.ItemPerks[type];
    }
}
