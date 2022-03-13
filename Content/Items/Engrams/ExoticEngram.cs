using Microsoft.Xna.Framework;
using Terraria;

namespace DestinyMod.Content.Items.Engrams
{
	public class ExoticEngram : Engram
	{
		public override void DestinySetDefaults()
		{
			Item.value = Item.buyPrice(platinum: 1);
		}

		public override void PostUpdate()
		{
			Lighting.AddLight(Item.Center, Color.Yellow.ToVector3() * Main.essScale);
		}
	}
}