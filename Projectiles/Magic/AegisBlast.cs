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

        public override bool OnTileCollide(Vector2 oldVelocity) {
            Collision.HitTiles(projectile.position + projectile.velocity, projectile.velocity, projectile.width, projectile.height);
            Main.PlaySound(SoundID.Item10, projectile.position);
            projectile.Kill();
            return true;
        }

        public override void ModifyDamageHitbox(ref Rectangle hitbox) {
            if (Main.player[projectile.owner].GetModPlayer<DestinyPlayer>().aegisCharge < 30 && Main.player[projectile.owner].GetModPlayer<DestinyPlayer>().aegisCharge >= 1) {
                hitbox = Rectangle.Empty;
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {
            Dust.NewDust(target.position, target.width, target.height, DustID.BlueCrystalShard);
        }
    }
}