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

        public override void PostUpdateMiscEffects()
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
					ShaderTime = 180;
					Filters.Scene.Activate("DestinyMod:Shockwave", Main.LocalPlayer.Center); // I am going to have an anuerism 
					Shaders.ShockwaveEffect.Value.Parameters["uTargetPosition"].SetValue(Main.LocalPlayer.Center);
					//Shaders.ShockwaveEffect.Value.Parameters["active"].SetValue(true);
					//Shaders.ShockwaveEffect.Value.Parameters["rCount"].SetValue(3f);
					//Shaders.ShockwaveEffect.Value.Parameters["rSize"].SetValue(5f);
					//Shaders.ShockwaveEffect.Value.Parameters["rSpeed"].SetValue(15f);
					Shaders.ShockwaveEffect.Value.Parameters["uColor"].SetValue(new Vector3(3, 5, 15));
				}
			}

			if (ShaderTime > 0)
			{
				if (Main.netMode != NetmodeID.Server && Shaders.Shockwave.IsActive())
				{
					float progress = (180f - ShaderTime) / 180f;
					Shaders.ShockwaveEffect.Value.Parameters["uProgress"].SetValue(progress);
					Shaders.ShockwaveEffect.Value.Parameters["uOpacity"].SetValue(100 * (1 - progress));
				}
			}
			else if (Main.netMode != NetmodeID.Server && ShaderTime <= 0 && Shaders.Shockwave.IsActive())
			{
				//Shaders.ShockwaveEffect.Value.Parameters["active"].SetValue(false);
				Shaders.Shockwave.Deactivate();
			}
		}
    }
}
