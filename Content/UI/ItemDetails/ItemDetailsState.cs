using Terraria;
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

namespace DestinyMod.Content.UI.ItemDetails
{
	public partial class ItemDetailsState : DestinyModUIState
	{
		public Item InspectedItem { get; }

		public ItemData InspectedItemData { get; }

		public UIImage MasterBackground { get; private set; }

		public DisplayItemSlot InspectedItemDisplay { get; private set; }

		public UIText InspectedItemName { get; private set; }

		public static Color SeperatorColor = new Color(68, 70, 74);

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
			Asset<Texture2D> masterBackgroundTexture = ModContent.Request<Texture2D>("DestinyMod/Content/UI/ItemDetails/ItemDetailsBackground", AssetRequestMode.ImmediateLoad);
			MasterBackground = new UIImage(masterBackgroundTexture);
			MasterBackground.Width.Set(masterBackgroundTexture.Width(), 0);
			MasterBackground.Height.Set(masterBackgroundTexture.Height(), 0);
			MasterBackground.HAlign = 0.5f;
			MasterBackground.VAlign = 0.5f;
			MasterBackground.SetPadding(0f);
			Append(MasterBackground);

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
			top = InitialisePerksSection(top);
			top = InitialiseModsSection(top);
		}

        public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);

			if (Main.keyState.IsKeyDown(Keys.Escape))
			{
				ModContent.GetInstance<ItemDetailsState>().UserInterface.SetState(null);
			}
		}
	}
}