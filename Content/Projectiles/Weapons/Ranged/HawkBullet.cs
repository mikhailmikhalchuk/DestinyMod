using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using DestinyMod.Common.Projectiles.ProjectileType;
using DestinyMod.Content.Buffs;

namespace DestinyMod.Content.Projectiles.Weapons.Ranged
{
    public class HawkBullet : Bullet
    {
        public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.GoldenBullet;

        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(lightColor.R * 0.9f, lightColor.G, lightColor.B * 0.4f, lightColor.A);
        }

        private void HandleApplyingParacausalCharge()
		{
            Player player = Main.player[Projectile.owner];
            int paracausalCharge = ModContent.BuffType<ParacausalCharge>();
            int buffIndex = player.FindBuffIndex(paracausalCharge);
            int buffTime = buffIndex == -1 ? 60 : player.buffTime[buffIndex] + 60;
            player.AddBuff(paracausalCharge, buffTime);
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (!target.friendly && target.damage > 0 && target.life <= 0)
            {
                HandleApplyingParacausalCharge();
            }
        }

        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            if (target.statLife <= 0)
            {
                HandleApplyingParacausalCharge();
            }
        }
    }
}