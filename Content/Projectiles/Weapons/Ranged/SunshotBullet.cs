using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using DestinyMod.Common.Projectiles.ProjectileType;
using DestinyMod.Common.GlobalNPCs;

namespace DestinyMod.Content.Projectiles.Weapons.Ranged
{
    public class SunshotBullet : Bullet
    {
        public override string Texture => "Terraria/Projectile_" + ProjectileID.ExplosiveBullet;

        public override bool PreKill(int timeLeft) 
        {
            Projectile.type = ProjectileID.ExplosiveBullet;
            return base.PreKill(timeLeft);
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) => target.GetGlobalNPC<StatsNPC>().HighlightDuration = 180;

        public override Color? GetAlpha(Color lightColor) => new Color(lightColor.R, lightColor.G * 0.5f, lightColor.B * 0.1f, lightColor.A);
    }
}