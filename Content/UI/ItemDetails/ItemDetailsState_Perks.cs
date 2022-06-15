using Terraria.GameContent.UI.Elements;
using Microsoft.Xna.Framework;
using DestinyMod.Core.UI;
using DestinyMod.Common.UI;
using System.Collections.Generic;
using DestinyMod.Common.Items.Modifiers;
using DestinyMod.Common.GlobalItems;
using System.Linq;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using DestinyMod.Content.UI.MouseText;
using Terraria.UI;
using Microsoft.Xna.Framework.Graphics;
using DestinyMod.Core.Extensions;

namespace DestinyMod.Content.UI.ItemDetails
{
	public class ItemDetailsState_Perks : UIElement
	{
		public ItemDetailsState ItemDetailsState { get; private set; }

		public UIText WeaponPerksTitle { get; private set; }

		public UISeparator WeaponPerksSeparator { get; private set; }

		public IList<ItemPerkDisplay> ItemPerks { get; private set; }

		private bool InternalVisible;

		public bool Visible
		{
			get => InternalVisible;
			set
			{
				InternalVisible = value;
				IgnoresMouseInteraction = !InternalVisible;
			}
		}

		public ItemDetailsState_Perks(ItemDetailsState itemDetailsState)
		{
			ItemDetailsState = itemDetailsState;

			WeaponPerksTitle = new UIText("Weapon Perks");
			Append(WeaponPerksTitle);

			WeaponPerksSeparator = new UISeparator();
			WeaponPerksSeparator.Top.Pixels = 20;
			WeaponPerksSeparator.Width.Pixels = 300f;
			WeaponPerksSeparator.Height.Pixels = 2f;
			WeaponPerksSeparator.Color = ItemDetailsState.BaseColor_Light;
			Append(WeaponPerksSeparator);

			ItemPerks = new List<ItemPerkDisplay>();

			float lowestPerkPosition = 28;
			ItemDataItem inspectedItem = ItemDetailsState.InspectedItem.GetGlobalItem<ItemDataItem>();

			if (inspectedItem.PerkPool != null)
			{
				for (int i = 0; i < inspectedItem.PerkPool.Count; i++)
				{
					ItemPerkPool perkTypePool = inspectedItem.PerkPool[i];
					IList<ItemPerkDisplay> itemPerksWithinType = new List<ItemPerkDisplay>();
					for (int perkIndexer = 0; perkIndexer < perkTypePool.Perks.Count; perkIndexer++)
					{
						ItemPerk perkInQuestion = perkTypePool.Perks[perkIndexer];
						bool isPerkActive = inspectedItem.ActivePerks.FirstOrDefault(perk => perk.Name == perkInQuestion.Name) != null;
						ItemPerkDisplay perk = new ItemPerkDisplay(perkInQuestion, isPerkActive);
						perk.Left.Pixels = ItemPerk.TextureSize * (5f / 3f) * i;
						perk.Top.Pixels = 28 + ItemPerk.TextureSize * (6f / 5f) * perkIndexer;
						lowestPerkPosition = Math.Max(lowestPerkPosition, perk.Top.Pixels);

						perk.OnUpdate += (evt) =>
						{
							if (!perk.ContainsPoint(Main.MouseScreen))
							{
								return;
							}

							ItemDetailsState.MouseText_TitleAndSubtitle.UpdateData(perk.ItemPerk.DisplayName ?? perk.ItemPerk.Name, perkTypePool.TypeName);
							ItemDetailsState.MouseText_BodyText.UpdateData(perk.ItemPerk.Description);
							MouseTextState mouseTextState = ModContent.GetInstance<MouseTextState>();
							mouseTextState.AppendToMasterBackground(ItemDetailsState.MouseText_TitleAndSubtitle);
							mouseTextState.AppendToMasterBackground(ItemDetailsState.MouseText_BodyText);

							if (!perk.IsActive)
							{
								mouseTextState.AppendToMasterBackground(ItemDetailsState.MouseText_ClickIndicator);
							}
						};

						perk.OnMouseDown += (evt, listeningElement) =>
						{
							if (perk.IsActive)
							{
								return;
							}

							perk.ToggleActive();
							SyncActivePerks();
							SoundEngine.PlaySound(SoundID.Grab);
						};

						ItemPerks.Add(perk);
						itemPerksWithinType.Add(perk);
						Append(perk);
					}

					foreach (ItemPerkDisplay itemPerkDisplay in itemPerksWithinType)
					{
						itemPerkDisplay.InterconnectedPerks = itemPerksWithinType.Where(itemPerk => itemPerk != itemPerkDisplay).ToList();
					}
				}

				for (int i = 1; i < inspectedItem.PerkPool.Count; i++)
				{
					UISeparator perkSeparator = new UISeparator();
					perkSeparator.Left.Pixels = ItemPerk.TextureSize * (5f / 3f) * i - (ItemPerk.TextureSize / 3f);
					perkSeparator.Top.Pixels = 28;
					perkSeparator.Width.Pixels = 2f;
					perkSeparator.Height.Pixels = lowestPerkPosition - 20 + ItemPerk.TextureSize;
					perkSeparator.Color = ItemDetailsState.BaseColor_Light;
					Append(perkSeparator);
				}
			}

			Vector2 size = this.CalculateChildrenSize();
			Width.Pixels = size.X;
			Height.Pixels = size.Y;
		}

		public override bool ContainsPoint(Vector2 point) => Visible && base.ContainsPoint(point);

		public override void Update(GameTime gameTime)
		{
			if (!Visible)
			{
				return;
			}

			base.Update(gameTime);
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			if (!Visible)
			{
				return;
			}

			base.Draw(spriteBatch);
		}

		public void SyncActivePerks()
        {
			ItemDataItem inspectedItemData = ItemDetailsState.InspectedItem.GetGlobalItem<ItemDataItem>();
			inspectedItemData.ActivePerks.Clear();
			foreach (ItemPerkDisplay itemPerk in ItemPerks)
			{
				if (!itemPerk.IsActive)
				{
					continue;
				}

				if (itemPerk.ItemPerk.IsInstanced)
				{
					inspectedItemData.ActivePerks.Add(ItemPerk.CreateInstance(itemPerk.ItemPerk.Name));
				}
				else
				{
					inspectedItemData.ActivePerks.Add(itemPerk.ItemPerk);
				}
				ItemDetailsState.InspectedItem.ModItem?.SetDefaults();
				inspectedItemData.SetDefaults(ItemDetailsState.InspectedItem);
			}
        }
	}
}