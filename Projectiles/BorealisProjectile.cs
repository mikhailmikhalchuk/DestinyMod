using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheDestinyMod.Projectiles
{
    public class BorealisProjectile : ModProjectile
    {
        public override string Texture => "Terraria/Projectile_" + ProjectileID.Bullet;

        public override void SetDefaults() {
            projectile.CloneDefaults(ProjectileID.Bullet);
            aiType = ProjectileID.Bullet;
            projectile.hide = true;
            projectile.tileCollide = false;
        }

        public override void AI() {
            Main.NewText(projectile.ai[0]);
            Projectile p = Projectile.NewProjectileDirect(projectile.position, projectile.velocity, (int)projectile.ai[1], projectile.damage, projectile.knockBack, projectile.owner);
            p.ranged = false;
            if (projectile.ai[0] == 0f) {
                p.melee = true;
            }
            else {
                p.magic = true;
            }
            projectile.Kill();
        }

        public override void ModifyDamageHitbox(ref Rectangle hitbox) {
            hitbox = Rectangle.Empty;
        }
    }
}