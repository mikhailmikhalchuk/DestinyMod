using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace TheDestinyMod.Projectiles.Ranged
{
    //If you are summoning this projectile in you MUST set ai[0] to the total number of bullets you want the fusion rifle to fire and ai[1] to the type of the bullet originally fired from the fusion rifle! Otherwise defaults to 5 bullets and generic bullet type
    public class FusionShot : ModProjectile
    {
        private bool fired;

        private int countFires;

        private int delayFire;

        private static SoundEffectInstance charge;

        public override string Texture => "Terraria/Projectile_" + ProjectileID.Bullet;

        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Bullet");
        }

        public override void SetDefaults() {
            projectile.CloneDefaults(ProjectileID.Bullet);
            aiType = ProjectileID.Bullet;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.tileCollide = false;
            projectile.hide = true;
            projectile.penetrate = -1;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor) {
            return false;
        }

        public override void AI() {
            projectile.localAI[0]++;
            if (projectile.ai[0] <= 0) {
                projectile.ai[0] = 5;
            } 
            if (charge == null && !fired) {
                charge = mod.GetSound("Sounds/Item/FusionRifleCharge").CreateInstance();
                charge.Play();
            }
            Player player = Main.player[projectile.owner];
            projectile.position = player.Center + projectile.velocity;
            if (projectile.owner == Main.myPlayer) {
                Vector2 diff = Main.MouseWorld - player.Center;
                diff.Normalize();
                projectile.velocity = diff;
                projectile.direction = Main.MouseWorld.X > player.position.X ? 1 : -1;
                projectile.netUpdate = true;
            }
            int dir = projectile.direction;
            player.ChangeDir(dir);
            player.heldProj = projectile.whoAmI;
            player.itemAnimation = player.itemTime = 2;
            player.itemRotation = (float)Math.Atan2(projectile.velocity.Y * dir, projectile.velocity.X * dir);
            if (charge != null) {
                if (charge.State == SoundState.Stopped && !fired) {
                    Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/FusionRifleFire"), projectile.Center);
                    fired = true;
                    charge?.Stop();
                    charge = null;
                    player.channel = false;
                    Vector2 perturbedSpeed = (10 * projectile.velocity * 2f).RotatedByRandom(MathHelper.ToRadians(15));
                    Projectile.NewProjectile(new Vector2(projectile.position.X, projectile.position.Y - 5), perturbedSpeed, (int)projectile.ai[1] > 0 ? (int)projectile.ai[1] : ProjectileID.Bullet, projectile.damage, projectile.knockBack, player.whoAmI);
                    countFires = 1;
                    delayFire = 4;
                }
                else if (!player.channel && charge.State == SoundState.Playing && !fired && player.whoAmI == Main.myPlayer) {
                    projectile.Kill();
                }
            }
            if (countFires >= 1) {
                delayFire--;
                if (delayFire <= 0 && countFires < projectile.ai[0]) {
                    Vector2 perturbedSpeed = (10 * projectile.velocity * 2f).RotatedByRandom(MathHelper.ToRadians(15));
                    Projectile.NewProjectile(new Vector2(projectile.position.X, projectile.position.Y - 5), perturbedSpeed, (int)projectile.ai[1] > 0 ? (int)projectile.ai[1] : ProjectileID.Bullet, projectile.damage, projectile.knockBack, player.whoAmI);
                    countFires++;
                    delayFire = 4;
                }
                if (countFires >= projectile.ai[0] && player.whoAmI == Main.myPlayer) {
                    projectile.Kill();
                }
            }
        }

        public override void Kill(int timeLeft) {
            if (countFires < projectile.ai[0]) {
                charge?.Stop();
                charge = null;
                Main.player[projectile.owner].channel = false;
                Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/FusionRifleRelease"), projectile.Center);
            }
            countFires = delayFire = 0;
        }

        public override void ModifyDamageHitbox(ref Rectangle hitbox) {
            hitbox = Rectangle.Empty;
        }
    }
}