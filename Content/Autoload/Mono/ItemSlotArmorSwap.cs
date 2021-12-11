using TheDestinyMod.Core.Autoloading;
using Terraria;

namespace TheDestinyMod.Content.Autoloading.Mono
{
	public class ItemSlotArmorSwap : IAutoloadable
	{
		public void IAutoloadable_Load(IAutoloadable createdObject) => On.Terraria.UI.ItemSlot.ArmorSwap += ItemSlot_ArmorSwap;

		public void IAutoloadable_PostSetUpContent() { }

		public void IAutoloadable_Unload() { }

        private Item ItemSlot_ArmorSwap(On.Terraria.UI.ItemSlot.orig_ArmorSwap orig, Item item, out bool success)
        {
            success = false;
            DestinyPlayer player = Main.LocalPlayer.GetModPlayer<DestinyPlayer>();
            if (item.modItem is IClassArmor armor)
            {
                if (armor.ArmorClassType() != player.classType && DestinyClientConfig.Instance.RestrictClassItems)
                {
                    return item;
                }
            }
            return orig.Invoke(item, out success);
        }
    }
}