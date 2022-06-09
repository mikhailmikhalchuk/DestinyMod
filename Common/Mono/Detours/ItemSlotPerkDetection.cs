using DestinyMod.Common.GlobalItems;
using DestinyMod.Common.Items;
using DestinyMod.Content.UI.ItemDetails;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;

namespace DestinyMod.Common.Mono.Detours
{
    public sealed class ItemSlotPerkSystem : ILoadable
    {
        public void Load(Mod mod)
        {
            On.Terraria.UI.ItemSlot.RightClick_ItemArray_int_int += ItemSlot_RightClick_ItemArray_int_int;
        }

        public void Unload() { }

        private void ItemSlot_RightClick_ItemArray_int_int(On.Terraria.UI.ItemSlot.orig_RightClick_ItemArray_int_int orig, Item[] inv, int context, int slot)
        {
            orig.Invoke(inv, context, slot);

            Item item = inv[slot];
            if (item.IsAir || !Main.mouseRight || !Main.mouseRightRelease || !ItemData.ItemDatasByID.ContainsKey(item.type))
            {
                return;
            }

            UserInterface itemDetailsState = ModContent.GetInstance<ItemDetailsState>().UserInterface;
            if (itemDetailsState.CurrentState == null)
            {
                SoundEngine.PlaySound(SoundID.MenuOpen);
                itemDetailsState.SetState(new ItemDetailsState(item));
            }
            else
            {
                SoundEngine.PlaySound(SoundID.MenuClose);
                itemDetailsState.SetState(null);
            }
        }
    }
}