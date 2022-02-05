using Terraria;
using Terraria.ID;
using DestinyMod.Common.Projectiles;

namespace DestinyMod.Content.Projectiles.NPCs.Bosses.SepiksPrime
{
    public class ServitorBlast : DestinyModProjectile
    {
        public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.AmethystBolt;

        public override void DestinySetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.AmethystBolt);
            AIType = ProjectileID.AmethystBolt;
            Projectile.hostile = true;
            Projectile.penetrate = -1;
        }
    }
}