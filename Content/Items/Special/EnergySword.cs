using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using DestinyMod.Common.Items;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using ReLogic.Content;
using Terraria.Audio;
using Terraria.Graphics.Shaders;
using System;
using System.Reflection;

namespace DestinyMod.Content.Items.Special
{
    public class EnergySword : DestinyModItem
    {
        public static Asset<Texture2D> SwordTexture { get; private set; }

        public static Asset<Texture2D> OutlineTexture { get; private set; }

        public static Asset<Texture2D> HandleTexture { get; private set; }

        public static Asset<Texture2D> Noise { get; private set; }

        public int PulloutTimer;

        public static int PulloutReach { get; private set; } = 10;

        public float ShaderOffset;

        public override void Load()
        {
            if (Main.dedServ)
			{
                return;
			}

            SwordTexture = ModContent.Request<Texture2D>(Texture, AssetRequestMode.ImmediateLoad);
            OutlineTexture = ModContent.Request<Texture2D>(Texture + "_Outline", AssetRequestMode.ImmediateLoad);
            HandleTexture = ModContent.Request<Texture2D>(Texture + "_Handle", AssetRequestMode.ImmediateLoad);
            Noise = ModContent.Request<Texture2D>("DestinyMod/Assets/Noise/EnergySwordNoise", AssetRequestMode.ImmediateLoad);
            Asset<Effect> energySwordEffect = Mod.Assets.Request<Effect>("Assets/Effects/Shaders/EnergySword", AssetRequestMode.ImmediateLoad);
            Ref<Effect> refEnergySwordEffect = new Ref<Effect>(energySwordEffect.Value);
            MiscShaderData energySwordShaderData = new MiscShaderData(refEnergySwordEffect, "EnergySword");
            energySwordShaderData.GetType().GetField("_uImage1", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(energySwordShaderData, Noise);
            GameShaders.Misc["DestinyMod:EnergySword"] = energySwordShaderData;
        }

		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Type-I Energy Sword");
            Tooltip.SetDefault("Ignores enemy defense"
                + "\n\"A noble and ancient weapon, wielded by the strongest of Sangheili.\""
                + "\n\"Requires great skill and bravery to use, and inspires fear in those who face its elegant plasma blade.\"");
		}

		public override void DestinySetDefaults()
        {
            Item.damage = 100;
            Item.DamageType = DamageClass.Melee;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.Rapier;
            Item.knockBack = 2;
            Item.rare = ItemRarityID.Master;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
        }

		public override void OnHold(Player player)
		{
            if (++PulloutTimer == PulloutReach - 5)
			{
                SoundEngine.PlaySound(SoundLoader.GetLegacySoundSlot(Mod, "Sounds/Special/EnergySword/Ready"), player.Center);
            }
		}

        public override void OnRelease(Player player) => PulloutTimer = 0;

        public void ApplyEnergySwordShader()
		{
            ShaderOffset += 0.5f / 512f;
            if (ShaderOffset > 1)
            {
                ShaderOffset = 0;
            }
            MiscShaderData energySwordShader = GameShaders.Misc["DestinyMod:EnergySword"];
            double blueIntensityWave = Math.Sin(Main.GameUpdateCount / 60f * Math.PI);
            float blueIntensity = (float)(blueIntensityWave / 5f + 0.5f);
            double noiseIntensityWave = Math.Sin(Main.GameUpdateCount / 40f * Math.PI);
            float noiseIntensity = (float)(noiseIntensityWave / 8f + 0.625f);
            energySwordShader.UseShaderSpecificData(new Vector4(blueIntensity, ShaderOffset, ShaderOffset, noiseIntensity));
            energySwordShader.Apply();
        }

        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
		{
            spriteBatch.Draw(HandleTexture.Value, position, frame, drawColor, 0f, origin, scale, SpriteEffects.None, 0f);

            if (PulloutTimer == 0)
			{
                return false;
			}

            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.UIScaleMatrix);

            ApplyEnergySwordShader();
            float pulloutProgress = Utils.Clamp((float)PulloutTimer / PulloutReach, 0f, 1f);
            Rectangle swordSourceRectangle = new Rectangle(0, (int)(frame.Height * (1 - pulloutProgress)), (int)(frame.Width * pulloutProgress), (int)(frame.Height * pulloutProgress));
            Vector2 swordPosition = position + new Vector2(0, frame.Height - swordSourceRectangle.Height) * scale;
            spriteBatch.Draw(SwordTexture.Value, swordPosition, swordSourceRectangle, drawColor * 0.33f, 0f, origin, scale, SpriteEffects.None, 0f);

            spriteBatch.Draw(OutlineTexture.Value, swordPosition, swordSourceRectangle, drawColor * 0.8f, 0f, origin, scale, SpriteEffects.None, 0f);

            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Main.UIScaleMatrix);

            spriteBatch.Draw(OutlineTexture.Value, swordPosition, swordSourceRectangle, drawColor * 0.75f, 0f, origin, scale, SpriteEffects.None, 0f);
            return false;
		}

		public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
		{
            spriteBatch.Draw(HandleTexture.Value, Item.position - Main.screenPosition, HandleTexture.Value.Bounds, lightColor, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);

            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.ZoomMatrix);

            ApplyEnergySwordShader();

            spriteBatch.Draw(SwordTexture.Value, Item.position - Main.screenPosition, HandleTexture.Value.Bounds, lightColor * 0.33f, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);

            spriteBatch.Draw(OutlineTexture.Value, Item.position - Main.screenPosition, HandleTexture.Value.Bounds, lightColor * 0, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);

            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
            
            spriteBatch.Draw(OutlineTexture.Value, Item.position - Main.screenPosition, HandleTexture.Value.Bounds, lightColor * 0.75f, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            return false;
        }
	}
}