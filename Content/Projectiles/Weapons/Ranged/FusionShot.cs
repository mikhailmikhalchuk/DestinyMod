using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework.Audio;
using Terraria.Audio;

// We need to talk babe...
namespace DestinyMod.Content.Projectiles.Weapons.Ranged
{
    //If you are summoning this projectile in you MUST set ai[0] to the total number of bullets you want the fusion rifle to fire and ai[1] to the type of the bullet originally fired from the fusion rifle! Otherwise defaults to 5 bullets and generic bullet type
    public class FusionShot : ModProjectile
    {
        private bool fired;

        private int countFires;

        private int delayFire;

        private static SoundEffectInstance ChargeSound;

        public float ProjectileCount { get => Projectile.ai[0]; set => Projectile.ai[0] = value; }

        public float ProjectileType { get => Projectile.ai[1]; set => Projectile.ai[1] = value; }

        public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.Bullet;

        public override void SetStaticDefaults() => DisplayName.SetDefault("Bullet");

        public override void SetDefaults()
        {
            do this later god
            Projectile.CloneDefaults(ProjectileID.Bullet);
            AIType = ProjectileID.Bullet;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            // projectile.tileCollide = false;
            Projectile.hide = true;
            Projectile.penetrate = -1;
        }

        public override void ModifyDamageHitbox(ref Rectangle hitbox) => hitbox = Rectangle.Empty;

        public override bool PreDraw(ref Color lightColor) => false;

        public override void AI()
        {
            Projectile.localAI[0]++;
            if (Projectile.ai[0] <= 0)
            {
                Projectile.ai[0] = 5;
            }

            if (ChargeSound == null && !fired)
            {
                if (Main.soundVolume <= 0)
                {
                    ChargeSound = SoundLoader.GetLegacySoundSlot("Sounds/Item/FusionRifleCharge").GetRandomSound().CreateInstance();
                    ChargeSound.Volume = 0;
                    ChargeSound.Play();
                }
                else
                {
                    ChargeSound = SoundEngine.PlaySound(SoundLoader.GetLegacySoundSlot("Sounds/Item/FusionRifleCharge"), Projectile.Center);
                }
            }

            Player player = Main.player[Projectile.owner];
            Projectile.position = player.Center + Projectile.velocity;

            if (Projectile.owner == Main.myPlayer)
            {
                Vector2 difference = Vector2.Normalize(Main.MouseWorld - player.Center);
                Projectile.velocity = difference;
                Projectile.direction = Main.MouseWorld.X > player.position.X ? 1 : -1;
                Projectile.netUpdate = true;
            }

            int dir = Projectile.direction;
            player.ChangeDir(dir);
            player.heldProj = Projectile.whoAmI;
            player.itemAnimation = player.itemTime = 2;
            player.itemRotation = (float)Math.Atan2(Projectile.velocity.Y * dir, Projectile.velocity.X * dir);

            if (ChargeSound != null)
            {
                if (ChargeSound.State == SoundState.Stopped && !fired)
                {
                    SoundEngine.PlaySound(SoundLoader.GetLegacySoundSlot("Sounds/Item/FusionRifleFire"), Projectile.Center);
                    fired = true;
                    ChargeSound?.Stop();
                    ChargeSound = null;
                    // player.channel = false;
                    Vector2 perturbedSpeed = (10 * Projectile.velocity * 2f).RotatedByRandom(MathHelper.ToRadians(15));
                    Projectile.NewProjectile(player.GetProjectileSource_Item(player.HeldItem), new Vector2(Projectile.position.X, Projectile.position.Y - 5), perturbedSpeed, (int)Projectile.ai[1] > 0 ? (int)Projectile.ai[1] : ProjectileID.Bullet, Projectile.damage, Projectile.knockBack, player.whoAmI);
                    countFires = 1;
                    delayFire = 4;
                }
                else if (!player.channel && ChargeSound.State == SoundState.Playing && !fired && player.whoAmI == Main.myPlayer)
                {
                    Projectile.Kill();
                }
            }

            if (countFires >= 1)
            {
                delayFire--;
                if (delayFire <= 0 && countFires < Projectile.ai[0])
                {
                    Vector2 perturbedSpeed = (10 * Projectile.velocity * 2f).RotatedByRandom(MathHelper.ToRadians(15));
                    Projectile.NewProjectile(player.GetProjectileSource_Item(player.HeldItem), new Vector2(Projectile.position.X, Projectile.position.Y - 5), perturbedSpeed, (int)Projectile.ai[1] > 0 ? (int)Projectile.ai[1] : ProjectileID.Bullet, Projectile.damage, Projectile.knockBack, player.whoAmI);
                    countFires++;
                    delayFire = 4;
                }
                if (countFires >= Projectile.ai[0] && player.whoAmI == Main.myPlayer)
                {
                    Projectile.Kill();
                }
            }
        }

        public override void Kill(int timeLeft)
        {
            if (countFires < Projectile.ai[0])
            {
                ChargeSound?.Stop();
                ChargeSound = null;
                SoundEngine.PlaySound(SoundLoader.GetLegacySoundSlot("Sounds/Item/FusionRifleRelease"), Projectile.Center);
            }
            countFires = delayFire = 0;
        }
    }
}