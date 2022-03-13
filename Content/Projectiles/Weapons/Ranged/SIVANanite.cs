using DestinyMod.Common.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DestinyMod.Content.Projectiles.Weapons.Ranged
{
    public class SIVANanite : DestinyModProjectile
    {
        public float AITimer
        {
            get => Projectile.ai[0];
            set => Projectile.ai[0] = value;
        }

        public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.SnowBallFriendly;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("SIVA Nanite");
            ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
        }

        public override void DestinySetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.SnowBallFriendly);
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.ignoreWater = true;
        }

        public override void AI()
        {
            if (++AITimer < 20)
            {
                return;
            }

            int target = HomeInOnNPC(200f, 15f, false);
            if (target < 0 && Projectile.alpha < 200 && Projectile.velocity.Y > 0f)
            {
                Projectile.velocity.Y--;
                if (Projectile.velocity.Y < 0f)
                {
                    Projectile.velocity.Y = 0f;
                }
            }

            if (target >= 0)
            {
                Projectile.alpha = 0;
            }

            if (AITimer > 400)
            {
                Projectile.alpha += 2;
            }

            if (Projectile.alpha >= 255)
            {
                Projectile.Kill();
            }

            Projectile.rotation += 0.1f;
            Projectile.Center += Vector2.UnitX.RotatedBy(Projectile.rotation, Vector2.Zero) * 0.5f;
        }

        public override void ModifyDamageHitbox(ref Rectangle hitbox)
        {
            if (AITimer < 20)
            {
                hitbox = Rectangle.Empty;
            }
        }

        public override void PostDraw(Color lightColor)
        {
            Lighting.AddLight(Projectile.Center, Color.Red.ToVector3() * 0.55f * Main.essScale);
        }
    }
}