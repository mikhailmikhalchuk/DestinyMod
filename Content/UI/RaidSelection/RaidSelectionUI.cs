using Terraria;
using Terraria.ID;
using Terraria.UI;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.GameContent.UI.Elements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using DestinyMod.Common.UI;
using ReLogic.Content;
using ReLogic.Graphics;
using Terraria.Audio;
using System.Linq;
using DestinyMod.Core.UI;

namespace DestinyMod.Content.UI.RaidSelection
{
	public class RaidSelectionUI : DestinyModUIState
	{
		internal UIDraggablePanel raidDragable;

		private UIText currentCheckpoint;

		private UITextPanel<string> clearCheckpoint;

		private string Raid;

		private int Clears;

		private bool DownedRequirement;

		private string DownedName;

		public override void PreLoad(ref string name)
		{
			AutoSetState = false;
			AutoAddHandler = true;
		}

		public override UIHandler Load() => new UIHandler(UserInterface, string.Empty, LayerName);

		public RaidSelectionUI(string raid, int clears, bool downedRequirement, string downedName)
		{
			Raid = raid;
			Clears = clears;
			DownedRequirement = downedRequirement;
			DownedName = downedName;
		}

		public override void OnInitialize()
		{
			raidDragable = new UIDraggablePanel();
			raidDragable.SetPadding(0);
			raidDragable.Left.Set(400f, 0f);
			raidDragable.Top.Set(175f, 0f);
			raidDragable.Width.Set(400f, 0f);
			raidDragable.Height.Set(300f, 0f);
			raidDragable.BackgroundColor = Terraria.ModLoader.UI.UICommon.MainPanelBackground;

			UIText raidName = new UIText(Raid, 1.5f);
			raidName.Left.Set(20, 0);
			raidName.Top.Set(20, 0);
			raidName.Width.Set(50, 0);
			raidName.Height.Set(30, 0);
			raidDragable.Append(raidName);

			UIText clearCount = new UIText($"Times cleared: {Clears}");
			clearCount.Left.Set(20, 0);
			clearCount.Top.Set(70, 0);
			clearCount.Width.Set(50, 0);
			clearCount.Height.Set(30, 0);
			raidDragable.Append(clearCount);

			UIText recommendedLevel = new UIText($"Recommended: {(DownedRequirement ? $"[c/00FF00:{DownedName}]" : $"[c/FF0000:{DownedName}]")}"); // || Main.hardMode
			recommendedLevel.Left.Set(20, 0);
			recommendedLevel.Top.Set(100, 0);
			recommendedLevel.Width.Set(50, 0);
			recommendedLevel.Height.Set(30, 0);
			raidDragable.Append(recommendedLevel);

			currentCheckpoint = new UIText($"Current checkpoint: {GetCheckpointString()}");
			currentCheckpoint.Left.Set(20, 0);
			currentCheckpoint.Top.Set(140, 0);
			currentCheckpoint.Width.Set(50, 0);
			currentCheckpoint.Height.Set(30, 0);
			raidDragable.Append(currentCheckpoint);

			if (GetCheckpointString() != "None" && GetCheckpointString() != "Unknown")
			{
				clearCheckpoint = new UITextPanel<string>(Language.GetTextValue("GameUI.Clear"));
				clearCheckpoint.Left.Set(190 + Terraria.GameContent.FontAssets.ItemStack.Value.MeasureString(GetCheckpointString()).X, 0);
				clearCheckpoint.Top.Set(128, 0);
				clearCheckpoint.Width.Set(50, 0);
				clearCheckpoint.Height.Set(30, 0);
				clearCheckpoint.OnClick += ClearButtonClicked;
				clearCheckpoint.OnMouseOver += MouseOverPanel;
				clearCheckpoint.OnMouseOut += MouseOutPanel;
				raidDragable.Append(clearCheckpoint);
			}

			Asset<Texture2D> buttonDeleteTexture = ModContent.Request<Texture2D>("TheDestinyMod/UI/ButtonCancel");
			UIImageButton closeButton = new UIImageButton(buttonDeleteTexture);
			closeButton.Left.Set(370, 0f);
			closeButton.Top.Set(10, 0f);
			closeButton.Width.Set(22, 0f);
			closeButton.Height.Set(22, 0f);
			closeButton.OnClick += CloseButtonClicked;
			raidDragable.Append(closeButton);

			UITextPanel<string> startRaid = new UITextPanel<string>("Begin", 1.2f);
			startRaid.Left.Set(40, 0);
			startRaid.Top.Set(225, 0);
			startRaid.Width.Set(50, 0);
			startRaid.Height.Set(30, 0);
			startRaid.OnClick += StartButtonClicked;
			startRaid.OnMouseOver += MouseOverPanel;
			startRaid.OnMouseOut += MouseOutPanel;
			raidDragable.Append(startRaid);

			Append(raidDragable);
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);

			CalculatedStyle dims = raidDragable.GetInnerDimensions();

			Utils.DrawBorderStringFourWay(spriteBatch, ModContent.Request<DynamicSpriteFont>("Assets/Fonts/FuturaBold").Value, Raid.ToUpper(), dims.X + 20, dims.Y + 20, Color.White, Color.Transparent, Vector2.Zero, 0.5f);

			Utils.DrawBorderStringFourWay(spriteBatch, ModContent.Request<DynamicSpriteFont>("Assets/Fonts/FuturaBook").Value, $"Times cleared: {Clears}", dims.X + 20, dims.Y + 70, Color.White, Color.Transparent, Vector2.Zero, 0.8f);

			Utils.DrawBorderStringFourWay(spriteBatch, ModContent.Request<DynamicSpriteFont>("Assets/Fonts/FuturaBook").Value, $"Recommended: {(DownedRequirement ? $"[c/00FF00:{DownedName}]" : $"[c/FF0000:{DownedName}]")}", dims.X + 20, dims.Y + 100, Color.White, Color.Transparent, Vector2.Zero, 0.8f);
		}

		private void MouseOverPanel(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(SoundID.MenuTick);
			((UITextPanel<string>)evt.Target).BackgroundColor = Terraria.ModLoader.UI.UICommon.DefaultUIBlue;
		}

		private void MouseOutPanel(UIMouseEvent evt, UIElement listeningElement)
		{
			((UITextPanel<string>)evt.Target).BackgroundColor = Terraria.ModLoader.UI.UICommon.DefaultUIBlueMouseOver;
		}

		private string GetCheckpointString()
		{
			if (Raid == "Vault of Glass")
			{
				switch (Common.ModSystems.VaultOfGlassSystem.Checkpoint)
				{
					case 0:
						return "None";
					case 1:
						return "Confluxes";
					case 2:
						return "Oracles";
					case 3:
						return "Templar";
					case 4:
						return "Labyrinth";
					case 5:
						return "Gatekeepers";
					case 6:
						return "Atheon";
					default:
						return "Unknown";
				}
			}
			return "Unknown";
		}

		private void CloseButtonClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			ModContent.GetInstance<RaidSelectionUI>().UserInterface.SetState(null);
			SoundEngine.PlaySound(SoundID.MenuClose);
		}

		private void StartButtonClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			bool canStart = true;
			if (Main.CurrentFrameFlags.ActivePlayersCount > 1)
			{
				for (int i = 0; i < Main.maxPlayers; i++)
				{
					Player player = Main.player[i];
					if (!player.active)
					{
						return;
					}
					if (player.Distance(Main.LocalPlayer.position) > 150)
					{
						canStart = false;
					}
				}
			}
			if (canStart)
			{
				/*bool result = DestinyPlayer.Enter("TheDestinyMod_Vault of Glass") ?? false;
				if (!result && TheDestinyMod.SubworldLibrary != null)
					Main.NewText($"Something went wrong while trying to enter the raid: {TheDestinyMod.currentSubworldID.Substring(14)}.", Color.Red);

				if (result)
					ModContent.GetInstance<TheDestinyMod>().raidInterface.SetState(null);*/
			}
			else
			{
				Main.NewText("All players must be near the portal to begin the raid", Color.Red);
			}
		}

		private void ClearButtonClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			clearCheckpoint.SetText("Sure?");

			clearCheckpoint.OnClick -= ClearButtonClicked;
			clearCheckpoint.OnClick += ConfirmButtonClicked;

			raidDragable.DragEnd(evt);
		}

		private void ConfirmButtonClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			switch (Raid)
			{
				case "Vault of Glass":
					Common.ModSystems.VaultOfGlassSystem.Checkpoint = 0;
					break;
			}

			clearCheckpoint.Remove();

			currentCheckpoint.SetText($"Current checkpoint: {GetCheckpointString()}");

			raidDragable.DragEnd(evt);
		}
	}
}