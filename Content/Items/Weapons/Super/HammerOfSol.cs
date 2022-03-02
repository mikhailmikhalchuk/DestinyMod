using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using DestinyMod.Common.Items.ItemTypes;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;

namespace DestinyMod.Content.Items.Weapons.Super
{
    public class HammerOfSol : SuperItem
    {
        public override void SetStaticDefaults() => DisplayName.SetDefault("Hammer of Sol");

        public override void DestinySetDefaults()
        {
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 35;
            Item.useAnimation = 35;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.Weapons.Super.HammerOfSol>();
            Item.shootSpeed = 20;
            Item.UseSound = SoundID.Item1;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, position, velocity, type, 50, knockback, player.whoAmI);
            return false;
        }
    }
}