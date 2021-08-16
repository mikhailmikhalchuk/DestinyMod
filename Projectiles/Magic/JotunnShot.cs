using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework.Graphics;

namespace TheDestinyMod.Projectiles.Magic
{
	public class JotunnShot : ModProjectile
	{
		public bool fired = false;

		public bool played = false;

        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Jötunn Shot");
        }

        public override void SetDefaults() {
			projectile.CloneDefaults(ProjectileID.RocketI);
			aiType = ProjectileID.RocketI;
			projectile.width = 8;
			projectile.height = 14;
			projectile.timeLeft = 200;
			projectile.friendly = true;
            projectile.magic = true;
			projectile.penetrate = -1;
		}

		public override bool OnTileCollide(Vector2 oldVelocity) {
			Collision.HitTiles(projectile.position + projectile.velocity, projectile.velocity, projectile.width, projectile.height);
			Main.PlaySound(SoundID.Item10, projectile.position);
			projectile.Kill();
			return true;
		}

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor) {
			return fired;
        }

        public override void AI() {
            Player player = Main.player[projectile.owner];
            if (player.channel && projectile.localAI[0] < 80f && !fired) {
                projectile.position = player.Center + projectile.velocity;
                if (projectile.owner == Main.myPlayer) {
                    Vector2 diff = Main.MouseWorld - player.Center;
                    diff.Normalize();
                    projectile.velocity = diff;
                    projectile.direction = Main.MouseWorld.X > player.position.X ? 1 : -1;
                    projectile.netUpdate = true;
                }
                int dir = projectile.direction;
                player.ChangeDir(dir); // Set player direction to where we are shooting
                player.heldProj = projectile.whoAmI; // Update player's held projectile
                player.itemTime = 2; // Set item time to 2 frames while we are used
                player.itemAnimation = 2; // Set item animation time to 2 frames while we are used
                player.itemRotation = (float)Math.Atan2(projectile.velocity.Y * dir, projectile.velocity.X * dir); // Set the item rotation to where we are shooting
                projectile.localAI[0]++;
                projectile.damage = (int)projectile.localAI[0];
                Vector2 offset = projectile.velocity;
                offset *= 10f;
                Vector2 pos = player.Center + offset - new Vector2(10, 10);
                Vector2 dustVelocity = Vector2.UnitX * 18f;
                dustVelocity = dustVelocity.RotatedBy(projectile.rotation - 1.57f);
                Vector2 spawnPos = projectile.Center + dustVelocity;
                Vector2 spawn = spawnPos + ((float)Main.rand.NextDouble() * 6.28f).ToRotationVector2() * (12f - projectile.localAI[0]);
                Dust dust = Main.dust[Dust.NewDust(pos, 20, 20, DustID.Fire, projectile.velocity.X / 2f, projectile.velocity.Y / 2f)];
                dust.velocity = Vector2.Normalize(spawnPos - spawn) * 1.5f * (10f - projectile.localAI[0]) / 10f;
                dust.noGravity = true;
                dust.scale = Main.rand.Next(10, 20) * 0.05f;
            }
            else if (!player.channel && projectile.localAI[0] < 80f) {
                if (projectile.localAI[0] < 20f && !fired) {
                    Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/JotunnCharge1"), projectile.Center);
                }
                else if (projectile.localAI[0] >= 20f && projectile.localAI[0] < 40f && !fired) {
                    Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/JotunnCharge2"), projectile.Center);
                }
                else if (projectile.localAI[0] >= 40f && projectile.localAI[0] < 60f && !fired) {
                    Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/JotunnCharge3"), projectile.Center);
                }
                else if (projectile.localAI[0] >= 60f && !fired) {
                    Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/JotunnCharge4"), projectile.Center);
                }
                fired = true;
                projectile.velocity = 10 * projectile.velocity * 2f / 11f;
                AdjustMagnitude(ref projectile.velocity);
                Main.projectile[projectile.identity].aiStyle = ProjectileID.WoodenArrowFriendly;
            }
            else if (projectile.localAI[0] >= 80f) {
                if (!fired) {
                    Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/JotunnCharge5"), projectile.Center);
                }
                fired = true;
                projectile.velocity = 10 * projectile.velocity * 2f / 11f;
                AdjustMagnitude(ref projectile.velocity);
                if (projectile.alpha > 70) {
                    projectile.alpha -= 15;
                    if (projectile.alpha < 70) {
                        projectile.alpha = 70;
                    }
                }
                projectile.HomeInOnNPC(400f, 20f);
            }
        }

        public override void PostDraw(SpriteBatch spriteBatch, Color lightColor) {
            Lighting.AddLight(projectile.Center, Color.Orange.ToVector3() * 2f);
        }

        public override void ModifyDamageHitbox(ref Rectangle hitbox) {
            hitbox = new Rectangle((int)projectile.position.X, (int)projectile.position.Y, 8, 14);
            if (!fired) {
                hitbox = Rectangle.Empty;
            }
        }

        private void AdjustMagnitude(ref Vector2 vector) {
			float magnitude = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
			if (magnitude > 20f) {
				vector *= 20f / magnitude;
			}
		}
	}
}