using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using DestinyMod.Common.Projectiles;
using DestinyMod.Common.ModPlayers;
using System;

namespace DestinyMod.Content.Projectiles.Weapons.Magic
{
    public class ShieldThrow : DestinyModProjectile
    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.ShadowOrb;

        public override void DestinySetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.aiStyle = -1;
            Projectile.timeLeft = 400;
            Projectile.penetrate = 2;
            Projectile.friendly = true;
            Projectile.hostile = false;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (Projectile.velocity.X != oldVelocity.X && Math.Abs(oldVelocity.X) > 1f)
            {
                Projectile.velocity.X = oldVelocity.X * -0.9f;
            }

            if (Projectile.velocity.Y != oldVelocity.Y && Math.Abs(oldVelocity.Y) > 1f)
            {
                Projectile.velocity.Y = oldVelocity.Y * -0.9f;
            }
            return false;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(lightColor.R * 0.3f, lightColor.G, lightColor.B * 0.8f, lightColor.A);
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];
                if (npc.active && npc.CanBeChasedBy(Projectile) && npc.damage > 0 && Projectile.Center.Distance(npc.Center) < 400 && npc.whoAmI != target.whoAmI)
                {
                    Projectile.velocity = Projectile.DirectionTo(npc.Center) * 15f;
                    Projectile.netUpdate = true;
                    break;
                }
            }
        }
    }
}