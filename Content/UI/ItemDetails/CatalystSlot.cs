using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using ReLogic.Content;
using DestinyMod.Common.UI;
using DestinyMod.Common.Items.Modifiers;
using DestinyMod.Content.UI.MouseText;
using Terraria;
using DestinyMod.Common.ModPlayers;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using DestinyMod.Common.Data;

namespace DestinyMod.Content.UI.ItemDetails
{
    public class CatalystSlot : UIImageWithBackground
    {
        public ItemCatalyst Catalyst { get; private set; }

        public static Texture2D BackgroundTexture => ModContent.Request<Texture2D>("DestinyMod/Content/UI/ItemDetails/CatalystSlot", AssetRequestMode.ImmediateLoad).Value;

        public MouseText_TitleAndSubtitle NameElement { get; private set; }

        public MouseText_BodyText DescriptionElement { get; private set; }

        public MouseText_LineText ObjectivesNotice { get; private set; }

        public MouseText_Objectives ObjectivesElement { get; private set; }

        public CatalystSlot(ItemCatalyst itemCatalyst = null, int scaleSize = 34) : base(BackgroundTexture, null, scaleSize)
        {
            Catalyst = itemCatalyst;

            Main.NewText("Discovered Catalyst Count: " + Main.LocalPlayer.GetModPlayer<ItemDataPlayer>().CatalystData.Count);
            Main.NewText("Has Discovered This Catalyst: " + itemCatalyst.IsDiscovered);
            NameElement = new MouseText_TitleAndSubtitle(itemCatalyst.IsDiscovered ? Catalyst.DisplayName : "Empty Catalyst Socket", string.Empty);

            string description = itemCatalyst.IsDiscovered ? Catalyst.Description : "An Exotic catalyst can be inserted into this socket.";
            DescriptionElement = new MouseText_BodyText(description);

            ObjectivesNotice = new MouseText_LineText("Complete Objectives to Unlock")
            {
                BackgroundColor = DestinyColors.MouseTextWarning * MouseTextElement.CommonOpacity
            };

            IList<ItemCatalyst.ItemCatalystRequirement> requirements = itemCatalyst.HandleRequirementMouseText();
            if (requirements != null)
            {
                ObjectivesElement = new MouseText_Objectives(requirements.Select(req => new MouseText_Objective(req.RequirementName, req.RequirementProgress)).ToArray());
            }

            if (!itemCatalyst.IsDiscovered)
            {
                Image = null;
            }
            else
            {
                Image = ModContent.Request<Texture2D>(Catalyst.Texture, AssetRequestMode.ImmediateLoad);
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (ModContent.GetInstance<ItemDetailsState>().UserInterface.CurrentState is not ItemDetailsState itemDetailsState || itemDetailsState.InspectedItem == null)
            {
                return;
            }

            if (!ContainsPoint(Main.MouseScreen))
            {
                return;
            }

            MouseTextState mouseTextState = ModContent.GetInstance<MouseTextState>();
            NameElement.UpdateData(NameElement.TitlePreScale, NameElement.SubtitlePreScale, NameElement.TitleScale, NameElement.SubtitleScale); // Bandaid fix for later
            mouseTextState.AppendToMasterBackground(NameElement);

            if (ObjectivesNotice != null && !Catalyst.IsCompleted && Catalyst.IsDiscovered)
            {
                mouseTextState.AppendToMasterBackground(ObjectivesNotice);
            }

            DescriptionElement.UpdateData(DescriptionElement.TextPreScale, DescriptionElement.TextScale);
            mouseTextState.AppendToMasterBackground(DescriptionElement);

            if (ObjectivesElement != null && Catalyst.IsDiscovered)
            {
                mouseTextState.AppendToMasterBackground(ObjectivesElement);
            }
        }
    }
}