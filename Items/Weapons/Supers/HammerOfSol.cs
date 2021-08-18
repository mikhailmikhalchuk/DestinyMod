using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheDestinyMod.Items.Weapons.Supers
{
    public class HammerOfSol : SuperClass
    {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Hammer of Sol");
        }

        public override void SetSuperDefaults() {
            item.width = 48;
            item.height = 46;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.useTime = 35;
            item.useAnimation = 35;
            item.noUseGraphic = true;
            item.noMelee = true;
            item.shoot = ModContent.ProjectileType<Projectiles.Super.HammerOfSol>();
            item.shootSpeed = 20;
            item.UseSound = SoundID.Item1;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack) {
            Projectile.NewProjectile(position, new Vector2(speedX, speedY), type, 50, knockBack, player.whoAmI);
            return false;
        }
    }
}