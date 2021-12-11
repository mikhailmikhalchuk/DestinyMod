using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheDestinyMod.Items.Armor
{
    public abstract class ExoticArmor : ModItem
    {
        public int armorClassType;

        public override bool CanEquipAccessory(Player player, int slot) {
            if (player.DestinyPlayer().exoticEquipped) {
                return false;
            }
            return base.CanEquipAccessory(player, slot);
        }
    }
}