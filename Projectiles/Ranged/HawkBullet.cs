using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheDestinyMod.Buffs;

namespace TheDestinyMod.Projectiles.Ranged
{
    public class HawkBullet : ModProjectile
    {
        public override string Texture => "Terraria/Projectile_" + ProjectileID.GoldenBullet;

        public override void SetDefaults() {
            projectile.CloneDefaults(ProjectileID.Bullet);
            aiType = ProjectileID.Bullet;
        }

        public override void Kill(int timeLeft) {
            Collision.HitTiles(projectile.position + projectile.velocity, projectile.velocity, projectile.width, projectile.height);
            Main.PlaySound(SoundID.Item10, projectile.position);
        }

        public override Color? GetAlpha(Color lightColor) {
            return new Color(lightColor.R * 0.9f, lightColor.G, lightColor.B * 0.4f, lightColor.A);
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {
            if (!target.friendly && target.damage > 0 && target.life <= 0) {
                Main.LocalPlayer.DestinyPlayer().pCharge += 60;
                Main.LocalPlayer.AddBuff(ModContent.BuffType<ParacausalCharge>(), Main.LocalPlayer.DestinyPlayer().pCharge, true);
            }
        }

        public override void OnHitPvp(Player target, int damage, bool crit) {
            if (target.statLife <= 0) {
                Main.LocalPlayer.DestinyPlayer().pCharge += 60;
                Main.LocalPlayer.AddBuff(ModContent.BuffType<ParacausalCharge>(), Main.LocalPlayer.DestinyPlayer().pCharge, true);
            }
        }
    }
}