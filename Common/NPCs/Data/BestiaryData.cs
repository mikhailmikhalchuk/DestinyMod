using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.GameContent.Bestiary;
using Terraria.ModLoader;
using Terraria.UI;

namespace DestinyMod.Common.NPCs.Data
{
    public static class BestiaryData
    {
        public static class CommonTags
        {
            public static class Visuals
            {
                public static DestinySpawnConditionDecorativeOverlayInfoElement Cosmodrome = new DestinySpawnConditionDecorativeOverlayInfoElement("DestinyMod/Assets/Bestiary/Cosmodrome", Color.White)
                {
                    DisplayPriority = 1f
                };
            }
        }
    }

    public class DestinySpawnConditionDecorativeOverlayInfoElement : IBestiaryInfoElement, IBestiaryBackgroundOverlayAndColorProvider
    {
        private string _overlayImagePath;

        private Color? _overlayColor;

        public float DisplayPriority { get; set; }

        public DestinySpawnConditionDecorativeOverlayInfoElement(string overalyImagePath = null, Color? overlayColor = null)
        {
            _overlayImagePath = overalyImagePath;
            _overlayColor = overlayColor;
        }

        public Asset<Texture2D> GetBackgroundOverlayImage()
        {
            if (_overlayImagePath == null)
            {
                return null;
            }

            return ModContent.Request<Texture2D>(_overlayImagePath);
        }

        public Color? GetBackgroundOverlayColor()
        {
            return _overlayColor;
        }

        public UIElement ProvideUIElement(BestiaryUICollectionInfo info)
        {
            return null;
        }
    }
}
