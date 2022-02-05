using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using DestinyMod.Common.Projectiles;

namespace DestinyMod.Content.Projectiles.Weapons.Misc
{
    public class BorealisProjectile : DestinyModProjectile
    {
        public override string Texture => "Terraria/Projectile_" + ProjectileID.Bullet;

        public override void DestinySetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.Bullet);
            AIType = ProjectileID.Bullet;
            Projectile.hide = true;
        }

        public override void AI()
        {
            Player owner = Main.player[Projectile.owner];
            Projectile newProjectile = Projectile.NewProjectileDirect(owner.GetProjectileSource_Item(owner.HeldItem), Projectile.position, Projectile.velocity, (int)Projectile.ai[1], Projectile.damage, Projectile.knockBack, Projectile.owner);
            newProjectile.DamageType = Projectile.ai[0] == 0f ? DamageClass.Melee : DamageClass.Magic;
            Projectile.Kill();
        }

        public override void Kill(int timeLeft) => SoundEngine.PlaySound(SoundID.Item10, Projectile.position);

        public override void ModifyDamageHitbox(ref Rectangle hitbox) => hitbox = Rectangle.Empty;
    }
}