using Microsoft.Xna.Framework;
using DestinyMod.Common.Projectiles.ProjectileType;
using Terraria;
using Terraria.ModLoader;

namespace DestinyMod.Content.Projectiles.Weapons.Ranged
{
    public class GravitonBullet : Bullet
    {
        public override void DestinySetDefaults() => Projectile.light = 0.5f;

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Player player = Main.player[Projectile.owner];
            if (!target.friendly && target.damage > 0 && target.life <= 0)
            {
                Projectile.NewProjectile(player.GetProjectileSource_Item(player.HeldItem), target.Center, new Vector2(0, 0), ModContent.ProjectileType<VoidSeeker>(), damage / 4, knockback, Projectile.owner);
            }
        }

        public override Color? GetAlpha(Color lightColor) => new Color(lightColor.R * 0.5f, lightColor.G * 0.1f, lightColor.B, lightColor.A);
    }
}