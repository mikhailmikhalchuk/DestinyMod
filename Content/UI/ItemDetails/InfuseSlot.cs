using DestinyMod.Common.UI;
using DestinyMod.Content.Items.Materials;
using DestinyMod.Content.UI.MouseText;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace DestinyMod.Content.UI.ItemDetails
{
    public class InfuseSlot : UIImageWithBackground
    {
        public Keys Key { get; set; }

        public int PressDuration;

        public int PressCriterion { get; private set; }

        public InfuseSlot(int pressCriterion, Keys key = Keys.F) : base(ModContent.Request<Texture2D>("DestinyMod/Content/UI/ItemDetails/ModSlot", AssetRequestMode.ImmediateLoad).Value, 
            ModContent.Request<Texture2D>("DestinyMod/Content/UI/ItemDetails/InfuseSlot", AssetRequestMode.ImmediateLoad), 34)
        {
            PressCriterion = pressCriterion;
            Key = key;
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

            MouseText_RequiredItem reqItemDisplay = new MouseText_RequiredItem(ModContent.ItemType<UpgradeModule>(), 1);
            reqItemDisplay.Width.Pixels = 420;
            ModContent.GetInstance<MouseTextState>().AppendToMasterBackground(reqItemDisplay);

            if (Main.mouseItem == null || !Main.mouseItem.active || Main.mouseItem.type != itemDetailsState.InspectedItem.type)
            {
                PressDuration = 0;
                return;
            }

            if (Main.keyState.IsKeyDown(Key))
            {
                PressDuration++;
            }
            else
            {
                if (PressDuration > PressCriterion)
                {

                }

                PressDuration = 0;
            }
        }
    }
}
