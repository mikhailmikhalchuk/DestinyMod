using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;

namespace TheDestinyMod.Buffs
{
    public class HakkeBuff : ModBuff
    {
        public override void SetDefaults() {
            DisplayName.SetDefault("Hakke Craftsmanship");
            Description.SetDefault("Minor improvements to defense, movement speed, and ranged damage");
            Description.AddTranslation(GameCulture.Polish, "Niewielkie ulepszenie obrony, szybkości ruchu i obrażeń zasięgowych");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false;
        }

        public override void Update(Player player, ref int buffIndex) {
            player.statDefense += 2;
            player.moveSpeed += 0.15f;
            player.rangedDamage += 0.1f;
        }
    }
}