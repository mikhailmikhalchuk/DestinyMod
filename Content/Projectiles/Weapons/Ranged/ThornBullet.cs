using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using DestinyMod.Common.Projectiles.ProjectileType;
using DestinyMod.Content.Items.Buffers;

namespace DestinyMod.Content.Projectiles.Weapons.Ranged
{
    public class ThornBullet : Bullet
    {
        public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.CursedBullet;

        public override void DestinySetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.Bullet);
            AIType = ProjectileID.Bullet;
            Projectile.penetrate = -1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(ModContent.BuffType<Buffs.Debuffs.NecroticRot>(), 120);
            target.DestinyNPC().necroticApplier = Main.player[Projectile.owner];
            for (int i = 0; i < Main.rand.Next(15, 31); i++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.Center, 0, 0, DustID.PoisonStaff, 0f, 0f, Alpha: 100, Scale: 0.8f);
                dust.velocity *= 1.6f;
                dust.velocity.Y -= 1f;
                dust.velocity += Projectile.velocity;
                dust.noGravity = true;
            }
            if (!target.friendly && target.damage > 0 && target.life <= 0)
            {
                int i = Item.NewItem(target.Hitbox, ModContent.ItemType<Items.Buffers.ThornRemnant>());
                (Main.item[i].ModItem as Items.Buffers.ThornRemnant).RemnantOwner = Main.player[Projectile.owner];
            }
        }

        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            Player player = Main.player[Projectile.owner];
            target.AddBuff(ModContent.BuffType<NecroticRot>(), 120);
            target.DestinyPlayer().necroticApplier = player;
            for (int i = 0; i < Main.rand.Next(15, 31); i++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.Center, 0, 0, DustID.PoisonStaff, 0f, 0f, Alpha: 100, Scale: 0.8f);
                dust.velocity *= 1.6f;
                dust.velocity.Y -= 1f;
                dust.velocity += Projectile.velocity;
                dust.noGravity = true;
            }

            if (target.statLife <= 0)
            {
                ThornRemnant thornRemnant = Main.item[Item.NewItem(target.Hitbox, ModContent.ItemType<ThornRemnant>())].ModItem as ThornRemnant;
                thornRemnant.RemnantOwner = player;
            }
        }

        public override Color? GetAlpha(Color lightColor) => new Color(lightColor.R * 0.35f, lightColor.G, lightColor.B * 0f, lightColor.A);
    }
}