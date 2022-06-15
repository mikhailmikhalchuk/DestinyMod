using Terraria;
using Microsoft.Xna.Framework;
using DestinyMod.Core.UI;
using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;
using System.Linq;
using System.Collections.Generic;

namespace DestinyMod.Content.UI.MouseText
{
	/// <summary>Container class for mouseover info classes, such as perk hover info, mod hover info, etc.</summary>
	public class MouseTextState : DestinyModUIState
	{
		public bool Visible;

		public Vector2 OldMouseScreen { get; private set; }

		public const float CommonOpacity = 0.85f;

		public const int CommonBorder = 6;

		private UIElement MasterBackground;

		public const int DefaultWidth = 420;

		public int FocusTimer; // Scuffed way to fix an issue where Widths haven't been properly updated yet

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
				FocusTimer = 0;
				return;
            }

			base.Update(gameTime);

			FocusTimer++;

			Main.mouseText = false;

			if (Main.MouseScreen != OldMouseScreen)
			{
				Vector2 uiPosition = Main.MouseScreen + new Vector2(10);
				if (uiPosition.Y > Main.screenHeight / 2f)
                {
					uiPosition.Y -= MasterBackground.Height.Pixels;
                }
				MasterBackground.Left.Pixels = uiPosition.X;
				MasterBackground.Top.Pixels = uiPosition.Y;
				Recalculate();
			}

			OldMouseScreen = Main.MouseScreen;
		}

        public override void Draw(SpriteBatch spriteBatch)
        {
			if (!Visible || FocusTimer < 15)
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
				UIElement childElement = children[masterIndexer];
				if (childElement is not MouseTextElement child)
                {
					continue;
                }

				if (masterIndexer == 0)
				{
					child.Top.Pixels = 0; // Should be only handling TitleBackground
				}
				else
				{
					UIElement previousChild = children[masterIndexer - 1];
					child.Top.Pixels = previousChild.Top.Pixels + previousChild.Height.Pixels;
				}

				child.Width.Pixels = child.WidthPixels;
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

			if (greatestWidth <= 0)
            {
				greatestWidth = DefaultWidth;
			}

			MasterBackground.Width.Pixels = greatestWidth;

			for (int masterIndexer = 0; masterIndexer < children.Count; masterIndexer++)
			{
				UIElement childElement = children[masterIndexer];
				if (childElement is not MouseTextElement child)
				{
					continue;
				}

				child.Width.Pixels += child.WidthPercentage * greatestWidth;
			}

			Recalculate();
		}

		public void AppendToMasterBackground(MouseTextElement desiredElementToAppend)
        {
			MasterBackground.Append(desiredElementToAppend);
			RecalculateSpacing();
        }

		public void RemoveFromMasterBackground(MouseTextElement desiredElementToRemove)
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