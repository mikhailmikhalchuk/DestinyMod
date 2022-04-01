using DestinyMod.Content.Items.Special;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace DestinyMod.Content.DrawLayers
{
	public class EnergySwordPlayerLayer : PlayerDrawLayer
	{
		public override void Load()
		{
			On.Terraria.DataStructures.PlayerDrawLayers.DrawPlayer_RenderAllLayers += RenderEnergySword;
		}

		private void RenderEnergySword(On.Terraria.DataStructures.PlayerDrawLayers.orig_DrawPlayer_RenderAllLayers orig, ref PlayerDrawSet drawInfo)
		{
			orig.Invoke(ref drawInfo);

			Player player = drawInfo.drawPlayer;

			if (player == null || player.itemAnimation > 0)
			{
				return;
			}

			if (player.HeldItem == null || player.HeldItem.ModItem is not EnergySword energySword)
			{
				return;
			}

			Main.spriteBatch.End();
			RasterizerState rasterizerState = (player.gravDir == 1f) ? RasterizerState.CullCounterClockwise : RasterizerState.CullClockwise;
			Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, rasterizerState, null, Main.GameViewMatrix.TransformationMatrix);
			energySword.ApplyEnergySwordShader();

			Vector2 playerScreenPosition = player.Center - Main.screenPosition;
			bool faceLeft = player.direction < 0;
			float pulloutProgress = Utils.Clamp((float)energySword.PulloutTimer / EnergySword.PulloutReach, 0f, 1f);
			int posModBasedOnPullout = faceLeft ? (int)(EnergySword.HeldSwordTexture.Width() * (1 - pulloutProgress)) : 0;
			Vector2 handPosition = playerScreenPosition + new Vector2(faceLeft ? -42 : -2, 4) + new Vector2(posModBasedOnPullout, 0);
			Rectangle swordSourceRectangle = new Rectangle(0, 0, (int)(EnergySword.HeldSwordTexture.Width() * pulloutProgress), EnergySword.HeldSwordTexture.Height());
			Vector2 handleOrigin = new Vector2(12, 15);
			SpriteEffects swordSpriteEffects = faceLeft ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
			Main.spriteBatch.Draw(EnergySword.HeldSwordTexture.Value, handPosition, swordSourceRectangle, Color.White, 0f, handleOrigin, 1f, swordSpriteEffects, 0);
			Main.spriteBatch.Draw(EnergySword.HeldOutlineTexture.Value, handPosition, swordSourceRectangle, Color.White, 0f, handleOrigin, 1f, swordSpriteEffects, 0);

			Main.spriteBatch.End();
			Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, Main.Camera.Sampler, DepthStencilState.None, Main.Camera.Rasterizer, null, Main.Camera.GameViewMatrix.TransformationMatrix);
			Main.spriteBatch.Draw(EnergySword.HeldOutlineTexture.Value, handPosition, swordSourceRectangle, Color.White * 0.75f, 0f, handleOrigin, 1f, swordSpriteEffects, 0);
		}

		public override bool GetDefaultVisibility(PlayerDrawSet drawInfo) => drawInfo.drawPlayer.HeldItem?.type == ModContent.ItemType<EnergySword>();

		public override Position GetDefaultPosition() => new Between(PlayerDrawLayers.ArmOverItem, PlayerDrawLayers.HandOnAcc);

		protected override void Draw(ref PlayerDrawSet drawInfo)
		{
			Player player = drawInfo.drawPlayer;
			if (player.itemAnimation > 0)
			{
				return;
			}
			Vector2 playerScreenPosition = player.Center - Main.screenPosition;
			Vector2 handPosition = playerScreenPosition + new Vector2(player.direction < 0 ? -42 : -2, 4);
			Vector2 handleOrigin = new Vector2(12, 15);
			SpriteEffects swordSpriteEffects = player.direction > 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
			DrawData energySwordHandle = new DrawData(EnergySword.HeldHandleTexture.Value, handPosition, null, Color.White, 0f, handleOrigin, 1f, swordSpriteEffects, 0);
			drawInfo.DrawDataCache.Add(energySwordHandle);
			PlayerDrawSet energyDrawLayer = drawInfo;
			energyDrawLayer.compFrontArmFrame.X = energyDrawLayer.compFrontArmFrame.Width * 4;
			energyDrawLayer.compFrontArmFrame.Y = energyDrawLayer.compFrontArmFrame.Height;
			PlayerDrawLayers.DrawPlayer_28_ArmOverItem(ref energyDrawLayer);
		}
	}
}