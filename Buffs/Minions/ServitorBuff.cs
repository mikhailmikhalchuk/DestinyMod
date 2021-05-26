using Terraria;
using Terraria.ModLoader;

namespace TheDestinyMod.Buffs.Minions
{
	public class ServitorBuff : ModBuff
	{
		public override void SetDefaults() {
			DisplayName.SetDefault("Servitor");
			Description.SetDefault("The servitor will fight for you");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex) {
			DestinyPlayer modPlayer = player.GetModPlayer<DestinyPlayer>();
			if (player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.Minions.TinyServitor>()] > 0) {
				modPlayer.servitorMinion = true;
			}
			if (!modPlayer.servitorMinion) {
				player.DelBuff(buffIndex);
				buffIndex--;
			}
			else {
				player.buffTime[buffIndex] = 18000;
			}
		}
	}
}