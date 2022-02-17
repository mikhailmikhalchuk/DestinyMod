using DestinyMod.Content.Items.Equipables.Dyes;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace DestinyMod.Content.Load
{
    public class Shaders : ILoadable
    {
        public void Load(Mod mod)
        {
            if (Main.dedServ)
            {
                return;
            }

            GameShaders.Armor.BindShader(ModContent.ItemType<GambitDye>(), new ArmorShaderData(new Ref<Effect>(mod.Assets.Request<Effect>("Effects/Dyes/Gambit").Value), "GambitDyePass"))
                .UseColor(0, 1f, 0);
            GameShaders.Armor.BindShader(ModContent.ItemType<GuardianGamesDye>(), new ArmorShaderData(new Ref<Effect>(mod.Assets.Request<Effect>("Effects/Dyes/GuardianGames").Value), "GuardianGamesDyePass"))
                .UseColor(2f, 2f, 0f)
                .UseSecondaryColor(2f, 0.25f, 0.35f);
            Ref<Effect> screenRef = new Ref<Effect>(mod.Assets.Request<Effect>("Effects/Shaders/ShockwaveEffect").Value);
            Filters.Scene["TheDestinyMod:Shockwave"] = new Filter(new ScreenShaderData(screenRef, "Shockwave"), EffectPriority.VeryHigh);
            Filters.Scene["TheDestinyMod:Shockwave"].Load();
        }

        public void Unload() { }
    }
}