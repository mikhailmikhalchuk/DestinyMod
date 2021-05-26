using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;

namespace TheDestinyMod.Buffs
{
    public class SunWarrior : ModBuff
    {
        public override void SetDefaults() {
            DisplayName.SetDefault("Sun Warrior");
            Description.SetDefault("Increases throwing speed");
            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = false;
        }

        public override void Update(Player player, ref int buffIndex) {
            player.thrownVelocity *= 0.5f;
        }
    }
}