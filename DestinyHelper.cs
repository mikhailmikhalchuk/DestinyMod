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
        /// <param name="move">The projectile's current move position</param>
        /// <returns>True if the projectile found a target to lock onto. Otherwise returns false.</returns>
        public static bool HomeInOnNPC(this Projectile projectile, float distance, ref Vector2 move) {
            bool target = false;
            for (int k = 0; k < 200; k++) {
                if (Main.npc[k].active && !Main.npc[k].dontTakeDamage && !Main.npc[k].friendly && Main.npc[k].lifeMax > 5 && Main.npc[k].damage > 0 && Collision.CanHitLine(projectile.position, projectile.width, projectile.height, Main.npc[k].position, Main.npc[k].width, Main.npc[k].height)) {
                    Vector2 newMove = Main.npc[k].Center - projectile.Center;
                    float distanceTo = (float)Math.Sqrt(newMove.X * newMove.X + newMove.Y * newMove.Y);
                    if (distanceTo < distance) {
                        move = newMove;
                        distance = distanceTo;
                        target = true;
                    }
                }
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
                StructureHelper.Generator.GenerateStructure($"Structures/{structure}", location, TheDestinyMod.Instance);
                return true;
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
        Titan,
        Hunter,
        Warlock
    }
}