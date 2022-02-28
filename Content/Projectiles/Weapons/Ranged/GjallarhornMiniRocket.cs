using Microsoft.Xna.Framework;
using DestinyMod.Common.Projectiles;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Audio;

namespace DestinyMod.Content.Projectiles.Weapons.Ranged
{
	public class GjallarhornMiniRocket : DestinyModProjectile
	{
		public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.RocketI;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Wolfpack Round");
			ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
		}

		public override void DestinySetDefaults()
		{
			Projectile.CloneDefaults(ProjectileID.RocketI);
			AIType = ProjectileID.RocketI;
			Projectile.timeLeft = 200;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.tileCollide = true;
			Projectile.scale = 0.5f;
			Projectile.penetrate = 1;
		}

		public override void AI()
		{
			if (Projectile.timeLeft > 190)
			{
				return;
			}

			HomeInOnNPC(400f, 20f);
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
			Projectile.Kill();
			return true;
		}

		public override void ModifyDamageHitbox(ref Rectangle hitbox)
		{
			if (Projectile.timeLeft > 190)
			{
				hitbox = Rectangle.Empty;
			}
		}

		public override void Kill(int timeLeft)
		{
			SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
			Projectile.position = Projectile.Center;
			Projectile.width = 11;
			Projectile.height = 11;
			Projectile.position.X -= Projectile.width / 2;
			Projectile.position.Y -= Projectile.height / 2;
			for (int i = 0; i < 20; i++)
			{
				Dust dust = Dust.NewDustDirect(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 100, default, 3.5f);
				dust.noGravity = true;
				dust.velocity *= 7f;
				dust = Dust.NewDustDirect(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 100, default, 1.5f);
				dust.velocity *= 3f;
			}
		}
	}
}