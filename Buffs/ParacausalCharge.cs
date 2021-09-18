using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;

namespace TheDestinyMod.Buffs
{
	public class ParacausalCharge : ModBuff
	{
		public override void SetDefaults() {
			DisplayName.SetDefault("Paracausal Charge");
			DisplayName.AddTranslation(GameCulture.Polish, "ładunek Parakasualny");
			Description.SetDefault("Hawkmoon damage is doubled");
			Description.AddTranslation(GameCulture.Polish, "Zapewnia podwójne obrażenia podczas korzystania z Jastrzębiego Księżyca");
            Main.buffNoSave[Type] = true;
		}

        public override void Update(Player player, ref int buffIndex) {
            player.GetModPlayer<DestinyPlayer>().pCharge--;
        }
	}
}