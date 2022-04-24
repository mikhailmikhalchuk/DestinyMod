using Terraria;
using Terraria.UI;
using Terraria.ModLoader;
using Terraria.GameContent.UI.Elements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using DestinyMod.Core.UI;
using DestinyMod.Common.UI;
using Terraria.GameContent;

namespace DestinyMod.Content.UI.PerkSelection
{
    public class PerkSelection : DestinyModUIState
    {
        public UIPanel PerkSelectBackground;

        public override void PreLoad(ref string name)
        {
            AutoSetState = false;
            AutoAddHandler = true;
        }

        public override UIHandler Load() => new UIHandler(UserInterface, "Vanilla: Inventory", LayerName);

        public override void OnInitialize()
        {
            PerkSelectBackground = new UIPanel()
            {
                HAlign = 0.5f,
                VAlign = 0.5f,
                Width = StyleDimension.FromPixelsAndPercent(-8, 0.3f),
                Height = StyleDimension.FromPixelsAndPercent(-8, 0.25f)
            };
            Append(PerkSelectBackground);
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            CalculatedStyle dimensions = PerkSelectBackground.GetDimensions();
            Item heldItem = Main.mouseItem.IsAir ? Main.LocalPlayer.HeldItem : Main.mouseItem;

            Texture2D itemTexture = TextureAssets.Item[heldItem.type].Value;
            Rectangle frame = itemTexture.Frame();

            spriteBatch.Draw(itemTexture, new Rectangle((int)dimensions.X + itemTexture.Width, (int)dimensions.Y + itemTexture.Height, itemTexture.Width, itemTexture.Height), frame, Color.White, 0, frame.Size() / 2f, SpriteEffects.None, 0);

            base.DrawSelf(spriteBatch);
        }
    }
}