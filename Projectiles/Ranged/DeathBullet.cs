using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheDestinyMod.Projectiles.Ranged
{
    public class DeathBullet : ModProjectile
    {
        public override string Texture => "Terraria/Projectile_" + ProjectileID.ExplosiveBullet;

        public override void SetDefaults() {
            projectile.CloneDefaults(ProjectileID.Bullet);
            aiType = ProjectileID.Bullet;
        }

        public override void Kill(int timeLeft) {
            Collision.HitTiles(projectile.position + projectile.velocity, projectile.velocity, projectile.width, projectile.height);
            Main.PlaySound(SoundID.Item10, projectile.position);
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {
            if (!target.friendly && target.damage > 0 && target.life <= 0) {
                Main.LocalPlayer.statLife += 5;
                for (int i = 0; i < 5; i++) {
                    Dust.NewDust(Main.LocalPlayer.Center, 10, 10, DustID.HealingPlus);
                }
            }
        }

        public override void OnHitPvp(Player target, int damage, bool crit) {
            if (target.statLife <= 0) {
                Main.LocalPlayer.statLife += 5;
                for (int i = 0; i < 5; i++) {
                    Dust.NewDust(Main.LocalPlayer.Center, 10, 10, DustID.HealingPlus);
                }
            }
        }
    }
}