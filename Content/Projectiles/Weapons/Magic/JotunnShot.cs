using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using DestinyMod.Common.Projectiles;
using System.IO;

namespace DestinyMod.Content.Projectiles.Weapons.Magic
{
    public class JotunnShot : DestinyModProjectile
    {
        public bool Fired;

        public int Charge
        {
            get => (int)Projectile.ai[1];
            set => Projectile.ai[1] = value;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Jotunn Shot");
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
            Projectile.timeLeft = 3;
            return true;
        }

        public override bool PreDraw(ref Color lightColor) => Fired;

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            if (Projectile.timeLeft == 2)
            {
                Projectile.Resize(6, 6);

                for (int i = 0; i < 20; i++)
                {
                    Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 100, default, 3.5f);
                    dust.noGravity = true;
                    dust.velocity *= 7f;
                    dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 100, default, 1.5f);
                    dust.velocity *= 3f;
                }
                return;
            }
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
                player.ChangeDir(dir);
                player.heldProj = Projectile.whoAmI;
                player.itemTime = 2;
                player.itemAnimation = 2;
                player.itemRotation = new Vector2(Projectile.velocity.X * dir, Projectile.velocity.Y * dir).ToRotation();
                Projectile.damage = ++Charge;

                Vector2 pos = player.Center + Projectile.velocity * 10f - new Vector2(10);
                Vector2 spawnPos = Projectile.Center + (Vector2.UnitX * 18f).RotatedBy(Projectile.rotation - MathHelper.PiOver2);
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
                    SoundEngine.PlaySound(SoundLoader.GetLegacySoundSlot(Mod, "Assets/Sounds/Item/Weapons/Magic/JotunnCharge" + chargeMagnitude), Projectile.Center);
                }
                Fired = true;
                Projectile.netUpdate = true;

                Projectile.velocity = Projectile.velocity * 20f / 11f;
                AdjustMagnitude(ref Projectile.velocity);
                Main.projectile[Projectile.identity].aiStyle = ProjectileID.WoodenArrowFriendly;
            }
            else if (Charge >= 80f)
            {
                if (!Fired)
                {
                    SoundEngine.PlaySound(SoundLoader.GetLegacySoundSlot(Mod, "Assets/Sounds/Item/Weapons/Magic/JotunnCharge5"), Projectile.Center);
                }
                Fired = true;
                Projectile.netUpdate = true;

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

                GradualHomeInOnNPC(400f, 20f, 0.125f);
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) => Projectile.timeLeft = 2;

        public override void Kill(int timeLeft)
        {
            if (Charge >= 80f)
            {
                SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
            }
        }

        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(Fired);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            Fired = reader.ReadBoolean();
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