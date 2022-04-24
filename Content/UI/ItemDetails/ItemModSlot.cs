using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using ReLogic.Content;
using Terraria;
using DestinyMod.Common.UI;
using Terraria.GameContent;

namespace DestinyMod.Content.UI.ItemDetails
{
    public class ItemModSlot : UIImageWithBackground
    {
        public Item Item { get; private set; }

        public static Texture2D BackgroundTexture => ModContent.Request<Texture2D>("DestinyMod/Content/UI/ItemDetails/ModSlot", AssetRequestMode.ImmediateLoad).Value;

        public ItemModSlot(Item item = null, int scaleSize = 50) : base(BackgroundTexture, null, scaleSize)
        {
            Item = item;

            if (Item == null)
            {
                Item = new Item();
                Item.SetDefaults();
            }
            else
            {
                Main.instance.LoadItem(item.type);
                Image = TextureAssets.Item[item.type];
            }
        }

        public void UpdateItem(int type)
        {
            Item.SetDefaults(type);
            Main.instance.LoadItem(type);
            Image = TextureAssets.Item[type];
        }
    }
}