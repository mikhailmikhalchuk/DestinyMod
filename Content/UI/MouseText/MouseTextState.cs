using Terraria;
using Microsoft.Xna.Framework;
using DestinyMod.Core.UI;
using DestinyMod.Common.UI;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.GameContent;
using Terraria.UI;
using System.Linq;
using System.Collections.Generic;

namespace DestinyMod.Content.UI.MouseText
{
	public class MouseTextState : DestinyModUIState
	{
		public bool Visible;

		public Vector2 OldMouseScreen { get; private set; }

		public static readonly float CommonOpacity = 0.75f;

		public static readonly int CommonBorder = 6;

		private UIElement MasterBackground;

		public override void PreLoad(ref string name)
		{
			AutoSetState = true;
			AutoAddHandler = true;
		}

		public override UIHandler Load() => new UIHandler(UserInterface, "Vanilla: Mouse Text", LayerName);

		public override void OnInitialize()
		{
			MasterBackground = new UIElement();
			MasterBackground.Width.Pixels = 100;
			MasterBackground.Height.Pixels = 100;
			Append(MasterBackground);
		}

		public override void Update(GameTime gameTime)
		{
			if (!Visible)
            {
				return;
            }

			base.Update(gameTime);

			Main.mouseText = false;

			if (Main.MouseScreen != OldMouseScreen)
			{
				Vector2 uiPosition = Main.MouseScreen;
				if (uiPosition.Y > Main.screenHeight / 2)
                {
					uiPosition += new Vector2(10, -MasterBackground.Height.Pixels - 10);
                }
				else
                {
					uiPosition += new Vector2(10, 10);
				}
				MasterBackground.Left.Pixels = uiPosition.X;
				MasterBackground.Top.Pixels = uiPosition.Y;
				Recalculate();
			}

			OldMouseScreen = Main.MouseScreen;
		}

        public override void Draw(SpriteBatch spriteBatch)
        {
			if (!Visible)
			{
				return;
			}

			base.Draw(spriteBatch);

			CleanseAll(); // :crying This becomes so scuffed if you have frame skip off at FPS > 60
			// Oh well, lazy place to insert and Vanilla inserts game logic into draw code as well
			// I might* move this somewhere more appropriate when I have the brainpower to
			// * = Subject to future developments
		}

		public void RecalculateSpacing()
		{
			int greatestWidth = 0;
			List<UIElement> children = MasterBackground.Children.ToList();
			for (int masterIndexer = 0; masterIndexer < children.Count; masterIndexer++)
			{
				UIElement child = children[masterIndexer];
				if (masterIndexer == 0)
				{
					child.Top.Pixels = 0; // Should be only handling TitleBackground
				}
				else
				{
					UIElement previousChild = children[masterIndexer - 1];
					child.Top.Pixels = previousChild.Top.Pixels + previousChild.Height.Pixels;
				}

				int totalWidth = (int)(child.Left.Pixels + child.Width.Pixels);
				if (totalWidth > greatestWidth)
				{
					greatestWidth = totalWidth;
				}
			}

			MasterBackground.Height.Pixels = 0;
			int childrenCount = MasterBackground.Children.Count();
			if (childrenCount > 0)
			{
				UIElement lastChild = children[childrenCount - 1];
				MasterBackground.Height.Pixels = lastChild.Top.Pixels + lastChild.Height.Pixels;
			}

			MasterBackground.Width.Pixels = greatestWidth;
			Recalculate();
		}

		public void AppendToMasterBackground(UIElement desiredElementToAppend)
        {
			MasterBackground.Append(desiredElementToAppend);
			RecalculateSpacing();
        }

		public void RemoveFromMasterBackground(UIElement desiredElementToRemove)
        {
			MasterBackground.RemoveChild(desiredElementToRemove);
			RecalculateSpacing();
		}

		public void CleanseAll()
        {
			MasterBackground.RemoveAllChildren();
			MasterBackground.Width.Pixels = 0;
			MasterBackground.Height.Pixels = 0;
		}
	}
}