using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheDestinyMod.Projectiles.Minions;

namespace TheDestinyMod.Projectiles.Minions
{
	public class TinyServitor : ModProjectile
	{
		public override void SetStaticDefaults() {
			Main.projFrames[projectile.type] = 3;
			Main.projPet[projectile.type] = true;
			ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
			ProjectileID.Sets.Homing[projectile.type] = true;
			ProjectileID.Sets.MinionTargettingFeature[projectile.type] = true;
		}

		public override void SetDefaults() {
			projectile.netImportant = true;
			projectile.width = 24;
			projectile.height = 32;
			projectile.friendly = true;
			projectile.minion = true;
			projectile.minionSlots = 1;
			projectile.penetrate = -1;
			projectile.timeLeft = 18000;
			projectile.tileCollide = false;
			projectile.ignoreWater = true;
		}

        public override void AI() {
            Player player = Main.player[projectile.owner];
			DestinyPlayer modPlayer = player.DestinyPlayer();
			float spacing = (float)projectile.width;
			for (int k = 0; k < 1000; k++) {
				Projectile otherProj = Main.projectile[k];
				if (k != projectile.whoAmI && otherProj.active && otherProj.owner == projectile.owner && otherProj.type == projectile.type && Math.Abs(projectile.position.X - otherProj.position.X) + Math.Abs(projectile.position.Y - otherProj.position.Y) < spacing) {
					if (projectile.position.X < Main.projectile[k].position.X) {
						projectile.velocity.X -= 0.05f;
					}
					else {
						projectile.velocity.X += 0.05f;
					}
					if (projectile.position.Y < Main.projectile[k].position.Y) {
						projectile.velocity.Y -= 0.05f;
					}
					else {
						projectile.velocity.Y += 0.05f;
					}
				}
			}
			Vector2 targetPos = projectile.position;
			float targetDist = 400f;
			bool target = false;
			projectile.tileCollide = true;
			if (player.HasMinionAttackTargetNPC) {
				NPC npc = Main.npc[player.MinionAttackTargetNPC];
				if (Collision.CanHitLine(projectile.position, projectile.width, projectile.height, npc.position, npc.width, npc.height)) {
					targetDist = Vector2.Distance(projectile.Center, targetPos);
					targetPos = npc.Center;
					target = true;
				}
			}
			else {
				for (int k = 0; k < 200; k++) {
					NPC npc = Main.npc[k];
					if (npc.CanBeChasedBy(this, false)) {
						float distance = Vector2.Distance(npc.Center, projectile.Center);
						if ((distance < targetDist || !target) && Collision.CanHitLine(projectile.position, projectile.width, projectile.height, npc.position, npc.width, npc.height)) {
							targetDist = distance;
							targetPos = npc.Center;
							target = true;
						}
					}
				}
			}

			if (Vector2.Distance(player.Center, projectile.Center) > (target ? 1000f : 500f)) {
				projectile.ai[0] = 1f;
				projectile.netUpdate = true;
			}
			if (projectile.ai[0] == 1f) {
				projectile.tileCollide = false;
			}
			if (target && projectile.ai[0] == 0f) {
				Vector2 direction = targetPos - projectile.Center;
				if (direction.Length() > 200f) {
					direction.Normalize();
					projectile.velocity = (projectile.velocity * 40f + direction * 6f) / (40f + 1);
				}
				else {
					projectile.velocity *= (float)Math.Pow(0.97, 40.0 / 40f);
				}
			}
			else {
				if (!Collision.CanHitLine(projectile.Center, 1, 1, player.Center, 1, 1)) {
					projectile.ai[0] = 1f;
				}
				float speed = 6f;
				if (projectile.ai[0] == 1f) {
					speed = 15f;
				}
				Vector2 center = projectile.Center;
				Vector2 direction = player.Center - center;
				projectile.ai[1] = 3600f;
				projectile.netUpdate = true;
				int num = 1;
				for (int k = 0; k < projectile.whoAmI; k++) {
					if (Main.projectile[k].active && Main.projectile[k].owner == projectile.owner && Main.projectile[k].type == projectile.type) {
						num++;
					}
				}
				direction.X -= (float)((10 + num * 40) * player.direction);
				direction.Y -= 70f;
				float distanceTo = direction.Length();
				if (distanceTo > 200f && speed < 9f) {
					speed = 9f;
				}
				if (distanceTo < 100f && projectile.ai[0] == 1f && !Collision.SolidCollision(projectile.position, projectile.width, projectile.height)) {
					projectile.ai[0] = 0f;
					projectile.netUpdate = true;
				}
				if (distanceTo > 2000f) {
					projectile.Center = player.Center;
				}
				if (distanceTo > 48f) {
					direction.Normalize();
					direction *= speed;
					float temp = 40f / 2f;
					projectile.velocity = (projectile.velocity * temp + direction) / (temp + 1);
				}
				else {
					projectile.direction = Main.player[projectile.owner].direction;
					projectile.velocity *= (float)Math.Pow(0.9, 40.0 / 40f);
				}
			}
			projectile.rotation = projectile.velocity.X * 0.05f;
			if (projectile.velocity.X > 0f) {
				projectile.spriteDirection = projectile.direction = -1;
			}
			else if (projectile.velocity.X < 0f) {
				projectile.spriteDirection = projectile.direction = 1;
			}
			if (projectile.ai[1] > 0f) {
				projectile.ai[1] += 1f;
				if (Main.rand.NextBool(3)) {
					projectile.ai[1] += 1f;
				}
			}
			if (projectile.ai[1] > 80f) {
				projectile.ai[1] = 0f;
				projectile.netUpdate = true;
			}
			if (projectile.ai[0] == 0f) {
				if (target) {
					if ((targetPos - projectile.Center).X > 0f) {
						projectile.spriteDirection = projectile.direction = -1;
					}
					else if ((targetPos - projectile.Center).X < 0f) {
						projectile.spriteDirection = projectile.direction = 1;
					}
					if (projectile.ai[1] == 0f) {
						projectile.ai[1] = 1f;
						if (Main.myPlayer == projectile.owner) {
							Vector2 shootVel = targetPos - projectile.Center;
							if (shootVel == Vector2.Zero) {
								shootVel = new Vector2(0f, 1f);
							}
							shootVel.Normalize();
							shootVel *= 12f;
							int proj = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, shootVel.X, shootVel.Y, ProjectileID.BlackBolt, projectile.damage, projectile.knockBack, Main.myPlayer, 0f, 0f);
							Main.projectile[proj].timeLeft = 300;
							Main.projectile[proj].netUpdate = true;
							projectile.netUpdate = true;
						}
					}
				}
			}
			if (player.dead) {
				modPlayer.servitorMinion = false;
			}
			if (modPlayer.servitorMinion) {
				projectile.timeLeft = 2;
			}
			projectile.frameCounter++;
			if (projectile.frameCounter >= 8) {
				projectile.frameCounter = 0;
				projectile.frame = (projectile.frame + 1) % 3;
			}
        }
	}
}