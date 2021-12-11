using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;

namespace TheDestinyMod.Buffs
{
    public class MarkOfTheDevourer : ModBuff
    {
        public override void SetDefaults() {
            DisplayName.SetDefault("Mark of the Devourer");
            Description.SetDefault("Thorn poison damage increased by ");
            Main.buffNoSave[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex) {
            if (player.buffTime[buffIndex] <= 1) {
                player.DestinyPlayer().necroticDamageMult = 0f;
            }
        }

        public override void ModifyBuffTip(ref string tip, ref int rare) {
            tip += $"{Main.LocalPlayer.DestinyPlayer().necroticDamageMult * 100}%";
        }
    }
}