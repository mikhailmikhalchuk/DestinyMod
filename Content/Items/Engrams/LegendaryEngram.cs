using Microsoft.Xna.Framework;
using Terraria;

namespace DestinyMod.Content.Items.Engrams
{
	public class LegendaryEngram : Engram
	{
		public override void DestinySetDefaults() => Item.value = Item.buyPrice(gold: 10);

		public override void PostUpdate() => Lighting.AddLight(Item.Center, Color.Pink.ToVector3() * 0.55f * Main.essScale);
	}
}