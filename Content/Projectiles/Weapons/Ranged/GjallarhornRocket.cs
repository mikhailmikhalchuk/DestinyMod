using Microsoft.Xna.Framework;
using DestinyMod.Common.Projectiles;
using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Audio;

namespace DestinyMod.Content.Projectiles.Weapons.Ranged
{
	public class GjallarhornRocket : DestinyModProjectile
	{
		public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.RocketI;

		public int Target;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Wolfpack Rocket");
			ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
		}

		public override void DestinySetDefaults()
		{
			Projectile.width = 14;
			Projectile.height = 14;
			Projectile.timeLeft = 500;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.penetrate = 1;
			Projectile.aiStyle = -1;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 10;
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
					Dust dust = Dust.NewDustDirect(dustPosition, Projectile.width - 8, Projectile.height - 8, DustID.Torch, Alpha: 100);
					dust.scale *= 2f + Main.rand.Next(10) * 0.1f;
					dust.velocity *= 0.2f;
					dust.noGravity = true;

					dust = Dust.NewDustDirect(dustPosition, Projectile.width - 8, Projectile.height - 8, DustID.Smoke, Alpha: 100, Scale: 0.5f);
					dust.fadeIn = 1f + Main.rand.Next(5) * 0.1f;
					dust.velocity *= 0.05f;
				}
			}
			if (absoluteVelocity.X < 15f && absoluteVelocity.Y < 15f)
			{
				Projectile.velocity *= 1.1f;
			}

			Target = GradualHomeInOnNPC(400f, 20f, 0.15f);
		}

		public override void OnHitNPC(NPC npc, int damage, float knockback, bool crit)
		{
			if (Target == -1)
            {
				return;
            }

			for (int i = 0; i < 5; i++)
			{
				Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position, -Projectile.oldVelocity.RotatedByRandom(1.8), ModContent.ProjectileType<GjallarhornMiniRocket>(), damage / 5, 0, Projectile.owner);
			}

			Target = -1;
		}

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
			for (int i = 0; i < 5; i++)
			{
				Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position, -oldVelocity.RotatedByRandom(1.8), ModContent.ProjectileType<GjallarhornMiniRocket>(), 10, 0, Projectile.owner);
			}

			return true;
        }

        public override void Kill(int timeLeft)
		{
			Projectile.Resize(80, 80);
			Projectile.maxPenetrate = -1;
			Projectile.penetrate = -1;
			Projectile.Damage();
			SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
			for (int i = 0; i < 20; i++)
			{
				Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 100, default, 3.5f);
				dust.noGravity = true;
				dust.velocity *= 7f;
				dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 100, default, 1.5f);
				dust.velocity *= 3f;
			}
		}
	}
}