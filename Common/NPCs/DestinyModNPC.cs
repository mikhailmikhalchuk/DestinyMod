using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace DestinyMod.Common.NPCs
{
	public abstract class DestinyModNPC : ModNPC
	{
		public sealed override void SetDefaults()
		{
			AutomaticSetDefaults();
			DestinySetDefaults();
		}

		public void DrawForcefield(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor, Vector2 shieldOffset = default, float scaleAdjust = 1f)
        {
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
			DrawData forceFieldData = new DrawData((Texture2D)Main.Assets.Request<Texture2D>("Images/Misc/Perlin"), NPC.Center - screenPos - shieldOffset, (Rectangle?)new Rectangle(0, 0, 700, 470), drawColor, 0, new Vector2(350, 235), NPC.scale * scaleAdjust, SpriteEffects.None, 0);
			GameShaders.Misc["ForceField"].UseColor(new Vector3(1));
			GameShaders.Misc["ForceField"].Apply(forceFieldData);
			forceFieldData.Draw(spriteBatch);
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, Main.GameViewMatrix.TransformationMatrix);
		}

		public virtual void AutomaticSetDefaults() { }

		public virtual void DestinySetDefaults() { }

		/// <summary>
		/// Do NOT save instanced data here.
		/// </summary>
		/// <param name="tagCompound">The <see cref="TagCompound"/> to add data to.</param>
		public virtual void Save(TagCompound tagCompound) { }
		
		/// <summary>
		/// Do NOT load instanced data here.
		/// </summary>
		/// <param name="tagCompound">The <see cref="TagCompound"/> to load data from.</param>
		public virtual void Load(TagCompound tagCompound) { }
	}
}