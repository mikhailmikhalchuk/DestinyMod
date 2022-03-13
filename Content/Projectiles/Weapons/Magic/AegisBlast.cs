using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using DestinyMod.Common.Projectiles;
using DestinyMod.Common.ModPlayers;

namespace DestinyMod.Content.Projectiles.Weapons.Magic
{
    public class AegisBlast : DestinyModProjectile
    {
        public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.RocketFireworkBlue;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Aegis Blast");
        }

        public override void DestinySetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.RocketI);
            AIType = ProjectileID.RocketI;
            Projectile.width = 8;
            Projectile.height = 14;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 200;
        }

        public override bool PreAI()
        {
            Player player = Main.player[Projectile.owner];
            ItemPlayer itemPlayer = player.GetModPlayer<ItemPlayer>();
            if (itemPlayer.AegisCharge < 30 && itemPlayer.AegisCharge >= 1)
            {
                Projectile.hide = true;
                // projectile.tileCollide = false;
                Projectile.position = player.Center;
                itemPlayer.AegisCharge++;
                player.itemTime = player.itemAnimation = 3;
                player.itemLocation = player.Center;
                player.channel = true;
                return false;
            }

            for (int projectileCount = 0; projectileCount < Main.maxProjectiles; projectileCount++)
			{
                if (Projectile.whoAmI == projectileCount)
				{
                    continue;
				}

                Projectile projectile = Main.projectile[projectileCount];
                if (projectile.active && projectile.type == Projectile.type)
                {
                    projectile.Kill();
                }
            }

            // projectile.hide = false;
            Projectile.tileCollide = true;
            Projectile.damage = 100;
            if (itemPlayer.AegisCharge != 0)
            {
                player.velocity.X -= player.direction == 1 ? 5 : -5;
            }
            itemPlayer.AegisCharge = 0;
            return base.PreAI();
        }

        public override void AI()
        {
            HomeInOnNPC(400, 10f);
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
            Projectile.Kill();
            return true;
        }

        public override void ModifyDamageHitbox(ref Rectangle hitbox)
        {
            ItemPlayer itemPlayer = Main.player[Projectile.owner].GetModPlayer<ItemPlayer>();
            if (itemPlayer.AegisCharge < 30 && itemPlayer.AegisCharge >= 1)
            {
                hitbox = Rectangle.Empty;
            }
        }

        public override void Kill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
            for (int i = 0; i < 100; i++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.Center, 6, 6, DustID.Firework_Blue, Alpha: 100, Scale: 1.3f);
                Vector2 dustVelocity = dust.velocity;
                if (dustVelocity == Vector2.Zero)
                {
                    dustVelocity.X = 1f;
                }
                float length = dustVelocity.Length();
                dustVelocity *= 13f / length;
                dust.velocity *= 0.3f;
                dust.velocity += dustVelocity / 1.2f;
                dust.noGravity = true;
            }
        }
    }
}