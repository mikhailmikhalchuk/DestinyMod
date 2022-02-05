using Terraria;
using Terraria.ModLoader;
using DestinyMod.Common.Buffs;
using DestinyMod.Common.ModPlayers;
using Microsoft.Xna.Framework;

namespace DestinyMod.Content.Buffs.Pets
{
	public class Ghost : DestinyModBuff
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Little Light");
			Description.SetDefault("Eyes up, Guardian.");
			Main.buffNoTimeDisplay[Type] = true;
			Main.lightPet[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.buffTime[buffIndex] = 18000;
			player.GetModPlayer<PetPlayer>().Ghost = true;
			int petProjectile = ModContent.ProjectileType<Projectiles.Pets.Ghost>();
			if (player.ownedProjectileCounts[petProjectile] <= 0 && player.whoAmI == Main.myPlayer)
			{
				Projectile.NewProjectile(player.GetProjectileSource_Misc(0), player.Center, Vector2.Zero, petProjectile, 0, 0f, player.whoAmI);
			}
		}
	}
}