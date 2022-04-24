using Terraria.UI;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using ReLogic.Content;
using Microsoft.Xna.Framework;
using DestinyMod.Common.Items.PerksAndMods;
using System.Collections.Generic;
using Terraria.GameContent;

namespace DestinyMod.Content.UI.ItemDetails
{
    public class ItemPerkDisplay : UIElement
    {
        public ItemPerk ItemPerk { get; private set; }

        public bool IsActive { get; private set; }

        public Asset<Texture2D> PerkTexture { get; private set; }

        public IList<ItemPerkDisplay> InterconnectedPerks;

        public ItemPerkDisplay(ItemPerk itemPerk, bool isActive, params ItemPerkDisplay[] interconnectedPerks)
        {
            ItemPerk = itemPerk;
            IsActive = isActive;
            InterconnectedPerks = interconnectedPerks;
        }

        public override void OnInitialize()
        {
            Width.Pixels = ItemPerk.TextureSize;
            Height.Pixels = ItemPerk.TextureSize;
            PerkTexture = ModContent.Request<Texture2D>(ItemPerk.Texture);
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(TextureAssets.MagicPixel.Value, GetDimensions().ToRectangle(), IsActive ? Color.SkyBlue : Color.Transparent);
            spriteBatch.Draw(PerkTexture.Value, GetDimensions().ToRectangle(), Color.White);
        }

        public void ToggleActive()
        {
            IsActive = true;

            foreach (ItemPerkDisplay itemPerkDisplay in InterconnectedPerks)
            {
                itemPerkDisplay.IsActive = false;
            }
        }
    }
}