using DestinyMod.Common.Items;
using DestinyMod.Common.Items.Modifiers;
using DestinyMod.Common.ModPlayers;
using Terraria;

namespace DestinyMod.Content.Items.Perks.Weapon.Traits
{
    public class Outlaw : ItemPerk
    {
        private int _timer;

        public override bool IsInstanced => true;

        public override void SetDefaults()
        {
            DisplayName = "Outlaw";
            Description = "Precision kills greatly increase stability.";
            //Precision kills greatly decrease reload time.
        }

        public void Function(NPC npc, int damage, bool crit)
        {
            if (npc.damage <= 0 || npc.lifeMax <= 5 || npc.friendly)
            {
                return;
            }

            if (damage > npc.life && crit)
            {
                _timer = 360;
            }
        }

        public override void ModifyHitNPC(Player player, Item item, NPC target, ref int damage, ref float knockback, ref bool crit) => Function(target, damage, crit);

        public override void ModifyHitNPCWithProj(Player player, Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection) => Function(target, damage, crit);

        public override void Update(Player player)
        {
            ItemDataPlayer itemDataPlayer = player.GetModPlayer<ItemDataPlayer>();
            if (itemDataPlayer.Stability >= 0 && _timer > 0)
            {
                itemDataPlayer.Stability += 70;
            }

            if (_timer > 0)
            {
                _timer--;
            }
        }
    }
}