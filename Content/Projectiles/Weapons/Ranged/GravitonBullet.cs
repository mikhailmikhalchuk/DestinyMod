using Microsoft.Xna.Framework;
using DestinyMod.Common.Projectiles.ProjectileType;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DestinyMod.Content.Projectiles.Weapons.Ranged
{
    public class GravitonBullet : Bullet
    {
        public override void DestinySetDefaults()
        {
            Projectile.light = 0.5f;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Player player = Main.player[Projectile.owner];
            if (!target.friendly && target.damage > 0 && target.life <= 0)
            {
                Projectile.NewProjectile(player.GetProjectileSource_Item(player.HeldItem), target.Center, Vector2.Zero, ModContent.ProjectileType<VoidSeeker>(), damage / 4, knockback, Projectile.owner);

                int dustCount = 40;
                for (int i = 0; i < dustCount; i++)
                {
                    int dustDistance = 100;
                    Vector2 dustPosition = target.Center + new Vector2(dustDistance).RotatedBy(360f / dustCount * i);
                    Vector2 dustVelocity = (target.Center - dustPosition).SafeNormalize(Vector2.Zero) * 10f;
                    Dust dust = Dust.NewDustPerfect(dustPosition, DustID.GemAmethyst, dustVelocity, 100, Scale: 1.3f);
                    dust.noGravity = true;
                }
            }
        }

        public override Color? GetAlpha(Color lightColor) => new Color(lightColor.R * 0.5f, lightColor.G * 0.1f, lightColor.B, lightColor.A);
    }
}