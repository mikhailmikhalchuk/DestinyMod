using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheDestinyMod.Buffs;

namespace TheDestinyMod.Projectiles.Ranged
{
    public class MonteBullet : ModProjectile
    {
        public override string Texture => "Terraria/Projectile_" + ProjectileID.PartyBullet;

        public override void SetDefaults() {
            projectile.CloneDefaults(ProjectileID.Bullet);
            aiType = ProjectileID.Bullet;
        }

        public override bool OnTileCollide(Vector2 oldVelocity) {
            Collision.HitTiles(projectile.position + projectile.velocity, projectile.velocity, projectile.width, projectile.height);
            Main.PlaySound(SoundID.Item10, projectile.position);
			return true;
		}

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {
            if (!target.friendly && target.damage > 0 && target.life <= 0) {
                Main.LocalPlayer.GetModPlayer<DestinyPlayer>().monteMethod += 60;
                Main.LocalPlayer.AddBuff(ModContent.BuffType<MonteCarloMethod>(), Main.LocalPlayer.GetModPlayer<DestinyPlayer>().monteMethod, true);
            }
        }

        public override void OnHitPvp(Player target, int damage, bool crit) {
            if (target.statLife <= 0) {
                Main.LocalPlayer.GetModPlayer<DestinyPlayer>().monteMethod += 60;
                Main.LocalPlayer.AddBuff(ModContent.BuffType<MonteCarloMethod>(), Main.LocalPlayer.GetModPlayer<DestinyPlayer>().monteMethod, true);
            }
        }
    }
}