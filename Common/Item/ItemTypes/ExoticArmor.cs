using DestinyMod.Common.ModPlayers;
using Terraria;

namespace DestinyMod.Common.Items.ItemTypes
{
    public abstract class ExoticArmor : ClassArmor
    {
        public override bool CanEquip(Player player) => base.CanEquip(player) && !player.GetModPlayer<ClassPlayer>().ExoticEquipped;

        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<ClassPlayer>().ExoticEquipped = true;
        }
	}
}