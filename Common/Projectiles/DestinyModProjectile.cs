using DestinyMod.Common.GlobalProjectiles;
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

        public static void NewAmmoReturnProjectile(IEntitySource source, Vector2 position, Vector2 velocity, int Type, int Damage, float KnockBack, int Owner = 255, float ai0 = 0f, float ai1 = 0f)
        {
            NewAmmoReturnProjectile(source, position.X, position.Y, velocity.X, velocity.Y, Type, Damage, KnockBack, Owner, ai0, ai1);
        }
        
        public static void NewAmmoReturnProjectile(IEntitySource source, float X, float Y, float SpeedX, float SpeedY, int Type, int Damage, float KnockBack, int Owner = 255, float ai0 = 0f, float ai1 = 0f)
        {
            Projectile destinyProj = Projectile.NewProjectileDirect(source, new Vector2(X, Y), new Vector2(SpeedX, SpeedY), Type, Damage, KnockBack, Owner, ai0, ai1);
            destinyProj.GetGlobalProjectile<AmmoReturnProjectile>().AmmoReturnSource = (EntitySource_ItemUse_WithAmmo)source;
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
        /// <param name="source">The source of the projectile. By default, pass the <c>source</c> parameter provided by <c>ModItem.Shoot()</c></param>
        /// <param name="position">The position of the projectile. By default, pass the <c>position</c> parameter provided by <c>ModItem.Shoot()</c></param>
        /// <param name="velocity">The velocity of the projectile. By default, pass the <c>speedX</c> and <c>speedY</c> parameters provided by <c>ModItem.Shoot()</c></param>
        /// <param name="damage">The damage of the projectile. By default, pass the <c>damage</c> parameter provided by <c>ModItem.Shoot()</c></param>
        /// <param name="knockBack">The knockback of the projectile. By default, pass the <c>knockBack</c> parameter provided by <c>ModItem.Shoot()</c></param>
        /// <param name="chargeTime">The amount of time the player has to hold the fusion rifle down before it fires.</param>
        /// <param name="bulletsToFire">The amount of bullets the fusion rifle should fire.</param>
        /// <param name="projectileType">The type of the projectile the fusion rifle should fire.</param>
        /// <returns>The <seealso cref="Projectile"/> fired.</returns>
        public static Projectile FireFusionProjectile(Player owner, IEntitySource source, Vector2 position, Vector2 velocity, 
            int damage, float knockBack, int chargeTime, int bulletsToFire = 6, int projectileType = ProjectileID.Bullet)
        {
            Projectile proj = Projectile.NewProjectileDirect(source, position, velocity, ModContent.ProjectileType<FusionShot>(), damage, knockBack, owner.whoAmI, bulletsToFire, projectileType);
            ((FusionShot)proj.ModProjectile).ItemAmmoType = ((EntitySource_ItemUse_WithAmmo)source).AmmoItemIdUsed;
            ((FusionShot)proj.ModProjectile).ChargeTime = chargeTime;
            return proj;
        }

        /// <summary>
        /// Determines whether or not the projectile will be in collision with a tile after X steps
        /// </summary>
        /// <param name="steps">The amount of times to simulate a velocity change for the projectile</param>
        /// <param name="velocity">The velocity to use when simulating. Defaults to the projectile's current velocity</param>
        /// <returns><see langword="true"/> if the projectile would collide with a tile; otherwise, <see langword="false"/></returns>
        public bool WillCollideInSteps(int steps, Vector2? velocity = null)
        {
            if (steps <= 0)
            {
                steps = 1;
            }

            Point futurePosition = (Projectile.position + ((velocity ?? Projectile.velocity) * steps)).ToTileCoordinates();

            if (Main.tile[futurePosition.X, futurePosition.Y] == null || !Main.tile[futurePosition.X, futurePosition.Y].HasTile || Main.tile[futurePosition.X, futurePosition.Y].IsActuated)
            {
                return false;
            }
            return true;
        }
    }
}