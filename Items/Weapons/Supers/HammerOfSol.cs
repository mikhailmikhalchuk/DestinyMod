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
            item.melee = true;
            item.shoot = ModContent.ProjectileType<Projectiles.Super.HammerOfSol>();
            item.shootSpeed = 20;
            item.UseSound = SoundID.Item1;
        }
    }
}