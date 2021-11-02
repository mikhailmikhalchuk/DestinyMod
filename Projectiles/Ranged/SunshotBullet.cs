using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace TheDestinyMod.Projectiles.Ranged
{
    public class SunshotBullet : ModProjectile
    {
        public override string Texture => "Terraria/Projectile_" + ProjectileID.ExplosiveBullet;

        public override void SetDefaults() {
            projectile.CloneDefaults(ProjectileID.ExplosiveBullet);
            aiType = ProjectileID.ExplosiveBullet;
        }

        public override bool PreKill(int timeLeft) {
            projectile.type = ProjectileID.ExplosiveBullet;
            return base.PreKill(timeLeft);
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {
            target.GetGlobalNPC<NPCs.DestinyGlobalNPC>().highlightedTime = 180;
        }

        public override Color? GetAlpha(Color lightColor) {
            return new Color(lightColor.R, lightColor.G * 0.5f, lightColor.B * 0.1f, lightColor.A);
        }
    }
}