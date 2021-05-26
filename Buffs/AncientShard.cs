using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;

namespace TheDestinyMod.Buffs
{
    public class AncientShard : ModBuff
    {
        public override void SetDefaults() {
            DisplayName.SetDefault("Ancient Shard");
            Description.SetDefault("Increases the chance of finding Motes from enemies");
            Description.AddTranslation(GameCulture.Polish, "Zwiększa szanse na znalezienie okruchów z przeciwników");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false;
        }

        public override void Update(Player player, ref int buffIndex) {
            DestinyPlayer dPlayer = player.GetModPlayer<DestinyPlayer>();
            dPlayer.ancientShard = true;
        }
    }
}