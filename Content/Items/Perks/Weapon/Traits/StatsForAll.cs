using System.Collections.Generic;
using DestinyMod.Common.Items;
using DestinyMod.Common.Items.Modifiers;
using DestinyMod.Common.ModPlayers;
using Terraria;

namespace DestinyMod.Content.Items.Perks.Weapon.Traits
{
    public class StatsForAll : ItemPerk
    {
        private int _timer;

        private int _durationTimer;

        private float _multiplier;

        private List<int> _hitNPCs = new List<int>();

        public override bool IsInstanced => true;

        public override void SetDefaults()
        {
            DisplayName = "Stats for All";
            Description = "Hitting three separate targets increases weapon bullet speed, stability, and range for a moderate duration.";
        }

        public void Function(NPC npc)
        {
            if (!_hitNPCs.Contains(npc.whoAmI) && npc.damage > 0 && npc.lifeMax > 5 && !npc.friendly)
            {
                _hitNPCs.Add(npc.whoAmI);
                _timer = 180;
            }
            if (_hitNPCs.Count >= 3 && _durationTimer <= 0)
            {
                _durationTimer = 600;
                _hitNPCs.Clear();
            }
        }

        public override void ModifyHitNPC(Player player, Item item, NPC target, ref int damage, ref float knockback, ref bool crit) => Function(target);

        public override void ModifyHitNPCWithProj(Player player, Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection) => Function(target);

        public override void Update(Player player)
        {
            ItemDataPlayer itemDataPlayer = player.GetModPlayer<ItemDataPlayer>();
            if (itemDataPlayer.Stability >= 0 && _durationTimer > 0)
            {
                itemDataPlayer.Stability += 10;
                itemDataPlayer.Range += 10;
            }

            if (_timer <= 0)
            {
                _hitNPCs.Clear();
            }
            _timer--;
        }
    }
}