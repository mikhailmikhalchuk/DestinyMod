using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace TheDestinyMod.Projectiles.Ranged
{
    public class GjallarhornRocket : ModProjectile
    {
        public override string Texture => "Terraria/Projectile_" + ProjectileID.RocketI;

		private bool target = false;

        public override void SetStaticDefaults() {
			DisplayName.SetDefault("Wolfpack Round");
			ProjectileID.Sets.Homing[projectile.type] = true;
		}

        public override void SetDefaults() {
            projectile.CloneDefaults(ProjectileID.RocketI);
            aiType = ProjectileID.RocketI;
			projectile.timeLeft = 500;
			projectile.friendly = true;
			projectile.ranged = true;
        }

		public override void Kill(int timeLeft) {
			Main.PlaySound(SoundID.Item14, projectile.position);
			projectile.position = projectile.Center;
			projectile.width = 22;
			projectile.height = 22;
			projectile.position.X -= projectile.width / 2;
			projectile.position.Y -= projectile.height / 2;
			for (int i = 0; i < 20; i++) {
				Dust dust = Dust.NewDustDirect(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, DustID.Fire, 0f, 0f, 100, default, 3.5f);
				dust.noGravity = true;
				dust.velocity *= 7f;
				dust = Dust.NewDustDirect(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, DustID.Fire, 0f, 0f, 100, default, 1.5f);
				dust.velocity *= 3f;
			}
		}

		public override bool OnTileCollide(Vector2 oldVelocity) {
			Collision.HitTiles(projectile.position + projectile.velocity, projectile.velocity, projectile.width, projectile.height);
			projectile.Kill();
			return true;
		}

		public override void AI() {
			target = projectile.HomeInOnNPC(400f, 20f);
		}

        public override void OnHitNPC(NPC npc, int damage, float knockback, bool crit) {
			if (target) {
				for (int i = 0; i < 5; i++) {
					Vector2 velocity = Main.rand.NextVector2Unit() * Utils.NextFloat(Main.rand, 6f, 12f);
					Projectile.NewProjectile(projectile.position, velocity, ModContent.ProjectileType<GjallarhornMiniRocket>(), damage / 5, 0, projectile.owner);
				}
			}
			projectile.Kill();
        }
    }
}