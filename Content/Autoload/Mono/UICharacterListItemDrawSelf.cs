using TheDestinyMod.Core.Autoloading;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using System.Reflection;
using Microsoft.Xna.Framework.Graphics;

namespace TheDestinyMod.Content.Autoloading.Mono
{
    public class UICharacterListItemDrawSelf : IAutoloadable
    {
        public void IAutoloadable_Load(IAutoloadable createdObject) => On.Terraria.GameContent.UI.Elements.UICharacterListItem.DrawSelf += UICharacterListItem_DrawSelf;

        public void IAutoloadable_PostSetUpContent() { }

        public void IAutoloadable_Unload() { }

        private void UICharacterListItem_DrawSelf(On.Terraria.GameContent.UI.Elements.UICharacterListItem.orig_DrawSelf orig, Terraria.GameContent.UI.Elements.UICharacterListItem self, SpriteBatch spriteBatch)
        {
            orig.Invoke(self, spriteBatch);
            if (!DestinyClientConfig.Instance.CharacterClassLabels)
                return;
            float width = self.GetInnerDimensions().X + self.GetInnerDimensions().Width;
            Vector2 vector4 = new Vector2(self.GetDimensions().X + 100f, self.GetInnerDimensions().Y + 59f);
            Texture2D texture = ModContent.GetTexture("Terraria/UI/InnerPanelBackground");
            float num = width - vector4.X - 380f;
            spriteBatch.Draw(texture, vector4, new Rectangle(0, 0, 8, texture.Height), Color.White);
            spriteBatch.Draw(texture, new Vector2(vector4.X + 8f, vector4.Y), new Rectangle(8, 0, 8, texture.Height), Color.White, 0f, Vector2.Zero, new Vector2((num - 16f) / 8f, 1f), SpriteEffects.None, 0f);
            spriteBatch.Draw(texture, new Vector2(vector4.X + num - 8f, vector4.Y), new Rectangle(16, 0, 8, texture.Height), Color.White);
            string classType = "None";
            switch (((Terraria.IO.PlayerFileData)self.GetType().GetField("_data", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(self)).Player.GetModPlayer<DestinyPlayer>().classType)
            {
                case DestinyClassType.Titan:
                    classType = "Titan";
                    break;
                case DestinyClassType.Hunter:
                    classType = "Hunter";
                    break;
                case DestinyClassType.Warlock:
                    classType = "Warlock";
                    break;
            }
            vector4 += new Vector2(num * 0.5f - Main.fontMouseText.MeasureString(classType).X * 0.5f, 3f);
            Utils.DrawBorderString(spriteBatch, classType, vector4, Color.White);
        }
    }
}