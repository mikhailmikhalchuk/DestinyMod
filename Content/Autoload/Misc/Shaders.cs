using TheDestinyMod.Core.Autoloading;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;

namespace TheDestinyMod.Content.Autoloading.Misc
{
    public class Shaders : IAutoloadable
    {
        public void IAutoloadable_Load(IAutoloadable createdObject)
        {
            if (Main.dedServ)
            {
                return;
            }

            TheDestinyMod mod = TheDestinyMod.Instance;
            GameShaders.Armor.BindShader(ModContent.ItemType<Items.Dyes.GambitDye>(), new ArmorShaderData(new Ref<Effect>(mod.GetEffect("Effects/Dyes/Gambit")), "GambitDyePass")).UseColor(0, 1f, 0);
            GameShaders.Armor.BindShader(ModContent.ItemType<Items.Dyes.GuardianGamesDye>(), new ArmorShaderData(new Ref<Effect>(mod.GetEffect("Effects/Dyes/GuardianGames")), "GuardianGamesDyePass")).UseColor(2f, 2f, 0f).UseSecondaryColor(2f, 0.25f, 0.35f);
            Ref<Effect> screenRef = new Ref<Effect>(mod.GetEffect("Effects/Shaders/ShockwaveEffect"));
            Filters.Scene["TheDestinyMod:Shockwave"] = new Filter(new ScreenShaderData(screenRef, "Shockwave"), EffectPriority.VeryHigh);
            Filters.Scene["TheDestinyMod:Shockwave"].Load();
        }

        public void IAutoloadable_PostSetUpContent() { }

        public void IAutoloadable_Unload() { }
    }
}