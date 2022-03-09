using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using DestinyMod.Common.Projectiles.ProjectileType;
using DestinyMod.Content.Buffs;

namespace DestinyMod.Content.Projectiles.Weapons.Ranged
{
    public class MonteBullet : Bullet
    {
        public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.PartyBullet;

        public override Color? GetAlpha(Color lightColor) => new Color(lightColor.R, lightColor.G * 0.1f, lightColor.B * 0.8f, lightColor.A);

        private void HandleApplyingMonteCarloMethod()
        {
            Player player = Main.player[Projectile.owner];
            int monteCarloMethod = ModContent.BuffType<MonteCarloMethod>();
            int buffIndex = player.FindBuffIndex(monteCarloMethod);
            int buffTime = buffIndex == -1 ? 60 : player.buffTime[buffIndex] + 60;
            player.AddBuff(monteCarloMethod, buffTime);
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (!target.friendly && target.damage > 0 && target.life <= 0)
            {
                HandleApplyingMonteCarloMethod();
            }
        }

        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            if (target.statLife <= 0)
            {
                HandleApplyingMonteCarloMethod();
            }
        }
    }
}