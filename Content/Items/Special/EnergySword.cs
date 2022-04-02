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
using DestinyMod.Common.ModPlayers;
using Terraria.DataStructures;
using Microsoft.Xna.Framework.Audio;

namespace DestinyMod.Content.Items.Special
{
    public class EnergySword : DestinyModItem
    {
        public static Asset<Texture2D> SwordTexture { get; private set; }

        public static Asset<Texture2D> HeldSwordTexture { get; private set; }

        public static Asset<Texture2D> OutlineTexture { get; private set; }

        public static Asset<Texture2D> HeldOutlineTexture { get; private set; }

        public static Asset<Texture2D> HandleTexture { get; private set; }

        public static Asset<Texture2D> HeldHandleTexture { get; private set; }

        public static Asset<Texture2D> Noise { get; private set; }

        public int PulloutTimer;

        public static int PulloutReach { get; private set; } = 10;

        public float ShaderOffset;

        public SoundEffectInstance Hum { get; private set; }

        public override void Load()
        {
            if (Main.dedServ)
            {
                return;
            }

            SwordTexture = ModContent.Request<Texture2D>(Texture, AssetRequestMode.ImmediateLoad);
            HeldSwordTexture = ModContent.Request<Texture2D>(Texture + "_Held", AssetRequestMode.ImmediateLoad);

            string outlinePath = Texture + "_Outline";
            OutlineTexture = ModContent.Request<Texture2D>(outlinePath, AssetRequestMode.ImmediateLoad);
            HeldOutlineTexture = ModContent.Request<Texture2D>(outlinePath + "_Held", AssetRequestMode.ImmediateLoad);

            string handlePath = Texture + "_Handle";
            HandleTexture = ModContent.Request<Texture2D>(handlePath, AssetRequestMode.ImmediateLoad);
            HeldHandleTexture = ModContent.Request<Texture2D>(handlePath + "_Held", AssetRequestMode.ImmediateLoad);

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
                + "\nGreatly increases maximum running speed when held"
                + "\nRight Click to lunge"
                + "\n'A noble and ancient weapon, wielded by the strongest of Sangheili.'"
                + "\n'Requires great skill and bravery to use, and inspires fear in those who face its elegant plasma blade.'");
        }

        public override void DestinySetDefaults()
        {
            Item.damage = 100;
            Item.DamageType = DamageClass.Melee;
            Item.useTime = 10;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 2;
            Item.rare = ItemRarityID.Master;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.useTurn = true;
        }

        public override bool AltFunctionUse(Player player) => true;

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.useStyle = ItemUseStyleID.Thrust;
                DestinyModReuseDelay = 120;
            }
            else
			{
                Item.useStyle = ItemUseStyleID.Swing;
                DestinyModReuseDelay = 0;
            }
            return true;
		}
		public override void UseAnimation(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Vector2 direction = player.DirectionTo(Main.MouseWorld);
                player.velocity = direction * (20f + Math.Abs(player.velocity.Length()));
                player.direction = Math.Sign(player.velocity.X);
            }
        }

		public override void OnHold(Player player)
        {
            if (++PulloutTimer == PulloutReach - 2)
            {
                SoundEngine.PlaySound(SoundLoader.GetLegacySoundSlot(Mod, "Assets/Sounds/Special/EnergySword/Ready"), player.Center);
                Hum = SoundEngine.PlaySound(SoundLoader.GetLegacySoundSlot(Mod, "Assets/Sounds/Special/EnergySword/Hum").AsAmbient(), player.Center);
            }

            if (Hum != null && PulloutTimer >= PulloutReach - 2 && Hum.State == SoundState.Stopped)
			{
                Hum.Play();
            }
        }

        public override void OnRelease(Player player)
        {
            PulloutTimer = 0;

            if (Hum != null && Hum.State != SoundState.Stopped)
			{
                Hum.Stop();
			}
        }

        public override ItemPlayer.IterationContext PostUpdateRunSpeedsContext(Player player) => ItemPlayer.IterationContext.HeldItem;

        public override void PostUpdateRunSpeeds(Player player)
        {
            player.maxRunSpeed *= 1.5f;
            player.accRunSpeed *= 1.5f;
        }

		#region Drawing

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

        public override ItemPlayer.IterationContext HideDrawLayersContext(Player player) => ItemPlayer.IterationContext.HeldItem;

        public override void HideDrawLayers(Player player, PlayerDrawSet drawInfo)
        {
            if (player.itemAnimation <= 0)
            {
                PlayerDrawLayers.HeldItem.Hide();
                PlayerDrawLayers.ArmOverItem.Hide();
            }
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

		#endregion
	}
}