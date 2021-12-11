using TheDestinyMod.Core.Autoloading;
using Terraria;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using Terraria.ModLoader;

namespace TheDestinyMod.Content.Autoloading.Mono
{
    public class MainDrawMouseOver : IAutoloadable
    {
        public void IAutoloadable_Load(IAutoloadable createdObject) => On.Terraria.Main.DrawMouseOver += Main_DrawMouseOver;

        public void IAutoloadable_PostSetUpContent() { }

        public void IAutoloadable_Unload() { }

        private void Main_DrawMouseOver(On.Terraria.Main.orig_DrawMouseOver orig, Main self)
        {
            Rectangle mousePos = new Rectangle((int)(Main.mouseX + Main.screenPosition.X), (int)(Main.mouseY + Main.screenPosition.Y), 1, 1);
            List<int> TypesToNotDraw = new List<int>()
            {
                ModContent.NPCType<NPCs.Vex.VaultOfGlass.EncounterBox>(),
                ModContent.NPCType<NPCs.Vex.VaultOfGlass.DetainmentBubble>()
            };

            if (Main.npc.FirstOrDefault(npc => TypesToNotDraw.Contains(npc.type) && new Rectangle((int)npc.Bottom.X - npc.frame.Width / 2, (int)npc.Bottom.Y - npc.frame.Height, npc.frame.Width, npc.frame.Height).Intersects(mousePos)) == null)
            {
                orig.Invoke(self);
            }
        }
    }
}