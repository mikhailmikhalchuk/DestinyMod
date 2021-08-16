using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheDestinyMod.Buffs;

namespace TheDestinyMod.Projectiles.Ranged
{
    public class RiskrunnerBullet : ModProjectile
    {
        public override string Texture => "Terraria/Projectile_" + ProjectileID.NanoBullet;

        public override void SetDefaults() {
            projectile.CloneDefaults(ProjectileID.Bullet);
            aiType = ProjectileID.Bullet;
        }

        public override void Kill(int timeLeft) {
            Collision.HitTiles(projectile.position + projectile.velocity, projectile.velocity, projectile.width, projectile.height);
            Main.PlaySound(SoundID.Item10, projectile.position);
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {
            if (Main.rand.NextBool(10)) {
                target.AddBuff(ModContent.BuffType<Buffs.Debuffs.Conducted>(), 120);
            }
        }

        public override void OnHitPvp(Player target, int damage, bool crit) {
            if (Main.rand.NextBool(10)) {
                target.AddBuff(ModContent.BuffType<Buffs.Debuffs.Conducted>(), 120);
            }
        }
    }
}