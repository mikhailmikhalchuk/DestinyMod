using DestinyMod.Content.Projectiles.Weapons.Ranged;
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

        /// <summary>
        /// Causes a <see cref="Projectile"/> to instantly home in on an <see cref="NPC"/>.
        /// </summary>
        /// <param name="distance">The distance before the <see cref="Projectile"/> homes in on an <see cref="NPC"/>.</param>
        /// <param name="speed">The speed the <see cref="Projectile"/> travels when in range of an <see cref="NPC"/>.</param>
        /// <param name="checkTiles">If <see langword="false"/>, ignores tiles when checking for an <see cref="NPC"/> to home into.</param>
        /// <returns>The position of the target in <see cref="Main.npc"/>.</returns>
        public int HomeInOnNPC(float distance, float speed, bool checkTiles = true) //this is literally useless
        {
            int target = -1;
            for (int indexer = 0; indexer < Main.maxNPCs; indexer++)
            {
                NPC npc = Main.npc[indexer];
                if (npc.CanBeChasedBy(Projectile) && npc.damage > 0 && npc.lifeMax > 5 && !npc.dontTakeDamage && !npc.friendly)
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
        /// Causes a <see cref="Projectile"/> to gradually home in on an <see cref="NPC"/>.
        /// </summary>
        /// <param name="distance">The distance before the <see cref="Projectile"/> homes in on an <see cref="NPC"/>.</param>
        /// <param name="speed">The speed the <see cref="Projectile"/> travels when in range of an <see cref="NPC"/>.</param>
        /// <param name="scale">The amount to curve (more specifically, interpolate) the <see cref="Projectile"/> when in range of an <see cref="NPC"/>.</param>
        /// <param name="checkTiles">If <see langword="false"/>, ignores tiles when checking for an <see cref="NPC"/> to home into.</param>
        /// <returns>The position of the target in <see cref="Main.npc"/>.</returns>
        public int GradualHomeInOnNPC(float distance, float speed, float scale = 0.025f, bool checkTiles = true)
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
                Projectile.velocity = Vector2.Lerp(Projectile.velocity, Projectile.DirectionTo(Main.npc[target].Center) * speed, scale);
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
        public static Projectile FireFusionProjectile(Player owner, IEntitySource source, Vector2 position, Vector2 velocity, 
            int damage, float knockBack, int bulletsToFire = 6, int projectileType = ProjectileID.Bullet) => 
            Projectile.NewProjectileDirect(source, position, velocity, ModContent.ProjectileType<FusionShot>(), damage, knockBack, owner.whoAmI, bulletsToFire, projectileType);
    }
}