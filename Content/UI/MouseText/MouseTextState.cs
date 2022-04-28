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

		private UIElement TitleBackground;

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

			TitleBackground = new UIElement();
			TitleBackground.Width.Pixels = 100;
			TitleBackground.Height.Pixels = 0;
			MasterBackground.Append(TitleBackground);
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
				MasterBackground.Left.Pixels = Main.MouseScreen.X;
				MasterBackground.Top.Pixels = Main.MouseScreen.Y;
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

			Texture2D magicPixel = TextureAssets.MagicPixel.Value;
			spriteBatch.Draw(magicPixel, MasterBackground.GetDimensions().ToRectangle(), new Color(10, 10, 10) * CommonOpacity);
			spriteBatch.Draw(magicPixel, TitleBackground.GetDimensions().ToRectangle(), new Color(68, 70, 74) * CommonOpacity);
			base.Draw(spriteBatch);

			CleanseAll(); // :crying This becomes so scuffed if you have frame skip off at FPS > 60
			// Oh well, lazy place to insert and Vanilla inserts game logic into draw code as well
			// I might* move this somewhere more appropriate when I have the brainpower to
			// * = Subject to future developments
		}

		public void RecalculateSpacing()
		{
			int greatestWidth = 0;
			List<UIElement> titleBackgroundChildren = TitleBackground.Children.ToList();
			for (int titleIndexer = 0; titleIndexer < titleBackgroundChildren.Count; titleIndexer++)
			{
				UIElement child = titleBackgroundChildren[titleIndexer];

				child.Left.Pixels = CommonBorder;

				if (titleIndexer == 0)
				{
					child.Top.Pixels = CommonBorder;
				}
				else
				{
					UIElement previousChild = titleBackgroundChildren[titleIndexer - 1];
					child.Top.Pixels = previousChild.Top.Pixels + previousChild.Height.Pixels + CommonBorder;
				}

				if (child.Width.Pixels > greatestWidth)
                {
					greatestWidth = (int)child.Width.Pixels;
                }
			}

			TitleBackground.Height.Pixels = 0;
			int titleChildrenCount = TitleBackground.Children.Count();
			if (titleChildrenCount > 0)
			{
				UIElement titleBackgroundLastChild = titleBackgroundChildren[titleChildrenCount - 1];
				TitleBackground.Height.Pixels = titleBackgroundLastChild.Top.Pixels + titleBackgroundLastChild.Height.Pixels + CommonBorder;
			}

			List<UIElement> masterBackgroundChildren = MasterBackground.Children.ToList();
			for (int masterIndexer = 0; masterIndexer < masterBackgroundChildren.Count; masterIndexer++)
			{
				UIElement child = masterBackgroundChildren[masterIndexer];

				if (masterIndexer == 0)
				{
					child.Left.Pixels = 0;
					child.Top.Pixels = 0; // Should be only handling TitleBackground
				}
				else
				{
					UIElement previousChild = masterBackgroundChildren[masterIndexer - 1];
					child.Left.Pixels = CommonBorder;
					child.Top.Pixels = previousChild.Top.Pixels + previousChild.Height.Pixels + CommonBorder;

					if (child.Width.Pixels > greatestWidth)
					{
						greatestWidth = (int)child.Width.Pixels;
					}
				}
			}

			MasterBackground.Height.Pixels = TitleBackground.Height.Pixels;
			int masterChildrenCount = MasterBackground.Children.Count();
			if (masterChildrenCount > 1)
			{
				UIElement masterBackgroundLastChild = masterBackgroundChildren[masterChildrenCount - 1];
				MasterBackground.Height.Pixels = masterBackgroundLastChild.Top.Pixels + masterBackgroundLastChild.Height.Pixels + CommonBorder;
			}

			int newWidth = greatestWidth + 2 * CommonBorder;
			TitleBackground.Width.Pixels = newWidth;
			MasterBackground.Width.Pixels = newWidth;
			Recalculate();
		}

		public void AppendToMasterBackground(UIElement desiredElementToAppend)
        {
			MasterBackground.Append(desiredElementToAppend);
			RecalculateSpacing();
        }

		public void AppendToTitleBackground(UIElement desiredElementToAppend)
		{
			TitleBackground.Append(desiredElementToAppend);
			RecalculateSpacing();
		}

		public void RemoveFromMasterBackground(UIElement desiredElementToRemove)
        {
			MasterBackground.RemoveChild(desiredElementToRemove);
			RecalculateSpacing();
		}

		public void RemoveFromTitleBackground(UIElement desiredElementToRemove)
		{
			TitleBackground.RemoveChild(desiredElementToRemove);
			RecalculateSpacing();
		}

		public void CleanseAll()
        {
			TitleBackground.RemoveAllChildren();
			MasterBackground.RemoveAllChildren();
			MasterBackground.Append(TitleBackground);
			TitleBackground.Width.Pixels = 0;
			TitleBackground.Height.Pixels = 0;
			MasterBackground.Width.Pixels = 0;
			MasterBackground.Height.Pixels = 0;
		}
	}
}