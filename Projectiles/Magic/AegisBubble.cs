using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework.Graphics;

namespace TheDestinyMod.Projectiles.Magic
{
    public class AegisBubble : ModProjectile
    {
        public override string Texture => "Terraria/Projectile_" + ProjectileID.RocketFireworkBlue;

        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Aegis Bubble");
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

        public override void AI() {
            Player player = Main.player[projectile.owner];
            player.itemTime = player.itemAnimation = 3;
            projectile.position = player.Center;
            if (!player.channel) {
                projectile.Kill();
            }
        }

        public override void PostDraw(SpriteBatch spriteBatch, Color lightColor) {
            Lighting.AddLight(projectile.Center, Color.Orange.ToVector3() * 2f);
        }
    }
}