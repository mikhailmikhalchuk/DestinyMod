using DestinyMod.Common.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace DestinyMod.Content.Projectiles.Weapons.Ranged
{
    public class VoidSeekerExplosion : DestinyModProjectile
    {
		public override void SetStaticDefaults() => Main.projFrames[Projectile.type] = 12;

        public override void DestinySetDefaults()
        {
            Projectile.width = 44;
            Projectile.height = 44;
            Projectile.timeLeft = 999;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = -1;
        }

        public override void AI()
        {
            if (++Projectile.frameCounter % 5 == 0)
            {
                if (++Projectile.frame >= Main.projFrames[Projectile.type])
                {
                    Projectile.Kill();
                }
            }

            Projectile.velocity = Vector2.Zero;

            if (Main.rand.NextBool(7))
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.PurpleTorch, Projectile.velocity.X, Projectile.velocity.Y);
                dust.noGravity = true;
            }
        }

        public override bool? CanHitNPC(NPC target) => false;

        public override void PostDraw(Color lightColor) => Lighting.AddLight(Projectile.Center, Color.Purple.ToVector3() * 0.55f * Main.essScale);

        public override Color? GetAlpha(Color lightColor) => new Color(lightColor.R * 0.5f, lightColor.G * 0.1f, lightColor.B, lightColor.A);

        public override void Kill(int timeLeft)
        {
            Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
        }
    }
}