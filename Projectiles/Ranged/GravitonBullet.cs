using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheDestinyMod.Projectiles.Ranged
{
    public class GravitonBullet : ModProjectile
    {
        public override void SetDefaults() {
            projectile.CloneDefaults(ProjectileID.Bullet);
            projectile.width = 2;
            projectile.height = 16;
            projectile.light = 0.5f;
            aiType = ProjectileID.Bullet;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {
            Projectile.NewProjectile(target.position, new Vector2(0, 0), ModContent.ProjectileType<VoidSeeker>(), damage / 4, knockback, Main.LocalPlayer.whoAmI);
            if (!target.friendly && target.damage > 0 && target.life <= 0) {
                Projectile.NewProjectile(target.position, new Vector2(0, 0), ModContent.ProjectileType<VoidSeeker>(), damage / 4, knockback, Main.LocalPlayer.whoAmI);
            }
        }

        public override void Kill(int timeLeft) {
            Collision.HitTiles(projectile.position + projectile.velocity, projectile.velocity, projectile.width, projectile.height);
            Main.PlaySound(SoundID.Item10, projectile.position);
        }
    }
}