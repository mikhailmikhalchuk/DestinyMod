using DestinyMod.Common.Items.ItemTypes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace DestinyMod.Content.Items.Weapons.Ranged.Omolon
{
	public class OmolonWeapon : Gun
	{
		public virtual string GlowTexturePath => Texture + "_Glow";

		public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
		{
			Texture2D texture = ModContent.Request<Texture2D>(GlowTexturePath).Value;
			Vector2 drawPosition = Item.Center + new Vector2(0, 2) - Main.screenPosition;
			spriteBatch.Draw(texture, drawPosition, texture.Bounds, Color.White, rotation, texture.Size() * 0.5f, scale, SpriteEffects.None, 0f);
		}
	}
}