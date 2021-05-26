using System;
using System.Linq;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using TheDestinyMod.NPCs.Town;
using TheDestinyMod.Items;
using Terraria.GameInput;
using Terraria.DataStructures;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using TheDestinyMod.UI;

namespace TheDestinyMod
{
	public class DestinyPlayer : ModPlayer
	{
		public int motesGiven;
		public int drifterRewards;
		public int zavalaBounty = 0;
		public int zavalaEnemies = 0;
		public int pCharge;
		public int monteMethod;
		public int superChargeCurrent = 0;
		public int superActiveTime = 0;
		
		public bool ancientShard;
		public bool boughtCommon;
		public bool boughtUncommon;
		public bool boughtRare;
		public bool ghostPet;
		public bool servitorMinion;
		public bool releasedMouseLeft = false;
		public bool notifiedThatSuperIsReady = false;
		public bool titan;
		public bool warlock;
		public bool hunter;
		public bool exoticEquipped;

		public static bool gorgonsHaveSpotted = false;

		internal int superRegenTimer = 0;
		internal int trinaryCounter = -1;
		internal int timesClicked = 0;
		internal int spottedIntensity = 60;

		public override void ResetEffects() {
            ghostPet = false;
			ancientShard = false;
			servitorMinion = false;
			boughtCommon = false;
			hunter = false;
			titan = false;
			warlock = false;
			exoticEquipped = false;
			if (!player.channel) {
				Items.Weapons.Ranged.SweetBusiness.isUsing = false;
			}
        }

		public override void clientClone(ModPlayer clientClone) {
			DestinyPlayer clone = clientClone as DestinyPlayer;
		}

        public override float UseTimeMultiplier(Item item) {
			if (item.type == ModContent.ItemType<Items.Weapons.Supers.HammerOfSol>() && Main.LocalPlayer.HasBuff(ModContent.BuffType<Buffs.SunWarrior>())) {
				return 2f;
			}
            return base.UseTimeMultiplier(item);
        }

        public override void ModifyScreenPosition() {
			if (gorgonsHaveSpotted) {
				Main.screenPosition.X += Main.rand.NextFloat(0, spottedIntensity / 300);
				spottedIntensity++;
			}
        }

        public override void ProcessTriggers(TriggersSet triggersSet) {
			var itemPos = 0;
            if (TheDestinyMod.activateSuper.JustPressed && superChargeCurrent == 100 && !Main.LocalPlayer.dead) {
				foreach (Item item in Main.LocalPlayer.inventory) {
					itemPos++;
					if (itemPos >= 50) {
						break;
					}
					if (item.IsAir) {
						//Main.PlaySound(SoundID.Item74, Main.LocalPlayer.position);
						Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/HammerOfSolActivate"), Main.LocalPlayer.position);
						Projectile.NewProjectile(Main.LocalPlayer.position, new Vector2(0, 0), ProjectileID.StardustGuardianExplosion, 0, 0, Main.LocalPlayer.whoAmI);
						Main.LocalPlayer.QuickSpawnItem(ModContent.ItemType<Items.Weapons.Supers.GoldenGun>(), 1);
						superActiveTime = 600;
						notifiedThatSuperIsReady = false;
						break;
					}
				}
				if (superActiveTime != 600 && !Main.dedServ) {
					Main.NewText(Language.GetTextValue("Mods.TheDestinyMod.SuperInventory"), new Color(255, 0, 0));
				}
			}
			if (PlayerInput.Triggers.JustPressed.MouseLeft) {
				releasedMouseLeft = false;
				if (Main.LocalPlayer.HasBuff(ModContent.BuffType<Buffs.Debuffs.DeepFreeze>())) {
					timesClicked++;
					Main.PlaySound(SoundID.Item50, player.Center);
					if (timesClicked > 4) {
						Main.LocalPlayer.ClearBuff(ModContent.BuffType<Buffs.Debuffs.DeepFreeze>());
						timesClicked = 0;
					}
				}
				if (Main.LocalPlayer.HeldItem.type == ModContent.ItemType<Items.Weapons.Ranged.TrinarySystem>() || Main.mouseItem.type == ModContent.ItemType<Items.Weapons.Ranged.TrinarySystem>()) {
					Items.Weapons.Ranged.TrinarySystem.buffed = true;
					trinaryCounter = 30;
				}
			}
			if (PlayerInput.Triggers.JustReleased.MouseLeft) {
				releasedMouseLeft = true;
			}
			if (PlayerInput.Triggers.JustPressed.QuickHeal) {
				/*bool result = Enter(TheDestinyMod.mySubworldID) ?? false;
				if (!result)
					Main.NewText("Something went wrong, not entering " + TheDestinyMod.mySubworldID);*/
			}
        }

		public static bool? Enter(string id) {
			Mod subworldLibrary = ModLoader.GetMod("SubworldLibrary");
			if (subworldLibrary != null) {
				return subworldLibrary.Call("Enter", id) as bool?;
			}
			return null;
		}

		public override void PostUpdate() {
			if (trinaryCounter > 0) {
				trinaryCounter--;
			}
			else if (trinaryCounter == 0) {
				trinaryCounter = -1;
				Items.Weapons.Ranged.TrinarySystem.buffed = false;
			}
        }

        public override void PostUpdateEquips() {
			Mod subworldLibrary = ModLoader.GetMod("SubworldLibrary");
			if (subworldLibrary != null && (bool)subworldLibrary.Call("IsActive", TheDestinyMod.mySubworldID)) {
				player.noBuilding = true;
			}
		}

        public override TagCompound Save() {
			List<string> engramsPurchased = new List<string>();
			if (boughtCommon) {
				engramsPurchased.Add("common");
			}
			if (boughtUncommon) {
				engramsPurchased.Add("uncommon");
			}
			if (boughtRare) {
				engramsPurchased.Add("rare");
			}
			return new TagCompound {
				{"motesGiven", motesGiven},
				{"drifterRewards", drifterRewards},
				{"zavalaBounty", zavalaBounty},
				{"zavalaEnemies", zavalaEnemies},
				{"engramsPurchased", engramsPurchased},
				{"superChargeCurrent", superChargeCurrent},
				{"superActiveTime", superActiveTime},
				{"subclassTier", DestinyUI.selectedWhich}
			};
		}

        public override bool ShiftClickSlot(Item[] inventory, int context, int slot) {
			if ((Main.LocalPlayer.inventory[slot].type == ModContent.ItemType<CommonEngram>() || Main.LocalPlayer.inventory[slot].type == ModContent.ItemType<UncommonEngram>() || Main.LocalPlayer.inventory[slot].type == ModContent.ItemType<RareEngram>() || Main.LocalPlayer.inventory[slot].type == ModContent.ItemType<LegendaryEngram>() || Main.LocalPlayer.inventory[slot].type == ModContent.ItemType<ExoticEngram>()) && ModContent.GetInstance<TheDestinyMod>().CryptarchUserInterface.CurrentState != null) {
				if (CryptarchUI._vanillaItemSlot.Item.type == ItemID.None) {
					Item clone = Main.LocalPlayer.inventory[slot].Clone();
					CryptarchUI._vanillaItemSlot.Item = clone;
					Main.LocalPlayer.inventory[slot].TurnToAir();
					Main.PlaySound(SoundID.Grab);
					return true;
				}
			}
            return false;
        }

        public override void Load(TagCompound tag) {
			var engrams = tag.GetList<string>("engramsPurchased");
			boughtCommon = engrams.Contains("common");
			boughtUncommon = engrams.Contains("uncommon");
			boughtRare = engrams.Contains("rare");
			motesGiven = tag.GetInt("motesGiven");
			drifterRewards = tag.GetInt("drifterRewards");
			zavalaBounty = tag.GetInt("zavalaBounty");
			zavalaEnemies = tag.GetInt("zavalaEnemies");
			superChargeCurrent = tag.GetInt("superChargeCurrent");
			superActiveTime = tag.GetInt("superActiveTime");
			if (tag.ContainsKey("subclassTier")) {
				DestinyUI.selectedWhich = tag.GetInt("subclassTier");
			}
		}

        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit) {
			if (target.TypeName == "Zombie" && zavalaBounty == 1 && zavalaEnemies < 100 && target.life <= 1) {
				zavalaEnemies++;
			}
			if (target.TypeName == "Skeleton" && zavalaBounty == 3 && zavalaEnemies < 50 && target.life <= 1) {
				zavalaEnemies++;
			}
		}

        public override void PostBuyItem(NPC vendor, Item[] shopInventory, Item item) {
			if (vendor.type == ModContent.NPCType<Cryptarch>() && item.type == ModContent.ItemType<CommonEngram>()) {
				shopInventory[0].TurnToAir();
				boughtCommon = true;
			}
			else if (vendor.type == ModContent.NPCType<Cryptarch>() && item.type == ModContent.ItemType<UncommonEngram>()) {
				shopInventory[1].TurnToAir();
				boughtUncommon = true;
			}
			else if (vendor.type == ModContent.NPCType<Cryptarch>() && item.type == ModContent.ItemType<RareEngram>()) {
				shopInventory[2].TurnToAir();
				boughtRare = true;
			}
        }

        public override void PostSellItem(NPC vendor, Item[] shopInventory, Item item) {
			if (vendor.type == ModContent.NPCType<Cryptarch>() && item.type == ModContent.ItemType<CommonEngram>()) {
				boughtCommon = false;
			}
			else if (vendor.type == ModContent.NPCType<Cryptarch>() && item.type == ModContent.ItemType<UncommonEngram>()) {
				boughtUncommon = false;
			}
			else if (vendor.type == ModContent.NPCType<Cryptarch>() && item.type == ModContent.ItemType<RareEngram>()) {
				boughtRare = false;
			}
        }

        public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource) {
			if (superActiveTime > 0) {
				damage /= 4;
			}
            return true;
        }

        public override void PostUpdateMiscEffects() {
			if (!notifiedThatSuperIsReady && superChargeCurrent == 100 && !Main.dedServ && DestinyConfig.Instance.notifyOnSuper && superActiveTime == 0) {
				Main.NewText(Language.GetTextValue("Mods.TheDestinyMod.SuperCharge"), new Color(255, 255, 0));
				notifiedThatSuperIsReady = true;
			}
			superRegenTimer++;
            superChargeCurrent = Utils.Clamp(superChargeCurrent, 0, 100);
			if (superRegenTimer > 360) {
				superChargeCurrent++;
				superRegenTimer = 0;
			}
			if (superActiveTime > 0) {
				superActiveTime--;
				superChargeCurrent = (int)Math.Ceiling((double)superActiveTime / 60 * 10);
			}
			if (superActiveTime <= 0) {
				foreach (Item item in player.inventory) {
					if (item.type == ModContent.ItemType<Items.Weapons.Supers.GoldenGun>()) {
						item.TurnToAir();
					}
				}
				if (Main.mouseItem.type == ModContent.ItemType<Items.Weapons.Supers.GoldenGun>()) {
					Main.mouseItem.TurnToAir();
				}
			}
        }
	}
}