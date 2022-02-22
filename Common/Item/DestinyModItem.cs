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
			Texture2D itemTexture = ModContent.Request<Texture2D>(Texture, ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
			Item.width = itemTexture.Width;
			Item.height = utilisedHeight;
		}

		public virtual void DestinySetDefaults() { }

		public virtual bool CanEquip(Player player) => true;

		public virtual void OnHold(Player player) { }

		public virtual void OnRelease(Player player) { }

		public virtual IterationContext DeterminePostUpdateRunSpeedsContext(Player player) => IterationContext.None;

		public virtual void PostUpdateRunSpeeds(Player player) { }

		public virtual IterationContext DetermineModifyDrawInfoContext(Player player) => IterationContext.None;

		public virtual void ModifyDrawInfo(Player player, ref PlayerDrawSet drawInfo) { }

		public virtual IterationContext DetermineHideDrawLayersContext(Player player) => IterationContext.None;

		public virtual void HideDrawLayers(Player player, PlayerDrawSet drawInfo) { }
	}
}