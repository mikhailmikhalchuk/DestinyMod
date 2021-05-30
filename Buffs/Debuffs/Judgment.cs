using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheDestinyMod.Buffs.Debuffs
{
    public class Judgment : ModBuff
    {
        public override void SetDefaults() {
            DisplayName.SetDefault("Judgment");
            Description.SetDefault("You are weaker");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex) {
            player.allDamage -= (int)0.1;
        }
    }
}