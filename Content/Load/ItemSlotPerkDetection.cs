using DestinyMod.Common.GlobalItems;
using DestinyMod.Content.UI.ItemDetails;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace DestinyMod.Content.Load
{
    public sealed class ItemSlotPerkSystem : ILoadable
    {
        public void Load(Mod mod)
        {
            On.Terraria.UI.ItemSlot.RightClick_ItemArray_int_int += ItemSlot_RightClick_ItemArray_int_int;
        }

        private void ItemSlot_RightClick_ItemArray_int_int(On.Terraria.UI.ItemSlot.orig_RightClick_ItemArray_int_int orig, Item[] inv, int context, int slot)
        {
            orig.Invoke(inv, context, slot);

            if (inv[slot].IsAir || !Main.mouseRight || !Main.mouseRightRelease)
            {
                return;
            }
            if (ModContent.GetInstance<ItemDetailsState>().UserInterface.CurrentState == null)
            {
                SoundEngine.PlaySound(SoundID.MenuOpen);
                ModContent.GetInstance<ItemDetailsState>().UserInterface.SetState(new ItemDetailsState(inv[slot]));
            }
            else
            {
                SoundEngine.PlaySound(SoundID.MenuClose);
                ModContent.GetInstance<ItemDetailsState>().UserInterface.SetState(null);
            }
        }

        public void Unload() { }
    }
}