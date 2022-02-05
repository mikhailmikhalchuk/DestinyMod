using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace DestinyMod.Common.Projectiles.ProjectileType
{
	public class Bullet : DestinyModProjectile
	{
		public override void AutomaticSetDefaults()
		{
			Projectile.width = 4;
			Projectile.height = 4;
			Projectile.aiStyle = 1;
			Projectile.friendly = true;
			Projectile.penetrate = 1;
			Projectile.light = 0.5f;
			Projectile.timeLeft = 600;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.extraUpdates = 1;
			AIType = ProjectileID.Bullet;
		}

		public override void Kill(int timeLeft)
		{
			Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
			SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
		}
	}
}