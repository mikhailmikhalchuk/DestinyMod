using Terraria.ModLoader;

namespace DestinyMod.Common.ModPlayers
{
	public class SuperPlayer : ModPlayer
	{
		public int SuperChargeCurrent;

		public int SuperActiveTime;

		public float SuperDamageFlat;

		public float SuperDamageMultiplier = 1f;

		public float SuperKnockback;

		public int SuperCrit = 4;

		public int OrbOfPowerAdd = 0;

		public override void ResetEffects()
		{
			SuperDamageFlat = 0f;
			SuperDamageMultiplier = 1f;
			SuperKnockback = 0;
			SuperCrit = 4;
			OrbOfPowerAdd = 0;
		}
	}
}