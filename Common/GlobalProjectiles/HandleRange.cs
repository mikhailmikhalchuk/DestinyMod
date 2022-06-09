using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;

namespace DestinyMod.Common.GlobalProjectiles
{
    // Feel free to rename to something better 
    public class HandleRange : GlobalProjectile
    {
        public Vector2? StartingPosition = null;

        public float ProjectileRange = -1;

        public override bool InstancePerEntity => true;

        public override bool PreAI(Projectile projectile)
        {
            if (ProjectileRange >= 0 && StartingPosition == null)
            {
                StartingPosition = projectile.Center;
            }

            return true;
        }

        public override void ModifyDamageScaling(Projectile projectile, ref float damageScale)
        {
            if (!StartingPosition.HasValue)
            {
                return;
            }

            // Gets the distance travelled in whole blocks
            Vector2 startingPosInBlocks = StartingPosition.Value.ToTileCoordinates().ToVector2();
            Vector2 currentPosInBlocks = projectile.Center.ToTileCoordinates().ToVector2();
            int distanceTraversed = (int)currentPosInBlocks.Distance(startingPosInBlocks);

            // Feel free to tweak the range thingamajig however you please, this implementation is expected to only be temporary
            // Summary: Every blocks, damage drops off by 2%. The current implementation is linear for simplicity's sake
            // I.E. 5 blocks = 90% of damage, 10 blocks = 80% of damage, 50+ blocks = 1 damage
            // Range serves as a multiplier to that said 2%.
            // I.E. A weapon with 0 range recieves the full dropoff, whereas 50 range recieves only half, and 100 range recieves none
            float dropoffPreMultiplied = distanceTraversed * 0.02f;
            float dropoffAdjusted = dropoffPreMultiplied * (1 - Math.Clamp(ProjectileRange / 100f, 0, 1));
            damageScale = 1 - dropoffAdjusted;
        }
    }
}