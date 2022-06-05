using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using static DestinyMod.Common.ModPlayers.ItemPlayer;
using Terraria.DataStructures;

namespace DestinyMod.Common.Items
{
	public abstract class DestinyModItem : ModItem
	{
		public int DestinyModReuseDelay;

		public bool DestinyModChannel;

		public sealed override void SetDefaults()
		{
			AutomaticSetDefaults();
			DestinySetDefaults();
		}

		public virtual void AutomaticSetDefaults()
		{
			if (!Main.dedServ)
            {
				Texture2D itemTexture = ModContent.Request<Texture2D>(Texture, ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
				Item.width = itemTexture.Width;
				Item.height = itemTexture.Height;
			}
		}

		/// <inheritdoc cref="SetDefaults"/>
		public virtual void DestinySetDefaults() { }

		/// <summary>
		/// Allows you to disallow the player from equipping this equipable item. Return false to disallow equipping this equipable item. Returns true by default.
		/// </summary>
		/// <param name="player">The player who owns the item.</param>
		public virtual bool CanEquip(Player player) => true;

		/// <summary>
		/// Called when the player switches to this item. Calls on all clients.
		/// </summary>
		/// <param name="player">The player who switched to the item.</param>
		public virtual void OnHold(Player player) { }

		/// <summary>
		/// Called when the player switches away from this item. Calls on all clients.
		/// </summary>
		/// <param name="player">The player who switched away from the item.</param>
		public virtual void OnRelease(Player player) { }

		public virtual IterationContext PostUpdateRunSpeedsContext(Player player) => IterationContext.None;

		public virtual void PostUpdateRunSpeeds(Player player) { }

		public virtual IterationContext ModifyDrawInfoContext(Player player) => IterationContext.None;

		public virtual void ModifyDrawInfo(Player player, ref PlayerDrawSet drawInfo) { }

		public virtual IterationContext HideDrawLayersContext(Player player) => IterationContext.None;

		public virtual void HideDrawLayers(Player player, PlayerDrawSet drawInfo) { }
	}
}