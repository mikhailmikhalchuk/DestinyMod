using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using DestinyMod.Common.Projectiles.ProjectileType;
using DestinyMod.Common.ModPlayers;

namespace DestinyMod.Content.Projectiles.Weapons.Ranged
{
    public class VexBullet : Bullet
    {
        public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.Bullet;

        public override Color? GetAlpha(Color lightColor) => new Color(lightColor.R, lightColor.G * 0.7f, lightColor.B * 0.1f, lightColor.A);

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (!target.friendly && target.damage > 0 && target.life <= 0)
            {
                Player player = Main.player[Projectile.owner];
                ItemPlayer itemPlayer = player.GetModPlayer<ItemPlayer>();
                player.AddBuff(ModContent.BuffType<Buffs.Overcharge>(), 2);
                if (itemPlayer.OverchargeStacks < 3)
                {
                    itemPlayer.OverchargeStacks++;
                }
            }
        }

        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            if (target.statLife <= 0)
            {
                Player player = Main.player[Projectile.owner];
                ItemPlayer itemPlayer = player.GetModPlayer<ItemPlayer>();
                player.AddBuff(ModContent.BuffType<Buffs.Overcharge>(), 2);
                if (itemPlayer.OverchargeStacks < 3)
                {
                    itemPlayer.OverchargeStacks++;
                }
            }
        }
    }
}