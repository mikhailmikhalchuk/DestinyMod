using Terraria;
using DestinyMod.Common.Buffs;
using DestinyMod.Common.ModPlayers;

namespace DestinyMod.Content.Buffs
{
    public class MarkOfTheDevourer : DestinyModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mark of the Devourer");
            Description.SetDefault("Thorn poison damage increased by ");
            Main.buffNoSave[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if (player.buffTime[buffIndex] <= 1)
            {
                player.GetModPlayer<DebuffPlayer>().NecroticDamageMult = 0f;
            }
        }

        public override void ModifyBuffTip(ref string tip, ref int rare)
        {
            tip += Main.LocalPlayer.GetModPlayer<DebuffPlayer>().NecroticDamageMult * 100 + "%";
        }
	}
}