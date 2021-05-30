using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace TheDestinyMod.Items
{
	public class Laurel : ModItem
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("Deposit this at the Podium to rep your class and earn rewads");
		}

		public override void SetDefaults() {
			item.height = 24;
			item.width = 24;
			item.maxStack = 99;
			item.rare = ItemRarityID.Blue;
		}

		public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI) {
			DestinyPlayer dPlayer = Main.LocalPlayer.GetModPlayer<DestinyPlayer>();
			Texture2D texture = mod.GetTexture("Items/Laurel");
			if (dPlayer.warlock) {
				texture = mod.GetTexture("Items/WarlockLaurel");
			}
			else if (dPlayer.hunter) {
				texture = mod.GetTexture("Items/HunterLaurel");
			}
			Vector2 position = item.position - Main.screenPosition + new Vector2(item.width / 2, item.height - texture.Height * 0.5f + 2f);
			spriteBatch.Draw(texture, position, null, lightColor, rotation, texture.Size(), scale, SpriteEffects.None, 0f);
			return false;
		}

		public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale) {
			DestinyPlayer dPlayer = Main.LocalPlayer.GetModPlayer<DestinyPlayer>();
			Texture2D texture = mod.GetTexture("Items/Laurel");
			if (dPlayer.warlock) {
				texture = mod.GetTexture("Items/WarlockLaurel");
			}
			else if (dPlayer.hunter) {
				texture = mod.GetTexture("Items/HunterLaurel");
			}
			spriteBatch.Draw(texture, position, null, drawColor, 0, origin, scale, SpriteEffects.None, 0f);
			return false;
		}

		public override bool CanBurnInLava() => true;
	}
}