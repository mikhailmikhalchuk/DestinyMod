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
            projectile.timeLeft = 200;
        }

        public override bool PreAI() {
            Player player = Main.player[projectile.owner];
            DestinyPlayer dPlayer = player.GetModPlayer<DestinyPlayer>();
            if (dPlayer.aegisCharge < 30 && dPlayer.aegisCharge >= 1) {
                projectile.hide = true;
                projectile.tileCollide = false;
                projectile.position = player.Center;
                dPlayer.aegisCharge++;
                player.itemTime = player.itemAnimation = 3;
                player.itemLocation = player.Center;
                player.channel = true;
                return false;
            }
            foreach (Projectile projectileIterate in Main.projectile) {
                if (projectileIterate.type == projectile.type && projectileIterate.whoAmI != projectile.whoAmI && projectileIterate.active) {
                    projectileIterate.Kill();
                }
            }
            projectile.hide = false;
            projectile.tileCollide = true;
            projectile.damage = 100;
            if (dPlayer.aegisCharge != 0) {
                player.velocity.X -= player.direction == 1 ? 5 : -5;
            }
            dPlayer.aegisCharge = 0;
            return base.PreAI();
        }

        public override void AI() {
            projectile.HomeInOnNPC(400, 10f);
        }

        public override bool OnTileCollide(Vector2 oldVelocity) {
            Collision.HitTiles(projectile.position + projectile.velocity, projectile.velocity, projectile.width, projectile.height);
            projectile.Kill();
            return true;
        }

        public override void ModifyDamageHitbox(ref Rectangle hitbox) {
            if (Main.player[projectile.owner].GetModPlayer<DestinyPlayer>().aegisCharge < 30 && Main.player[projectile.owner].GetModPlayer<DestinyPlayer>().aegisCharge >= 1) {
                hitbox = Rectangle.Empty;
            }
        }

        public override void Kill(int timeLeft) {
            Main.PlaySound(SoundID.Item14, projectile.position);
            for (int i = 0; i < 100; i++) {
                Dust dust = Dust.NewDustDirect(projectile.Center, 6, 6, DustID.Firework_Blue, Alpha: 100, Scale: 1.3f);
                float dustX = dust.velocity.X;
                float dustY = dust.velocity.Y;
                if (dustX == 0f && dustY == 0f) {
                    dustX = 1f;
                }
                float num = (float)Math.Sqrt(dustX * dustX + dustY * dustY);
                num = 13f / num;
                dustX *= num;
                dustY *= num;
                dust.velocity *= 0.3f;
                dust.velocity.X += dustX / 1.2f;
                dust.velocity.Y += dustY / 1.2f;
                dust.noGravity = true;
            }
        }
    }
}