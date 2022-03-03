using DestinyMod.Common.ModPlayers;
using DestinyMod.Common.Projectiles;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;

namespace DestinyMod.Content.Projectiles.Weapons.Ranged
{
	public class SalvoGrenade : DestinyModProjectile
	{
		public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.Grenade;

		public override void DestinySetDefaults()
		{
			Projectile.CloneDefaults(ProjectileID.Grenade);
			Projectile.penetrate = 1;
			Projectile.timeLeft = 400;
		}

		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			damage -= (int)Projectile.Distance(target.Center);

			if (Main.expertMode)
			{
				if (target.type >= NPCID.EaterofWorldsHead && target.type <= NPCID.EaterofWorldsTail)
				{
					damage /= 5;
				}
			}
		}

        public override bool OnTileCollide(Vector2 oldVelocity)
		{
			if (Projectile.ai[1] != 0)
			{
				return true;
			}

			Projectile.soundDelay = 10;

			if (Projectile.velocity.X != oldVelocity.X && Math.Abs(oldVelocity.X) > 1f)
			{
				Projectile.velocity.X = oldVelocity.X * -0.9f;
			}

			if (Projectile.velocity.Y != oldVelocity.Y && Math.Abs(oldVelocity.Y) > 1f)
			{
				Projectile.velocity.Y = oldVelocity.Y * -0.9f;
			}
			return false;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) => Projectile.timeLeft = 2;

		public override void AI()
		{
			Player player = Main.player[Projectile.owner];
			player.itemAnimation = player.itemTime = 10;
			if ((Projectile.owner == Main.myPlayer && Projectile.timeLeft <= 3) || (player.GetModPlayer<StatsPlayer>().ChannelTime <= 0 && Projectile.timeLeft <= 390))
			{
				Projectile.alpha = 255;
				Projectile.position = Projectile.Center;
				Projectile.width = Projectile.height = 22;
				Projectile.Center = Projectile.position;
				Projectile.damage = 150;
				Projectile.knockBack = 10f;
				if (Projectile.timeLeft > 3)
				{
					Projectile.timeLeft = 3;
				}
			}
			else if (Main.rand.NextBool())
			{
				Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Smoke, 0f, 0f, 100, default, 1f);
				dust.scale = 0.1f + Main.rand.Next(5) * 0.1f;
				dust.fadeIn = 1.5f + Main.rand.Next(5) * 0.1f;
				dust.noGravity = true;
				dust.position = Projectile.Center + new Vector2(0f, -(float)Projectile.height / 2).RotatedBy(Projectile.rotation) * 1.1f;
				dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 100, default, 1f);
				dust.scale = 1f + Main.rand.Next(5) * 0.1f;
				dust.noGravity = true;
				dust.position = Projectile.Center + new Vector2(0f, -(float)Projectile.height / 2 - 6).RotatedBy(Projectile.rotation) * 1.1f;
			}

			if (++Projectile.ai[0] > 5f)
			{
				Projectile.ai[0] = 10f;
				if (Projectile.velocity.Y == 0f && Projectile.velocity.X != 0f)
				{
					Projectile.velocity.X *= 0.98f;
					if (Math.Abs(Projectile.velocity.X) < 0.01f)
					{
						Projectile.velocity.X = 0f;
						Projectile.netUpdate = true;
					}
				}

				Projectile.velocity.Y += 0.2f;
			}
		}

		public override void Kill(int timeLeft)
		{
			SoundEngine.PlaySound(SoundID.Item14, Projectile.position);

			for (int i = 0; i < 30; i++)
			{
				Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Smoke, 0f, 0f, 100, default, 1.5f);
				dust.velocity *= 1.4f;
			}

			for (int i = 0; i < 20; i++)
			{
				Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 100, default, 3.5f);
				dust.noGravity = true;
				dust.velocity *= 7f;
				dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 100, default, 1.5f);
				dust.velocity *= 3f;
			}

			for (int i = 0; i < 2; i++)
			{
				for (int goreCount = 0; goreCount < 4; goreCount++)
				{
					Gore gore = Gore.NewGoreDirect(Projectile.position, default, Main.rand.Next(61, 64));
					gore.velocity *= i == 1 ? 0.4f : 0.8f;
					gore.velocity.X += goreCount % 2 == 0 ? 1 : -1;
					gore.velocity.Y += goreCount < 2 ? 1 : -1;
				}
			}

			Projectile.position.X += Projectile.width / 2;
			Projectile.position.Y += Projectile.height / 2;
			Projectile.width = 10;
			Projectile.height = 10;
			Projectile.position.X -= Projectile.width / 2;
			Projectile.position.Y -= Projectile.height / 2;
		}
	}
}