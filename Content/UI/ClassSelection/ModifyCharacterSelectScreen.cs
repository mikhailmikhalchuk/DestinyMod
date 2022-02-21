using Terraria;
using Terraria.UI;
using Terraria.GameContent.UI.Elements;
using MonoMod.Cil;
using Terraria.ModLoader;
using Mono.Cecil.Cil;
using System;
using DestinyMod.Common.ModPlayers;
using Microsoft.Xna.Framework;
using Terraria.Localization;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria.IO;
using System.Reflection;
using ReLogic.Content;

namespace DestinyMod.Content.UI.ClassSelection
{
	public class ModifyCharacterSelectScreen : ILoadable
	{
		public Asset<Texture2D> InnerPanelTexture;

		public FieldInfo PlayerPanel;

		public FieldInfo Data;

		public void Load(Mod mod)
		{
			InnerPanelTexture = Main.Assets.Request<Texture2D>("Images/UI/InnerPanelBackground");

			PlayerPanel = typeof(UICharacterListItem).GetField("_playerPanel", BindingFlags.NonPublic | BindingFlags.Instance);
			Data = typeof(UICharacterListItem).GetField("_data", BindingFlags.NonPublic | BindingFlags.Instance);

			// IL.Terraria.GameContent.UI.States.UICharacterSelect.OnInitialize += ResizeCharacterSelect;
			IL.Terraria.GameContent.UI.Elements.UICharacterListItem.DrawSelf += InsertClassIndicator;
		}

		public void Unload() { }

		private void ResizeCharacterSelect(ILContext il)
		{
			ILCursor cursor = new ILCursor(il);

			if (!cursor.TryGotoNext(MoveType.After, i => i.MatchStfld<UIElement>("HAlign")))
			{
				DestinyMod.Instance.Logger.Error("Failed to match first target in ModifyCharacterSelectScreen.ResizeCharacterSelect(ILContext il)");
				return;
			}
			cursor.Emit(OpCodes.Ldloc_0);
			cursor.EmitDelegate<Action<UIElement>>(background => background.MaxWidth.Set(800f, 0f));
		}

		public void DrawPanel(SpriteBatch spriteBatch, Vector2 position, float width)
		{
			spriteBatch.Draw(InnerPanelTexture.Value, position, new Rectangle(0, 0, 8, InnerPanelTexture.Height()), Color.White);
			spriteBatch.Draw(InnerPanelTexture.Value, new Vector2(position.X + 8f, position.Y), new Rectangle(8, 0, 8, InnerPanelTexture.Height()), Color.White, 0f, Vector2.Zero, new Vector2((width - 16f) / 8f, 1f), SpriteEffects.None, 0f);
			spriteBatch.Draw(InnerPanelTexture.Value, new Vector2(position.X + width - 8f, position.Y), new Rectangle(16, 0, 8, InnerPanelTexture.Height()), Color.White);
		}

		private void InsertClassIndicator(ILContext il)
		{
			ILCursor cursor = new ILCursor(il);

			if (!cursor.TryGotoNext(MoveType.After, i => i.MatchCallvirt<SpriteBatch>("Draw")))
			{
				DestinyMod.Instance.Logger.Error("Failed to match first target in ModifyCharacterSelectScreen.InsertClassIndicator(ILContext il)");
				return;
			}
			cursor.Emit(OpCodes.Ldarg_0);
			cursor.Emit(OpCodes.Ldarg_1);
			cursor.EmitDelegate<Action<UICharacterListItem, SpriteBatch>>((character, spriteBatch) =>
			{
				UIElement innerCharacterList = character.Parent;
				UIElement characterList = innerCharacterList.Parent;
				UIElement backgroundPanel = characterList.Parent;
				UIElement trueBackground = backgroundPanel.Parent;
				if (trueBackground.MaxWidth.Pixels == 650)
				{
					DestinyMod.Instance.Logger.Info("Attempted to update MaxWidth");
					trueBackground.MaxWidth.Pixels = 800;
					trueBackground.Parent.Recalculate();
				}
				
				// A travesty
				UICharacter playerPanel = PlayerPanel.GetValue(character) as UICharacter;
				PlayerFileData data = Data.GetValue(character) as PlayerFileData;

				CalculatedStyle innerDimensions = character.GetInnerDimensions();
				CalculatedStyle dimensions = playerPanel.GetDimensions();
				Vector2 generalPosition = new Vector2(dimensions.X + dimensions.Width + 6f, innerDimensions.Y + 29f);

				float statsWidth = 200f;
				Vector2 statsPosition = generalPosition;
				DrawPanel(spriteBatch, generalPosition, statsWidth);
				spriteBatch.Draw(TextureAssets.Heart.Value, statsPosition + new Vector2(5f, 2f), Color.White);
				statsPosition.X += 10f + TextureAssets.Heart.Width();
				Utils.DrawBorderString(spriteBatch, data.Player.statLifeMax + Language.GetTextValue("GameUI.PlayerLifeMax"), statsPosition + new Vector2(0f, 3f), Color.White);
				statsPosition.X += 65f;
				spriteBatch.Draw(TextureAssets.Mana.Value, statsPosition + new Vector2(5f, 2f), Color.White);
				statsPosition.X += 10f + TextureAssets.Mana.Width();
				Utils.DrawBorderString(spriteBatch, data.Player.statManaMax + Language.GetTextValue("GameUI.PlayerManaMax"), statsPosition + new Vector2(0f, 3f), Color.White);
				generalPosition.X += statsWidth + 5f;

				Vector2 difficultyPosition = generalPosition;
				float difficultyWidth = GameCulture.FromCultureName(GameCulture.CultureName.Russian).IsActive ? 180f : 140f;
				DrawPanel(spriteBatch, difficultyPosition, difficultyWidth);
				string difficultyText = string.Empty;
				Color difficultyColor = Color.White;
				switch (data.Player.difficulty)
				{
					case 0:
						difficultyText = Language.GetTextValue("UI.Softcore");
						break;
					case 1:
						difficultyText = Language.GetTextValue("UI.Mediumcore");
						difficultyColor = Main.mcColor;
						break;
					case 2:
						difficultyText = Language.GetTextValue("UI.Hardcore");
						difficultyColor = Main.hcColor;
						break;
					case 3:
						difficultyText = Language.GetTextValue("UI.Creative");
						difficultyColor = Main.creativeModeColor;
						break;
				}

				difficultyPosition += new Vector2(difficultyWidth * 0.5f - FontAssets.MouseText.Value.MeasureString(difficultyText).X * 0.5f, 3f);
				Utils.DrawBorderString(spriteBatch, difficultyText, difficultyPosition, difficultyColor);
				generalPosition.X += difficultyWidth + 5f;

				Vector2 classPosition = generalPosition;
				float classPanelWidth = 140;
				DrawPanel(spriteBatch, generalPosition, classPanelWidth);
				DestinyClassType classType = data.Player.GetModPlayer<ClassPlayer>().ClassType;
				string classText = classType.ToString();
				Color classColor = Color.White;
				switch (classType)
				{
					case DestinyClassType.Titan:
						classColor = Color.Red;
						break;

					case DestinyClassType.Hunter:
						classColor = Color.Aqua;
						break;

					case DestinyClassType.Warlock:
						classColor = Color.Gold;
						break;
				}
				classPosition += new Vector2(classPanelWidth * 0.5f - FontAssets.MouseText.Value.MeasureString(classText).X * 0.5f, 3f);
				Utils.DrawBorderString(spriteBatch, classText, classPosition, classColor);
				generalPosition.X += classPanelWidth + 5f;

				Vector2 playTimePosition = generalPosition;
				float playTimePanelWidth = innerDimensions.X + innerDimensions.Width - playTimePosition.X - 2;
				DrawPanel(spriteBatch, playTimePosition, playTimePanelWidth);
				TimeSpan playTime = data.GetPlayTime();
				int totalHours = playTime.Days * 24 + playTime.Hours;
				string playTimeFormatted = ((totalHours < 10) ? "0" : string.Empty) + totalHours + playTime.ToString("\\:mm\\:ss");
				playTimePosition += new Vector2(playTimePanelWidth * 0.5f - FontAssets.MouseText.Value.MeasureString(playTimeFormatted).X * 0.5f, 3f);
				Utils.DrawBorderString(spriteBatch, playTimeFormatted, playTimePosition, Color.White);
				generalPosition.X += playTimePanelWidth + 5f;
			});
			cursor.Emit(OpCodes.Ret);
		}
	}
}