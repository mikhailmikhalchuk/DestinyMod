using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace TheDestinyMod.NPCs.SepiksPrime
{
    public class SepiksBlast : ModProjectile
    {
        public override string Texture => "Terraria/Projectile_" + ProjectileID.EnchantedBoomerang;

        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Eye Blast");
        }

        public override void SetDefaults() {
            projectile.CloneDefaults(ProjectileID.EnchantedBoomerang);
            projectile.aiStyle = 0;
            projectile.hostile = true;
            projectile.friendly = false;
            projectile.penetrate = -1;
        }

        public override bool OnTileCollide(Vector2 oldVelocity) {
            Collision.HitTiles(projectile.position + projectile.velocity, projectile.velocity, projectile.width, projectile.height);
            Main.PlaySound(SoundID.Item10, projectile.position);
            projectile.Kill();
			return true;
		}
    }
}