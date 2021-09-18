using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace TheDestinyMod.Projectiles.Super
{
    public class GoldenGunShot : ModProjectile
    {
        public override string Texture => "Terraria/Projectile_" + ProjectileID.DD2FlameBurstTowerT1Shot;

        public override void SetDefaults() {
            projectile.CloneDefaults(ProjectileID.DD2FlameBurstTowerT1Shot);
            aiType = ProjectileID.DD2FlameBurstTowerT1Shot;
        }

        public override void Kill(int timeLeft) {
            Collision.HitTiles(projectile.position + projectile.velocity, projectile.velocity, projectile.width, projectile.height);
            Main.PlaySound(SoundID.Item10, projectile.position);
        }

        public override Color? GetAlpha(Color lightColor) {
            return new Color(lightColor.R, lightColor.G * 0.75f, lightColor.B * 0.55f, lightColor.A);
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {
            if (!target.friendly && target.damage > 0 && target.life <= 0) {
                int i = Item.NewItem(Main.player[projectile.owner].Hitbox, ModContent.ItemType<Items.OrbOfPower>());
                (Main.item[i].modItem as Items.OrbOfPower).OrbOwner = Main.player[projectile.owner];
            }
        }
    }
}