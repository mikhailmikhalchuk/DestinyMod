using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace TheDestinyMod.Projectiles.Super
{
    public class DawnbladeShot : ModProjectile
    {
        public override void SetDefaults() {
            projectile.CloneDefaults(ProjectileID.Bullet);
            projectile.width = 42;
            projectile.height = 46;
        }

        public override void Kill(int timeLeft) {
            Collision.HitTiles(projectile.position + projectile.velocity, projectile.velocity, projectile.width, projectile.height);
            Main.PlaySound(SoundID.Item10, projectile.position);
            for (int i = 0; i < 50; i++) {
                Dust dust = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, DustID.Fire);
                dust.velocity = Main.rand.NextVector2Unit() * Utils.NextFloat(Main.rand, 3f, 5f);
            }
        }

        public override Color? GetAlpha(Color lightColor) {
            return new Color(lightColor.R, lightColor.G * 0.75f, lightColor.B * 0.55f, lightColor.A);
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {
            if (!target.friendly && target.damage > 0 && target.life <= 0) {
                int i = Item.NewItem(Main.player[projectile.owner].Hitbox, ModContent.ItemType<Items.Buffers.OrbOfPower>());
                (Main.item[i].modItem as Items.Buffers.OrbOfPower).OrbOwner = Main.player[projectile.owner];
            }
        }

        public override void AI() {
            projectile.HomeInOnNPC(150f, 15f);
            Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.Fire);
        }
    }
}