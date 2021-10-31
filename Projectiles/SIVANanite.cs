using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheDestinyMod.Buffs;

namespace TheDestinyMod.Projectiles
{
    public class SIVANanite : ModProjectile
    {
        public override string Texture => "Terraria/Projectile_" + ProjectileID.SnowBallFriendly;

        public override void SetStaticDefaults() {
            DisplayName.SetDefault("SIVA Nanite");
            ProjectileID.Sets.Homing[projectile.type] = true;
        }

        public override void SetDefaults() {
            projectile.CloneDefaults(ProjectileID.SnowBallFriendly);
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.ranged = true;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
        }

        public override void AI() {
			projectile.localAI[1]++;
			bool target = projectile.HomeInOnNPC(200f, 15f, false);
			if (!target && projectile.alpha < 200 && projectile.velocity.Y > 0f) {
                projectile.velocity.Y--;
                if (projectile.velocity.Y < 0f) {
                    projectile.velocity.Y = 0f;
                }
			}
            if (target) {
                projectile.alpha = 0;
            }
			if (projectile.localAI[1] > 400) {
				projectile.alpha += 2;
			}
			if (projectile.alpha >= 255) {
				projectile.Kill();
			}
            projectile.rotation += 0.1f;
			projectile.Center = projectile.Center + Vector2.UnitX.RotatedBy(projectile.rotation, Vector2.Zero) * 0.5f;
		}

        public override void PostDraw(SpriteBatch spriteBatch, Color lightColor) {
            Lighting.AddLight(projectile.Center, Color.Red.ToVector3() * 0.55f * Main.essScale);
        }
    }
}