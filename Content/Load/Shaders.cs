using DestinyMod.Content.Items.Equipables.Dyes;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace DestinyMod.Content.Load
{
    public sealed class Shaders : ILoadable
    {
        public static Ref<Effect> ShockwaveEffect;

        public void Load(Mod mod)
        {
            if (Main.dedServ)
            {
                return;
            }

            GameShaders.Armor.BindShader(ModContent.ItemType<GambitDye>(), new ArmorShaderData(new Ref<Effect>(mod.Assets.Request<Effect>("Assets/Effects/Dyes/Gambit").Value), "GambitDyePass"))
                .UseColor(0, 1f, 0);

            GameShaders.Armor.BindShader(ModContent.ItemType<GuardianGamesDye>(), new ArmorShaderData(new Ref<Effect>(mod.Assets.Request<Effect>("Assets/Effects/Dyes/GuardianGames").Value), "GuardianGamesDyePass"))
                .UseColor(2f, 2f, 0f)
                .UseSecondaryColor(2f, 0.25f, 0.35f);

            ShockwaveEffect = new Ref<Effect>(mod.Assets.Request<Effect>("Assets/Effects/Shaders/Shockwave", AssetRequestMode.ImmediateLoad).Value);
            Filters.Scene["DestinyMod:Shockwave"] = new Filter(new ScreenShaderData(ShockwaveEffect, "DestinyMod:Shockwave"), EffectPriority.VeryHigh);
            Filters.Scene["DestinyMod:Shockwave"].Load();
        }

        public void Unload() { }
    }
}