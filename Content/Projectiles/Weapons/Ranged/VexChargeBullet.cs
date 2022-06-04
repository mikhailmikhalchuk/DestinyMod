using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using DestinyMod.Common.ModPlayers;
using DestinyMod.Common.ModSystems;
using DestinyMod.Common.Projectiles;
using DestinyMod.Content.Buffs;
using Microsoft.Xna.Framework.Audio;
using Terraria.Audio;
using ReLogic.Utilities;

namespace DestinyMod.Content.Projectiles.Weapons.Ranged
{
    public class VexChargeBullet : DestinyModProjectile
    {
        public bool Fired { get => Projectile.ai[0] != 0; set => Projectile.ai[0] = value ? 1 : 0; }

        private SlotId FireSound;

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
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.hide = true;
        }

        public override void AI()
        {
            if (Counter <= 0)
            {
                FireSound = SoundEngine.PlaySound(new SoundStyle("DestinyMod/Assets/Sounds/Item/Weapons/Ranged/VexMythoclastStart"), Projectile.Center);
            }

            Player player = Main.player[Projectile.owner];
            Counter++;

            if (!Fired)
            {
                Projectile.position = player.MountedCenter - new Vector2(0, 4) + Projectile.velocity;
                int dir = Projectile.direction;
                player.ChangeDir(Projectile.direction);
                player.heldProj = Projectile.whoAmI;
                player.itemAnimation = player.itemTime = 2;
                player.itemRotation = (Projectile.velocity * dir).ToRotation();
            }
            if (Projectile.owner == Main.myPlayer && !Fired)
            {
                Projectile.velocity = (Main.MouseWorld - player.MountedCenter).SafeNormalize(Vector2.Zero);
                Projectile.direction = Main.MouseWorld.X > player.position.X ? 1 : -1;
                Projectile.netUpdate = true;
            }

            if (Counter == 90)
            {
                SoundEngine.PlaySound(new SoundStyle("DestinyMod/Assets/Sounds/Item/Weapons/Ranged/VexMythoclastFire"), Projectile.Center);
                Fired = true;
                player.channel = false;

                Item ammoItem = new Item();
                ammoItem.SetDefaults((int)Projectile.ai[1]);

                if (ammoItem.consumable)
                {
                    player.ConsumeItem((int)Projectile.ai[1]);
                }

                Projectile.velocity *= 20;
                Projectile.tileCollide = true;
                player.GetModPlayer<ItemPlayer>().OverchargeStacks -= 2;
            }
            else if (!player.channel && Counter < 90 && !Fired)
            {
                Projectile.Kill();
            }

            if (Fired)
            {
                for (int i = 0; i < 6; i++)
                {
                    Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.GemRuby, Alpha: 100, Scale: 0.5f);
                    dust.noGravity = true;
                    dust.velocity *= 0.5f;
                    dust.SetDustTimeLeft(4);
                }
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (!target.friendly && target.damage > 0 && target.life <= 0)
            {
                Player player = Main.player[Projectile.owner];
                ItemPlayer itemPlayer = player.GetModPlayer<ItemPlayer>();
                player.AddBuff(ModContent.BuffType<Overcharge>(), 2);
                if (itemPlayer.OverchargeStacks < 6)
                {
                    itemPlayer.OverchargeStacks++;
                }
            }
        }

        public override void Kill(int timeLeft)
        {
            if (!Fired)
            {
                SoundEngine.TryGetActiveSound(FireSound, out ActiveSound fireResult);
                fireResult?.Stop();
            }
        }

        public override void ModifyDamageHitbox(ref Rectangle hitbox)
        {
            if (!Fired)
            {
                hitbox = Rectangle.Empty;
            }
        }

        public override bool PreDraw(ref Color lightColor) => false;
    }
}