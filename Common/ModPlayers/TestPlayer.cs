using DestinyMod.Content.Load;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.ModLoader;

namespace DestinyMod.Common.ModPlayers
{
    public class TestPlayer : ModPlayer
    {
		public int ShaderTime = 0;

		public int ShockwaveDuration = 120;

        /*public override void PostUpdateMiscEffects()
        {
			if (Main.keyState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Z) && Main.oldKeyState.IsKeyUp(Microsoft.Xna.Framework.Input.Keys.Z))
			{
				ShaderTime = 0;
			}

			if (Main.keyState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.O) && Main.oldKeyState.IsKeyUp(Microsoft.Xna.Framework.Input.Keys.O))
			{
				if (Main.netMode != NetmodeID.Server && !Shaders.Shockwave.IsActive())
				{
					Main.NewText("Activating Shockwave");
					ShaderTime = ShockwaveDuration;
					Filters.Scene.Activate("DestinyMod:Shockwave", Main.LocalPlayer.Center); // I am going to have an anuerism 
					Shaders.ShockwaveEffect.Value.Parameters["uTargetPosition"].SetValue(Main.LocalPlayer.Center);
					Shaders.ShockwaveEffect.Value.Parameters["active"].SetValue(true);
					Shaders.ShockwaveEffect.Value.Parameters["uProgress"].SetValue(0.35f);
					Shaders.ShockwaveEffect.Value.Parameters["rBuffer"].SetValue(0.1f);
					Shaders.ShockwaveEffect.Value.Parameters["rDiameter"].SetValue(0.025f);
					//Shaders.ShockwaveEffect.Value.Parameters["rCount"].SetValue(3f);
					//Shaders.ShockwaveEffect.Value.Parameters["rCountScale"].SetValue(0.05f);
					// Shaders.ShockwaveEffect.Value.Parameters["uColor"].SetValue(new Vector3(3, 5, 15));
				}
			}

			if (--ShaderTime > 0)
			{
				if (Shaders.Shockwave.IsActive())
				{
					float progress = (ShockwaveDuration - ShaderTime) / (float)ShockwaveDuration;
					Main.NewText("Updating :( | " + progress);
					Shaders.ShockwaveEffect.Value.Parameters["rSize"].SetValue(progress);
					Shaders.ShockwaveEffect.Value.Parameters["rStrength"].SetValue((1 - progress) * 0.1f);
				}
			}
			else if (Main.netMode != NetmodeID.Server && ShaderTime <= 0 && Shaders.Shockwave.IsActive())
			{
				Shaders.ShockwaveEffect.Value.Parameters["active"].SetValue(false);
				Shaders.Shockwave.Deactivate();
			}
		}*/
    }
}
