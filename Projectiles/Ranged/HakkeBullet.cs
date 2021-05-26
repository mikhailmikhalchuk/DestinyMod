using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheDestinyMod.Projectiles.Ranged
{
	public class HakkeBullet : ModProjectile
	{
		public override void SetStaticDefaults() {
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}

		public override void SetDefaults() {
			projectile.width = 8;
			projectile.height = 8;
			projectile.aiStyle = 1;
			projectile.friendly = true;
			projectile.hostile = false;
			projectile.ranged = true;
			projectile.penetrate = 1;
			projectile.timeLeft = 600;
			projectile.light = 0.5f;
			projectile.ignoreWater = true;
			projectile.tileCollide = true;
			projectile.extraUpdates = 1;
			aiType = ProjectileID.Bullet;
		}
		
		public override void Kill(int timeLeft) {
            Collision.HitTiles(projectile.position + projectile.velocity, projectile.velocity, projectile.width, projectile.height);
            Main.PlaySound(SoundID.Item10, projectile.position);
        }
	}
}