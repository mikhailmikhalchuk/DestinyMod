using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace DestinyMod.Common.ModPlayers
{
	public class NecroticPlayer : ModPlayer
	{
		public Player NecroticApplier;

		public float NecroticDamageMult;

		public bool NecroticRot;

		public override void ResetEffects()
		{
			NecroticRot = false;
		}

		public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
		{
			if (NecroticRot && damage == 10 && hitDirection == 0 && damageSource.SourceOtherIndex == 8)
			{
				damageSource = PlayerDeathReason.ByCustomReason(Player.name + "'s partial necrosis became complete.");
			}

			return base.PreKill(damage, hitDirection, pvp, ref playSound, ref genGore, ref damageSource);
		}

		public override void UpdateBadLifeRegen()
		{
			void ApplyDebuff(int damage)
			{
				if (Player.lifeRegen > 0)
				{
					Player.lifeRegen = 0;
				}

				Player.lifeRegenTime = 0;
				Player.lifeRegen -= damage;
			}

			if (NecroticApplier != null)
			{
				ApplyDebuff(40 + (int)(40 * NecroticApplier.GetModPlayer<NecroticPlayer>().NecroticDamageMult));
			}
		}
	}
}