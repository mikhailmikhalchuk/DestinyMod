using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using TheDestinyMod.Projectiles.Ammo;
using TheDestinyMod.Items.Materials;
using Microsoft.Xna.Framework.Audio;
using System;

namespace TheDestinyMod.Items.Weapons.Ranged
{
	public class VexMythoclast : ModItem
	{
		private int cooldown;

		private bool alt;

        public override void SetStaticDefaults() {
			Tooltip.SetDefault("Kills with this weapon grant stacks of Overcharge\nRight-click with Overcharge to switch firing modes\nHold down the trigger in the alternative firing mode to fire a more powerful shot\n\"...a causal loop within the weapon's mechanism, suggesting that the firing process somehow binds space and time into...\"");
        }

        public override void SetDefaults() {
			item.damage = 84;
			item.ranged = true;
			item.noMelee = true;
			item.autoReuse = true;
			item.channel = true;
			item.rare = ItemRarityID.Yellow;
			item.knockBack = 0;
			item.width = 104;
			item.height = 46;
			item.useTime = 13;
			item.crit = 10;
			item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/VexMythoclast"); //thanks, fillinek
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.shootSpeed = 10f;
			item.useAnimation = 13;
			item.shoot = 10;
			item.useAmmo = AmmoID.Bullet;
			item.value = Item.buyPrice(0, 1, 0, 0);
			item.scale = 0.7f;
		}

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack) {
			if (!alt) {
                item.autoReuse = true;
				Projectile.NewProjectile(position.X, position.Y - 2, speedX, speedY, ModContent.ProjectileType<VexBullet>(), damage, knockBack, player.whoAmI);
			}
			else {
				Projectile.NewProjectile(position.X, position.Y - 2, speedX, speedY, ModContent.ProjectileType<VexChargeBullet>(), damage * 2, knockBack, player.whoAmI);
			}
			return false;
        }

		public override bool CanUseItem(Player player) {
			if (player.altFunctionUse == 2 && !alt && cooldown <= 0 && player.HasBuff(ModContent.BuffType<Buffs.Overcharge>())) {
				alt = true;
                item.autoReuse = false;
                item.UseSound = null;
				Main.PlaySound(SoundID.Item101);
				cooldown = 15;
			}
			else if (player.altFunctionUse == 2 && alt && cooldown <= 0) {
				alt = false;
                item.autoReuse = true;
                item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/VexMythoclast");
                Main.PlaySound(SoundID.Item101);
				cooldown = 15;
			}
			return player.altFunctionUse != 2;
		}

        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale) {
            if ((Main.LocalPlayer.GetModPlayer<DestinyPlayer>().overchargeStacks <= 0 || !Main.LocalPlayer.HasBuff(ModContent.BuffType<Buffs.Overcharge>())) && alt) {
                alt = false;
                item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/VexMythoclast");
                Main.LocalPlayer.ClearBuff(ModContent.BuffType<Buffs.Overcharge>());
            }
            if (cooldown > 0)
                cooldown--;

            return base.PreDrawInInventory(spriteBatch, position, frame, drawColor, itemColor, origin, scale);
        }

        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI) {
			scale *= 0.7f;
            return true;
        }

		public override bool AltFunctionUse(Player player) {
			return true;
		}

		public override Vector2? HoldoutOffset() {
			return new Vector2(-3, -2);
		}
	}

    public class VexChargeBullet : ModProjectile
    {
        private bool fired;

        private static SoundEffectInstance charge;

        public override string Texture => "Terraria/Projectile_" + ProjectileID.Bullet;

        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Bullet");
        }

        public override void SetDefaults() {
            projectile.CloneDefaults(ProjectileID.Bullet);
            aiType = ProjectileID.Bullet;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.tileCollide = false;
            projectile.hide = true;
        }

        public override bool OnTileCollide(Vector2 oldVelocity) {
            Collision.HitTiles(projectile.position + projectile.velocity, projectile.velocity, projectile.width, projectile.height);
            Main.PlaySound(SoundID.Item10, projectile.position);
            return true;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor) {
            return fired;
        }

        public override void AI() {
            if (charge == null && !fired) {
                charge = mod.GetSound("Sounds/Item/VexMythoclastStart").CreateInstance();
                charge.Play();
            }
            Player player = Main.player[projectile.owner];
            if (!fired) {
                projectile.position = player.Center + projectile.velocity;
                int dir = projectile.direction;
                player.ChangeDir(dir);
                player.heldProj = projectile.whoAmI;
                player.itemAnimation = player.itemTime = 2;
                player.itemRotation = (float)Math.Atan2(projectile.velocity.Y * dir, projectile.velocity.X * dir);
            }
            if (projectile.owner == Main.myPlayer && !fired) {
                Vector2 diff = Main.MouseWorld - player.Center;
                diff.Normalize();
                projectile.velocity = diff;
                projectile.direction = Main.MouseWorld.X > player.position.X ? 1 : -1;
                projectile.netUpdate = true;
            }
            if (charge != null) {
                if (charge.State == SoundState.Stopped && !fired) {
                    Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/VexMythoclastFire"), projectile.Center);
                    fired = true;
                    charge?.Stop();
                    charge = null;
                    Main.player[projectile.owner].channel = false;
                    Vector2 perturbedSpeed = (10 * projectile.velocity * 2f);
                    projectile.velocity = perturbedSpeed;
                    projectile.tileCollide = true;
                    projectile.hide = false;
                    Main.player[projectile.owner].GetModPlayer<DestinyPlayer>().overchargeStacks--;
                }
                else if (!player.channel && charge.State == SoundState.Playing && !fired && player.whoAmI == Main.myPlayer) {
                    projectile.Kill();
                }
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {
            Main.player[projectile.owner].AddBuff(ModContent.BuffType<Buffs.Overcharge>(), 10);
        }

        public override void Kill(int timeLeft) {
            if (!fired) {
                charge?.Stop();
                charge = null;
                Main.player[projectile.owner].channel = false;
            }
        }

        public override void ModifyDamageHitbox(ref Rectangle hitbox) {
            if (!fired) {
                hitbox = Rectangle.Empty;
            }
        }
    }

    public class VexBullet : ModProjectile
    {
        public override string Texture => "Terraria/Projectile_" + ProjectileID.Bullet;

        public override void SetDefaults() {
            projectile.CloneDefaults(ProjectileID.Bullet);
            aiType = ProjectileID.Bullet;
        }

        public override bool OnTileCollide(Vector2 oldVelocity) {
            Collision.HitTiles(projectile.position + projectile.velocity, projectile.velocity, projectile.width, projectile.height);
            Main.PlaySound(SoundID.Item10, projectile.position);
            return true;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {
            if (!target.friendly && target.damage > 0 && target.life <= 0) {
                DestinyPlayer player = Main.player[projectile.owner].GetModPlayer<DestinyPlayer>();
                Main.player[projectile.owner].AddBuff(ModContent.BuffType<Buffs.Overcharge>(), 2);
                if (player.overchargeStacks < 3)
                    player.overchargeStacks++;
            }
        }

        public override void OnHitPvp(Player target, int damage, bool crit) {
            if (target.statLife <= 0) {
                DestinyPlayer player = Main.player[projectile.owner].GetModPlayer<DestinyPlayer>();
                Main.player[projectile.owner].AddBuff(ModContent.BuffType<Buffs.Overcharge>(), 2);
                if (player.overchargeStacks < 3)
                    player.overchargeStacks++;
            }
        }
    }
}