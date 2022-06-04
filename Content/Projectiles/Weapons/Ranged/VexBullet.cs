using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using DestinyMod.Common.Projectiles.ProjectileType;
using DestinyMod.Common.ModPlayers;
using DestinyMod.Common.ModSystems;
using System;
using System.Collections.Generic;

namespace DestinyMod.Content.Projectiles.Weapons.Ranged
{
    public class VexBullet : Bullet
    {
        public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.Bullet;

        public override void DestinySetDefaults()
        {
            Projectile.hide = true;
        }

        public override Color? GetAlpha(Color lightColor) => new Color(lightColor.R, lightColor.G * 0.7f, lightColor.B * 0.1f, lightColor.A);

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (!target.friendly && target.damage > 0 && target.life <= 0)
            {
                ApplyOvercharge();
            }
        }

        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            if (target.statLife <= 0)
            {
                ApplyOvercharge();
            }
        }

        public override void AI()
        {
            for (int i = 0; i < 6; i++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.GemTopaz, Alpha: 100, Scale: 0.5f);
                dust.noGravity = true;
                dust.velocity *= 0.5f;
                dust.SetDustTimeLeft(4);
            }
        }

        private void ApplyOvercharge()
        {
            Player player = Main.player[Projectile.owner];
            ItemPlayer itemPlayer = player.GetModPlayer<ItemPlayer>();
            player.AddBuff(ModContent.BuffType<Buffs.Overcharge>(), 2);
            if (itemPlayer.OverchargeStacks < 6)
            {
                itemPlayer.OverchargeStacks++;
            }
        }
    }
}