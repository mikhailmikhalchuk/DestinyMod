using Terraria;
using Terraria.UI;
using Terraria.GameContent.UI.Elements;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;
using MonoMod.Cil;
using Terraria.ModLoader;
using Mono.Cecil.Cil;
using System;
using Terraria.GameContent.UI.States;
using System.Reflection;
using DestinyMod.Common.ModPlayers;

namespace DestinyMod.Content.UI.ClassSelection
{
	public partial class LoadClassSelection : ILoadable
	{
		public Player Player;

		public DestinyClassType PlayerClassType => Player.GetModPlayer<ClassPlayer>().ClassType;

		public UIElement MiddlePannel;

		public MethodInfo UnselectAllCategories;

		public UIColoredImageButton DestinyModOptionsButton;

		public ClassSelectionUI ClassSelection;

		public void Load(Mod mod)
		{
			DestinyModOptionsButton = new UIColoredImageButton(ModContent.Request<Texture2D>("DestinyMod/Content/UI/ClassSelection/DestinyCharacterSelect"))
			{
				VAlign = 0f,
				HAlign = 0f,
				Left = StyleDimension.FromPixelsAndPercent(216, 0.5f),
			};
			DestinyModOptionsButton.OnMouseDown += Click_ClassSelection;
			DestinyModOptionsButton.SetSnapPoint("Top", 2);

			UnselectAllCategories = typeof(UICharacterCreation).GetMethod("UnselectAllCategories", BindingFlags.NonPublic | BindingFlags.Instance);
			On.Terraria.GameContent.UI.States.UICharacterCreation.UnselectAllCategories += InsertDeselectDestinyModOptionsButton;
			IL.Terraria.GameContent.UI.States.UICharacterCreation.BuildPage += ModifyBuildPage;
			IL.Terraria.GameContent.UI.States.UICharacterCreation.MakeCategoriesBar += ModifyMakeCategoriesBar;
			On.Terraria.GameContent.UI.States.UICharacterCreation.MakeCategoriesBar += InsertDestinyModOption;
			On.Terraria.GameContent.UI.States.UICharacterCreation.ctor += ImplementClassSelectionUI;
			On.Terraria.GameContent.UI.States.UICharacterCreation.Click_NamingAndCreating += DetectClassSelected;
			IL.Terraria.GameContent.UI.States.UICharacterCreation.MakeBackAndCreatebuttons += WhyIsButtonsLowerCase;
		}

		public void Unload() { }

		private void ImplementClassSelectionUI(On.Terraria.GameContent.UI.States.UICharacterCreation.orig_ctor orig, UICharacterCreation self, Player player)
		{
			orig.Invoke(self, player);

			Player = player;

			ClassSelection = new ClassSelectionUI(Player, CreateButton)
			{
				Width = StyleDimension.FromPixelsAndPercent(-10f, 1f),
				Height = StyleDimension.FromPixelsAndPercent(0f, 1f),
				HAlign = 0.5f,
				VAlign = 0.5f
			};
			ClassSelection.SetPadding(0f);
		}

		private void InsertDeselectDestinyModOptionsButton(On.Terraria.GameContent.UI.States.UICharacterCreation.orig_UnselectAllCategories orig, UICharacterCreation self)
		{
			orig.Invoke(self);
			DestinyModOptionsButton.SetSelected(false);
			ClassSelection?.Remove();
		}

		private void InsertDestinyModOption(On.Terraria.GameContent.UI.States.UICharacterCreation.orig_MakeCategoriesBar orig, Terraria.GameContent.UI.States.UICharacterCreation self, UIElement categoryContainer)
		{
			orig(self, categoryContainer);
			categoryContainer.Append(DestinyModOptionsButton);
		}

		private void Click_ClassSelection(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(12);
			UnselectAllCategories.Invoke(Main.MenuUI.CurrentState, null);
			MiddlePannel.Append(ClassSelection);
			DestinyModOptionsButton.SetSelected(selected: true);
		}

		private void ModifyMakeCategoriesBar(ILContext il)
		{
			ILCursor cursor = new ILCursor(il);

			if (!cursor.TryGotoNext(MoveType.After, i => i.MatchLdcR4(-240)))
			{
				DestinyMod.Instance.Logger.Error("Failed to match first target in ClassSelectionUI.ModifyMakeCategoriesBar(ILContext il)");
				return;
			}
			cursor.Emit(OpCodes.Pop);
			cursor.Emit(OpCodes.Ldc_R4, -264f);
		}

		private void ModifyBuildPage(ILContext il)
		{
			ILCursor cursor = new ILCursor(il);

			if (!cursor.TryGotoNext(MoveType.After, i => i.MatchLdcR4(500)))
			{
				DestinyMod.Instance.Logger.Error("Failed to match first target in ClassSelectionUI.ModifyBuildPage(ILContext il)");
				return;
			}
			cursor.Emit(OpCodes.Pop);
			cursor.Emit(OpCodes.Ldc_R4, 548f);

			if (!cursor.TryGotoNext(MoveType.After, i => i.MatchStfld<UICharacterCreation>("_middleContainer")))
			{
				DestinyMod.Instance.Logger.Error("Failed to match second target in ClassSelectionUI.ModifyBuildPage(ILContext il)");
				return;
			}
			cursor.Emit(OpCodes.Ldloc, 4);
			cursor.EmitDelegate<Action<UIElement>>(element => MiddlePannel = element);
		}
	}
}