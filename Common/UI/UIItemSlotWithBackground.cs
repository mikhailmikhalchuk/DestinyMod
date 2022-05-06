using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.UI;

namespace DestinyMod.Common.UI
{
    // This only handles items in stacks of 1
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

        public delegate void DelegateOnUpdateItem(UIItemSlotWithBackground uIItemSlotWithBackground);

        public event DelegateOnUpdateItem OnUpdateItem;

        public static Player Player => Main.LocalPlayer;

        public UIItemSlotWithBackground(Texture2D background, int? scaleSize = null, Item item = null, Func<Item, bool> isItemValid = null) : base(background, scaleSize: scaleSize ?? background.Width)
        {
            InternalItem = new Item();
            Item = item;
            IsItemValid = isItemValid;
        }

        public override void MouseDown(UIMouseEvent evt)
        {
            base.MouseDown(evt);

            if (BlockItemInput)
            {
                return;
            }

            if (!PlayerInput.IgnoreMouseInterface)
            {
                Main.LocalPlayer.mouseInterface = true;
                HandleLeftClick();
            }
        }

        public override void RightMouseDown(UIMouseEvent evt)
        {
            base.RightMouseDown(evt);

            if (BlockItemInput)
            {
                return;
            }

            if (!PlayerInput.IgnoreMouseInterface)
            {
                Main.LocalPlayer.mouseInterface = true;
                HandleRightClick();
            }
        }

        public void SwapMouseAndCurrentItem()
        {
            Item currentItem = Item.Clone();
            Item = Main.mouseItem;
            Main.mouseItem = currentItem;
        }

        public static void ConsumeMouseItem()
        {
            Main.mouseItem.stack--;
            if (Main.mouseItem.stack < 0)
            {
                Main.mouseItem.SetDefaults();
            }
        }

        public void HandleLeftClick()
        {
            IEntitySource uiItemSlotEntitySource = Player.GetSource_Misc("UI");

            Item currentItem = Item.Clone();
            if (Main.keyState.IsKeyDown(Keys.LeftShift) || Main.keyState.IsKeyDown(Keys.RightShift))
            {
                Player.QuickSpawnClonedItem(uiItemSlotEntitySource, Item);
                Item = new Item();
                goto ret;
            }

            if (Main.mouseItem.IsAir)
            {
                Main.mouseItem = currentItem;
                Item = new Item();
                goto ret;
            }

            if (IsItemValid == null || IsItemValid(Main.mouseItem))
            {
                Item = Main.mouseItem;
                ConsumeMouseItem();
                if (Main.mouseItem.IsAir)
                {
                    Main.mouseItem = currentItem;
                    goto ret;
                }

                if (Main.mouseItem.type == currentItem.type && Main.mouseItem.stack < Main.mouseItem.maxStack)
                {
                    Main.mouseItem.stack++;
                }
                else
                {
                    Player.QuickSpawnClonedItem(uiItemSlotEntitySource, currentItem);
                }
            }

        ret: // Evil beyond belief
            if (currentItem.type != Item.type)
            {
                SoundEngine.PlaySound(SoundID.Grab);
            }
            OnUpdateItem?.Invoke(this);
        }

        public void HandleRightClick()
        {
            IEntitySource uiItemSlotEntitySource = Player.GetSource_Misc("UI");

            Item currentItem = Item.Clone();
            if (Main.mouseItem.IsAir)
            {
                Main.mouseItem = currentItem;
                Item = new Item();
                goto ret;
            }

            if (IsItemValid == null || IsItemValid(Main.mouseItem))
            {
                Item = Main.mouseItem;
                ConsumeMouseItem();
                if (Main.mouseItem.IsAir)
                {
                    Main.mouseItem = currentItem;
                    goto ret;
                }

                if (Main.mouseItem.type == currentItem.type && Main.mouseItem.stack < Main.mouseItem.maxStack)
                {
                    Main.mouseItem.stack++;
                }
                else
                {
                    Player.QuickSpawnClonedItem(uiItemSlotEntitySource, currentItem);
                }
            }

        ret: // Evil beyond belief
            if (currentItem.type != Item.type)
            {
                SoundEngine.PlaySound(SoundID.Grab);
            }
            OnUpdateItem?.Invoke(this);
        }

        public void InvokeOnUpdateItem() => OnUpdateItem?.Invoke(this); // Supposedly bad practice but Is dos nots cares
    }
}