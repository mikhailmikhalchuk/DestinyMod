using Microsoft.Xna.Framework;
using DestinyMod.Common.Projectiles.ProjectileType;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using DestinyMod.Common.GlobalNPCs;

namespace DestinyMod.Content.Projectiles.Weapons.Ranged
{
    public class OutbreakBullet : Bullet
    {
        public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.ExplosiveBullet;

        public override Color? GetAlpha(Color lightColor) => new Color(lightColor.R, lightColor.G * 0.5f, lightColor.B * 0.1f, lightColor.A);

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Player owner = Main.player[Projectile.owner];
            if (!target.friendly && target.damage > 0)
            {
                DebuffNPC debuffNPC = target.GetGlobalNPC<DebuffNPC>();
                debuffNPC.OutbreakHitCount++;
                debuffNPC.OutbreakHitDuration = 60;
                if ((crit && target.life <= 0) || debuffNPC.OutbreakHitCount >= 12)
                {
                    debuffNPC.OutbreakHitCount = 0;
                    for (int k = 0; k < 4; k++)
                    {
                        Vector2 velocity = Main.rand.NextVector2Unit() * Utils.NextFloat(Main.rand, 3f, 5f);
                        Projectile.NewProjectile(owner.GetProjectileSource_Item(owner.HeldItem), target.Center, velocity, ModContent.ProjectileType<SIVANanite>(), 20, 0, Projectile.owner);
                    }
                }
            }
        }
    }
}