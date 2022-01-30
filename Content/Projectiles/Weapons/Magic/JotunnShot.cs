using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using DestinyMod.Common.Projectiles;

namespace DestinyMod.Content.Projectiles.Weapons.Melee
{
    public class JotunnShot : DestinyModProjectile
    {
        public bool Fired { get => (int)Projectile.ai[0] != 0; set => Projectile.ai[0] = value ? 1 : 0; }

        public int Charge { get => (int)Projectile.ai[0]; set => Projectile.ai[0] = value; }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Jötunn Shot");
            ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
        }

        public override void DestinySetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.RocketI);
            AIType = ProjectileID.RocketI;
            Projectile.width = 8;
            Projectile.height = 14;
            Projectile.timeLeft = 200;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = -1;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
            Projectile.Kill();
            return true;
        }

        public override bool PreDraw(ref Color lightColor) => Fired;

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            if (player.channel && Charge < 80f && !Fired)
            {
                Projectile.position = player.Center + Projectile.velocity;
                if (Projectile.owner == Main.myPlayer)
                {
                    Projectile.velocity = Vector2.Normalize(Main.MouseWorld - player.Center);
                    Projectile.direction = Main.MouseWorld.X > player.position.X ? 1 : -1;
                    Projectile.netUpdate = true;
                }

                int dir = Projectile.direction;
                player.ChangeDir(dir); // Set player direction to where we are shooting
                player.heldProj = Projectile.whoAmI; // Update player's held projectile
                player.itemTime = 2; // Set item time to 2 frames while we are used
                player.itemAnimation = 2; // Set item animation time to 2 frames while we are used
                player.itemRotation = new Vector2(Projectile.velocity.X * dir, Projectile.velocity.Y * dir).ToRotation(); // Set the item rotation to where we are shooting
                Projectile.damage = ++Charge;

                Vector2 offset = Projectile.velocity * 10f;
                Vector2 pos = player.Center + offset - new Vector2(10);
                Vector2 dustVelocity = (Vector2.UnitX * 18f).RotatedBy(Projectile.rotation - MathHelper.PiOver2);
                Vector2 spawnPos = Projectile.Center + dustVelocity;
                Vector2 spawn = spawnPos + (Main.rand.NextFloat() * MathHelper.TwoPi).ToRotationVector2() * (12f - Charge);
                Dust dust = Dust.NewDustDirect(pos, 20, 20, DustID.Torch, Projectile.velocity.X / 2f, Projectile.velocity.Y / 2f);
                dust.velocity = Vector2.Normalize(spawnPos - spawn) * 1.5f * (10f - Charge) / 10f;
                dust.noGravity = true;
                dust.scale = Main.rand.Next(10, 20) * 0.05f;
            }
            else if (!player.channel && Charge < 80f)
            {
                int chargeMagnitude = Utils.Clamp(Charge / 20 + 1, 1, 4);
                if (!Fired)
                {
                    SoundEngine.PlaySound(SoundLoader.GetLegacySoundSlot("Sounds/Item/JotunnCharge" + chargeMagnitude), Projectile.Center);
                }
                Fired = true;

                Projectile.velocity = Projectile.velocity * 20f / 11f;
                AdjustMagnitude(ref Projectile.velocity);
                Main.projectile[Projectile.identity].aiStyle = ProjectileID.WoodenArrowFriendly;
            }
            else if (Charge >= 80f)
            {
                if (!Fired)
                {
                    SoundEngine.PlaySound(SoundLoader.GetLegacySoundSlot("Sounds/Item/JotunnCharge5"), Projectile.Center);
                }
                Fired = true;

                Projectile.velocity = Projectile.velocity * 20f / 11f;
                AdjustMagnitude(ref Projectile.velocity);

                if (Projectile.alpha > 70)
                {
                    Projectile.alpha -= 15;
                    if (Projectile.alpha < 70)
                    {
                        Projectile.alpha = 70;
                    }
                }

                HomeInOnNPC(400f, 20f);
            }
        }

        public override void Kill(int timeLeft)
        {
            if (Charge >= 80f)
            {
                SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
                Projectile.position = Projectile.Center;
                Projectile.width = 11;
                Projectile.height = 11;
                Projectile.position.X -= Projectile.width / 2;
                Projectile.position.Y -= Projectile.height / 2;

                for (int i = 0; i < 20; i++)
                {
                    Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 100, default, 3.5f);
                    dust.noGravity = true;
                    dust.velocity *= 7f;
                    dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 100, default, 1.5f);
                    dust.velocity *= 3f;
                }
            }
        }

        public override Color? GetAlpha(Color lightColor) => new Color(lightColor.R, lightColor.G * 0.75f, lightColor.B * 0.55f, lightColor.A);

        public override void ModifyDamageHitbox(ref Rectangle hitbox) => hitbox = Fired ? Projectile.Hitbox : Rectangle.Empty;

        public static void AdjustMagnitude(ref Vector2 vector)
        {
            float length = vector.Length();
            if (length > 20f)
            {
                vector *= 20f / length;
            }
        }
    }
}