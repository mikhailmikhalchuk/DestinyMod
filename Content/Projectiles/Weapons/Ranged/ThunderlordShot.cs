using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using DestinyMod.Common.Projectiles.ProjectileType;
using Terraria.Audio;

namespace DestinyMod.Content.Projectiles.Weapons.Ranged
{
	public class ThunderlordShot : Bullet
    {
        public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.NanoBullet;

        public override Color? GetAlpha(Color lightColor) => new Color(lightColor.R * 0.1f, lightColor.G * 0.5f, lightColor.B, lightColor.A);
     
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Player owner = Main.player[Projectile.owner];
            if (!target.friendly && target.life <= 0 && target.damage > 0 && Projectile.owner == Main.myPlayer)
            {
                Projectile p = Projectile.NewProjectileDirect(owner.GetSource_OnHit(target), target.position - new Vector2(0, 1000), new Vector2(0, 25), ProjectileID.CultistBossLightningOrbArc, 30, 0, owner.whoAmI, new Vector2(0, 10).ToRotation(), Main.rand.Next(100));
                p.friendly = true;
                SoundEngine.PlaySound(SoundID.Item122, target.position);
            }
        }
    }
}