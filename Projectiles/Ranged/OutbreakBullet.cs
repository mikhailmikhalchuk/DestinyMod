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

        public override void Kill(int timeLeft) {
            Main.PlaySound(SoundID.Item10, projectile.position);
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {
            if (!target.friendly && target.damage > 0 && target.life <= 0 && crit || !target.friendly && target.damage > 0 && crit && Main.rand.NextBool(3)) {
                for (int k = 0; k < 4; k++) {
                    Vector2 velocity = Main.rand.NextVector2Unit() * 150f;
                    Projectile.NewProjectile(target.position, velocity, ModContent.ProjectileType<SIVANanite>(), 20, 0, Main.LocalPlayer.whoAmI);
                }
            }
        }
    }
}