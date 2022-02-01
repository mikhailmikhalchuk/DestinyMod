using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using DestinyMod.Common.Projectiles;

namespace DestinyMod.Content.Projectiles.NPCs.Bosses.SepiksPrime
{
    public class SepiksBlast : DestinyModProjectile
    {
        public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.EnchantedBoomerang;

        public override void SetStaticDefaults() => DisplayName.SetDefault("Eye Blast");

        public override void DestinySetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.EnchantedBoomerang);
            Projectile.aiStyle = 0;
            Projectile.hostile = true;
            Projectile.penetrate = -1;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
            Projectile.Kill();
            return true;
        }
    }
}