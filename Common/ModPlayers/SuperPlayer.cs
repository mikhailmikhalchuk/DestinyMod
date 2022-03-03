using DestinyMod.Content.Items.Weapons.Super;
using DestinyMod.Content.Load;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace DestinyMod.Common.ModPlayers
{
	public class SuperPlayer : ModPlayer
	{
		public int SuperChargeCurrent;

		public int SuperActiveTime;

		public bool NotifiedThatSuperIsReady;

		public int SuperRegenTimer;

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

		public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
		{
			if (SuperActiveTime > 0)
			{
				damage /= 4;
			}

			return true;
		}

		public override void ProcessTriggers(TriggersSet triggersSet)
		{
			if (HotKeys.ActiveSuper.JustPressed && SuperChargeCurrent == 100 && !Player.dead)
			{
				SuperActiveTime = 600;
				NotifiedThatSuperIsReady = false;
				bool PlaceSuperInventory(int superItem)
				{
					for (int itemSlot = 0; itemSlot < Player.inventory.Length - 1; itemSlot++)
					{
						Item item = Player.inventory[itemSlot];
						if (item.IsAir)
						{
							Projectile.NewProjectile(Player.GetProjectileSource_Misc(0), Player.position, Vector2.Zero, ProjectileID.StardustGuardianExplosion, 0, 0, Player.whoAmI);
							Player.QuickSpawnItem(Player.GetItemSource_Misc(superItem), superItem);
							return true;
						}
					}
					return false;
				}

				PlaceSuperInventory(ModContent.ItemType<Dawnblade>());
			}
		}

		public override void PostUpdateMiscEffects()
		{
			if (!NotifiedThatSuperIsReady && SuperChargeCurrent == 100 && !Main.dedServ && DestinyClientConfig.Instance.NotifyOnSuper && SuperActiveTime == 0 && !Player.dead)
			{
				Main.NewText(Language.GetTextValue("Mods.DestinyMod.Common.SuperCharge"), new Color(255, 255, 0));
				NotifiedThatSuperIsReady = true;
			}

			// SuperRegenTimer++;
			SuperChargeCurrent = Utils.Clamp(SuperChargeCurrent, 0, 100);
			if (SuperRegenTimer > 360)
			{
				SuperChargeCurrent++;
				SuperRegenTimer = 0;
			}

			if (SuperActiveTime > 0)
			{
				SuperActiveTime--;
				SuperChargeCurrent = (int)Math.Ceiling((double)SuperActiveTime / 60 * 10);
			}

			if (SuperActiveTime <= 0)
			{
				foreach (Item item in Player.inventory)
				{
					if (item.type == ModContent.ItemType<GoldenGun>())
					{
						item.TurnToAir();
					}
				}

				if (Main.mouseItem.type == ModContent.ItemType<GoldenGun>())
				{
					Main.mouseItem.TurnToAir();
				}
			}
		}
	}
}