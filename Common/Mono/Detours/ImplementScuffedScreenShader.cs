using DestinyMod.Content.Load;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.ModLoader;

namespace DestinyMod.Common.Mono.Detours
{
    public class ImplementScuffedScreenShader : ILoadable
	{
		public static int ShaderTime = 0;

		public void Load(Mod mod)
		{
            On.Terraria.GameContent.Events.ScreenObstruction.Draw += ScreenObstruction_Draw;
		}

        private void ScreenObstruction_Draw(On.Terraria.GameContent.Events.ScreenObstruction.orig_Draw orig, SpriteBatch spriteBatch)
        {
			orig.Invoke(spriteBatch);

			if (Main.gameMenu || Main.mapFullscreen)
			{
				return;
			}

			if (Main.keyState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.O) && Main.oldKeyState.IsKeyUp(Microsoft.Xna.Framework.Input.Keys.O))
			{
				Main.NewText("Activating Shockwave");
				if (Main.netMode != NetmodeID.Server && !Filters.Scene["DestinyMod:Shockwave"].IsActive())
				{
					ShaderTime = 180;
					Filters.Scene["DestinyMod:Shockwave"].Activate(Main.LocalPlayer.Center);
					Vector2 value = new Vector2(Main.offScreenRange, Main.offScreenRange);
					Vector2 value2 = new Vector2(Main.screenWidth, Main.screenHeight) / Main.GameViewMatrix.Zoom;
					Vector2 value3 = new Vector2(Main.screenWidth, Main.screenHeight) * 0.5f;
					Vector2 value4 = Main.screenPosition + value3 * (Vector2.One - Vector2.One / Main.GameViewMatrix.Zoom);
					Shaders.ShockwaveEffect.Value.Parameters["uTime"].SetValue(Main.GlobalTimeWrappedHourly);
					Shaders.ShockwaveEffect.Value.Parameters["uScreenResolution"].SetValue(value2);
					Shaders.ShockwaveEffect.Value.Parameters["uScreenPosition"].SetValue(value4 - value);
					Shaders.ShockwaveEffect.Value.Parameters["uZoom"].SetValue(Main.GameViewMatrix.Zoom);

					Shaders.ShockwaveEffect.Value.Parameters["uTargetPosition"].SetValue(Main.LocalPlayer.Center);
					Shaders.ShockwaveEffect.Value.Parameters["active"].SetValue(true);
					Shaders.ShockwaveEffect.Value.Parameters["rCount"].SetValue(3f);
					Shaders.ShockwaveEffect.Value.Parameters["rSize"].SetValue(5f);
					Shaders.ShockwaveEffect.Value.Parameters["rSpeed"].SetValue(15f);
				}
			}

			if (ShaderTime-- > 0)
			{
				Main.spriteBatch.End();
				Main.spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null);
				Shaders.ShockwaveEffect.Value.CurrentTechnique.Passes[0].Apply();
				Main.spriteBatch.Draw(ModContent.Request<Texture2D>("Terraria/Images/MagicPixel").Value, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), Color.Transparent);

				if (Main.netMode != NetmodeID.Server && Filters.Scene["DestinyMod:Shockwave"].IsActive())
				{
					float progress = (180f - ShaderTime) / 60f;
					Shaders.ShockwaveEffect.Value.Parameters["uProgress"].SetValue(progress);
					Shaders.ShockwaveEffect.Value.Parameters["uOpacity"].SetValue(100 * (1 - progress / 3f));
				}
			}
			else if (Main.netMode != NetmodeID.Server && ShaderTime <= 0 && Filters.Scene["DestinyMod:Shockwave"].IsActive())
			{
				Filters.Scene["DestinyMod:Shockwave"].GetShader().Shader.Parameters["active"].SetValue(false);
				Filters.Scene["DestinyMod:Shockwave"].Deactivate();
			}
		}

        public void Unload() { }
	}
}
