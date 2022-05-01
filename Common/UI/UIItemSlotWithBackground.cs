using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.GameInput;
using Terraria.UI;

namespace DestinyMod.Common.UI
{
    public class UIItemSlotWithBackground : UIImageWithBackground
    {
        private Item InternalItem;

        public Item Item
        {
            get => InternalItem;
            set
            {
                if (value == null)
                {
                    InternalItem.SetDefaults();
                    Image = null;
                }
                else
                {
                    InternalItem.SetDefaults(value.type);
                    Main.instance.LoadItem(InternalItem.type);
                    Image = TextureAssets.Item[InternalItem.type];
                }
            }
        }

        public bool BlockItemInput;

        public Func<Item, bool> IsItemValid;

        public event Action<UIItemSlotWithBackground> OnUpdateItem;

        public UIItemSlotWithBackground(Texture2D background, int? scaleSize = null, Item item = null, Func<Item, bool> isItemValid = null) : base(background, scaleSize: scaleSize ?? background.Width)
        {
            InternalItem = new Item();
            Item = item;
            IsItemValid = isItemValid;
        }

        public override void Click(UIMouseEvent evt)
        {
            base.Click(evt);

            if (BlockItemInput)
            {
                return;
            }

            if (!PlayerInput.IgnoreMouseInterface && ContainsPoint(Main.MouseScreen))
            {
                Main.LocalPlayer.mouseInterface = true;

                if (IsItemValid == null || IsItemValid(Main.mouseItem))
                {
                    Item = Main.mouseItem;
                    OnUpdateItem?.Invoke(this);
                }
            }
        }

        public void InvokeOnUpdateItem() => OnUpdateItem?.Invoke(this); // Supposedly bad practice but I do not care
    }
}