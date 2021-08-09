using Terraria;
using Terraria.ID;
using Terraria.UI;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.GameContent.UI.Elements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheDestinyMod.UI
{
    internal class RaidDragableUI : UIPanel
    {
		// Stores the offset from the top left of the UIPanel while dragging.
		private Vector2 offset;
		public bool dragging;

		public override void MouseDown(UIMouseEvent evt) {
			base.MouseDown(evt);
			DragStart(evt);
		}

		public override void MouseUp(UIMouseEvent evt) {
			base.MouseUp(evt);
			DragEnd(evt);
		}

		private void DragStart(UIMouseEvent evt) {
			offset = new Vector2(evt.MousePosition.X - Left.Pixels, evt.MousePosition.Y - Top.Pixels);
			dragging = true;
		}

		private void DragEnd(UIMouseEvent evt) {
			Vector2 end = evt.MousePosition;
			dragging = false;

			Left.Set(end.X - offset.X, 0f);
			Top.Set(end.Y - offset.Y, 0f);

			Recalculate();
		}

		public override void Update(GameTime gameTime) {
			base.Update(gameTime);

			if (ContainsPoint(Main.MouseScreen)) {
				Main.LocalPlayer.mouseInterface = true;
			}

			if (dragging) {
				Left.Set(Main.mouseX - offset.X, 0f);
				Top.Set(Main.mouseY - offset.Y, 0f);
				Recalculate();
			}

			var parentSpace = Parent.GetDimensions().ToRectangle();
			if (!GetDimensions().ToRectangle().Intersects(parentSpace)) {
				Left.Pixels = Utils.Clamp(Left.Pixels, 0, parentSpace.Right - Width.Pixels);
				Top.Pixels = Utils.Clamp(Top.Pixels, 0, parentSpace.Bottom - Height.Pixels);
				
				Recalculate();
			}
		}
	}

	public class RaidSelectionUI : UIState
	{
		internal RaidDragableUI raidDragable;

        public override void OnInitialize() {
			raidDragable = new RaidDragableUI();
			raidDragable.SetPadding(0);
			raidDragable.Left.Set(400f, 0f);
			raidDragable.Top.Set(175f, 0f);
			raidDragable.Width.Set(400f, 0f);
			raidDragable.Height.Set(300f, 0f);
			raidDragable.BackgroundColor = new Color(73, 94, 171);

			UIText raidName = new UIText("Vault of Glass", 1.5f);
			raidName.Left.Set(20, 0);
			raidName.Top.Set(20, 0);
			raidName.Width.Set(50, 0);
			raidName.Height.Set(30, 0);
			raidDragable.Append(raidName);

			Texture2D buttonDeleteTexture = ModContent.GetTexture("TheDestinyMod/UI/ButtonCancel");
			UIImageButton closeButton = new UIImageButton(buttonDeleteTexture);
			closeButton.Left.Set(370, 0f);
			closeButton.Top.Set(10, 0f);
			closeButton.Width.Set(22, 0f);
			closeButton.Height.Set(22, 0f);
			closeButton.OnClick += new MouseEvent(CloseButtonClicked);
			raidDragable.Append(closeButton);

			UITextPanelButton<string> startRaid = new UITextPanelButton<string>("Begin", 1.2f);
			startRaid.Left.Set(50, 0);
			startRaid.Top.Set(225, 0);
			startRaid.Width.Set(50, 0);
			startRaid.Height.Set(30, 0);
			startRaid.OnClick += new MouseEvent(StartButtonClicked);
			raidDragable.Append(startRaid);

			Append(raidDragable);
		}

		private void CloseButtonClicked(UIMouseEvent evt, UIElement listeningElement) {
			ModContent.GetInstance<TheDestinyMod>().raidInterface.SetState(null);
		}

		private void StartButtonClicked(UIMouseEvent evt, UIElement listeningElement) {
			bool canStart = true;
			if (Main.ActivePlayersCount > 1) {
				for (int i = 0; i < Main.maxPlayers; i++) {
					Player player = Main.player[i];
					if (!player.active) {
						return;
					}
					if (player.Distance(Main.LocalPlayer.position) > 150) {
						canStart = false;
					}
				}
			}
			if (canStart) {
				bool result = DestinyPlayer.Enter("TheDestinyMod_Vault of Glass") ?? false;
				if (!result && ModLoader.GetMod("StructureHelper") != null && ModLoader.GetMod("SubworldLibrary") != null)
					Main.NewText($"Something went wrong while trying to enter the raid: {TheDestinyMod.currentSubworldID.Substring(14)}.", Color.Red);
			}
			else {
				Main.NewText("All players must be near the portal to begin the raid", Color.Red);
			}
		}
    }

	internal class UITextPanelButton<T> : UITextPanel<T>
	{
		internal bool tickPlayed;

		public UITextPanelButton(T text, float textScale = 1f, bool large = false) : base(text, textScale, large) {
			SetText(text, textScale, large);
		}

		protected override void DrawSelf(SpriteBatch spriteBatch) {
			base.DrawSelf(spriteBatch);

			if (IsMouseHovering) {
				BackgroundColor.A = 100;
				if (!tickPlayed) {
					Main.PlaySound(SoundID.MenuTick);
					tickPlayed = true;
				}
			}
			else {
				BackgroundColor.A = 178;
				tickPlayed = false;
			}
		}
	}
}