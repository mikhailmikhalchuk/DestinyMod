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

        public static Filter Shockwave;

        public void Load(Mod mod)
        {
            if (Main.dedServ)
            {
                return;
            }

            ShockwaveEffect = new Ref<Effect>(mod.Assets.Request<Effect>("Assets/Effects/Shaders/Shockwave", AssetRequestMode.ImmediateLoad).Value);
            Shockwave = new Filter(new ScreenShaderData(ShockwaveEffect, "Shockwave"), EffectPriority.VeryHigh);
            Filters.Scene["DestinyMod:Shockwave"] = Shockwave;
            Shockwave.Load();
        }

        public void Unload() { }
    }
}