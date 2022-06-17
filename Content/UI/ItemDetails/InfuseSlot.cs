using DestinyMod.Common.Data;
using DestinyMod.Common.GlobalItems;
using DestinyMod.Common.UI;
using DestinyMod.Content.Items.Materials;
using DestinyMod.Content.UI.MouseText;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace DestinyMod.Content.UI.ItemDetails
{
    public class InfuseSlot : UIImageWithBackground
    {
        public Keys Key { get; set; }

        public int PressDuration;

        public int PressCriterion { get; private set; }

        public MouseText_RequiredItem RequiredItemElement { get; private set; }

        public MouseText_KeyIndicator InfuseProgressElement { get; private set; }

        public MouseText_LineText PowerWarningElement { get; private set; }

        public InfuseSlot(int pressCriterion, Keys key = Keys.F) : base(ModContent.Request<Texture2D>("DestinyMod/Content/UI/ItemDetails/ModSlot", AssetRequestMode.ImmediateLoad).Value, 
            ModContent.Request<Texture2D>("DestinyMod/Content/UI/ItemDetails/InfuseSlot", AssetRequestMode.ImmediateLoad), 34)
        {
            PressCriterion = pressCriterion;
            Key = key;
            RequiredItemElement = new MouseText_RequiredItem(ModContent.ItemType<UpgradeModule>(), 1);
            InfuseProgressElement = new MouseText_KeyIndicator("Dismantle for Infusion");
            PowerWarningElement = new MouseText_LineText("Base Power Too Low")
            {
                BackgroundColor = DestinyColors.MouseTextWarning * MouseTextElement.CommonOpacity
            };
        }

        public override void Update(GameTime gameTime)
        {
            if (ModContent.GetInstance<ItemDetailsState>().UserInterface.CurrentState is not ItemDetailsState itemDetailsState || itemDetailsState.InspectedItem == null)
            {
                PressDuration = 0;
                return;
            }

            if (!ContainsPoint(Main.MouseScreen))
            {
                PressDuration = 0;
                return;
            }

            InfuseProgressElement.Progress = (float)PressDuration / PressCriterion;

            MouseTextState mouseTextState = ModContent.GetInstance<MouseTextState>();

            if (Main.mouseItem == null || Main.mouseItem.IsAir || !Main.mouseItem.active || !Main.LocalPlayer.HasItem(ModContent.ItemType<UpgradeModule>()))
            {
                PressDuration = 0;
                mouseTextState.AppendToMasterBackground(RequiredItemElement);
                mouseTextState.AppendToMasterBackground(InfuseProgressElement);
                return;
            }

            ItemDataItem mouseItemData = Main.mouseItem.GetGlobalItem<ItemDataItem>();
            ItemDataItem inspectedItemData = itemDetailsState.InspectedItemData;
            if (mouseItemData.LightLevel <= inspectedItemData.LightLevel)
            {
                PressDuration = 0;
                mouseTextState.AppendToMasterBackground(PowerWarningElement);
                mouseTextState.AppendToMasterBackground(RequiredItemElement);
                mouseTextState.AppendToMasterBackground(InfuseProgressElement);
                return;
            }

            mouseTextState.AppendToMasterBackground(RequiredItemElement);
            mouseTextState.AppendToMasterBackground(InfuseProgressElement);

            if (Main.keyState.IsKeyDown(Key))
            {
                PressDuration++;
            }
            else
            {
                if (PressDuration > PressCriterion)
                {
                    inspectedItemData.LightLevel = mouseItemData.LightLevel;
                    inspectedItemData.SetDefaults(itemDetailsState.InspectedItem);
                    itemDetailsState.InspectedItemPowerLevel.SetText(inspectedItemData.LightLevel.ToString());

                    Main.LocalPlayer.ConsumeItem(ModContent.ItemType<UpgradeModule>());
                    SoundEngine.PlaySound(SoundID.Grab);

                    Main.mouseItem.stack--;
                    if (Main.mouseItem.stack < 0)
                    {
                        Main.mouseItem.TurnToAir();
                    }

                    PressDuration = 0;
                }

                PressDuration = 0;
            }
        }
    }
}
