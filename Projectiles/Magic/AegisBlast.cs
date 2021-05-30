using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework.Graphics;

namespace TheDestinyMod.Projectiles.Magic
{
    public class AegisBlast : ModProjectile
    {
        public override string Texture => "Terraria/Projectile_" + ProjectileID.RocketFireworkBlue;

        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Aegis Blast");
        }

        public override void SetDefaults() {
            projectile.CloneDefaults(ProjectileID.RocketI);
            aiType = ProjectileID.RocketI;
            projectile.width = 8;
            projectile.height = 14;
            projectile.friendly = true;
            projectile.magic = true;
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