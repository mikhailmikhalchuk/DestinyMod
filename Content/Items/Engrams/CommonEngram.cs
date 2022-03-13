using Microsoft.Xna.Framework;
using Terraria;

namespace DestinyMod.Content.Items.Engrams
{
	public class CommonEngram : Engram
	{
		public override void DestinySetDefaults()
		{
			Item.value = Item.buyPrice(silver: 1);
		}

		public override void PostUpdate()
		{
			Lighting.AddLight(Item.Center, Color.WhiteSmoke.ToVector3() * Main.essScale);
		}
	}
}