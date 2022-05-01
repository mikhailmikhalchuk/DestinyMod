using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using DestinyMod.Common.Projectiles.ProjectileType;

namespace DestinyMod.Content.Projectiles.Weapons.Ranged
{
    public class AceBullet : Bullet
    {
        public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.GoldenBullet;

        public override Color? GetAlpha(Color lightColor) => new Color(lightColor.R * 0.9f, lightColor.G, lightColor.B * 0.4f, lightColor.A);

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Player player = Main.player[Projectile.owner];
            if (!target.friendly && target.damage > 0 && target.life <= 0)
            {
                Projectile.NewProjectile(player.GetSource_OnHit(target), target.position.X, target.position.Y, 0, 0, ProjectileID.DD2ExplosiveTrapT3Explosion, damage / 2, 0, Projectile.owner);
                SoundEngine.PlaySound(SoundID.DD2_ExplosiveTrapExplode, target.position);
            }
        }

        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            Player player = Main.player[Projectile.owner];
            if (target.statLife <= 0)
            {
                Projectile.NewProjectile(player.GetSource_OnHit(target), target.position.X, target.position.Y, 0, 0, ProjectileID.DD2ExplosiveTrapT3Explosion, damage / 2, 0, Projectile.owner);
                SoundEngine.PlaySound(SoundID.DD2_ExplosiveTrapExplode, target.position);
            }
        }
    }
}