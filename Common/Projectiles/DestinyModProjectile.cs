using Microsoft.Xna.Framework;
using System;
using Terraria;
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
    }
}