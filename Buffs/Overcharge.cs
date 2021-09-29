using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;

namespace TheDestinyMod.Buffs
{
	public class Overcharge : ModBuff
	{
		public override void SetDefaults() {
			DisplayName.SetDefault("Overcharge");
			Description.SetDefault("Right-click with Vex Mythoclast to switch firing modes");
			Main.buffNoTimeDisplay[Type] = true;
			Main.buffNoSave[Type] = true;
		}

        public override void Update(Player player, ref int buffIndex) {
			player.GetModPlayer<DestinyPlayer>().overcharged = true;
			player.buffTime[buffIndex] = 18000;
			if (player.GetModPlayer<DestinyPlayer>().overchargeStacks <= 0) {
				player.ClearBuff(Type);
			}
		}

        public override void ModifyBuffTip(ref string tip, ref int rare) {
			tip += $" ({Main.LocalPlayer.GetModPlayer<DestinyPlayer>().overchargeStacks})";
        }
    }
}