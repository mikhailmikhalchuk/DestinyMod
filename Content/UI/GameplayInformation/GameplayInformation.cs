using Microsoft.Xna.Framework;
using DestinyMod.Core.UI;
using Terraria.UI;
using DestinyMod.Core.Extensions;

namespace DestinyMod.Content.UI.GameplayInformation
{
    // Soon to be expanded with information about supers and class abilities
    public partial class GameplayInformationState : DestinyModUIState
    {
        public bool Visible;

        public UIElement MasterBackground { get; private set; }

        public override void PreLoad(ref string name)
        {
            AutoSetState = true;
            AutoAddHandler = true;
        }

        public override UIHandler Load() => new UIHandler(UserInterface, "Vanilla: Inventory", LayerName);

        public AmmoDisplay AmmoDisplay;

        public override void OnInitialize()
        {
            MasterBackground = new UIElement();
            MasterBackground.Width.Pixels = 100;
            MasterBackground.Height.Pixels = 100;

            AmmoDisplay = new AmmoDisplay();
            AmmoDisplay.Left.Pixels = 0;
            AmmoDisplay.Top.Pixels = 0;
            MasterBackground.Append(AmmoDisplay);

            Vector2 size = MasterBackground.CalculateChildrenSize();
            MasterBackground.Left.Pixels = 100;
            MasterBackground.Width.Pixels = size.X;
            MasterBackground.Height.Pixels = size.Y;
            MasterBackground.Top.Set(-size.Y - 50, 1f);
            Append(MasterBackground);
        }
    }
}