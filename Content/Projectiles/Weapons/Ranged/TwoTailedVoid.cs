using Microsoft.Xna.Framework;
using System;
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
			Projectile.aiStyle = -1;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 10;
		}

		public override Color? GetAlpha(Color lightColor) => new Color(lightColor.R, lightColor.G * 0.1f, lightColor.B * 0.8f, lightColor.A);

		public override void Kill(int timeLeft)
		{
			Projectile.Resize(90, 90);
			Projectile.maxPenetrate = -1;
			Projectile.penetrate = -1;
			Projectile.Damage();
			SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
			for (int i = 0; i < 20; i++)
			{
				Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.PurpleTorch, 0f, 0f, 100, default, 3.5f);
				dust.noGravity = true;
				dust.velocity *= 7f;
				dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.PurpleTorch, 0f, 0f, 100, default, 1.5f);
				dust.velocity *= 3f;
			}
		}

		public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;

			Vector2 absoluteVelocity = new Vector2(Math.Abs(Projectile.velocity.X), Math.Abs(Projectile.velocity.Y));
			if (absoluteVelocity.X >= 8 || absoluteVelocity.Y >= 8)
			{
				for (int i = 0; i < 2; i++)
				{
					Vector2 dustPosModifier = new Vector2(3) + (i == 1 ? Projectile.velocity * 0.5f : Vector2.Zero);
					Vector2 dustPosition = Projectile.position + dustPosModifier - Projectile.velocity * 0.5f;
					Dust dust = Dust.NewDustDirect(dustPosition, Projectile.width - 8, Projectile.height - 8, DustID.PurpleTorch, Alpha: 100);
					dust.scale *= 2f + Main.rand.Next(10) * 0.1f;
					dust.velocity *= 0.2f;
					dust.noGravity = true;

					//dust = Dust.NewDustDirect(dustPosition, Projectile.width - 8, Projectile.height - 8, DustID.Smoke, Alpha: 100, Scale: 0.5f);
					//dust.fadeIn = 1f + Main.rand.Next(5) * 0.1f;
					//dust.velocity *= 0.05f;
				}
			}
			if (absoluteVelocity.X < 15f && absoluteVelocity.Y < 15f)
			{
				Projectile.velocity *= 1.1f;
			}

			GradualHomeInOnNPC(400f, 20f, 0.15f);
		}
	}
}