using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using DestinyMod.Common.ModPlayers;
using DestinyMod.Common.Projectiles;
using DestinyMod.Content.Buffs;
using Microsoft.Xna.Framework.Audio;
using Terraria.Audio;

namespace DestinyMod.Content.Projectiles.Weapons.Ranged
{
    public class VexChargeBullet : DestinyModProjectile
    {
        public bool Fired { get => Projectile.ai[0] != 0; set => Projectile.ai[0] = value ? 1 : 0; }

        public static SoundEffectInstance Charge;

        public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.Bullet;

        public override void SetStaticDefaults() => DisplayName.SetDefault("Bullet");

        public override void DestinySetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.Bullet);
            AIType = ProjectileID.Bullet;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.hide = true;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
            return true;
        }

        public override bool PreDraw(ref Color lightColor) => Fired;

        public override void AI()
        {
            if (Charge == null && !Fired)
            {
                Charge = SoundLoader.GetLegacySoundSlot("Sounds/Item/VexMythoclastStart").GetRandomSound().CreateInstance();
                Charge.Play();
            }

            Player player = Main.player[Projectile.owner];
            if (!Fired)
            {
                Projectile.position = player.Center + Projectile.velocity;
                int dir = Projectile.direction;
                player.ChangeDir(Projectile.direction);
                player.heldProj = Projectile.whoAmI;
                player.itemAnimation = player.itemTime = 2;
                player.itemRotation = (Projectile.velocity * dir).ToRotation();
            }

            if (Projectile.owner == Main.myPlayer && !Fired)
            {
                Projectile.velocity = (Main.MouseWorld - player.Center).SafeNormalize(Vector2.Zero);
                Projectile.direction = Main.MouseWorld.X > player.position.X ? 1 : -1;
                Projectile.netUpdate = true;
            }

            if (Charge != null)
            {
                if (Charge.State == SoundState.Stopped && !Fired)
                {
                    SoundEngine.PlaySound(SoundLoader.GetLegacySoundSlot(Mod, "Sounds/Item/VexMythoclastFire"), Projectile.Center);
                    Fired = true;
                    Charge?.Stop();
                    Charge = null;

                    Projectile.velocity *= 20;
                    Projectile.tileCollide = true;
                    player.GetModPlayer<ItemPlayer>().OverchargeStacks--;
                }
                else if (!player.channel && Charge.State == SoundState.Playing && !Fired && player.whoAmI == Main.myPlayer)
                {
                    Projectile.Kill();
                }
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) => Main.player[Projectile.owner].AddBuff(ModContent.BuffType<Overcharge>(), 10);

        public override void Kill(int timeLeft)
        {
            if (!Fired)
            {
                Charge?.Stop();
                Charge = null;
            }
        }

        public override void ModifyDamageHitbox(ref Rectangle hitbox)
        {
            if (!Fired)
            {
                hitbox = Rectangle.Empty;
            }
        }
    }
}