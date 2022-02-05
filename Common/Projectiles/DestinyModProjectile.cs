using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace DestinyMod.Common.Projectiles
{
	public abstract class DestinyModProjectile : ModProjectile
	{
		public sealed override void SetDefaults()
		{
			AutomaticSetDefaults();
			DestinySetDefaults();
		}

		public virtual void AutomaticSetDefaults() { }

		public virtual void DestinySetDefaults() { }

        public int HomeInOnNPC(float distance, float speed, bool checkTiles = true)
        {
            int target = -1;
            for (int indexer = 0; indexer < Main.maxNPCs; indexer++)
            {
                NPC npc = Main.npc[indexer];
                if (npc.CanBeChasedBy(Projectile) && npc.damage > 0)
                {
                    if (checkTiles && !Collision.CanHitLine(Projectile.Center, 1, 1, npc.Center, 1, 1))
                    {
                        continue;
                    }

                    if (Projectile.DistanceSQ(npc.Center) <= Math.Pow(distance, 2))
                    {
                        target = indexer;
                        break;
                    }
                }
            }

            if (target >= 0)
            {
                Projectile.velocity = Projectile.DirectionTo(Main.npc[target].Center) * speed;
            }

            return target;
        }

        /// <summary>
        /// Fires a fusion rifle bullet, causing the current weapon to act like a fusion rifle. Should be called within <c>ModItem.Shoot()</c>
        /// </summary>
        /// <param name="owner">The owner of the projectile. By default, pass the <c>player</c> parameter provided by <c>ModItem.Shoot()</c></param>
        /// <param name="position">The position of the projectile. By default, pass the <c>position</c> parameter provided by <c>ModItem.Shoot()</c></param>
        /// <param name="velocity">The velocity of the projectile. By default, pass the <c>speedX</c> and <c>speedY</c> parameters provided by <c>ModItem.Shoot()</c></param>
        /// <param name="damage">The damage of the projectile. By default, pass the <c>damage</c> parameter provided by <c>ModItem.Shoot()</c></param>
        /// <param name="knockBack">The knockback of the projectile. By default, pass the <c>knockBack</c> parameter provided by <c>ModItem.Shoot()</c></param>
        /// <param name="bulletsToFire">The amount of bullets the fusion rifle should fire.</param>
        /// <param name="projectileType">The type of the projectile the fusion rifle should fire.</param>
        /// <returns>The <seealso cref="Projectile"/> fired.</returns>
        public static Projectile FireFusionProjectile(Player owner, IProjectileSource source, Vector2 position, Vector2 velocity, 
            int damage, float knockBack, int bulletsToFire = 6, int projectileType = ProjectileID.Bullet) => 
            Projectile.NewProjectileDirect(source, position, velocity, ModContent.ProjectileType<FusionShot>(), damage, knockBack, owner.whoAmI, bulletsToFire, projectileType);
    }
}