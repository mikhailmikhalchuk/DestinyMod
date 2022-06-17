using DestinyMod.Common.GlobalItems;
using DestinyMod.Common.Items;
using DestinyMod.Common.Items.Modifiers;
using DestinyMod.Common.ModPlayers;
using Terraria;

namespace DestinyMod.Content.Items.Perks.Weapon.KillTrackers
{
    public class EnemyTracker : ItemPerk
    {
        public override void SetDefaults()
        {
            DisplayName = "Enemy Tracker";
            Description = "This weapon tracks the number of enemies you defeated with it.";
        }

        public static void Function(Player player, NPC npc, int damage)
        {
            if (npc.damage > 0 && npc.lifeMax > 5 && !npc.friendly && damage > npc.life && player.HeldItem.TryGetGlobalItem(out ItemDataItem item))
            {
                item.EnemiesKilled++;
            }
        }

        public override void ModifyHitNPC(Player player, Item item, NPC target, ref int damage, ref float knockback, ref bool crit) => Function(player, target, damage);

        public override void ModifyHitNPCWithProj(Player player, Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection) => Function(player, target, damage);
    }
}