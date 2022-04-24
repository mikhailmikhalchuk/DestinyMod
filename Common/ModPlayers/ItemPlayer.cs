using DestinyMod.Common.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace DestinyMod.Common.ModPlayers
{
	public class ItemPlayer : ModPlayer
	{
		public int AegisCharge = 0;

		public int OverchargeStacks = 0;

		public bool GlaiveShielded;

		public Item OldHeldItem;

		[Flags]
		public enum IterationContext
		{
			None = 0,
			HeldItem = 1,
			Inventory = 2,
			Armor = 4
		}

        public override void PostUpdateMiscEffects()
		{
			for (int inventoryCount = 0; inventoryCount < Player.inventory.Length - 1; inventoryCount++)
			{
				if (Player.selectedItem == inventoryCount)
				{
					continue;
				}

				Item inventoryItem = Player.inventory[inventoryCount];
				if (inventoryItem == null || inventoryItem.IsAir || inventoryItem.ModItem is not DestinyModItem inventoryDestinyModItem || inventoryItem != OldHeldItem)
				{
					continue;
				}

				inventoryDestinyModItem.OnRelease(Player);
			}

			Item heldItem = Main.mouseItem.IsAir ? Player.HeldItem : Main.mouseItem;
			if (heldItem.ModItem is DestinyModItem destinyModItem && heldItem != OldHeldItem)
			{
				destinyModItem.OnHold(Player);
			}

			OldHeldItem = heldItem;
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

				onSuccessfulIteration.Invoke(armorDestinyModItem);
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

				onSuccessfulIteration.Invoke(inventoryDestinyModItem);
			}

			Item heldItem = Main.mouseItem.IsAir ? Player.HeldItem : Main.mouseItem;
			if (heldItem == null || heldItem.IsAir || heldItem.ModItem is not DestinyModItem heldDestinyModItem
				|| !determineContext(heldDestinyModItem).HasFlag(IterationContext.HeldItem))
			{
				return;
			}

			onSuccessfulIteration.Invoke(heldDestinyModItem);
		}

		public override void PostUpdateRunSpeeds()
        {
			ImplementItemIteration(destinyModItem => destinyModItem.PostUpdateRunSpeedsContext(Player), destinyModItem => destinyModItem.PostUpdateRunSpeeds(Player));
		}

		public override void ModifyDrawInfo(ref PlayerDrawSet drawInfo)
		{
			foreach (Item armorItem in Player.armor)
			{
				if (armorItem == null || armorItem.IsAir || armorItem.ModItem is not DestinyModItem armorDestinyModItem
					|| !armorDestinyModItem.ModifyDrawInfoContext(Player).HasFlag(IterationContext.Armor))
				{
					continue;
				}

				armorDestinyModItem.ModifyDrawInfo(Player, ref drawInfo);
			}

			for (int inventoryCount = 0; inventoryCount < Player.inventory.Length - 1; inventoryCount++)
			{
				Item inventoryItem = Player.inventory[inventoryCount];
				if (inventoryItem == null || inventoryItem.IsAir || inventoryItem.ModItem is not DestinyModItem inventoryDestinyModItem
					|| !inventoryDestinyModItem.ModifyDrawInfoContext(Player).HasFlag(IterationContext.Inventory))
				{
					continue;
				}

				inventoryDestinyModItem.ModifyDrawInfo(Player, ref drawInfo);
			}

			Item heldItem = Main.mouseItem.IsAir ? Player.HeldItem : Main.mouseItem;
			if (heldItem == null || heldItem.IsAir || heldItem.ModItem is not DestinyModItem heldDestinyModItem
				|| !heldDestinyModItem.ModifyDrawInfoContext(Player).HasFlag(IterationContext.HeldItem))
			{
				return;
			}

			heldDestinyModItem.ModifyDrawInfo(Player, ref drawInfo);
		}

		public override void HideDrawLayers(PlayerDrawSet drawInfo)
        {
			ImplementItemIteration(destinyModItem => destinyModItem.HideDrawLayersContext(Player), destinyModItem => destinyModItem.HideDrawLayers(Player, drawInfo));
		}

		public override bool CanBeHitByNPC(NPC npc, ref int cooldownSlot) => !GlaiveShielded;

		public override bool CanBeHitByProjectile(Projectile proj) => !GlaiveShielded;

        public override void DrawEffects(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {
			if (GlaiveShielded)
            {
				Main.spriteBatch.End();
				Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
				DrawData forceFieldData = new DrawData((Texture2D)Main.Assets.Request<Texture2D>("Images/Misc/Perlin"), Player.MountedCenter - Main.screenPosition, (Rectangle?)new Rectangle(0, 0, 100, 100), Color.White * 0.75f, 0, new Vector2(50), 1, SpriteEffects.None, 0);
				GameShaders.Misc["ForceField"].UseColor(new Vector3(1));
				GameShaders.Misc["ForceField"].Apply(forceFieldData);
				forceFieldData.Draw(Main.spriteBatch);
				Main.spriteBatch.End();
				Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, Main.GameViewMatrix.TransformationMatrix);
			}
		}

        public override void ModifyHitNPC(Item item, NPC target, ref int damage, ref float knockback, ref bool crit)
        {
            base.ModifyHitNPC(item, target, ref damage, ref knockback, ref crit);
        }

        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            base.ModifyHitNPCWithProj(proj, target, ref damage, ref knockback, ref crit, ref hitDirection);
        }
    }
}