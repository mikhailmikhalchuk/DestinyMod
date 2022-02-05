using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using DestinyMod.Common.Projectiles.ProjectileType;

namespace DestinyMod.Content.Projectiles.Weapons.Ranged
{
    public class DeathBullet : Bullet
    {
        public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.ExplosiveBullet;

        public override Color? GetAlpha(Color lightColor) => new Color(lightColor.R, lightColor.G * 0.5f, lightColor.B * 0.1f, lightColor.A);

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (!target.friendly && target.damage > 0 && target.life <= 0)
            {
                DeathBulletHeal();
            }
        }

        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            if (target.statLife <= 0)
            {
                DeathBulletHeal();
            }
        }

        public void DeathBulletHeal()
		{
            Player player = Main.player[Projectile.owner];
            player.statLife += 5;
            for (int i = 0; i < 5; i++)
            {
                Dust.NewDust(player.Center, 10, 10, DustID.HealingPlus);
            }
        }
    }
}