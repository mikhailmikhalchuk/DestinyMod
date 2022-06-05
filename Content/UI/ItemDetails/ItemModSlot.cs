using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using ReLogic.Content;
using DestinyMod.Common.UI;
using DestinyMod.Common.Items.Modifiers;

namespace DestinyMod.Content.UI.ItemDetails
{
    public class ItemModSlot : UIImageWithBackground
    {
        public ItemMod ItemMod { get; private set; }

        public static Texture2D BackgroundTexture => ModContent.Request<Texture2D>("DestinyMod/Content/UI/ItemDetails/ModSlot", AssetRequestMode.ImmediateLoad).Value;

        public ItemModSlot(ItemMod itemMod = null, int scaleSize = 34) : base(BackgroundTexture, null, scaleSize) => UpdateItemMod(itemMod);

        public void UpdateItemMod(ItemMod newMod)
        {
            ItemMod = newMod;
            if (ItemMod == null)
            {
                Image = ModContent.Request<Texture2D>("DestinyMod/Content/UI/ItemDetails/ModSlot", AssetRequestMode.ImmediateLoad);
            }
            else
            {
                Image = ModContent.Request<Texture2D>(ItemMod.Texture, AssetRequestMode.ImmediateLoad);
            }
        }
    }
}