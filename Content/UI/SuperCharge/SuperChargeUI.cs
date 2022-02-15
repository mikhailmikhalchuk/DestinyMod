using Terraria;
using Terraria.ID;
using Terraria.UI;
using Terraria.UI.Gamepad;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;
using DestinyMod.Core.UI;
using Terraria.GameInput;
using Terraria.ModLoader;
using DestinyMod.Common.ModPlayers;

namespace DestinyMod.Content.UI.ClassSelection
{
	public class SuperChargeUI : DestinyModUIState
	{
		public UIImage BarFrame;

		public UIText SuperText;

		public override void PreLoad(ref string name)
		{
			AutoSetState = false;
			AutoAddHandler = true;
		}

		public override UIHandler Load() => new UIHandler(UserInterface, "Vanilla: Resource Bars", LayerName);

		public override void OnInitialize()
		{
			BarFrame = new UIImage(DestinyMod.Instance.Assets.Request<Texture2D>("Content/UI/SuperCharge/SuperChargeFrame"));
			BarFrame.Left.Set(22, 0.8f);
			BarFrame.Top.Set(0, 0.05f);
			BarFrame.Width.Set(138, 0f);
			BarFrame.Height.Set(34, 0f);

			SuperText = new UIText("", 0.8f);
			SuperText.Left.Set(22, 0.8f);
			SuperText.Top.Set(22, 0.1f);
			SuperText.Width.Set(138, 0f);
			SuperText.Height.Set(34, 0f);

			Append(BarFrame);
			Append(SuperText);
		}

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);

			SuperPlayer superPlayer = Main.LocalPlayer.GetModPlayer<SuperPlayer>();

			float quotient = (float)superPlayer.SuperChargeCurrent / 100;
			quotient = Utils.Clamp(quotient, 0f, 1f);

			Rectangle hitbox = BarFrame.GetInnerDimensions().ToRectangle();
			hitbox.X += 12;
			hitbox.Width -= 24;
			hitbox.Y += 8;
			hitbox.Height -= 16;

			int steps = (int)((hitbox.Right - hitbox.Left) * quotient);
			for (int i = 0; i < steps; i++)
            {
				float percent = (float)i / (hitbox.Right - hitbox.Left);
				spriteBatch.Draw((Texture2D)ModContent.Request<Texture2D>("Terraria/Images/MagicPixel"), new Rectangle(hitbox.Left + i, hitbox.Y, 1, hitbox.Height), Color.Lerp(new Color(255, 200, 0), new Color(255, 255, 0), percent));
            }
        }

        public override void Update(GameTime gameTime)
        {
			if (DestinyClientConfig.Instance.SuperBarText)
            {
				SuperPlayer superPlayer = Main.LocalPlayer.GetModPlayer<SuperPlayer>();

				SuperText.SetText($"Super: {superPlayer.SuperChargeCurrent} / 100");
			}
			else
            {
				SuperText.SetText("");
            }

            base.Update(gameTime);
        }
    }
}