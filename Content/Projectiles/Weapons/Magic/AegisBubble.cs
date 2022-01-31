using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using DestinyMod.Common.Projectiles;
using DestinyMod.Common.ModPlayers;

namespace DestinyMod.Content.Projectiles.Weapons.Magic
{
    public class AegisBubble : DestinyModProjectile
    {
        public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.RocketFireworkBlue;

        public override void DestinySetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.WoodenArrowFriendly);
            AIType = ProjectileID.WoodenArrowFriendly;
            Projectile.width = 8;
            Projectile.height = 14;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = -1;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            if (!player.channel && player.whoAmI == Main.myPlayer || Main.time % 10 == 0 && !player.CheckMana(player.inventory[player.selectedItem].mana, true) && player.whoAmI == Main.myPlayer || player.GetModPlayer<ItemPlayer>().AegisCharge > 0 && player.whoAmI == Main.myPlayer)
            {
                Projectile.Kill();
            }
            player.itemTime = player.itemAnimation = 3;
            player.itemLocation = player.Center;
            Projectile.position = player.Center;
        }

        public override void PostDraw(Color lightColor) => Lighting.AddLight(Projectile.Center, Color.LightBlue.ToVector3() * 2f);
    }
}