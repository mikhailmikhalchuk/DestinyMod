using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace DestinyMod.Common.GlobalNPCs
{
	public class MiscNPC : GlobalNPC
	{
        public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (npc.TypeName == "Zombie" && new Rectangle(npc.Hitbox.X, npc.Hitbox.Y, npc.Hitbox.Width, 8).Intersects(projectile.Hitbox))
            {
                crit = true;
            }
        }
    }
}