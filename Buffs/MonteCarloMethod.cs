using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;

namespace TheDestinyMod.Buffs
{
	public class MonteCarloMethod : ModBuff
	{
		public override void SetDefaults() {
			DisplayName.SetDefault("Monte Carlo Method");
			DisplayName.AddTranslation(GameCulture.Polish, "Metoda Monte Carlo");
			Description.SetDefault("Grants increased damage to melee weapons");
			Description.AddTranslation(GameCulture.Polish, "Zwiększone obrażenia dla broni białej");
            Main.buffNoSave[Type] = true;
		}

        public override void Update(Player player, ref int buffIndex) {
            player.meleeDamageMult = 1.25f;
            DestinyPlayer dPlayer = player.GetModPlayer<DestinyPlayer>();
			dPlayer.monteMethod--;
        }
	}
}