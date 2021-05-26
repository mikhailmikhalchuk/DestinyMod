using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheDestinyMod.Buffs.Debuffs;

namespace TheDestinyMod.Projectiles.Ammo
{
    public class StasisShard : ModProjectile
    {
        public override string Texture => "Terraria/Projectile_" + ProjectileID.BallofFrost;

        public override void SetDefaults() {
            projectile.width = 16;
            projectile.height = 16;
            projectile.friendly = true;
            projectile.magic = true;
            aiType = ProjectileID.Bullet;
        }

        public override bool OnTileCollide(Vector2 oldVelocity) {
            Collision.HitTiles(projectile.position + projectile.velocity, projectile.velocity, projectile.width, projectile.height);
            Main.PlaySound(SoundID.Item10, projectile.position);
			return true;
		}

        public override void OnHitPvp(Player target, int damage, bool crit) {
            if (target.HasBuff(ModContent.BuffType<DeepFreeze>()) == false) {
                target.AddBuff(ModContent.BuffType<DeepFreeze>(), 120);
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {
            if (target.HasBuff(ModContent.BuffType<DeepFreeze>()) == false) {
                target.AddBuff(ModContent.BuffType<DeepFreeze>(), 120);
            }
        }
    }
}