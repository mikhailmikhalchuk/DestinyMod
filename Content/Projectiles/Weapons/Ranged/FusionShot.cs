using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Audio;
using Terraria.Audio;
using DestinyMod.Common.Projectiles;

namespace DestinyMod.Content.Projectiles.Weapons.Ranged
{
    public class FusionShot : DestinyModProjectile
    {
        private bool SwappedData;

        private SoundEffectInstance FireSound;

        public int FireDelay
        {
            get => (int)Projectile.ai[0];
            set => Projectile.ai[0] = value;
        }

        public int ProjectileCount
		{
            get => (int)(SwappedData ? Projectile.localAI[0] : Projectile.ai[0]);
            set => Projectile.localAI[0] = value;
		}

        public int ProjectileType => (int)(SwappedData ? Projectile.localAI[1] : Projectile.ai[1]);

        public int UtilisedProjectileType => ProjectileType > 0 ? ProjectileType : ProjectileID.Bullet;


        public int ItemAmmoType;

        public int ChargeTime;


        private bool Fired;


        private int CountFires;

        private int Counter;

        public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.Bullet;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bullet");
        }

        public override void DestinySetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.Bullet);
            AIType = ProjectileID.Bullet;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.tileCollide = false;
            Projectile.hide = true;
            Projectile.penetrate = -1;
        }

		public override bool PreAI()
		{
            if (!SwappedData)
			{
                Projectile.localAI[0] = Projectile.ai[0];
                Projectile.localAI[1] = Projectile.ai[1];

                if (ProjectileCount <= 0)
                {
                    ProjectileCount = 5;
                }

                SwappedData = true;
            }
            return true;
		}

        public override void AI()
        {
            if (Counter <= 0)
            {
                FireSound = SoundEngine.PlaySound(SoundLoader.GetLegacySoundSlot(Mod, "Assets/Sounds/Item/Weapons/Ranged/FusionRifleCharge"), Projectile.Center);
            }

            Player player = Main.player[Projectile.owner];
            Projectile.position = player.MountedCenter + Projectile.velocity;
            Counter++;

            if (Projectile.owner == Main.myPlayer)
            {
                Vector2 difference = Vector2.Normalize(Main.MouseWorld - player.MountedCenter);
                Projectile.velocity = difference;
                Projectile.direction = Main.MouseWorld.X > player.position.X ? 1 : -1;
                Projectile.netUpdate = true;
            }

            int dir = Projectile.direction;
            player.ChangeDir(dir);
            player.heldProj = Projectile.whoAmI;
            player.itemAnimation = player.itemTime = 2;
            player.itemRotation = (Projectile.velocity * dir).ToRotation();

            /*if (Main.rand.NextBool(Math.Clamp(ChargeTime / Counter * 2, 1, 100)) && (float)Counter / (float)ChargeTime > 0.4f)
            {
                Dust dust = Dust.NewDustDirect(player.MountedCenter + new Vector2(0, -5) + Projectile.velocity.ToRotation().ToRotationVector2() * 40f, 1, 1, DustID.PurpleTorch, player.velocity.X * 0.4f, player.velocity.Y * 0.4f, 100, default, 1.25f);
                dust.noGravity = true;
                dust.velocity *= 2f;
                dust.velocity.Y -= 0.3f;
            }*/

            if (Counter == ChargeTime)
            {
                SoundEngine.PlaySound(SoundLoader.GetLegacySoundSlot(Mod, "Assets/Sounds/Item/Weapons/Ranged/FusionRifleFire"), Projectile.Center);
                Fired = true;

                Item ammoItem = new Item();
                ammoItem.SetDefaults(ItemAmmoType);

                if (ammoItem.consumable)
                {
                    player.ConsumeItem(ItemAmmoType);
                }
                FireProjectile();
                CountFires = 1;
            }
            else if (!player.channel && Counter < ChargeTime && !Fired)
            {
                Projectile.Kill();
            }

            if (CountFires >= 1)
            {
                if (--FireDelay <= 0 && CountFires < ProjectileCount)
                {
                    FireProjectile();

                    if (++CountFires >= ProjectileCount)
                    {
                        Projectile.Kill();
                    }
                }
            }
        }

        public void FireProjectile()
		{
            Player player = Main.player[Projectile.owner];
            Vector2 perturbedSpeed = (10 * Projectile.velocity * 2f).RotatedByRandom(MathHelper.ToRadians(15));
            Projectile.NewProjectile(player.GetProjectileSource_Item(player.HeldItem), new Vector2(Projectile.position.X, Projectile.position.Y - 5), perturbedSpeed, UtilisedProjectileType, Projectile.damage, Projectile.knockBack, player.whoAmI);
            FireDelay = 4;
        }

        public override void Kill(int timeLeft)
        {
            if (CountFires < ProjectileCount)
            {
                FireSound?.Stop(true);
                FireSound = null;
                SoundEngine.PlaySound(SoundLoader.GetLegacySoundSlot(Mod, "Assets/Sounds/Item/Weapons/Ranged/FusionRifleRelease"), Projectile.Center);
            }
            CountFires = FireDelay = 0;
        }

        public override void ModifyDamageHitbox(ref Rectangle hitbox)
        {
            hitbox = Rectangle.Empty;
        }

        public override bool PreDraw(ref Color lightColor) => false;
    }
}