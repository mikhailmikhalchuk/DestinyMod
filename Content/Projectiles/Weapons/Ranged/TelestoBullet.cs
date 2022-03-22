using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;
using DestinyMod.Common.Projectiles;

namespace DestinyMod.Content.Projectiles.Weapons.Ranged
{
	public class TelestoBullet : DestinyModProjectile
	{
		public float DetonationTimer
		{
			get => Projectile.ai[1];
			set => Projectile.ai[1] = value;
		}

		public override void DestinySetDefaults()
		{
			Projectile.CloneDefaults(ProjectileID.Bullet);
			AIType = ProjectileID.Bullet;
			Projectile.height = 14;
			Projectile.width = 14;
			Projectile.penetrate = 5;
			Projectile.tileCollide = false;
		}

		public override Color? GetAlpha(Color lightColor) => new Color(lightColor.R, lightColor.G * 0.1f, lightColor.B * 0.8f, lightColor.A);

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			Projectile.Kill();
		}

		public override void OnHitPvp(Player target, int damage, bool crit)
		{
			Projectile.Kill();
		}

		public override void AI()
		{
			try
			{
				Point projectileTilePosition = Projectile.position.ToTileCoordinates() - new Point(1, 1);
				Point projectileTileDimensions = (Projectile.position.ToTileCoordinates() + Projectile.Size.ToTileCoordinates()) + new Point(2, 2);
				projectileTilePosition.X = Utils.Clamp(projectileTilePosition.X, 0, Main.maxTilesX - projectileTileDimensions.X);
				projectileTilePosition.Y = Utils.Clamp(projectileTilePosition.Y, 0, Main.maxTilesY - projectileTileDimensions.Y);
				//projectileTileDimensions.X = Utils.Clamp(projectileTileDimensions.X, 0, Main.maxTilesX);
				//projectileTileDimensions.Y = Utils.Clamp(projectileTileDimensions.Y, 0, Main.maxTilesY);

				for (int i = projectileTilePosition.X; i < projectileTileDimensions.X; i++)
				{
					for (int j = projectileTilePosition.Y; j < projectileTileDimensions.Y; j++)
					{
						Tile tile = Framing.GetTileSafely(i, j);
						if (!tile.HasUnactuatedTile || !Main.tileSolid[tile.TileType] || (Main.tileSolidTop[tile.TileType] && tile.TileFrameY != 0))
						{
							continue;
						}

						Vector2 tileWorldPosition = new Vector2(i, j).ToWorldCoordinates();
						if (Projectile.position.X + Projectile.width - 4f > tileWorldPosition.X && Projectile.position.X + 4f < tileWorldPosition.X + 16f 
							&& Projectile.position.Y + Projectile.height - 4f > tileWorldPosition.Y && Projectile.position.Y + 4f < tileWorldPosition.Y + 16f)
						{
							Projectile.velocity = Vector2.Zero;
							DetonationTimer++;
						}
					}
				}
			}
			catch
			{
				Main.NewText("Error occurred at line 66 of TelestoBullet (this probably shouldn't ever happen)", Color.Red);
			}

			if (DetonationTimer > 50)
			{
				Projectile.Kill();
			}
		}

		public override void Kill(int timeLeft)
		{
			Player owner = Main.player[Projectile.owner];
			Projectile projectile = Projectile.NewProjectileDirect(owner.GetProjectileSource_Item(owner.HeldItem), new Vector2(Projectile.Center.X, Projectile.Center.Y - 48), Vector2.Zero, ProjectileID.DD2ExplosiveTrapT2Explosion, Projectile.damage / 2, 0, Projectile.owner);
			projectile.friendly = true;
			projectile.DamageType = DamageClass.Ranged;
			SoundEngine.PlaySound(SoundID.DD2_ExplosiveTrapExplode, Projectile.Center);
		}
	}
}