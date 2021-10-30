using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace TheDestinyMod.Projectiles.Ranged
{
    public class ThornBullet : ModProjectile
    {
        public override string Texture => "Terraria/Projectile_" + ProjectileID.CursedBullet;

        public override void SetDefaults() {
            projectile.CloneDefaults(ProjectileID.Bullet);
            aiType = ProjectileID.Bullet;
            projectile.penetrate = -1;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = -1;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {
            target.AddBuff(ModContent.BuffType<Buffs.Debuffs.NecroticRot>(), 120);
            for (int i = 0; i < Main.rand.Next(15, 31); i++) {
                Dust dust = Dust.NewDustDirect(projectile.Center, 0, 0, DustID.PoisonStaff, 0f, 0f, Alpha: 100, Scale: 0.8f);
                dust.velocity *= 1.6f;
                dust.velocity.Y -= 1f;
                dust.velocity += projectile.velocity;
                dust.noGravity = true;
            }
            if (!target.friendly && target.damage > 0 && target.life <= 0) {
                int i = Item.NewItem(target.Hitbox, ModContent.ItemType<Items.Buffers.ThornRemnant>());
                (Main.item[i].modItem as Items.Buffers.ThornRemnant).RemnantOwner = Main.player[projectile.owner];
            }
            projectile.damage += (int)(projectile.damage * Main.player[projectile.owner].GetModPlayer<DestinyPlayer>().thornPierceAdd);
        }

        public override void OnHitPvp(Player target, int damage, bool crit) {
            target.AddBuff(ModContent.BuffType<Buffs.Debuffs.NecroticRot>(), 120);
            if (target.statLife <= 0) {
                int i = Item.NewItem(target.Hitbox, ModContent.ItemType<Items.Buffers.ThornRemnant>());
                (Main.item[i].modItem as Items.Buffers.ThornRemnant).RemnantOwner = Main.player[projectile.owner];
            }
            projectile.damage += (int)(projectile.damage * Main.player[projectile.owner].GetModPlayer<DestinyPlayer>().thornPierceAdd);
        }

        public override Color? GetAlpha(Color lightColor) {
            return new Color(lightColor.R * 0.35f, lightColor.G, lightColor.B * 0f, lightColor.A);
        }

        public override void Kill(int timeLeft) {
            Collision.HitTiles(projectile.position + projectile.velocity, projectile.velocity, projectile.width, projectile.height);
            Main.PlaySound(SoundID.Item10, projectile.position);
        }
    }
}