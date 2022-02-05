using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using DestinyMod.Common.Projectiles.ProjectileType;
using Terraria.ModLoader;
using DestinyMod.Content.Buffs.Debuffs;

namespace DestinyMod.Content.Projectiles.Weapons.Ranged
{
    public class RiskrunnerBullet : Bullet
    {
        public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.NanoBullet;

        public override Color? GetAlpha(Color lightColor) => new Color(lightColor.R * 0.1f, lightColor.G * 0.5f, lightColor.B, lightColor.A);

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (Main.rand.NextBool(10))
            {
                target.AddBuff(ModContent.BuffType<Conducted>(), 120);
            }
        }

        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            if (Main.rand.NextBool(10))
            {
                target.AddBuff(ModContent.BuffType<Conducted>(), 120);
            }
        }
    }
}