using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheDestinyMod.Projectiles.Ranged
{
	internal class SalvoGrenade : ModProjectile
	{
        public override string Texture => "Terraria/Projectile_" + ProjectileID.Grenade;

        public override void SetDefaults() {
			projectile.CloneDefaults(ProjectileID.Grenade);
			projectile.penetrate = -1;
			projectile.timeLeft = 800;
		}

		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection) {
			Vector2 dist = target.Center - projectile.Center;
			damage -= (int)Math.Sqrt(dist.X * dist.X + dist.Y * dist.Y);
			if (Main.expertMode) {
				if (target.type >= NPCID.EaterofWorldsHead && target.type <= NPCID.EaterofWorldsTail) {
					damage /= 5;
				}
			}
		}

        public override bool OnTileCollide(Vector2 oldVelocity) {
			if (projectile.ai[1] != 0) {
				return true;
			}
			projectile.soundDelay = 10;

			if (projectile.velocity.X != oldVelocity.X && Math.Abs(oldVelocity.X) > 1f) {
				projectile.velocity.X = oldVelocity.X * -0.9f;
			}
			if (projectile.velocity.Y != oldVelocity.Y && Math.Abs(oldVelocity.Y) > 1f) {
				projectile.velocity.Y = oldVelocity.Y * -0.9f;
			}
			return false;
		}

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {
			projectile.timeLeft = 2;
        }

        public override void AI() {
			Main.player[projectile.owner].itemAnimation = Main.player[projectile.owner].itemTime = 10;
			if (projectile.owner == Main.myPlayer && projectile.timeLeft <= 3 || Main.player[projectile.owner].GetModPlayer<DestinyPlayer>().releasedMouseLeft && projectile.timeLeft <= 770) {
				projectile.tileCollide = false;
				projectile.alpha = 255;
				projectile.position = projectile.Center;
				projectile.width = projectile.height = 175;
				projectile.Center = projectile.position;
				projectile.damage = 150;
				projectile.knockBack = 10f;
				projectile.timeLeft = 0;
			}
			else {
				if (Main.rand.NextBool()) {
					int dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, DustID.Smoke, 0f, 0f, 100, default(Color), 1f);
					Main.dust[dustIndex].scale = 0.1f + Main.rand.Next(5) * 0.1f;
					Main.dust[dustIndex].fadeIn = 1.5f + Main.rand.Next(5) * 0.1f;
					Main.dust[dustIndex].noGravity = true;
					Main.dust[dustIndex].position = projectile.Center + new Vector2(0f, (float)(-(float)projectile.height / 2)).RotatedBy(projectile.rotation, default(Vector2)) * 1.1f;
					dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, DustID.Fire, 0f, 0f, 100, default(Color), 1f);
					Main.dust[dustIndex].scale = 1f + Main.rand.Next(5) * 0.1f;
					Main.dust[dustIndex].noGravity = true;
					Main.dust[dustIndex].position = projectile.Center + new Vector2(0f, (float)(-(float)projectile.height / 2 - 6)).RotatedBy(projectile.rotation, default(Vector2)) * 1.1f;
				}
			}
			projectile.ai[0] += 1f;
			if (projectile.ai[0] > 5f) {
				projectile.ai[0] = 10f;
				if (projectile.velocity.Y == 0f && projectile.velocity.X != 0f) {
					projectile.velocity.X *= 0.97f;
					{
						projectile.velocity.X *= 0.99f;
					}
					if (projectile.velocity.X > -0.01 && projectile.velocity.X < 0.01) {
						projectile.velocity.X = 0f;
						projectile.netUpdate = true;
					}
				}
				projectile.velocity.Y += 0.2f;
			}
			return;
		}

		public override void Kill(int timeLeft) {
			Main.PlaySound(SoundID.Item14, projectile.position);
			for (int i = 0; i < 50; i++) {
				int dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, DustID.Smoke, 0f, 0f, 100, default(Color), 2f);
				Main.dust[dustIndex].velocity *= 1.4f;
			}
			for (int i = 0; i < 80; i++) {
				int dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, DustID.Fire, 0f, 0f, 100, default(Color), 3f);
				Main.dust[dustIndex].noGravity = true;
				Main.dust[dustIndex].velocity *= 5f;
				dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, DustID.Fire, 0f, 0f, 100, default(Color), 2f);
				Main.dust[dustIndex].velocity *= 3f;
			}
			for (int g = 0; g < 2; g++) {
				int goreIndex = Gore.NewGore(new Vector2(projectile.position.X + (projectile.width / 2) - 24f, projectile.position.Y + (projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
				Main.gore[goreIndex].scale = 1.5f;
				Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
				Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
				goreIndex = Gore.NewGore(new Vector2(projectile.position.X + (projectile.width / 2) - 24f, projectile.position.Y + (projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
				Main.gore[goreIndex].scale = 1.5f;
				Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
				Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
				goreIndex = Gore.NewGore(new Vector2(projectile.position.X + (projectile.width / 2) - 24f, projectile.position.Y + (projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
				Main.gore[goreIndex].scale = 1.5f;
				Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
				Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
				goreIndex = Gore.NewGore(new Vector2(projectile.position.X + (projectile.width / 2) - 24f, projectile.position.Y + (projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
				Main.gore[goreIndex].scale = 1.5f;
				Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
				Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
			}
			projectile.position.X += projectile.width / 2;
			projectile.position.Y += projectile.height / 2;
			projectile.width = 10;
			projectile.height = 10;
			projectile.position.X -= projectile.width / 2;
			projectile.position.Y -= projectile.height / 2;
		}
	}
}