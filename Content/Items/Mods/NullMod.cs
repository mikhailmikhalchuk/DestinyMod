using Terraria;
using DestinyMod.Common.Items.PerksAndMods;

namespace DestinyMod.Content.Items.Mods
{
    public class NullMod : ItemMod
    {
        public override void SetStaticDefaults() => Tooltip.SetDefault("How'd you get this you sneaky so and so?");

        public override void DestinySetDefaults()
        {
            ApplyType = 0;
            Item.maxStack = 1;
        }
    }
}
