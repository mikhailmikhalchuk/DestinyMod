using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheDestinyMod.Buffs;

namespace TheDestinyMod.Projectiles.Ranged
{
    public class TelestoBullet : ModProjectile
    {
        public override string Texture => "Terraria/Projectile_" + ProjectileID.ExplosiveBullet;

        public override void SetDefaults() {
            projectile.CloneDefaults(ProjectileID.Bullet);
            aiType = ProjectileID.Bullet;
            projectile.height = 5;
            projectile.width = 5;
			projectile.penetrate = 5;
			projectile.tileCollide = false;
        }

        public override Color? GetAlpha(Color lightColor) {
            return new Color(lightColor.R, lightColor.G * 0.5f, lightColor.B * 0.1f, lightColor.A);
        }

        public override void AI() {
			try {
				int num337 = (int)(projectile.position.X / 16f) - 1;
				int num338 = (int)((projectile.position.X + projectile.width) / 16f) + 2;
				int num339 = (int)(projectile.position.Y / 16f) - 1;
				int num340 = (int)((projectile.position.Y + projectile.height) / 16f) + 2;
				if (num337 < 0) {
					num337 = 0;
				}
				if (num338 > Main.maxTilesX) {
					num338 = Main.maxTilesX;
				}
				if (num339 < 0) {
					num339 = 0;
				}
				if (num340 > Main.maxTilesY) {
					num340 = Main.maxTilesY;
				}
				Vector2 vec = default;
				for (int i = num337; i < num338; i++) {
					for (int j = num339; j < num340; j++) {
						if (Main.tile[i, j] != null && Main.tile[i, j].nactive() && (Main.tileSolid[Main.tile[i, j].type] || (Main.tileSolidTop[Main.tile[i, j].type] && Main.tile[i, j].frameY == 0))) {
							vec.X = i * 16;
							vec.Y = j * 16;
							if (projectile.position.X + projectile.width - 4f > vec.X && projectile.position.X + 4f < vec.X + 16f && projectile.position.Y + projectile.height - 4f > vec.Y && projectile.position.Y + 4f < vec.Y + 16f) {
								projectile.velocity *= 0f;
								projectile.ai[1] += 1f;
							}
						}
					}
				}
			}
			catch {
			}
			if (projectile.ai[1] > 50) {
				Projectile proj = Projectile.NewProjectileDirect(projectile.Center, Vector2.Zero, ProjectileID.DD2ExplosiveTrapT2Explosion, projectile.damage / 2, 0, projectile.owner);
				proj.friendly = true;
				proj.hostile = true;
				Main.PlaySound(SoundID.DD2_ExplosiveTrapExplode, projectile.Center);
				projectile.Kill();
			}
		}
    }
}