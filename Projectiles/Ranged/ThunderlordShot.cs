using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheDestinyMod.Projectiles.Ranged
{
    public class ThunderlordShot : ModProjectile
    {
        public override string Texture => "Terraria/Projectile_" + ProjectileID.NanoBullet;

        public override void SetDefaults() {
            projectile.CloneDefaults(ProjectileID.Bullet);
            aiType = ProjectileID.Bullet;
        }

        public override bool OnTileCollide(Vector2 oldVelocity) {
            Collision.HitTiles(projectile.position + projectile.velocity, projectile.velocity, projectile.width, projectile.height);
            Main.PlaySound(SoundID.Item10, projectile.position);
            return true;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {
            if (!target.friendly && target.life <= 0 && target.damage > 0) {
                Projectile p = Projectile.NewProjectileDirect(target.position - new Vector2(0, 1000), new Vector2(0, 25), ProjectileID.CultistBossLightningOrbArc, 30, 0, Main.player[projectile.owner].whoAmI, new Vector2(0, 10).ToRotation(), Main.rand.Next(100));
                p.friendly = true;
                p.hostile = false;
                Main.PlaySound(SoundID.Item122, target.position);
            }
        }
    }
}