using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheDestinyMod.Buffs;

namespace TheDestinyMod.Projectiles
{
    public class DestinyGlobalProjectile : GlobalProjectile
    {
        public override void OnHitNPC(Projectile projectile, NPC target, int damage, float knockback, bool crit) {
            DestinyPlayer player = Main.LocalPlayer.DestinyPlayer();
            if (target.TypeName == "Zombie" && player.zavalaBounty == 1 && player.zavalaEnemies < 100 && target.life <= 1 && projectile.owner == Main.LocalPlayer.whoAmI) {
				player.zavalaEnemies++;
			}
            if (target.TypeName == "Skeleton" && player.zavalaBounty == 3 && player.zavalaEnemies < 50 && target.life <= 1 && projectile.owner == Main.LocalPlayer.whoAmI) {
                player.zavalaEnemies++;
            }
        }
    }
}