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
					Dust dust = Dust.NewDustDirect(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, DustID.Smoke, 0f, 0f, 100, default, 1f);
					dust.scale = 0.1f + Main.rand.Next(5) * 0.1f;
					dust.fadeIn = 1.5f + Main.rand.Next(5) * 0.1f;
					dust.noGravity = true;
					dust.position = projectile.Center + new Vector2(0f, (float)(-(float)projectile.height / 2)).RotatedBy(projectile.rotation, default) * 1.1f;
					dust = Dust.NewDustDirect(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, DustID.Fire, 0f, 0f, 100, default, 1f);
					dust.scale = 1f + Main.rand.Next(5) * 0.1f;
					dust.noGravity = true;
					dust.position = projectile.Center + new Vector2(0f, (float)(-(float)projectile.height / 2 - 6)).RotatedBy(projectile.rotation, default) * 1.1f;
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
				Dust dust = Dust.NewDustDirect(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, DustID.Smoke, 0f, 0f, 100, default, 2f);
				dust.velocity *= 1.4f;
			}
			for (int i = 0; i < 80; i++) {
				Dust dust = Dust.NewDustDirect(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, DustID.Fire, 0f, 0f, 100, default, 3f);
				dust.noGravity = true;
				dust.velocity *= 5f;
				dust = Dust.NewDustDirect(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, DustID.Fire, 0f, 0f, 100, default, 2f);
				dust.velocity *= 3f;
			}
			for (int g = 0; g < 2; g++) {
				Gore gore = Gore.NewGoreDirect(new Vector2(projectile.position.X + (projectile.width / 2) - 24f, projectile.position.Y + (projectile.height / 2) - 24f), default, Main.rand.Next(61, 64), 1f);
				gore.scale = 1.5f;
				gore.velocity.X = gore.velocity.X + 1.5f;
				gore.velocity.Y = gore.velocity.Y + 1.5f;
				gore = Gore.NewGoreDirect(new Vector2(projectile.position.X + (projectile.width / 2) - 24f, projectile.position.Y + (projectile.height / 2) - 24f), default, Main.rand.Next(61, 64), 1f);
				gore.scale = 1.5f;
				gore.velocity.X = gore.velocity.X - 1.5f;
				gore.velocity.Y = gore.velocity.Y + 1.5f;
				gore = Gore.NewGoreDirect(new Vector2(projectile.position.X + (projectile.width / 2) - 24f, projectile.position.Y + (projectile.height / 2) - 24f), default, Main.rand.Next(61, 64), 1f);
				gore.scale = 1.5f;
				gore.velocity.X = gore.velocity.X + 1.5f;
				gore.velocity.Y = gore.velocity.Y - 1.5f;
				gore = Gore.NewGoreDirect(new Vector2(projectile.position.X + (projectile.width / 2) - 24f, projectile.position.Y + (projectile.height / 2) - 24f), default, Main.rand.Next(61, 64), 1f);
				gore.scale = 1.5f;
				gore.velocity.X = gore.velocity.X - 1.5f;
				gore.velocity.Y = gore.velocity.Y - 1.5f;
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