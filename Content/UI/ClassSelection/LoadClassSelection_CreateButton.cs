using Terraria;
using Terraria.UI;
using Terraria.GameContent.UI.Elements;
using MonoMod.Cil;
using Terraria.ModLoader;
using Mono.Cecil.Cil;
using System;
using Terraria.GameContent.UI.States;
using DestinyMod.Common.ModPlayers;
using Microsoft.Xna.Framework;
using Terraria.Localization;

namespace DestinyMod.Content.UI.ClassSelection
{
	public partial class LoadClassSelection : ILoadable
	{
		public UITextPanel<LocalizedText> CreateButton;

		private void WhyIsButtonsLowerCase(ILContext il)
		{
			ILCursor cursor = new ILCursor(il);

			if (!cursor.TryGotoNext(MoveType.Before, i => i.MatchRet()))
			{
				DestinyMod.Instance.Logger.Error("Failed to match first target in ClassSelectionUI.WhyIsButtonsLowerCase(ILContext il)");
				return;
			}
			cursor.Emit(OpCodes.Ldloc_1);
			cursor.EmitDelegate<Action<UITextPanel<LocalizedText>>>(createButton =>
			{
				CreateButton = createButton;

				createButton.OnMouseOut += (UIMouseEvent evt, UIElement listeningElement) =>
				CreateButton.BackgroundColor = PlayerClassType == DestinyClassType.None ? Color.DarkRed * 0.8f : new Color(63, 82, 151) * 0.8f;

				createButton.OnMouseOver += (UIMouseEvent evt, UIElement listeningElement) =>
				CreateButton.BackgroundColor = PlayerClassType == DestinyClassType.None ? Color.DarkRed : new Color(73, 94, 171);

				createButton.BackgroundColor = Color.DarkRed * 0.8f;
			});
		}

		private void DetectClassSelected(On.Terraria.GameContent.UI.States.UICharacterCreation.orig_Click_NamingAndCreating orig, UICharacterCreation self, UIMouseEvent evt, UIElement listeningElement)
		{
			if (Player.GetModPlayer<ClassPlayer>().ClassType != DestinyClassType.None)
			{
				orig.Invoke(self, evt, listeningElement);
			}
		}
	}
}