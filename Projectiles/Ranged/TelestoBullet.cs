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
				int projX = (int)(projectile.position.X / 16f) - 1;
				int projWidth = (int)((projectile.position.X + projectile.width) / 16f) + 2;
				int projY = (int)(projectile.position.Y / 16f) - 1;
				int projHeight = (int)((projectile.position.Y + projectile.height) / 16f) + 2;
				if (projX < 0) {
					projX = 0;
				}
				if (projWidth > Main.maxTilesX) {
					projWidth = Main.maxTilesX;
				}
				if (projY < 0) {
					projY = 0;
				}
				if (projHeight > Main.maxTilesY) {
					projHeight = Main.maxTilesY;
				}
				Vector2 vec;
				for (int i = projX; i < projWidth; i++) {
					for (int j = projY; j < projHeight; j++) {
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
				Main.NewText("Test");
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