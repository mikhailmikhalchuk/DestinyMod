using DestinyMod.Common.ModPlayers;
using DestinyMod.Common.Projectiles;
using Terraria;
using Terraria.ID;

namespace DestinyMod.Content.Projectiles.Pets
{
	public class Ghost : DestinyModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ghost");
			Main.projFrames[Projectile.type] = 10;
			Main.projPet[Projectile.type] = true;
			ProjectileID.Sets.LightPet[Projectile.type] = true;
		}

		public override void SetDefaults()
		{
			Projectile.CloneDefaults(ProjectileID.Wisp);
			AIType = ProjectileID.Wisp;
			Projectile.width = 42;
			Projectile.height = 42;
			Projectile.netImportant = true;
		}

		public override void AI()
		{
			Player player = Main.player[Projectile.owner];

			if (!player.active)
			{
				return;
			}

			if (player.dead)
			{
				// modPlayer.ghostPet = false;
			}

			PetPlayer petPlayer = player.GetModPlayer<PetPlayer>();
			if (petPlayer.Ghost)
			{
				Projectile.timeLeft = 2;
			}
		}
	}
}