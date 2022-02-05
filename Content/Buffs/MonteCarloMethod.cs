using Terraria;
using DestinyMod.Common.Buffs;
using Terraria.ModLoader;

namespace DestinyMod.Content.Buffs
{
	public class MonteCarloMethod : DestinyModBuff
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Monte Carlo Method");
			Description.SetDefault("25% increased melee damage");
			Main.buffNoSave[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex) => player.GetDamage(DamageClass.Melee) *= 1.25f;
	}
}