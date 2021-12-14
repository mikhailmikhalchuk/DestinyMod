using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheDestinyMod.Items;
using TheDestinyMod.NPCs;
using TheDestinyMod.Projectiles;

namespace TheDestinyMod
{
    public static class DestinyHelper
    {
        /// <summary>
        /// Homes in on an <see cref="NPC"/>.
        /// </summary>
        /// <param name="distance">The current distance from the <see cref="NPC"/>.</param>
        /// <param name="maxM">The projectile's max magnitude.</param>
        /// <param name="checkTiles">Whether or not to home in if there are tiles in the way. Default <see langword="true"/>.</param>
        /// <returns><see langword="true"/> if the projectile found a target to lock onto; otherwise, <see langword="false"/>.</returns>
        public static bool HomeInOnNPC(this Projectile projectile, float distance, float maxM, bool checkTiles = true) {
            bool target = false;
            Vector2 move = Vector2.Zero;
            for (int k = 0; k < 200; k++) {
                if (Main.npc[k].active && !Main.npc[k].dontTakeDamage && !Main.npc[k].friendly && Main.npc[k].lifeMax > 5 && Main.npc[k].damage > 0 && Main.npc[k].CanBeChasedBy(projectile)) {
                    if (!Collision.CanHitLine(projectile.Center, 1, 1, Main.npc[k].Center, 1, 1) && checkTiles)
                        continue;

                    Vector2 newMove = Main.npc[k].Center - projectile.Center;
                    float distanceTo = (float)Math.Sqrt(newMove.X * newMove.X + newMove.Y * newMove.Y);
                    if (distanceTo < distance) {
                        move = newMove;
                        target = true;
                        break;
                    }
                }
            }
            void AdjustMagnitude(ref Vector2 vector) {
                float magnitude = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
                if (magnitude > maxM) {
                    vector *= maxM / magnitude;
                }
            }
            if (target) {
                AdjustMagnitude(ref move);
                projectile.velocity = (10 * projectile.velocity + move) / 11f;
                AdjustMagnitude(ref projectile.velocity);
            }
            return target;
        }


        /// <summary>
        /// Used to set the tooltips of Destiny armor items. Should be called within <c>tooltips.Add()</c>
        /// </summary>
        /// <param name="classType">The <see cref="DestinyClassType"/> of the armor.</param>
        /// <returns>A <see cref="TooltipLine"/> containing the data in which to add to the tooltip; otherwise, an empty <see cref="TooltipLine"/>.</returns>
        public static TooltipLine GetRestrictedClassTooltip(DestinyClassType classType) {
            if (Main.LocalPlayer.DestinyPlayer().classType != classType && DestinyClientConfig.Instance.RestrictClassItems) {
                return new TooltipLine(TheDestinyMod.Instance, "HasClass", $"You must be a {classType} to equip this")
                {
                    overrideColor = new Color(255, 0, 0)
                };
            }
            return new TooltipLine(TheDestinyMod.Instance, "", "");
        }

        public static DestinyGlobalItem DestinyItem(this Item item) => item.GetGlobalItem<DestinyGlobalItem>();

        public static DestinyGlobalNPC DestinyNPC(this NPC npc) => npc.GetGlobalNPC<DestinyGlobalNPC>();

        public static DestinyGlobalProjectile DestinyProjectile(this Projectile projectile) => projectile.GetGlobalProjectile<DestinyGlobalProjectile>();

        public static DestinyPlayer DestinyPlayer(this Player player) => player.GetModPlayer<DestinyPlayer>();

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
        public static Projectile FireFusionProjectile(Player owner, Vector2 position, Vector2 velocity, int damage, float knockBack, int bulletsToFire = 6, int projectileType = ProjectileID.Bullet) => Projectile.NewProjectileDirect(position, velocity, ModContent.ProjectileType<Projectiles.Ranged.FusionShot>(), damage, knockBack, owner.whoAmI, bulletsToFire, projectileType);
    }

    /// <summary>Represents an armor type with a specified <see cref="DestinyClassType"/>.</summary>
    public interface IClassArmor
    {
        DestinyClassType ArmorClassType();
    }
}