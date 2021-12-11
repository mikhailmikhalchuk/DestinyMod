using TheDestinyMod.Core.Autoloading;
using Terraria;

namespace TheDestinyMod.Content.Autoloading.Mono
{
    public class ItemSlotLeftClick : IAutoloadable
    {
        public void IAutoloadable_Load(IAutoloadable createdObject) => On.Terraria.UI.ItemSlot.LeftClick_ItemArray_int_int += ItemSlot_LeftClick_ItemArray_int_int;

        public void IAutoloadable_PostSetUpContent() { }

        public void IAutoloadable_Unload() { }

        private void ItemSlot_LeftClick_ItemArray_int_int(On.Terraria.UI.ItemSlot.orig_LeftClick_ItemArray_int_int orig, Item[] inv, int context, int slot)
        {
            DestinyPlayer player = Main.LocalPlayer.GetModPlayer<DestinyPlayer>();
            if (Main.mouseItem.modItem is IClassArmor armor)
            {
                if (armor.ArmorClassType() != player.classType && DestinyClientConfig.Instance.RestrictClassItems && context == 8)
                {
                    return;
                }
            }
            orig.Invoke(inv, context, slot);
        }
    }
}