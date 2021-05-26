using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheDestinyMod.Projectiles.Pets
{
	public class Ghost : ModProjectile
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Ghost");
			Main.projFrames[projectile.type] = 10;
			Main.projPet[projectile.type] = true;
			ProjectileID.Sets.LightPet[projectile.type] = true;
		}

		public override void SetDefaults() {
			projectile.CloneDefaults(ProjectileID.Wisp);
			aiType = ProjectileID.Wisp;
			projectile.width = 42;
			projectile.height = 42;
			projectile.netImportant = true;
		}

		public override void AI() {
			Player player = Main.player[projectile.owner];
            DestinyPlayer modPlayer = player.GetModPlayer<DestinyPlayer>();
			if (!player.active) {
				projectile.active = false;
				return;
			}
			if (player.dead) {
				modPlayer.ghostPet = false;
			}
			if (modPlayer.ghostPet) {
				projectile.timeLeft = 2;
			}
		}
	}
}