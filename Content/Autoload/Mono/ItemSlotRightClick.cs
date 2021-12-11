using TheDestinyMod.Core.Autoloading;
using Terraria;

namespace TheDestinyMod.Content.Autoloading.Mono
{
    public class ItemSlotRightClick : IAutoloadable
    {
        public void IAutoloadable_Load(IAutoloadable createdObject) => On.Terraria.UI.ItemSlot.RightClick_ItemArray_int_int += ItemSlot_RightClick_ItemArray_int_int;

        public void IAutoloadable_PostSetUpContent() { }

        public void IAutoloadable_Unload() { }

        private void ItemSlot_RightClick_ItemArray_int_int(On.Terraria.UI.ItemSlot.orig_RightClick_ItemArray_int_int orig, Item[] inv, int context, int slot)
        {
            DestinyPlayer player = Main.LocalPlayer.GetModPlayer<DestinyPlayer>();
            if (!Main.LocalPlayer.armor.IndexInRange(slot))
            {
                orig.Invoke(inv, context, slot);
                return;
            }
            if (Main.LocalPlayer.armor[slot].modItem is IClassArmor armor)
            {
                if (armor.ArmorClassType() != player.classType && DestinyClientConfig.Instance.RestrictClassItems && context == 9)
                {
                    return;
                }
            }
            orig.Invoke(inv, context, slot);
        }
    }
}