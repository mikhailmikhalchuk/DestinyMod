using Terraria;
using DestinyMod.Common.Buffs;

namespace DestinyMod.Content.Buffs
{
	public class ParacausalCharge : DestinyModBuff
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Paracausal Charge");
			Description.SetDefault("Hawkmoon damage is doubled");
			Main.buffNoSave[Type] = true;
		}
	}
}