using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using DestinyMod.Common.Projectiles;
using DestinyMod.Content.Items.Buffers;

namespace DestinyMod.Content.Projectiles.Weapons.Super
{
    public class HammerOfSol : DestinyModProjectile
    {
        public override void DestinySetDefaults()
        {
            Projectile.width = 48;
            Projectile.height = 46;
            Projectile.damage = 50;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.timeLeft = 500;
        }

        public override Color? GetAlpha(Color lightColor) => new Color(252, 148, 3, 0);

        public override void AI()
        {
            Projectile.rotation += 0.25f;
            Projectile.velocity.Y += 0.35f;
            Lighting.AddLight(Projectile.Center, Color.OrangeRed.ToVector3());
        }

        public override void Kill(int timeLeft)
        {
            Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (!target.friendly && target.damage > 0 && target.life <= 0)
            {
                OrbOfPower orbOfPowah = Main.item[Item.NewItem(Main.player[Projectile.owner].Hitbox, ModContent.ItemType<OrbOfPower>())].ModItem as OrbOfPower;
                orbOfPowah.OrbOwner = Main.player[Projectile.owner];
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Player player = Main.player[Projectile.owner];
            Point checkTile = Projectile.Center.ToTileCoordinates();
            bool youFailed = false;
            if (!WorldGen.EmptyTileCheck(checkTile.X, checkTile.X, checkTile.Y, checkTile.Y + 2))
            {
                for (int projectileCount = 0; projectileCount < Main.maxProjectiles; projectileCount++)
                {
                    Projectile otherProjectile = Main.projectile[projectileCount];
                    if (!otherProjectile.active || otherProjectile.type != ModContent.ProjectileType<Sunshot>())
                    {
                        continue;
                    }

                    if (Projectile.DistanceSQ(otherProjectile.Center) <= 40000)
                    {
                        youFailed = true;
                    }
                }

                if (!youFailed)
                {
                    Projectile.NewProjectile(player.GetProjectileSource_Misc(0), new Vector2(Projectile.position.X, Projectile.position.Y + 40), Vector2.Zero, ModContent.ProjectileType<Sunspot>(), 0, 0, Projectile.owner);
                }
            }
            return true;
        }
    }
}