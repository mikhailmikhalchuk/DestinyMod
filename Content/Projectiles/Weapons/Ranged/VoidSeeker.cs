using DestinyMod.Common.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace DestinyMod.Content.Projectiles.Weapons.Ranged
{
    public class VoidSeeker : DestinyModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
        }

        public override void DestinySetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.SnowBallFriendly);
            Projectile.width = 12;
            Projectile.height = 12;
            Projectile.scale = 1.5f;
            Projectile.timeLeft = 360;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 10;
        }

        public override void AI()
        {
            if (Projectile.velocity.Y == 0)
            {
                Projectile.velocity.X *= 0.98f;
            }

            if (GradualHomeInOnNPC(200f, 15f, 0.05f) == -1)
            {
                Projectile.velocity.Y += 0.2f;
            }

            if (Main.rand.NextBool(7))
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.PurpleTorch, Projectile.velocity.X, Projectile.velocity.Y);
                dust.noGravity = true;
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Player player = Main.player[Projectile.owner];

            SoundEngine.PlaySound(SoundID.Item14, Projectile.position);

            Main.NewText("sussy");
            Projectile.NewProjectile(player.GetProjectileSource_Item(player.HeldItem), target.Center, Vector2.Zero, ModContent.ProjectileType<VoidSeekerExplosion>(), damage, knockback, player.whoAmI);

            if (!target.friendly && target.damage > 0 && target.life <= 0)
            {
                Projectile.NewProjectile(player.GetProjectileSource_Item(player.HeldItem), target.Center, Vector2.Zero, ModContent.ProjectileType<VoidSeeker>(), damage, knockback, player.whoAmI);
            }

            Projectile.Kill();
        }

        public override void PostDraw(Color lightColor)
        {
            Lighting.AddLight(Projectile.Center, Color.Purple.ToVector3() * 0.55f * Main.essScale);
        }

        public override Color? GetAlpha(Color lightColor) => new Color(lightColor.R * 0.5f, lightColor.G * 0.1f, lightColor.B, lightColor.A);

        public override void Kill(int timeLeft)
        {
            Projectile.Resize(44, 44);
            Projectile.Damage();
            Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
        }
    }
}