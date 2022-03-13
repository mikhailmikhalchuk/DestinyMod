using DestinyMod.Common.Items;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace DestinyMod.Common.ModPlayers
{
	public class ItemPlayer : ModPlayer
	{
		public int AegisCharge = 0;

		public int OverchargeStacks = 0;

		[Flags]
		public enum IterationContext
		{
			None = 0,
			HeldItem = 1,
			Inventory = 2,
			Armor = 4
		}

		public override void PostUpdate()
		{
			for (int inventoryCount = 0; inventoryCount < Player.inventory.Length - 1; inventoryCount++)
			{
				if (Player.selectedItem == inventoryCount)
				{
					continue;
				}

				Item inventoryItem = Player.inventory[inventoryCount];
				if (inventoryItem == null || inventoryItem.IsAir || inventoryItem.ModItem is not DestinyModItem inventoryDestinyModItem)
				{
					continue;
				}

				inventoryDestinyModItem.OnRelease(Player);
			}

			Item heldItem = Main.mouseItem.IsAir ? Player.HeldItem : Main.mouseItem;
			if (heldItem.ModItem is DestinyModItem destinyModItem)
			{
				destinyModItem.OnHold(Player);
			}
		}

		// Perhaps we should not for performance?
		public void ImplementItemIteration(Func<DestinyModItem, IterationContext> determineContext, Action<DestinyModItem> onSuccessfulIteration)
		{
			foreach (Item armorItem in Player.armor)
			{
				if (armorItem == null || armorItem.IsAir || armorItem.ModItem is not DestinyModItem armorDestinyModItem
					|| !determineContext(armorDestinyModItem).HasFlag(IterationContext.Armor))
				{
					continue;
				}

				onSuccessfulIteration(armorDestinyModItem);
			}

			for (int inventoryCount = 0; inventoryCount < Player.inventory.Length - 1; inventoryCount++)
			{
				if (Player.selectedItem == inventoryCount)
				{
					continue;
				}

				Item inventoryItem = Player.inventory[inventoryCount];
				if (inventoryItem == null || inventoryItem.IsAir || inventoryItem.ModItem is not DestinyModItem inventoryDestinyModItem
					|| !determineContext(inventoryDestinyModItem).HasFlag(IterationContext.Inventory))
				{
					continue;
				}

				onSuccessfulIteration(inventoryDestinyModItem);
			}

			Item heldItem = Main.mouseItem.IsAir ? Player.HeldItem : Main.mouseItem;
			if (heldItem == null || heldItem.IsAir || heldItem.ModItem is not DestinyModItem heldDestinyModItem
				|| !determineContext(heldDestinyModItem).HasFlag(IterationContext.HeldItem))
			{
				return;
			}

			onSuccessfulIteration(heldDestinyModItem);
		}

		public override void PostUpdateRunSpeeds()
        {
			ImplementItemIteration(destinyModItem => destinyModItem.DeterminePostUpdateRunSpeedsContext(Player), destinyModItem => destinyModItem.PostUpdateRunSpeeds(Player));
		}

		public override void ModifyDrawInfo(ref PlayerDrawSet drawInfo)
		{
			foreach (Item armorItem in Player.armor)
			{
				if (armorItem == null || armorItem.IsAir || armorItem.ModItem is not DestinyModItem armorDestinyModItem
					|| !armorDestinyModItem.DetermineModifyDrawInfoContext(Player).HasFlag(IterationContext.Armor))
				{
					continue;
				}

				armorDestinyModItem.ModifyDrawInfo(Player, ref drawInfo);
			}

			for (int inventoryCount = 0; inventoryCount < Player.inventory.Length - 1; inventoryCount++)
			{
				Item inventoryItem = Player.inventory[inventoryCount];
				if (inventoryItem == null || inventoryItem.IsAir || inventoryItem.ModItem is not DestinyModItem inventoryDestinyModItem
					|| !inventoryDestinyModItem.DetermineModifyDrawInfoContext(Player).HasFlag(IterationContext.Inventory))
				{
					continue;
				}

				inventoryDestinyModItem.ModifyDrawInfo(Player, ref drawInfo);
			}

			Item heldItem = Main.mouseItem.IsAir ? Player.inventory[Player.selectedItem] : Main.mouseItem;
			if (heldItem == null || heldItem.IsAir || heldItem.ModItem is not DestinyModItem heldDestinyModItem
				|| !heldDestinyModItem.DetermineModifyDrawInfoContext(Player).HasFlag(IterationContext.HeldItem))
			{
				return;
			}

			heldDestinyModItem.ModifyDrawInfo(Player, ref drawInfo);
		}

		public override void HideDrawLayers(PlayerDrawSet drawInfo)
        {
			ImplementItemIteration(destinyModItem => destinyModItem.DetermineHideDrawLayersContext(Player), destinyModItem => destinyModItem.HideDrawLayers(Player, drawInfo));
		}
	}
}