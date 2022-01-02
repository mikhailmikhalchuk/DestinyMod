using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;

namespace DestinyMod.Common.Items
{
	public abstract class DestinyModItem : ModItem
	{
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
	}
}