using Terraria;
using Terraria.UI;
using Microsoft.Xna.Framework.Graphics;

namespace DestinyMod.Content.UI.PerkSelection
{
    public class PerkOption : UIElement
    {
        public Texture2D PerkTexture;

        public string PerkName;

        public bool Selected;

        public PerkOption(Texture2D perkTexture, string perkName)
        {
            PerkTexture = perkTexture;
            PerkName = perkName;
        }

        public override void MouseOver(UIMouseEvent evt)
        {
            base.MouseOver(evt);
        }
    }
}