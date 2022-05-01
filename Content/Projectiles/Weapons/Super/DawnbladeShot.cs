using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using DestinyMod.Common.Projectiles;
using DestinyMod.Content.Items.Buffers;

namespace DestinyMod.Content.Projectiles.Weapons.Super
{
    public class DawnbladeShot : DestinyModProjectile
    {
        public override void DestinySetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.Bullet);
            Projectile.width = 42;
            Projectile.height = 46;
        }

        public override void Kill(int timeLeft)
        {
            Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);

            for (int i = 0; i < 50; i++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Torch);
                dust.velocity = Main.rand.NextVector2Unit() * Utils.NextFloat(Main.rand, 3f, 5f);
            }
        }

        public override Color? GetAlpha(Color lightColor) => new Color(lightColor.R, lightColor.G * 0.75f, lightColor.B * 0.55f, lightColor.A);

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (!target.friendly && target.damage > 0 && target.life <= 0)
            {
                Player owner = Main.player[Projectile.owner];
                OrbOfPower orbOfPowah = Main.item[Item.NewItem(owner.GetSource_OnHit(target), owner.Hitbox, ModContent.ItemType<OrbOfPower>())].ModItem as OrbOfPower;
                orbOfPowah.OrbOwner = owner;
            }
        }

        public override void AI()
        {
            HomeInOnNPC(150f, 15f);
            Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Torch);
        }
    }
}