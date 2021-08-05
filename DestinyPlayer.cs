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
		public int overchargeStacks;
		public int aegisCharge;
		public int destinyWeaponDelay;
		public int superCrit;
		public int borealisCooldown;

		public float businessReduceUse = 0.2f;
		public float thunderlordReduceUse = 1f;
		public float superDamageAdd;
		public float superDamageMult = 1f;
		public float superKnockback;
		
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
		private int countThunderlord = 0;

		public override void ResetEffects() {
			ResetVariables();
        }

        public override void UpdateDead() {
			ResetVariables();
        }

        private void ResetVariables() {
			ghostPet = false;
			ancientShard = false;
			servitorMinion = false;
			boughtCommon = false;
			hunter = false;
			titan = false;
			warlock = false;
			exoticEquipped = false;
			superDamageAdd = 0f;
			superDamageMult = 1f;
			superCrit = 0;
			superKnockback = 0;
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

        public override void PostUpdate() {
			countThunderlord++;
			if (countThunderlord >= 30 && player.channel && player.HeldItem.type == ModContent.ItemType<Items.Weapons.Ranged.Thunderlord>() && thunderlordReduceUse < 1.5f) {
				countThunderlord = 0;
				thunderlordReduceUse += 0.05f;
			}
			if (destinyWeaponDelay > 0) {
				destinyWeaponDelay--;
			}
			if (borealisCooldown > 0) {
				borealisCooldown--;
			}
			if (ModContent.GetInstance<TheDestinyMod>().CryptarchUserInterface.CurrentState == null && CryptarchUI._vanillaItemSlot?.Item.type > 0) {
				CryptarchUI._vanillaItemSlot.Item.position = player.Center;
				Item item = player.GetItem(player.whoAmI, CryptarchUI._vanillaItemSlot.Item, noText: true);
				if (item.stack > 0) {
					int placed = Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, item.type, item.stack, false, CryptarchUI._vanillaItemSlot.Item.prefix, true);
					Main.item[placed] = item.Clone();
					Main.item[placed].newAndShiny = false;
				}
				CryptarchUI._vanillaItemSlot.Item = new Item();
			}
        }

        public override void PostUpdateRunSpeeds() {
			if (player.channel && player.HeldItem.type == ModContent.ItemType<Items.Weapons.Magic.TheAegis>()) {
				player.maxRunSpeed /= 2;
				player.accRunSpeed /= 2;
				player.dashDelay = 10;
				player.controlJump = false;
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
				countThunderlord = 0;
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
				businessReduceUse = 0.2f;
				thunderlordReduceUse = 1f;
			}
			if (PlayerInput.Triggers.JustPressed.QuickHeal) {
				bool result = Enter("TheDestinyMod_Vault of Glass") ?? false;
				if (!result && ModLoader.GetMod("StructureHelper") != null && ModLoader.GetMod("SubworldLibrary") != null)
					Main.NewText($"Something went wrong while trying to enter the raid: {TheDestinyMod.currentSubworldID.Substring(14)}.", new Color(255, 0, 0));
			}
			if (PlayerInput.Triggers.JustPressed.QuickBuff) {
				bool result = Exit() ?? false;
				if (!result)
					Main.NewText($"Something went wrong while trying to exit the raid: {TheDestinyMod.currentSubworldID.Substring(14)}.", new Color(255, 0, 0));
			}
        }

        public override void UpdateBadLifeRegen() {
			if (player.HasBuff(ModContent.BuffType<Buffs.Debuffs.Conducted>())) {
				if (player.lifeRegen > 0) {
					player.lifeRegen = 0;
				}
				player.lifeRegenTime = 0;
				player.lifeRegen -= 15;
			}
		}

        /// <summary>
        /// Used to enter a subworld using SubworldLibrary
        /// </summary>
        /// <param name="id">The subworld ID</param>
        /// <returns>True if the subworld was succesfully entered, otherwise false. Returns null by default.</returns>
        public static bool? Enter(string id) {
            Mod subworldLibrary = ModLoader.GetMod("SubworldLibrary");
			if (ModLoader.GetMod("StructureHelper") == null || subworldLibrary == null) {
				Main.NewText("You must have the Subworld Library and Structure Helper mods enabled to enter a raid.", Color.Red);
			}
            else {
                Main.mapEnabled = false;
				TheDestinyMod.currentSubworldID = id;
				try {
					subworldLibrary.Call("DrawUnderworldBackground", false);
					return subworldLibrary.Call("Enter", id) as bool?;
				}
				catch (Exception e) {
					TheDestinyMod.Logger.Error($"TheDestinyMod: Got exception of type {e} while trying to enter raid: {TheDestinyMod.currentSubworldID.Substring(14)}.");
                }
            }
            return null;
        }

		/// <summary>
		/// Used to exit a subworld using SubworldLibrary
		/// </summary>
		/// <returns>True if the subworld was succesfully exited, otherwise false. Returns null by default.</returns>
		public static bool? Exit() {
			Mod subworldLibrary = ModLoader.GetMod("SubworldLibrary");
			if (subworldLibrary != null) {
				Main.mapEnabled = true;
				TheDestinyMod.currentSubworldID = string.Empty;
				return subworldLibrary.Call("Exit") as bool?;
			}
			return null;
		}

        public override void PostUpdateEquips() {
			if (TheDestinyMod.currentSubworldID != string.Empty) {
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

		public override void ModifyDrawInfo(ref PlayerDrawInfo drawInfo) {
			if (player.channel && player.HeldItem.type == ModContent.ItemType<Items.Weapons.Magic.TheAegis>()) {
				player.headRotation = 0.3f * player.direction;
			}
		}

		public override void ModifyDrawLayers(List<PlayerLayer> layers) {
			Action<PlayerDrawInfo> layerTarget = s => DrawAegis(s);
			PlayerLayer layer = new PlayerLayer("TheDestinyMod", "Aegis Shield", layerTarget);
			layers.Insert(layers.IndexOf(layers.FirstOrDefault(n => n.Name == "Arms")) + 1, layer);
			if (!Main.gameMenu && (Main.LocalPlayer.HeldItem.type == ModContent.ItemType<Items.Weapons.Magic.TheAegis>() && Main.LocalPlayer.channel || Main.LocalPlayer.GetModPlayer<DestinyPlayer>().aegisCharge > 0)) {
				layers.RemoveAt(layers.IndexOf(layers.FirstOrDefault(n => n.Name == "ShieldAcc")));
			}
		}

		private void DrawAegis(PlayerDrawInfo info) {
			Microsoft.Xna.Framework.Graphics.Texture2D tex = ModContent.GetTexture("TheDestinyMod/Items/Weapons/Magic/TheAegis_Shield");

			if (info.drawPlayer.HeldItem.type == ModContent.ItemType<Items.Weapons.Magic.TheAegis>() && info.drawPlayer.channel || info.drawPlayer.GetModPlayer<DestinyPlayer>().aegisCharge > 0) {
				Main.playerDrawData.Add(
					new DrawData(
						tex,
						info.itemLocation - Main.screenPosition + new Vector2(info.drawPlayer.direction == 1 ? 4 : -4, 20),
						tex.Frame(),
						Lighting.GetColor((int)info.drawPlayer.Center.X / 16, (int)info.drawPlayer.Center.Y / 16),
						info.drawPlayer.GetModPlayer<DestinyPlayer>().aegisCharge > 0 ? 0f : info.drawPlayer.headRotation - (info.drawPlayer.direction == 1 ? 0.1f : -0.1f),
						new Vector2(info.drawPlayer.direction == 1 ? 0 : tex.Frame().Width, tex.Frame().Height),
						info.drawPlayer.HeldItem.scale * 0.8f,
						info.spriteEffects,
						0
					)
				);
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
			if (player.HasBuff(ModContent.BuffType<Buffs.Debuffs.MarkedByVoid>()) && Main.BlackFadeIn < 255 && Main.LocalPlayer == player && !Main.dedServ) {
				Main.BlackFadeIn = markedByVoidTimer;
				markedByVoidDelay--;
				if (markedByVoidDelay <= 0) {
					markedByVoidTimer++;
					markedByVoidDelay = 2;
				}
			}
			if (aegisCharge >= 1 && aegisCharge < 30) {
				player.controlLeft = false;
				player.controlRight = false;
				player.controlUp = false;
				player.controlDown = false;
				player.controlHook = false;
				player.controlJump = false;
			}
		}
    }
}