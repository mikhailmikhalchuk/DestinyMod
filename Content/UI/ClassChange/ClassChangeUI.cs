using Terraria;
using Terraria.UI;
using Terraria.GameContent.UI.Elements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using DestinyMod.Core.UI;
using Terraria.ModLoader;
using DestinyMod.Common.ModPlayers;
using DestinyMod.Common.Configs;

namespace DestinyMod.Content.UI.ClassChange
{
	public class ClassChangeUI : DestinyModUIState
	{
		public UIImageButton TitanIcon;

		public UIImageButton WarlockIcon;

		public UIImageButton HunterIcon;

		public override void PreLoad(ref string name)
		{
			AutoSetState = false;
			AutoAddHandler = true;
		}

		public override UIHandler Load() => new UIHandler(UserInterface, "Vanilla: Resource Bars", LayerName);

		public override void OnInitialize()
		{
			TitanIcon = new UIImageButton(DestinyMod.Instance.Assets.Request<Texture2D>("Content/UI/ClassChange/ClassChangeTitanIcon"));
			TitanIcon.Left.Set(0, 0);
			TitanIcon.Top.Set(0, 0);
			TitanIcon.Width.Set(26, 0f);
			TitanIcon.Height.Set(26, 0f);
            TitanIcon.OnClick += TitanIcon_OnClick;

			WarlockIcon = new UIImageButton(DestinyMod.Instance.Assets.Request<Texture2D>("Content/UI/ClassChange/ClassChangeWarlockIcon"));
			WarlockIcon.Left.Set(0, -0.02f);
			WarlockIcon.Top.Set(0, 0.02f);
			WarlockIcon.Width.Set(26, 0f);
			WarlockIcon.Height.Set(26, 0f);
            WarlockIcon.OnClick += WarlockIcon_OnClick;

			HunterIcon = new UIImageButton(DestinyMod.Instance.Assets.Request<Texture2D>("Content/UI/ClassChange/ClassChangeHunterIcon"));
			HunterIcon.Left.Set(0, 0.02f);
			HunterIcon.Top.Set(0, 0.02f);
			HunterIcon.Width.Set(26, 0f);
			HunterIcon.Height.Set(26, 0f);
            HunterIcon.OnClick += HunterIcon_OnClick;

			Append(TitanIcon);
			Append(WarlockIcon);
			Append(HunterIcon);
		}

        private void HunterIcon_OnClick(UIMouseEvent evt, UIElement listeningElement)
        {
			PostSelectClass();
		}

        private void WarlockIcon_OnClick(UIMouseEvent evt, UIElement listeningElement)
        {
			PostSelectClass();
		}

        private void TitanIcon_OnClick(UIMouseEvent evt, UIElement listeningElement)
        {
			PostSelectClass();
        }

		private static void PostSelectClass()
        {
			if (!Main.LocalPlayer.HasItem(ModContent.ItemType<Items.Consumables.GuardianCrest>()))
			{
				Main.NewText("No Guardian Crest in inventory!", Color.Red);
				return;
			}
			Main.LocalPlayer.ConsumeItem(ModContent.ItemType<Items.Consumables.GuardianCrest>());
			ModContent.GetInstance<ClassChangeUI>().UserInterface.SetState(null);
		}

        protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			base.DrawSelf(spriteBatch);

			//Main.NewText(Main.LocalPlayer.Center - Main.screenPosition);
			Vector2 pos = Main.LocalPlayer.Top.ToScreenPosition() - new Vector2(Main.LocalPlayer.width / 2, Main.LocalPlayer.height / 2);
			Left.Set(pos.X, 0);
			Top.Set(pos.Y + Main.LocalPlayer.gfxOffY - 4, 0);

			if (TitanIcon.ContainsPoint(Main.MouseScreen))
            {
				Main.instance.MouseText("Titan");
				Main.LocalPlayer.mouseInterface = true;
			}
			if (WarlockIcon.ContainsPoint(Main.MouseScreen))
			{
				Main.instance.MouseText("Warlock");
				Main.LocalPlayer.mouseInterface = true;
			}
			if (HunterIcon.ContainsPoint(Main.MouseScreen))
			{
				Main.instance.MouseText("Hunter");
				Main.LocalPlayer.mouseInterface = true;
			}
		}
	}
}