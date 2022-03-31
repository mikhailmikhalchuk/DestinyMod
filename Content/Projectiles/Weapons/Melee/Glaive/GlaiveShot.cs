using System;
using Terraria;
using Terraria.ModLoader;
using DestinyMod.Common.Projectiles.ProjectileType;
using DestinyMod.Common.Items.ItemTypes;
using Microsoft.Xna.Framework;

namespace DestinyMod.Content.Projectiles.Weapons.Melee.Glaive
{
    public class GlaiveShot : Bullet
    {
        public override string Texture => "Terraria/Images/Projectile_230";

        public override void DestinySetDefaults()
        {
            Projectile.width = 18;
            Projectile.height = 20;
            Projectile.DamageType = DamageClass.Melee;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Player owner = Main.player[Projectile.owner];
            if (!target.friendly && target.damage > 5)
            {
                if (owner.HeldItem.ModItem is GlaiveItem glaiveItem && glaiveItem.GlaiveCharge < 100)
                {
                    glaiveItem.GlaiveCharge += 20;
                }
            }
        }
    }
}
