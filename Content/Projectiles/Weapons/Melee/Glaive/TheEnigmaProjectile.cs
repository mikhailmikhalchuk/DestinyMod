using Terraria;
using Terraria.ID;
using DestinyMod.Common.Projectiles.ProjectileType;

namespace DestinyMod.Content.Projectiles.Weapons.Melee.Glaive
{
    public class TheEnigmaProjectile : GlaiveProjectile
    {
        public override string Texture => "Terraria/Images/Projectile_939";

        public override void DestinySetDefaults()
        {
            Projectile.width = 18;
            Projectile.height = 18;
        }
    }
}
