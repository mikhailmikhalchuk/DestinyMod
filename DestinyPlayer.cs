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
using Terraria.Graphics.Effects;

namespace TheDestinyMod
{
	public class DestinyPlayer : ModPlayer
	{
		public int motesGiven;
		public int drifterRewards;
		public int zavalaBounty;
		public int zavalaEnemies;
		public int pCharge;
		public int monteMethod;
		public int superChargeCurrent;
		public int superActiveTime;
		public int markedByVoidTimer;
		public int markedByVoidDelay;
		
		public bool ancientShard;
		public bool boughtCommon;
		public bool boughtUncommon;
		public bool boughtRare;
		public bool ghostPet;
		public bool servitorMinion;
		public bool releasedMouseLeft;
		public bool notifiedThatSuperIsReady;
		public bool titan;
		public bool warlock;
		public bool hunter;
		public bool exoticEquipped;

		public static bool gorgonsHaveSpotted;

		private int superRegenTimer = 0;
		private int timesClicked = 0;
		private int spottedIntensity = 60;

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
			if (item.type == ModContent.ItemType<Items.Weapons.Supers.HammerOfSol>() && player.HasBuff(ModContent.BuffType<Buffs.SunWarrior>())) {
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
            if (TheDestinyMod.activateSuper.JustPressed && superChargeCurrent == 100 && !player.dead) {
				foreach (Item item in Main.LocalPlayer.inventory) {
					itemPos++;
					if (itemPos >= 50) {
						break;
					}
					if (item.IsAir) {
						//Main.PlaySound(SoundID.Item74, Main.LocalPlayer.position);
						Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/HammerOfSolActivate"), player.position);
						Projectile.NewProjectile(Main.LocalPlayer.position, new Vector2(0, 0), ProjectileID.StardustGuardianExplosion, 0, 0, Main.LocalPlayer.whoAmI);
						player.QuickSpawnItem(ModContent.ItemType<Items.Weapons.Supers.GoldenGun>(), 1);
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
				if (player.HasBuff(ModContent.BuffType<Buffs.Debuffs.DeepFreeze>())) {
					timesClicked++;
					Main.PlaySound(SoundID.Item50, player.Center);
					if (timesClicked > 4) {
						player.ClearBuff(ModContent.BuffType<Buffs.Debuffs.DeepFreeze>());
						timesClicked = 0;
					}
				}
			}
			if (PlayerInput.Triggers.JustReleased.MouseLeft) {
				releasedMouseLeft = true;
			}
			if (PlayerInput.Triggers.JustPressed.QuickHeal) {
				player.AddBuff(ModContent.BuffType<Buffs.Debuffs.MarkedByVoid>(), 2);
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
				{"subclassTier", SubclassUI.selectedWhich}
			};
		}

        public override bool ShiftClickSlot(Item[] inventory, int context, int slot) {
			if ((player.inventory[slot].type == ModContent.ItemType<CommonEngram>() || player.inventory[slot].type == ModContent.ItemType<UncommonEngram>() || player.inventory[slot].type == ModContent.ItemType<RareEngram>() || player.inventory[slot].type == ModContent.ItemType<LegendaryEngram>() || player.inventory[slot].type == ModContent.ItemType<ExoticEngram>()) && ModContent.GetInstance<TheDestinyMod>().CryptarchUserInterface.CurrentState != null) {
				if (CryptarchUI._vanillaItemSlot.Item.type == ItemID.None) {
					Item clone = player.inventory[slot].Clone();
					CryptarchUI._vanillaItemSlot.Item = clone;
					player.inventory[slot].TurnToAir();
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
				SubclassUI.selectedWhich = tag.GetInt("subclassTier");
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
			if (!notifiedThatSuperIsReady && superChargeCurrent == 100 && !Main.dedServ && DestinyConfig.Instance.notifyOnSuper && superActiveTime == 0 && !player.dead) {
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
			if (player.HasBuff(ModContent.BuffType<Buffs.Debuffs.MarkedByVoid>()) && Main.BlackFadeIn < 255 && Main.LocalPlayer == player) {
				Main.BlackFadeIn = markedByVoidTimer;
				markedByVoidDelay--;
				if (markedByVoidDelay <= 0) {
					markedByVoidTimer++;
					markedByVoidDelay = 2;
				}
			}
		}
    }
}