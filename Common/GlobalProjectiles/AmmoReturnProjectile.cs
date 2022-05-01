using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;

namespace DestinyMod.Common.GlobalProjectiles
{
    public class AmmoReturnProjectile : GlobalProjectile
    {
        public EntitySource_ItemUse_WithAmmo AmmoReturnSource;

        private bool HasHitEntity;

        public override bool InstancePerEntity => true;

        public override bool OnTileCollide(Projectile projectile, Vector2 oldVelocity)
        {
            Player owner = Main.player[projectile.owner];
            if (AmmoReturnSource != null && Main.rand.NextBool(5) && AmmoReturnSource?.AmmoItemIdUsed != 3104 && !HasHitEntity)
            {
                owner.QuickSpawnItem(AmmoReturnSource, AmmoReturnSource.AmmoItemIdUsed);
            }
            return true;
        }

        public override void OnHitNPC(Projectile projectile, NPC target, int damage, float knockback, bool crit)
        {
            HasHitEntity = true;
        }
    }
}