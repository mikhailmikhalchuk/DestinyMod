using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.GameContent.UI.Elements;
using Microsoft.Xna.Framework;
using DestinyMod.Core.UI;
using Microsoft.Xna.Framework.Input;
using Terraria.ModLoader;
using Terraria.GameInput;
using DestinyMod.Common.UI;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using DestinyMod.Common.Items;
using System.Collections.Generic;
using DestinyMod.Common.Items.PerksAndMods;
using DestinyMod.Common.GlobalItems;
using System.Linq;
using DestinyMod.Content.UI.MouseText;
using Terraria.UI;
using Terraria.GameContent;
using DestinyMod.Core.Extensions;

namespace DestinyMod.Content.UI.ItemDetails
{
	public partial class ItemDetailsState : DestinyModUIState
	{
		public Item InspectedItem { get; }

		public ItemData InspectedItemData { get; }

		public UIElement MasterBackground { get; private set; }

		public DisplayItemSlot InspectedItemDisplay { get; private set; }

		public UIText InspectedItemName { get; private set; }

		public UIText InspectedItemPowerLevel { get; private set; }

		public ItemDetailsState_Perks Perks { get; private set; }

		public ItemDetailsState_Mods Mods { get; private set; }

		public ItemDetailState_Customization Customization { get; private set; }

		public MouseText_TitleAndSubtitle MouseText_TitleAndSubtitle { get; private set; }

		public MouseText_BodyText MouseText_BodyText { get; private set; }

		public MouseText_ClickIndicator MouseText_ClickIndicator { get; private set; }

		public static Color BaseColor_Light = new Color(68, 70, 74);

		public static Color BaseColor_Dark = new Color(37, 37, 38);

		public ItemDetailsState() { }

		public ItemDetailsState(Item inspectedItem)
        {
			InspectedItem = inspectedItem;
			if (ItemData.ItemDatasByID.TryGetValue(InspectedItem.type, out ItemData itemData))
            {
				InspectedItemData = itemData;
            }
		}

		public override void PreLoad(ref string name)
		{
			AutoSetState = false;
			AutoAddHandler = true;
		}

		public override UIHandler Load() => new UIHandler(UserInterface, "Vanilla: Inventory", LayerName);

		public override void OnInitialize()
		{
			MouseText_TitleAndSubtitle = new MouseText_TitleAndSubtitle(420, string.Empty, string.Empty, titleScale: 0f, subtitleScale: 0f);;
			MouseText_BodyText = new MouseText_BodyText(420, string.Empty, scale: 0f);
			MouseText_ClickIndicator = new MouseText_ClickIndicator();
			MouseText_ClickIndicator.Width.Pixels = 420;
			ModContent.GetInstance<MouseTextState>().CleanseAll();

			MasterBackground = new UIElement();
			MasterBackground.Width.Pixels = 600;
			MasterBackground.Height.Pixels = 480;
			MasterBackground.HAlign = 0.5f;
			MasterBackground.VAlign = 0.5f;
			MasterBackground.SetPadding(0f);

			int top = 10;
			InspectedItemDisplay = new DisplayItemSlot(InspectedItem.type);
			InspectedItemDisplay.Top.Pixels = top;
			InspectedItemDisplay.Left.Pixels = 10;
			MasterBackground.Append(InspectedItemDisplay);

			InspectedItemName = new UIText(InspectedItem.HoverName, 0.66f, large: true);
			InspectedItemName.Left.Pixels = 10 + InspectedItemDisplay.Width.Pixels;
			InspectedItemName.VAlign = 0.5f;
			InspectedItemDisplay.Append(InspectedItemName);

			top += (int)InspectedItemDisplay.Height.Pixels + 20;

			Perks = new ItemDetailsState_Perks(this);
			Perks.Visible = true;
			Perks.Left.Pixels = 10;
			Perks.Top.Pixels = top;
			MasterBackground.Append(Perks);

			Mods = new ItemDetailsState_Mods(this);
			Mods.Visible = true;
			Mods.Left.Pixels = 10;
			top += (int)Perks.Height.Pixels + 8;
			Mods.Top.Pixels = top;
			MasterBackground.Append(Mods);

			Customization = new ItemDetailState_Customization(this);
			Customization.Visible = true;
			Customization.Left.Pixels = 10;
			top += (int)Mods.Height.Pixels + 8;
			Customization.Top.Pixels = top;
			MasterBackground.Append(Customization);

			top += (int)Customization.Height.Pixels + 8;
			MasterBackground.Height.Pixels = top;

			InspectedItemPowerLevel = new UIText(InspectedItem.GetGlobalItem<ItemDataItem>().LightLevel.ToString(), 0.7f, large: true);
			InspectedItemPowerLevel.Left.Pixels = 375;
			InspectedItemPowerLevel.Top.Pixels = 260;
			InspectedItemPowerLevel.VAlign = 0.5f;
			InspectedItemDisplay.Append(InspectedItemPowerLevel);

			UIText InspectedItemPowerLevelText = new UIText("Power", 0.8f);
			InspectedItemPowerLevelText.Left.Pixels = 440;
			InspectedItemPowerLevelText.Top.Pixels = 265;
			InspectedItemPowerLevelText.VAlign = 0.5f;
			InspectedItemDisplay.Append(InspectedItemPowerLevelText);

			UIImageButton CloseButton = new UIImageButton(Main.Assets.Request<Texture2D>("Images/UI/SearchCancel"));
			CloseButton.Left.Pixels = MasterBackground.Width.Pixels - 33;
			CloseButton.Top.Pixels = MasterBackground.Top.Pixels + 10;
			CloseButton.OnClick += (evt, listeningElement) =>
			{
				SoundEngine.PlaySound(SoundID.MenuClose);
				ModContent.GetInstance<ItemDetailsState>().UserInterface.SetState(null);
			};
			MasterBackground.Append(CloseButton);

			Vector2 size = MasterBackground.CalculateChildrenSize();
			MasterBackground.Width.Pixels = size.X + 20;
			MasterBackground.Height.Pixels = size.Y + 20;
			Append(MasterBackground);
		}

        public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);

			/*if (Main.GameUpdateCount % 60 == 0)
			Main.NewText(GetElementAt(Main.MouseScreen).ToString());*/

			if (Main.keyState.IsKeyDown(Keys.Escape))
			{
				SoundEngine.PlaySound(SoundID.MenuClose);
				ModContent.GetInstance<MouseTextState>().Visible = false;
				ModContent.GetInstance<ItemDetailsState>().UserInterface.SetState(null);
			}

			if (MasterBackground.ContainsPoint(Main.MouseScreen))
            {
				Main.LocalPlayer.mouseInterface = true;
            }

			ModContent.GetInstance<MouseTextState>().Visible = MasterBackground.ContainsPoint(Main.MouseScreen);
		}

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
			CalculatedStyle backgroundDimensions = MasterBackground.GetDimensions();
			Texture2D magicPixel = TextureAssets.MagicPixel.Value;
			Rectangle backgroundRect = backgroundDimensions.ToRectangle();
			spriteBatch.Draw(magicPixel, backgroundRect, BaseColor_Light);
			backgroundRect.Inflate(-2, -2);
			spriteBatch.Draw(magicPixel, backgroundRect, BaseColor_Dark);
		}
    }
}