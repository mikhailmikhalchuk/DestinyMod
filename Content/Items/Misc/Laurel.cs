using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using DestinyMod.Common.Items;
using DestinyMod.Common.ModPlayers;
using Terraria.GameContent;

namespace DestinyMod.Content.Items.Misc
{
	public class Laurel : DestinyModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Deposit this at the Podium to rep your class and earn rewards");
		}

		public override void DestinySetDefaults()
		{
			Item.maxStack = 99;
			Item.rare = ItemRarityID.Blue;
		}

		public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
		{
			ClassPlayer classPlayer = Main.LocalPlayer.GetModPlayer<ClassPlayer>();
			Texture2D texture = TextureAssets.Item[Item.type].Value;
			if (classPlayer.ClassType != DestinyClassType.None)
			{
				texture = Mod.Assets.Request<Texture2D>("Content/Items/Misc/" + classPlayer.ClassType.ToString() + "Laurel").Value;
			}
			Vector2 position = Item.Center - Main.screenPosition;
			spriteBatch.Draw(texture, position, null, lightColor, rotation, texture.Size(), scale, SpriteEffects.None, 0f);
			return false;
		}

		public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
		{
			ClassPlayer classPlayer = Main.LocalPlayer.GetModPlayer<ClassPlayer>();
			Texture2D texture = TextureAssets.Item[Item.type].Value;
			if (classPlayer.ClassType != DestinyClassType.None)
			{
				texture = Mod.Assets.Request<Texture2D>("Content/Items/Misc/" + classPlayer.ClassType.ToString() + "Laurel").Value;
			}
			spriteBatch.Draw(texture, position, null, drawColor, 0, origin, scale, SpriteEffects.None, 0f);
			return false;
		}

		public override bool? CanBurnInLava() => true;
	}
}