using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using static DestinyMod.Common.ModPlayers.ItemPlayer;

namespace DestinyMod.Common.Items
{
	public abstract class DestinyModItem : ModItem
	{
		public int DestinyModReuseDelay;

		public sealed override void SetDefaults()
		{
			AutomaticSetDefaults();
			DestinySetDefaults();
		}

		public virtual void AutomaticSetDefaults()
		{
			Texture2D itemTexture = TextureAssets.Item[Item.type].Value;
			Item.width = itemTexture.Width;
			Item.height = itemTexture.Height;
		}

		public virtual void DestinySetDefaults() { }

		public virtual bool CanEquip(Player player) => true;

		public virtual IterationContext DeterminePostUpdateRunSpeedsContext(Player player) => IterationContext.None;

		public virtual void PostUpdateRunSpeeds(Player player) { }
	}
}