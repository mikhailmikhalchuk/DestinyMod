using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Audio;
using Terraria.Audio;
using DestinyMod.Common.Projectiles;

namespace DestinyMod.Content.Projectiles.Weapons.Ranged
{
    //If you are summoning this projectile in you MUST set ai[0] to the total number of bullets you want the fusion rifle to fire and ai[1] to the type of the bullet originally fired from the fusion rifle! Otherwise defaults to 5 bullets and generic bullet type
    public class FusionShot : DestinyModProjectile
    {
        private static SoundEffectInstance ChargeSound;

        public bool SwappedData;

        public int ProjectileCount
		{
            get => (int)(SwappedData ? Projectile.localAI[0] : Projectile.ai[0]);
            set => Projectile.localAI[0] = value;
		}

        public int ProjectileType => (int)(SwappedData ? Projectile.localAI[1] : Projectile.ai[1]);

        public int UtilisedProjectileType => ProjectileType > 0 ? ProjectileType : ProjectileID.Bullet;

        private bool Fired;

        private int FireDelay
        {
            get => (int)Projectile.ai[0];
            set => Projectile.ai[0] = value;
        }

        private int CountFires
        {
            get => (int)Projectile.ai[1];
            set => Projectile.ai[1] = value;
        }

        public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.Bullet;

        public override void SetStaticDefaults() => DisplayName.SetDefault("Bullet");

        public override void DestinySetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.Bullet);
            AIType = ProjectileID.Bullet;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            // projectile.tileCollide = false;
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
            if (ChargeSound == null && !Fired)
            {
                LegacySoundStyle fusionRifleCharge = SoundLoader.GetLegacySoundSlot("Sounds/Item/FusionRifleCharge");
                if (Main.soundVolume <= 0)
                {
                    ChargeSound = fusionRifleCharge.GetRandomSound().CreateInstance();
                    ChargeSound.Volume = 0;
                    ChargeSound.Play();
                }
                else
                {
                    ChargeSound = SoundEngine.PlaySound(fusionRifleCharge, Projectile.Center);
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
            player.itemRotation = (Projectile.velocity * dir).ToRotation();

            if (ChargeSound != null)
            {
                if (ChargeSound.State == SoundState.Stopped && !Fired)
                {
                    SoundEngine.PlaySound(SoundLoader.GetLegacySoundSlot("Sounds/Item/FusionRifleFire"), Projectile.Center);
                    Fired = true;
                    ChargeSound?.Stop();
                    ChargeSound = null;

                    FireProjectile();
                    CountFires = 1;
                }
                else if (!player.channel && ChargeSound.State == SoundState.Playing && !Fired)
                {
                    Projectile.Kill();
                }
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
                ChargeSound?.Stop();
                ChargeSound = null;
                SoundEngine.PlaySound(SoundLoader.GetLegacySoundSlot("Sounds/Item/FusionRifleRelease"), Projectile.Center);
            }
            CountFires = FireDelay = 0;
        }

        public override void ModifyDamageHitbox(ref Rectangle hitbox) => hitbox = Rectangle.Empty;

        public override bool PreDraw(ref Color lightColor) => false;
    }
}