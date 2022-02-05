using Terraria;
using DestinyMod.Common.Buffs;
using DestinyMod.Content.Items.Weapons.Super;
using Terraria.ModLoader;

namespace DestinyMod.Content.Buffs
{
    public class SunWarrior : DestinyModBuff
    {
        public override void SetStaticDefaults() 
        {
            DisplayName.SetDefault("Sun Warrior");
            Description.SetDefault("Increases throwing speed");
            Main.buffNoTimeDisplay[Type] = true;
        }

		public override float UseTimeMultiplier(Player player, Item item) => item.type == ModContent.ItemType<HammerOfSol>() ? 2f : 1f;
	}
}