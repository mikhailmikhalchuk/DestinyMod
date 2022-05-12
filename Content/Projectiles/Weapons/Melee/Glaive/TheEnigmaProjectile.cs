using Terraria;
using Terraria.ID;
using DestinyMod.Common.Projectiles.ProjectileType;

namespace DestinyMod.Content.Projectiles.Weapons.Melee.Glaive
{
    public class TheEnigmaProjectile : GlaiveProjectile
    {
        public override void DestinySetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 32;
            SpriteWidth = 62;
            SpriteHeight = 68;
        }
    }
}
