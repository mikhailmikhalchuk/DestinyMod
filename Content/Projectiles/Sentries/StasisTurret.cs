using DestinyMod.Common.Projectiles;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DestinyMod.Content.Projectiles.Sentries
{
    public class StasisTurret : DestinyModProjectile
    {
        public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.RainbowCrystal;

        public int Timer
        {
            get => (int)Projectile.ai[1];
            set => Projectile.ai[1] = value;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Stasis Turret");

            Main.projFrames[Projectile.type] = 8;
        }

        public override void DestinySetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.sentry = true;
            Projectile.netImportant = true;
            Projectile.DamageType = DamageClass.Summon;
        }

        public override void AI()
        {
            Timer++;
            NPC target = null;
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC iterateNPC = Main.npc[i];
                if (iterateNPC.CanBeChasedBy(Projectile))
                {
                    if (Projectile.Distance(iterateNPC.Center) < 700f && Collision.CanHitLine(Projectile.Center, 0, 0, iterateNPC.Center, 0, 0))
                    {
                        target = iterateNPC;
                        Projectile.netUpdate = true;
                        break;
                    }
                }
            }
            if (target != null && (Timer == 90 || Timer == 100 || Timer == 110))
            {
                Vector2 toTarget = Projectile.DirectionTo(target.Center);
                if (Projectile.owner == Main.myPlayer)
                {
                    Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, toTarget * 15f, ProjectileID.Bullet, 10, 0, Projectile.owner);
                }
            }
            if (Timer >= 110)
            {
                Timer = 0;
            }
        }
    }
}
