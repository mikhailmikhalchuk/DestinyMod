using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework.Graphics;

namespace TheDestinyMod.Projectiles.Magic
{
    public class AegisBubble : ModProjectile
    {
        public override string Texture => "Terraria/Projectile_" + ProjectileID.RocketFireworkBlue;

        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Aegis Bubble");
        }

        public override void SetDefaults() {
            projectile.CloneDefaults(ProjectileID.WoodenArrowFriendly);
            aiType = ProjectileID.WoodenArrowFriendly;
            projectile.width = 8;
            projectile.height = 14;
            projectile.friendly = true;
            projectile.magic = true;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
        }

        public override void AI() {
            Player player = Main.player[projectile.owner];
            if (!player.channel && player.whoAmI == Main.myPlayer || Main.time % 10 < 1 && !player.CheckMana(player.inventory[player.selectedItem].mana, true) && player.whoAmI == Main.myPlayer || player.GetModPlayer<DestinyPlayer>().aegisCharge > 0 && player.whoAmI == Main.myPlayer) {
                projectile.Kill();
            }
            player.itemTime = player.itemAnimation = 3;
            player.itemLocation = player.Center;
            projectile.position = player.Center;
        }

        public override void PostDraw(SpriteBatch spriteBatch, Color lightColor) {
            Lighting.AddLight(projectile.Center, Color.LightBlue.ToVector3() * 2f);
        }
    }
}