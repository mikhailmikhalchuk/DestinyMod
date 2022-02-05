using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using DestinyMod.Common.Projectiles;

namespace DestinyMod.Content.Projectiles.Weapons.Ranged
{
	public class TwoTailedVoid : DestinyModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Void Rocket");
			ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
		}

		public override void DestinySetDefaults()
		{
			Projectile.width = 10;
			Projectile.height = 26;
			Projectile.timeLeft = 500;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.penetrate = 1;
		}

		public override Color? GetAlpha(Color lightColor) => new Color(lightColor.R, lightColor.G * 0.1f, lightColor.B * 0.8f, lightColor.A);

		public override void Kill(int timeLeft)
		{
			SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
			Projectile.position = Projectile.Center;
			Projectile.width = 22;
			Projectile.height = 22;
			Projectile.position.X -= Projectile.width / 2;
			Projectile.position.Y -= Projectile.height / 2;
			for (int i = 0; i < 20; i++)
			{
				Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.PurpleCrystalShard, 0f, 0f, 100, default, 2f);
				dust.noGravity = true;
				dust.velocity *= 4f;
				dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.PurpleCrystalShard, 0f, 0f, 100, default, 1.5f);
				dust.velocity *= 3f;
			}
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
			Projectile.Kill();
			return true;
		}

		public override void AI()
		{
			Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
			HomeInOnNPC(400f, 20f);
		}
	}
}