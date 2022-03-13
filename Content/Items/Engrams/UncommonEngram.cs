using Microsoft.Xna.Framework;
using Terraria;

namespace DestinyMod.Content.Items.Engrams
{
	public class UncommonEngram : Engram
	{
        public override void DestinySetDefaults()
        {
            Item.value = Item.buyPrice(silver: 10);
        }

        public override void PostUpdate()
        {
            Lighting.AddLight(Item.Center, Color.LightGreen.ToVector3() * Main.essScale);
        }
    }
}