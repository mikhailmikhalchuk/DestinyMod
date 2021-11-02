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
		public int blackFadeInTimer;
		public int markedByVoidDelay;
		public int overchargeStacks;
		public int aegisCharge;
		public int destinyWeaponDelay;
		public int superCrit = 4;
		public int orbOfPowerAdd = 0;
		public int borealisCooldown;
		public int duneRunTime;
		public int lamentRevUp;

		public float businessReduceUse = 0.2f;
		public float thunderlordReduceUse = 1f;
		public float superDamageAdd;
		public float superDamageMult = 1f;
		public float superKnockback;
		public float necroticDamageMult;
		
		public bool ancientShard;
		public bool boughtCommon;
		public bool boughtUncommon;
		public bool boughtRare;
		public bool ghostPet;
		public bool servitorMinion;
		public bool mouseLeftDown;
		public bool notifiedThatSuperIsReady;
		public DestinyClassType classType;
		public bool exoticEquipped;
		public bool overcharged;
		public bool hakkeCraftsmanship;
		public bool linearActuators;
		public bool conducted;
		public bool stasisFrozen;
		public bool detained;
		public bool markedByVoid;
		public bool markedForNegation;
		public bool judged;
		public bool sunWarrior;
		public bool paracausalCharge;
		public bool necroticRot;

		public Player necroticApplier;

		public List<int> commonItemsDecrypted = new List<int>();
		public List<int> uncommonItemsDecrypted = new List<int>();
		public List<int> rareItemsDecrypted = new List<int>();

		public static bool gorgonsHaveSpotted;
		public static DestinyClassType classAwaitingAssign;

		private int superRegenTimer = 0;
		private int timesClicked = 0;
		private int spottedIntensity = 60;
		private int countThunderlord = 0;
		public bool isThundercrash = false;
		private bool shouldBeThundercrashed = false;
		private DestinyDamageType elementAwaitingAssign;
		private List<int> fragmentsAwaitingAssign;
		private List<int> abilitiesAwaitingAssign;

		public override void ResetEffects() {
			ResetVariables();
        }

        public override void UpdateDead() {
			ResetVariables();
        }

        public override void Initialize() {
			classType = DestinyClassType.None;
        }

        private void ResetVariables() {
			ghostPet = false;
			ancientShard = false;
			servitorMinion = false;
			boughtCommon = false;
			exoticEquipped = false;
			overcharged = false;
			hakkeCraftsmanship = false;
			linearActuators = false;
			conducted = false;
			stasisFrozen = false;
			detained = false;
			markedByVoid = false;
			markedForNegation = false;
			judged = false;
			sunWarrior = false;
			paracausalCharge = false;
			necroticRot = false;
			superDamageAdd = 0f;
			superDamageMult = 1f;
			superCrit = 4;
			superKnockback = 0;
			orbOfPowerAdd = 0;
		}

        public override float UseTimeMultiplier(Item item) {
			if (item.type == ModContent.ItemType<Items.Weapons.Supers.HammerOfSol>() && sunWarrior) {
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

        public override void ModifyHitNPC(Item item, NPC target, ref int damage, ref float knockback, ref bool crit) {
			if (item.melee && linearActuators) {
				damage *= 4;
				player.ClearBuff(ModContent.BuffType<Buffs.LinearActuators>());
			}
		}

        public override void ModifyHitPvp(Item item, Player target, ref int damage, ref bool crit) {
			if (item.melee && linearActuators) {
				damage *= 4;
				player.ClearBuff(ModContent.BuffType<Buffs.LinearActuators>());
			}
		}

        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection) {
			if (proj.melee && linearActuators) {
				damage *= 4;
				player.ClearBuff(ModContent.BuffType<Buffs.LinearActuators>());
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
			if (player.channel && player.HeldItem.type == ModContent.ItemType<Items.Weapons.Magic.TheAegis>() || Main.mouseRight && player.HeldItem.type == ModContent.ItemType<Items.Weapons.Melee.TheLament>() && lamentRevUp <= 90) {
				player.maxRunSpeed /= 2;
				player.accRunSpeed /= 2;
				player.dashDelay = 10;
				player.controlJump = false;
			}
		}

        public override void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource) {
			for (int i = 0; i < Main.maxItems; i++) {
				if (Main.item[i].modItem is Items.Buffers.OrbOfPower orb) {
					if (orb.OrbOwner == player) {
						Main.item[i].active = false;
					}
				}
			}
        }

        public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource) {
			if (conducted && damage == 10 && hitDirection == 0 && damageSource.SourceOtherIndex == 8) {
				damageSource = PlayerDeathReason.ByCustomReason(player.name + " energy was dispersed.");
			}
			if (necroticRot && damage == 10 && hitDirection == 0 && damageSource.SourceOtherIndex == 8) {
				damageSource = PlayerDeathReason.ByCustomReason(player.name + "'s partial necrosis became complete.");
			}
			return base.PreKill(damage, hitDirection, pvp, ref playSound, ref genGore, ref damageSource);
        }

        public override void ProcessTriggers(TriggersSet triggersSet) {
            if (TheDestinyMod.activateSuper.JustPressed && superChargeCurrent == 100 && !player.dead) {
				superActiveTime = 600;
				notifiedThatSuperIsReady = false;
				bool PlaceSuperInventory(int superItem) {
					var itemPos = 0;
					foreach (Item item in player.inventory) {
						itemPos++;
						if (itemPos >= 50) {
							return false;
						}
						if (item.IsAir) {
							//Main.PlaySound(SoundID.Item74, Main.LocalPlayer.position);
							Projectile.NewProjectile(player.position, new Vector2(0, 0), ProjectileID.StardustGuardianExplosion, 0, 0, player.whoAmI);
							player.QuickSpawnItem(superItem, 1);
							return true;
						}
					}
					return false;
				}
				PlaceSuperInventory(ModContent.ItemType<Items.Weapons.Supers.Dawnblade>());
				/*switch (TheDestinyMod.Instance.SubclassUI.element) {
					case DestinyDamageType.Arc when classType == DestinyClassType.Titan:
						Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/HammerOfSolActivate"), player.position);
						Projectile.NewProjectile(player.position, new Vector2(0, 0), ProjectileID.StardustGuardianExplosion, 0, 0, player.whoAmI);
						if (player.mount.Active) {
							player.mount.Dismount(player);
						}
						isThundercrash = true;
						break;
					case DestinyDamageType.Arc when classType == DestinyClassType.Hunter:
					case DestinyDamageType.Solar when classType == DestinyClassType.Hunter:
						if (!PlaceSuperInventory(ModContent.ItemType<Items.Weapons.Supers.GoldenGun>())) {
							Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/GoldenGunActivate"), player.position);
							Main.NewText(Language.GetTextValue("Mods.TheDestinyMod.SuperInventory"), new Color(255, 0, 0));
						}
						break;
					case DestinyDamageType.Solar when classType == DestinyClassType.Titan:
						if (!PlaceSuperInventory(ModContent.ItemType<Items.Weapons.Supers.HammerOfSol>())) {
							Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/HammerOfSolActivate"), player.position);
							Main.NewText(Language.GetTextValue("Mods.TheDestinyMod.SuperInventory"), new Color(255, 0, 0));
						}
						break;
					default:
						Main.NewText("Either a valid subclass is not equipped or something went wrong on our end! Let a developer know of your plight", Color.Red);
						superActiveTime = 0;
						notifiedThatSuperIsReady = true;
						break;
				}*/
			}
			if (PlayerInput.Triggers.JustPressed.MouseLeft) {
				countThunderlord = 0;
				mouseLeftDown = true;
				if (stasisFrozen) {
					timesClicked++;
					Main.PlaySound(SoundID.Item50, player.Center);
					if (timesClicked > 4) {
						player.ClearBuff(ModContent.BuffType<Buffs.Debuffs.DeepFreeze>());
						timesClicked = 0;
					}
				}
			}
			if (Main.mouseRight && player.HeldItem.type == ModContent.ItemType<Items.Weapons.Melee.TheLament>() && lamentRevUp <= 90) {
				lamentRevUp++;
				if (lamentRevUp % 5 == 0)
					Main.PlaySound(SoundID.Item22);
			}
			if (!Main.mouseRight && lamentRevUp <= 90) {
				lamentRevUp = 0;
			}
			if (lamentRevUp > 90) {
				lamentRevUp++;
				if (lamentRevUp > 300) {
					lamentRevUp = 0;
				}
			}
			if (lamentRevUp == 92) {
				Main.PlaySound(SoundID.Item23);
			}
			if (PlayerInput.Triggers.JustReleased.MouseLeft) {
				mouseLeftDown = false;
				businessReduceUse = 0.2f;
				thunderlordReduceUse = 1f;
			}
			if (PlayerInput.Triggers.JustPressed.QuickBuff) {
				superChargeCurrent = 100;
				player.AddBuff(ModContent.BuffType<Buffs.Debuffs.NecroticRot>(), 120);
			}
        }

        public override void UpdateBadLifeRegen() { // lifeRegen deducts 1/2 of the value you give it!! (passing 8 as damage deducts 4 life per second)
			void ApplyDebuff(bool debuff, int damage) {
				if (debuff) {
					if (player.lifeRegen > 0) {
						player.lifeRegen = 0;
					}
					player.lifeRegenTime = 0;
					player.lifeRegen -= damage;
				}
			}
			ApplyDebuff(conducted, 4);
			if (necroticApplier != null) {
				ApplyDebuff(necroticRot, 40 + (int)(40 * necroticApplier.GetModPlayer<DestinyPlayer>().necroticDamageMult));
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
				TheDestinyMod.currentSubworldID = id;
				ModContent.GetInstance<TheDestinyMod>().raidInterface.SetState(null);
				try {
					subworldLibrary.Call("DrawUnderworldBackground", false);
					return subworldLibrary.Call("Enter", id) as bool?;
				}
				catch (Exception e) {
					TheDestinyMod.Instance.Logger.Error($"TheDestinyMod: Got exception of type {e} while trying to enter raid: {TheDestinyMod.currentSubworldID.Substring(14)}.");
                }
            }
            return null;
        }

		/// <summary>
		/// Used to exit a subworld using SubworldLibrary
		/// </summary>
		/// <returns>True if the subworld was successfully exited, otherwise false. Returns null by default.</returns>
		public static bool? Exit() {
			Mod subworldLibrary = ModLoader.GetMod("SubworldLibrary");
            return subworldLibrary?.Call("Exit") as bool?;
        }

        public override void PostUpdateEquips() {
			if (TheDestinyMod.currentSubworldID != string.Empty) {
				player.noBuilding = true;
				if (player.mount.Active) {
					player.mount.Dismount(player);
				}
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
			fragmentsAwaitingAssign = new List<int>()
			{
				TheDestinyMod.Instance.SubclassUI.fragmentChoice1,
				TheDestinyMod.Instance.SubclassUI.fragmentChoice2,
				TheDestinyMod.Instance.SubclassUI.fragmentChoice3,
				TheDestinyMod.Instance.SubclassUI.fragmentChoice4
			};
			abilitiesAwaitingAssign = new List<int>()
			{
				TheDestinyMod.Instance.SubclassUI.abilityChoice1,
				TheDestinyMod.Instance.SubclassUI.abilityChoice2,
				TheDestinyMod.Instance.SubclassUI.abilityChoice3,
				TheDestinyMod.Instance.SubclassUI.abilityChoice4
			};
			return new TagCompound {
				{"motesGiven", motesGiven},
				{"drifterRewards", drifterRewards},
				{"zavalaBounty", zavalaBounty},
				{"zavalaEnemies", zavalaEnemies},
				{"engramsPurchased", engramsPurchased},
				{"superChargeCurrent", superChargeCurrent},
				{"superActiveTime", superActiveTime},
				{"commonItemsDecrypted", commonItemsDecrypted},
				{"uncommonItemsDecrypted", uncommonItemsDecrypted},
				{"rareItemsDecrypted", rareItemsDecrypted},
				{"subclassElement", (byte)TheDestinyMod.Instance.SubclassUI.element},
				{"subclassFragments", fragmentsAwaitingAssign},
				{"subclassAbilities", abilitiesAwaitingAssign},
				{"classType", (byte)classType}
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
			if (tag.ContainsKey("commonItemsDecrypted")) {
				commonItemsDecrypted = (List<int>)tag.GetList<int>("commonItemsDecrypted");
				uncommonItemsDecrypted = (List<int>)tag.GetList<int>("uncommonItemsDecrypted");
				rareItemsDecrypted = (List<int>)tag.GetList<int>("rareItemsDecrypted");
			}
			if (tag.ContainsKey("classType")) {
				classType = (DestinyClassType)tag.GetByte("classType");
			}
			if (tag.ContainsKey("subclassSelected")) {
				elementAwaitingAssign = (DestinyDamageType)tag.GetByte("subclassElement");
			}
			if (tag.ContainsKey("subclassFragments")) {
				fragmentsAwaitingAssign = (List<int>)tag.GetList<int>("subclassFragments");
			}
			if (tag.ContainsKey("subclassAbilities")) {
				abilitiesAwaitingAssign = (List<int>)tag.GetList<int>("subclassAbilities");
			}
		}

        public override void OnEnterWorld(Player player) {
			TheDestinyMod.Instance.SubclassUI.element = elementAwaitingAssign;
			if (fragmentsAwaitingAssign != null) {
				TheDestinyMod.Instance.SubclassUI.fragmentChoice1 = fragmentsAwaitingAssign[0];
				TheDestinyMod.Instance.SubclassUI.fragmentChoice2 = fragmentsAwaitingAssign[1];
				TheDestinyMod.Instance.SubclassUI.fragmentChoice3 = fragmentsAwaitingAssign[2];
				TheDestinyMod.Instance.SubclassUI.fragmentChoice4 = fragmentsAwaitingAssign[3];
			}
			if (abilitiesAwaitingAssign != null) {
				TheDestinyMod.Instance.SubclassUI.abilityChoice1 = abilitiesAwaitingAssign[0];
				TheDestinyMod.Instance.SubclassUI.abilityChoice2 = abilitiesAwaitingAssign[1];
				TheDestinyMod.Instance.SubclassUI.abilityChoice3 = abilitiesAwaitingAssign[2];
				TheDestinyMod.Instance.SubclassUI.abilityChoice4 = abilitiesAwaitingAssign[3];
			}
		}

        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit) {
			if (target.TypeName == "Zombie" && zavalaBounty == 1 && zavalaEnemies < 100 && target.life <= 0) {
				zavalaEnemies++;
			}
			if (target.TypeName == "Skeleton" && zavalaBounty == 3 && zavalaEnemies < 50 && target.life <= 0) {
				zavalaEnemies++;
			}
		}

        public override void PostBuyItem(NPC vendor, Item[] shopInventory, Item item) {
			if (vendor.type == ModContent.NPCType<Cryptarch>() && item.type == ModContent.ItemType<CommonEngram>()) {
				shopInventory[0].TurnToAir();
				boughtCommon = true;
			}
			else if (vendor.type == ModContent.NPCType<Cryptarch>() && item.type == ModContent.ItemType<UncommonEngram>()) {
				shopInventory.FirstOrDefault(i => i.type == item.type)?.TurnToAir();
				boughtUncommon = true;
				DestinyWorld.daysPassed = 0;
			}
			else if (vendor.type == ModContent.NPCType<Cryptarch>() && item.type == ModContent.ItemType<RareEngram>()) {
				shopInventory.FirstOrDefault(i => i.type == item.type)?.TurnToAir();
				boughtRare = true;
				DestinyWorld.daysPassed = 0;
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
			if (Main.menuMode == 2) {
				classType = classAwaitingAssign;
			}
			player.fullRotationOrigin = player.Hitbox.Size() / 2;
			if (isThundercrash) {
                player.fullRotation = player.fullRotation.AngleLerp(player.velocity.ToRotation() + MathHelper.PiOver2, 0.1f);
				player.bodyFrame.Y = 280;
				player.legFrame.Y = 280;
				player.direction = -1;
				if (player.fullRotation > 0) {
					player.direction = 1;
				}
				player.headRotation = -0.5f * player.direction;
			}
			if (!isThundercrash && shouldBeThundercrashed) {
				player.fullRotation = player.fullRotation.AngleLerp(0f, 0.5f);
			}
			if (!isThundercrash && shouldBeThundercrashed && player.fullRotation != 0f) {
				shouldBeThundercrashed = true;
			}
			else {
				shouldBeThundercrashed = isThundercrash;
			}
		}

		public override void ModifyDrawLayers(List<PlayerLayer> layers) {
			Action<PlayerDrawInfo> layerTarget = DrawAegis;
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

        public override void UpdateEquips(ref bool wallSpeedBuff, ref bool tileSpeedBuff, ref bool tileRangeBuff) {
			if (TheDestinyMod.currentSubworldID != string.Empty) {
				player.wings = 0;
				player.wingsLogic = 0; //figure out wings | player.armor.wingSlot
			}
        }

        public override void DrawEffects(PlayerDrawInfo drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright) {
			if (isThundercrash) {
				r = 0.1f;
				g = 0.9f;
				b = 0.9f;
				Dust dust = Dust.NewDustDirect(new Vector2(player.position.X - 2f, player.position.Y - 2f), player.width + 4, player.height + 4, DustID.Electric, 0f, 0f, 100, default, 0.5f);
				dust.velocity *= 1.6f;
				dust.velocity.Y -= 1f;
				dust.position = Vector2.Lerp(dust.position, player.Center, 0.5f);
				drawInfo.drawArms = false;
			}
        }

        public float GetSuperDamage(float damage) {
			if (Main.rand.Next(1, 101) <= superCrit) {
				return Math.Max(0, damage * 2 * superDamageMult + superDamageAdd);
			}
			return Math.Max(0, damage * superDamageMult + superDamageAdd);
		}

        public override void PostUpdateMiscEffects() {
			if (!notifiedThatSuperIsReady && superChargeCurrent == 100 && !Main.dedServ && DestinyClientConfig.Instance.NotifyOnSuper && superActiveTime == 0 && !player.dead) {
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
			if (markedByVoid && Main.BlackFadeIn < 255 && Main.LocalPlayer == player && !Main.dedServ) {
				Main.BlackFadeIn = blackFadeInTimer;
				markedByVoidDelay--;
				if (markedByVoidDelay <= 0) {
					blackFadeInTimer++;
					markedByVoidDelay = 2;
				}
			}
			if (aegisCharge >= 1 && aegisCharge < 30 || isThundercrash) {
				player.controlLeft = false;
				player.controlRight = false;
				player.controlUp = false;
				player.controlDown = false;
				player.controlHook = false;
				player.controlJump = false;
			}
			if (TheDestinyMod.currentSubworldID != string.Empty) {
				player.controlHook = false;
			}
			if (player.Distance(DestinyWorld.vogPosition.ToWorldCoordinates()) > 150 && ModContent.GetInstance<TheDestinyMod>().raidInterface.CurrentState != null) {
				ModContent.GetInstance<TheDestinyMod>().raidInterface.SetState(null);
				Main.PlaySound(SoundID.MenuClose);
			}
			if (isThundercrash) {
				if (superActiveTime > 580) {
					player.velocity += (Main.MouseWorld - player.Center) / 300;
				}
				else {
					player.velocity = (Main.MouseWorld - player.Center) / 20;
				}
				player.bodyFrame.Y = 0;
				if (superActiveTime <= 20) {
					player.velocity.X = Utils.Clamp(player.velocity.X, superActiveTime * -1f, superActiveTime);
					player.velocity.Y = Utils.Clamp(player.velocity.Y, superActiveTime * -1f, superActiveTime);
				}
				else {
					player.velocity.X = Utils.Clamp(player.velocity.X, -20f, 20f);
					player.velocity.Y = Utils.Clamp(player.velocity.Y, -30f, 40f);
					if (player.velocity.Length() < 20 && superActiveTime < 580) {
						player.velocity.Normalize();
						player.velocity *= 20;
					}
				}
			}
			if (isThundercrash && superActiveTime < 590 && (player.TouchedTiles.Count > 0 || Main.npc.Any(npc => npc.active && npc.Hitbox.Intersects(player.Hitbox) && !npc.dontTakeDamage && !npc.townNPC) || Main.player.Any(playeR => player.active && playeR.Hitbox.Intersects(player.Hitbox) && playeR.team != player.team && player.hostile) || superActiveTime <= 0)) {
				isThundercrash = false;
				if (superActiveTime <= 0)
					return;

				Projectile p = Projectile.NewProjectileDirect(player.Center, new Vector2(0, 0), ProjectileID.DD2ExplosiveTrapT3Explosion, (int)GetSuperDamage(500), 5f + superKnockback, player.whoAmI);
				p.scale = 1.5f;
				Projectile p2 = Projectile.NewProjectileDirect(player.Center, new Vector2(0, 0), ProjectileID.DD2ExplosiveTrapT3Explosion, (int)GetSuperDamage(500), 5f + superKnockback, player.whoAmI);
				p2.scale = 1.5f;
				p2.rotation = 135;
				player.velocity *= 0;
                Main.PlaySound(SoundID.Item122, player.Center);
				superActiveTime = 0;
				superChargeCurrent = 0;
			}
		}
    }
}