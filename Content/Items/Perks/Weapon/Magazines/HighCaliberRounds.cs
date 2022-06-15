using DestinyMod.Common.GlobalItems;
using DestinyMod.Common.Items.Modifiers;
using Terraria;

namespace DestinyMod.Content.Items.Perks.Weapon.Magazines
{
    public class HighCaliberRounds : ItemPerk
    {
        public override void SetDefaults()
        {
            DisplayName = "High-Caliber Rounds";
            Description = "Bullets fired from this weapon have greater knockback."
                + "\n- Slightly increases range";
        }

        public override void SetItemDefaults(Item item)
        {
            ItemDataItem itemDataItem = item.GetGlobalItem<ItemDataItem>();
            if (itemDataItem.Range >= 0)
            {
                itemDataItem.Range += 5;
            }
        }

        public override void ModifyHitNPCWithProj(Player player, Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection) => knockback += 2.5f;
    }
}