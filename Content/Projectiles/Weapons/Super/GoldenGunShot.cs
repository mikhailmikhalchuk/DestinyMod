using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using DestinyMod.Common.Projectiles;
using DestinyMod.Content.Items.Buffers;

namespace DestinyMod.Content.Projectiles.Weapons.Super
{
    public class GoldenGunShot : DestinyModProjectile
    {
        public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.DD2FlameBurstTowerT1Shot;

        public override void DestinySetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.DD2FlameBurstTowerT1Shot);
            AIType = ProjectileID.DD2FlameBurstTowerT1Shot;
        }

        public override void Kill(int timeLeft)
        {
            Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
        }

        public override Color? GetAlpha(Color lightColor) => new Color(lightColor.R, lightColor.G * 0.75f, lightColor.B * 0.55f, lightColor.A);

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (!target.friendly && target.damage > 0 && target.life <= 0)
            {
                OrbOfPower orbOfPowah = Main.item[Item.NewItem(Main.player[Projectile.owner].GetItemSource_Misc(ModContent.ItemType<OrbOfPower>()), Main.player[Projectile.owner].Hitbox, ModContent.ItemType<OrbOfPower>())].ModItem as OrbOfPower;
                orbOfPowah.OrbOwner = Main.player[Projectile.owner];
            }
        }
    }
}