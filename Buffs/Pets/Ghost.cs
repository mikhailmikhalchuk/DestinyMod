using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;

namespace TheDestinyMod.Buffs.Pets
{
	public class Ghost : ModBuff
	{
		public override void SetDefaults() {
			DisplayName.SetDefault("Little Light");
			DisplayName.AddTranslation(GameCulture.Polish, "Małe światło");
			Description.SetDefault("\"Eyes up, Guardian.\"");
			Description.AddTranslation(GameCulture.Polish, "Wzrok w górę Strażniku");
			Main.buffNoTimeDisplay[Type] = true;
			Main.lightPet[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex) {
			player.buffTime[buffIndex] = 18000;
			player.DestinyPlayer().ghostPet = true;
			bool petProjectileNotSpawned = player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.Pets.Ghost>()] <= 0;
			if (petProjectileNotSpawned && player.whoAmI == Main.myPlayer) {
				Projectile.NewProjectile(player.position.X + (player.width / 2), player.position.Y + (player.height / 2), 0f, 0f, ModContent.ProjectileType<Projectiles.Pets.Ghost>(), 0, 0f, player.whoAmI, 0f, 0f);
			}
		}
	}
}