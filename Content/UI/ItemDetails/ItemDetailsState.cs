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
using DestinyMod.Common.Items.Modifiers;
using DestinyMod.Common.GlobalItems;
using System.Linq;
using DestinyMod.Content.UI.MouseText;
using Terraria.UI;
using Terraria.GameContent;
using DestinyMod.Core.Extensions;
using DestinyMod.Core.Utils;
using System;
using Terraria.Graphics.Shaders;
using Terraria.DataStructures;

namespace DestinyMod.Content.UI.ItemDetails
{
	public partial class ItemDetailsState : DestinyModUIState
	{
		public Item InspectedItem { get; }

		public ItemData InspectedItemTypeData { get; }

		public ItemDataItem InspectedItemData { get; }

		public UIElement MasterBackground { get; private set; }

		public DisplayItemSlot InspectedItemDisplay { get; private set; }

		public UIText InspectedItemName { get; private set; }

		public UIText InspectedItemPowerLevel { get; private set; }

		public UIElement InspectedItemLargeDisplay { get; private set; }

		public ItemDetailsState_Perks Perks { get; private set; }

		public ItemDetailsState_Mods Mods { get; private set; }

		public ItemDetailsState_Customization Customization { get; private set; }

		public ItemDetailsState_ItemStats Stats { get; private set; }

		public MouseText_TitleAndSubtitle MouseText_TitleAndSubtitle { get; private set; }

		public MouseText_BodyText MouseText_BodyText { get; private set; }

		public MouseText_ClickIndicator MouseText_ClickIndicator { get; private set; }

		public static Color BaseColor_Light = new Color(68, 70, 74);

		public static Color BaseColor_Dark = new Color(37, 37, 38);

		public static readonly RasterizerState OverflowHiddenRasterizerState = new RasterizerState
		{
			CullMode = CullMode.None,
			ScissorTestEnable = true
		};

		public ItemDetailsState() { }

		public ItemDetailsState(Item inspectedItem)
        {
			InspectedItem = inspectedItem;
			if (ItemData.ItemDatasByID.TryGetValue(InspectedItem.type, out ItemData itemData))
            {
				InspectedItemTypeData = itemData;
            }
			InspectedItemData = InspectedItem.GetGlobalItem<ItemDataItem>();
		}

		public override void PreLoad(ref string name)
		{
			AutoSetState = false;
			AutoAddHandler = true;
		}

		public override UIHandler Load() => new UIHandler(UserInterface, "Vanilla: Inventory", LayerName);

		public override void OnInitialize()
		{
			MouseText_TitleAndSubtitle = new MouseText_TitleAndSubtitle(string.Empty, string.Empty, titleScale: 0f, subtitleScale: 0f);;
			MouseText_BodyText = new MouseText_BodyText(string.Empty, scale: 0f);
			MouseText_ClickIndicator = new MouseText_ClickIndicator();
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
			InspectedItemLargeDisplay = new UIElement();
			InspectedItemLargeDisplay.Left.Pixels = 310;
			InspectedItemLargeDisplay.Top.Pixels = top;
			InspectedItemLargeDisplay.Width.Pixels = 300;
			InspectedItemLargeDisplay.Height.Pixels = 150;
			MasterBackground.Append(InspectedItemLargeDisplay);

            Perks = new ItemDetailsState_Perks(this)
            {
                Visible = true
            };
            Perks.Left.Pixels = 10;
			Perks.Top.Pixels = top;
			MasterBackground.Append(Perks);
			top += (int)Perks.Height.Pixels + 8;

            Mods = new ItemDetailsState_Mods(this)
            {
                Visible = true
            };
            Mods.Left.Pixels = 10;
			Mods.Top.Pixels = top;
			MasterBackground.Append(Mods);
			top += (int)Mods.Height.Pixels + 8;

            Customization = new ItemDetailsState_Customization(this)
            {
                Visible = true
            };
            Customization.Left.Pixels = 10;
			Customization.Top.Pixels = top;
			MasterBackground.Append(Customization);
			top += (int)Customization.Height.Pixels + 8;

			Stats = new ItemDetailsState_ItemStats(this)
			{
				Visible = true
			};
			Stats.Left.Pixels = MasterBackground.Width.Pixels - 10 - Stats.Width.Pixels;
			Stats.Top.Pixels = top - Stats.Height.Pixels + 5;
			MasterBackground.Append(Stats);

			MasterBackground.Height.Pixels = top;

			UIImageButton CloseButton = new UIImageButton(Main.Assets.Request<Texture2D>("Images/UI/SearchCancel"));
			CloseButton.Left.Pixels = MasterBackground.Width.Pixels - 10;
			CloseButton.Top.Pixels = MasterBackground.Top.Pixels + 10;
			CloseButton.OnClick += (evt, listeningElement) =>
			{
				SoundEngine.PlaySound(SoundID.MenuClose);
				ModContent.GetInstance<ItemDetailsState>().UserInterface.SetState(null);
			};
			MasterBackground.Append(CloseButton);

			Vector2 size = MasterBackground.CalculateChildrenSize();
			MasterBackground.Width.Pixels = size.X + 10;
			MasterBackground.Height.Pixels = size.Y + 10;
			Append(MasterBackground);
		}

        public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);

			if (Main.keyState.IsKeyDown(Keys.Escape))
			{
				SoundEngine.PlaySound(SoundID.MenuClose);
				ModContent.GetInstance<MouseTextState>().Visible = false;
				ModContent.GetInstance<ItemDetailsState>().UserInterface.SetState(null);
				return;
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

			Rectangle croppedBackground = InspectedItemLargeDisplay.GetDimensions().ToRectangle();
			croppedBackground.Inflate(0, 75);

			Main.instance.LoadItem(InspectedItem.type);
			Texture2D itemTexture = TextureAssets.Item[InspectedItem.type].Value;
			bool isXGreaterThanY = itemTexture.Width > itemTexture.Height;
			int greaterDimension = Math.Max(itemTexture.Width, itemTexture.Height);
			float scaleRatio = isXGreaterThanY ? croppedBackground.Width / greaterDimension : croppedBackground.Height / greaterDimension;
			int destWidth = (int)(itemTexture.Width * scaleRatio);
			int destHeight = (int)(itemTexture.Height * scaleRatio);
			int destX = (int)(croppedBackground.X + (croppedBackground.Width - destWidth) / 2f);
			int destY = (int)(croppedBackground.Y + (croppedBackground.Height - destHeight) / 2f);
			Rectangle destinationRect = new Rectangle(destX, destY, destWidth, destHeight);

			DrawData itemDisplay = new DrawData(itemTexture, destinationRect, Color.White);
			SamplerState anisotropicClamp = SamplerState.AnisotropicClamp;
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.UIScaleMatrix);
			if (InspectedItemData.Shader != null && InspectedItemData.Shader.dye > 0)
			{
				GameShaders.Armor.GetShaderFromItemId(InspectedItemData.Shader.type).Apply(InspectedItem, itemDisplay);
			}

			itemDisplay.effect = SpriteEffects.FlipHorizontally;
			itemDisplay.Draw(spriteBatch);
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, anisotropicClamp, DepthStencilState.None, OverflowHiddenRasterizerState, null, Main.UIScaleMatrix);
		}
    }
}