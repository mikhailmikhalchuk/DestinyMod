using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace DestinyMod.Content.Projectiles.Weapons.Melee
{
	public class BoltCasterProjectile : AnimatedSwordProjectile
    {
        public override Color? GetAlpha(Color lightColor) => new Color(185, 237, 229, 0) * (1f - Projectile.alpha / 255f);

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (Main.rand.NextBool(10))
            {
                target.AddBuff(BuffID.Frostburn, 60);
            }
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (Main.rand.NextBool(10))
            {
                target.AddBuff(BuffID.Frostburn, 60);
            }
        }
    }
}