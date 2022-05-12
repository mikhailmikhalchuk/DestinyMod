using DestinyMod.Content.UI.ItemDetails;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace DestinyMod.Content.Load
{
    public sealed class ItemSlotPerkSystem : ILoadable
    {
        private double _past;

        public void Load(Mod mod)
        {
            On.Terraria.UI.ItemSlot.MouseHover_ItemArray_int_int += ItemSlot_MouseHover_ItemArray_int_int;
        }

        private void ItemSlot_MouseHover_ItemArray_int_int(On.Terraria.UI.ItemSlot.orig_MouseHover_ItemArray_int_int orig, Item[] inv, int context, int slot)
        {
            orig.Invoke(inv, context, slot);

            if (Main.gameTimeCache.TotalGameTime.TotalMilliseconds - _past < 300)
            {
                return;
            }
            if (Main.mouseRight && ModContent.GetInstance<ItemDetailsState>().UserInterface.CurrentState == null)
            {
                SoundEngine.PlaySound(SoundID.MenuOpen);
                ModContent.GetInstance<ItemDetailsState>().UserInterface.SetState(new ItemDetailsState(inv[slot]));
                _past = Main.gameTimeCache.TotalGameTime.TotalMilliseconds;
            }
            else if (Main.mouseRight && ModContent.GetInstance<ItemDetailsState>().UserInterface.CurrentState != null)
            {
                SoundEngine.PlaySound(SoundID.MenuClose);
                ModContent.GetInstance<ItemDetailsState>().UserInterface.SetState(null);
                _past = Main.gameTimeCache.TotalGameTime.TotalMilliseconds;
            }
        }

        public void Unload() { }
    }
}