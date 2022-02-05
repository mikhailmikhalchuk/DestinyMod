using Terraria;
using DestinyMod.Common.Buffs;
using DestinyMod.Common.ModPlayers;

namespace DestinyMod.Content.Buffs
{
	public class Overcharge : DestinyModBuff
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Overcharge");
			Description.SetDefault("Right-click with Vex Mythoclast to switch firing modes");
			Main.buffNoTimeDisplay[Type] = true;
			Main.buffNoSave[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.buffTime[buffIndex] = 18000;
			if (player.GetModPlayer<ItemPlayer>().OverchargeStacks <= 0)
			{
				player.ClearBuff(Type);
			}
		}

		public override void ModifyBuffTip(ref string tip, ref int rare) => tip += " (" + Main.LocalPlayer.GetModPlayer<ItemPlayer>().OverchargeStacks + ")";
	}
}