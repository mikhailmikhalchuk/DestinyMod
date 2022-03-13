using Microsoft.Xna.Framework;
using Terraria;

namespace DestinyMod.Content.Items.Engrams
{
	public class RareEngram : Engram
	{
		public override void DestinySetDefaults()
		{
			Item.value = Item.buyPrice(gold: 1);
		}

		public override void PostUpdate()
		{
			Lighting.AddLight(Item.Center, Color.LightBlue.ToVector3() * Main.essScale);
		}
	}
}