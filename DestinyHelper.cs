using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheDestinyMod
{
    public static class DestinyHelper
    {
        private static Mod structureHelper = ModLoader.GetMod("StructureHelper");

        /// <summary>
        /// Homes in on an NPC
        /// </summary>
        /// <param name="distance">The current distance from the NPC</param>
        /// <param name="maxM">The projectile's max magnitude</param>
        /// <param name="checkTiles">Whether or not to home in if there are tiles in the way. Default true</param>
        /// <returns>True if the projectile found a target to lock onto. Otherwise returns false.</returns>
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

        ///<summary>
        ///Used to generate a structure using StructureHelper.
        ///</summary>
        ///<param name="location">The location to place the structure</param>
        ///<param name="structure">The structure file name</param>
        ///<example>
        ///<code>
        ///StructureHelperGenerateStructure(new Vector2(100, 150), "Example");
        ///</code>
        ///</example>
        ///<returns>
        ///True if successful, otherwise false
        ///</returns>
        public static bool StructureHelperGenerateStructure(Vector2 location, string structure) {
            return StructureHelperGenerateStructure(location.ToPoint16(), structure);
        }

        ///<summary>
        ///Used to generate a structure using StructureHelper.
        ///</summary>
        ///<param name="location">The location to place the structure</param>
        ///<param name="structure">The structure file name</param>
        ///<example>
        ///<code>
        ///StructureHelperGenerateStructure(new Point16(100, 150), "Example");
        ///</code>
        ///</example>
        ///<returns>
        ///True if successful, otherwise false
        ///</returns>
        public static bool StructureHelperGenerateStructure(Terraria.DataStructures.Point16 location, string structure) {
            if (structureHelper != null) {
                return StructureHelper.Generator.GenerateStructure($"Structures/{structure}", location, TheDestinyMod.Instance);
            }
            return false;
        }
    }

    public interface IClassArmor
    {
        DestinyClassType ArmorClassType();
    }

    public enum DestinyClassType : byte
    {
        None,
        Titan,
        Hunter,
        Warlock
    }
}