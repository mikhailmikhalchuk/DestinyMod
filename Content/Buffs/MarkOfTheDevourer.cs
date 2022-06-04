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
            Description.SetDefault("Thorn poison damage increased by 40%");
            Main.buffNoSave[Type] = true;
        }
	}
}