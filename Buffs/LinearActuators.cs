using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;

namespace TheDestinyMod.Buffs
{
    public class LinearActuators : ModBuff
    {
        public override void SetDefaults() {
            DisplayName.SetDefault("Linear Actuators");
            Description.SetDefault("300% increased damage for next melee strike");
            Main.buffNoTimeDisplay[Type] = true;
            Main.buffNoSave[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex) {
            player.DestinyPlayer().linearActuators = true;
            player.buffTime[buffIndex] = 18000;
        }
    }
}