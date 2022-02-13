using Terraria;
using Terraria.ID;
using Terraria.UI;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader.UI;
using Terraria.ModLoader;
using Terraria.Audio;
using DestinyMod.Common.ModPlayers;

namespace DestinyMod.Content.UI.ClassSelection
{
	public class ClassOption : UIElement
	{
		public UITextPanel<string> Select;

		public UIText Description;

		public DestinyClassType ClassType;

		public string ClassDescription;

		public ClassOption(DestinyClassType classType, string classDescription)
		{
			ClassType = classType;
			ClassDescription = classDescription;
		}

		public override void OnInitialize()
		{
			HAlign = 0.5f
			Width.Pixels = 200;
			Height.Pixels = 114;

			string className = ClassType.ToString();
			Select = new UITextPanel<string>(ClassType.ToString(), 0.7f, true)
			{
				HAlign = 0.5f
			};
			Description.Top.Pixels = 50;
			Select.Width.Pixels = 200;
			Select.Height.Pixels = 50;
			Select.OnClick += Click;
			Select.OnMouseOver += MouseOver;
			Select.OnMouseOut += MouseOut;
			Select.SetSnapPoint(ClassType.ToString(), 0);
			Append(Select);

			Description = new UIText(string.Empty)
			{
				HAlign = 0.5f,
			};
			Description.Top.Pixels = 64; 
			Description.Width.Pixels = 100;
			Description.Height.Pixels = 50;
			Append(Description);
		}

		private void Click(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(SoundID.MenuOpen);
			Main.menuMode = 2;
			Main.LocalPlayer.GetModPlayer<ClassPlayer>().ClassType = ClassType;
			ModContent.GetInstance<TheDestinyMod>().classSelectionInterface?.SetState(null);
		}

		private void MouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.PendingResolutionHeight > 750)
			{
				Description.SetText(ClassDescription);
			}

			SoundEngine.PlaySound(SoundID.MenuTick);
			Select.BackgroundColor = UICommon.DefaultUIBlue;
		}

		private void MouseOut(UIMouseEvent evt, UIElement listeningElement)
		{
			Description.SetText(string.Empty);
			Select.BackgroundColor = UICommon.DefaultUIBlueMouseOver;
		}
	}
}