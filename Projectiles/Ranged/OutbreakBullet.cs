using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheDestinyMod.Buffs;

namespace TheDestinyMod.Projectiles.Ranged
{
    public class OutbreakBullet : ModProjectile
    {
        public override string Texture => "Terraria/Projectile_" + ProjectileID.ExplosiveBullet;

        public override void SetDefaults() {
            projectile.CloneDefaults(ProjectileID.Bullet);
            aiType = ProjectileID.Bullet;
            projectile.height = 5;
            projectile.width = 5;
        }

        public override Color? GetAlpha(Color lightColor) {
            return new Color(lightColor.R, lightColor.G * 0.5f, lightColor.B * 0.1f, lightColor.A);
        }

        public override void Kill(int timeLeft) {
            Collision.HitTiles(projectile.position + projectile.velocity, projectile.velocity, projectile.width, projectile.height);
            Main.PlaySound(SoundID.Item10, projectile.position);
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {
            if (!target.friendly && target.damage > 0 && target.life <= 0 && (crit || Main.rand.NextBool(5))) {
                for (int k = 0; k < 4; k++) {
                    Vector2 velocity = Main.rand.NextVector2Unit() * Utils.NextFloat(Main.rand, 3f, 5f);
                    Projectile.NewProjectile(target.position, velocity, ModContent.ProjectileType<SIVANanite>(), 20, 0, Main.LocalPlayer.whoAmI);
                }
            }
        }
    }
}