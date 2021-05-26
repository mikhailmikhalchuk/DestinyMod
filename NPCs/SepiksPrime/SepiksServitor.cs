using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace TheDestinyMod.NPCs.SepiksPrime
{
    public class SepiksServitor : ModNPC
    {
        private int randomFireTime = Main.rand.Next(90, 200);

        public override void SetDefaults() {
            npc.aiStyle = -1;
            npc.noGravity = true;
            npc.width = 48;
            npc.height = 48;
            npc.knockBackResist = 0f;
            npc.damage = 10;
            npc.defense = 15;
            npc.lifeMax = 200;
            npc.HitSound = SoundID.NPCHit4;
            npc.lavaImmune = true;
            npc.noTileCollide = true;
        }

        public override void AI() {
            npc.ai[0]++;
            npc.TargetClosest(true);
            Player target = Main.player[npc.target];
            npc.rotation = (float)Math.Atan2(npc.position.Y + (float)npc.height - 59f - target.position.Y - (float)(target.height/2), npc.position.X + (float)(npc.width/2) - target.position.X - (float)(target.width/2)) + (float)Math.PI / 2f;
            if (npc.ai[0] >= (float)randomFireTime) {
                Vector2 delta = target.Center - npc.Center;
                float magnitude = (float)Math.Sqrt(delta.X * delta.X + delta.Y * delta.Y);
                if (magnitude > 0) {
                    delta *= 10f / magnitude;
                }
                else {
                    delta = new Vector2(0f, 5f);
                }
                Projectile.NewProjectile(npc.Center, delta, ModContent.ProjectileType<ServitorBlast>(), 20, 5, Main.myPlayer, npc.whoAmI);
                npc.netUpdate = true;
                randomFireTime = Main.rand.Next(90, 200);
                npc.ai[0] = 0f;
            }
        }
    }

    public class ServitorBlast : ModProjectile
    {
        public override string Texture => "Terraria/Projectile_" + ProjectileID.AmethystBolt;

        public override void SetDefaults() {
            projectile.CloneDefaults(ProjectileID.AmethystBolt);
            aiType = ProjectileID.AmethystBolt;
            projectile.hostile = true;
            projectile.friendly = false;
            projectile.penetrate = -1;
        }
    }
}