using Microsoft.Xna.Framework;
using DestinyMod.Common.Projectiles;
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
			DisplayName.SetDefault("Wolfpack Round");
			ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
		}

		public override void DestinySetDefaults()
		{
			Projectile.CloneDefaults(ProjectileID.RocketI);
			AIType = ProjectileID.RocketI;
			Projectile.timeLeft = 500;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.ai[0] = -1;
		}

		public override void AI() => Target = HomeInOnNPC(400f, 20f);

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
			Projectile.Kill();
			return true;
		}

		public override void OnHitNPC(NPC npc, int damage, float knockback, bool crit)
		{
			Player player = Main.player[Projectile.owner];

			if (Target == -1)
            {
				return;
            }

			for (int i = 0; i < 5; i++)
			{
				Projectile.NewProjectile(player.GetProjectileSource_Item(player.HeldItem), Projectile.position, Main.rand.NextVector2Unit() * Utils.NextFloat(Main.rand, 6f, 12f), ModContent.ProjectileType<GjallarhornMiniRocket>(), damage / 5, 0, Projectile.owner);
			}
			Projectile.Kill();
		}

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
				Dust dust = Dust.NewDustDirect(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 100, default, 3.5f);
				dust.noGravity = true;
				dust.velocity *= 7f;
				dust = Dust.NewDustDirect(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 100, default, 1.5f);
				dust.velocity *= 3f;
			}
		}
	}
}