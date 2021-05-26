using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace TheDestinyMod.Projectiles.Ammo
{
    public class GoldenGunShot : ModProjectile
    {
        public override string Texture => "Terraria/Projectile_" + ProjectileID.DD2FlameBurstTowerT1Shot;

        public override void SetDefaults() {
            projectile.CloneDefaults(ProjectileID.DD2FlameBurstTowerT1Shot);
            aiType = ProjectileID.DD2FlameBurstTowerT1Shot;
        }

        public override bool OnTileCollide(Vector2 oldVelocity) {
            Collision.HitTiles(projectile.position + projectile.velocity, projectile.velocity, projectile.width, projectile.height);
            Main.PlaySound(SoundID.Item10, projectile.position);
            return true;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {
            if (target.friendly == false && target.damage > 0 && target.life <= 0) {
                Item.NewItem(target.Hitbox, ModContent.ItemType<Items.OrbOfPower>());
            }
        }
    }
}