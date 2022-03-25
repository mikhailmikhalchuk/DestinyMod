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

                for (int i = 0; i < 20; i++)
                {
                    Dust dust = Dust.NewDustDirect(target.Center, 1, 1, DustID.GemAmethyst, Alpha: 100, Scale: 1.3f);
                    Vector2 dustVelocity = dust.velocity;
                    if (dustVelocity == Vector2.Zero)
                    {
                        dustVelocity.X = 1f;
                    }
                    float length = dustVelocity.Length();
                    dustVelocity *= 13f / length;
                    dust.velocity *= 0.3f;
                    dust.velocity += dustVelocity / 10f;
                    dust.noGravity = true;
                }
            }
        }

        public override Color? GetAlpha(Color lightColor) => new Color(lightColor.R * 0.5f, lightColor.G * 0.1f, lightColor.B, lightColor.A);
    }
}