using System.Collections.Generic;
using DestinyMod.Common.Items;
using DestinyMod.Common.Items.Modifiers;
using DestinyMod.Common.ModPlayers;
using Terraria;

namespace DestinyMod.Content.Items.Perks.Weapon.Traits
{
    public class Rampage : ItemPerk
    {
        private int _timer;

        private int _hits;

        public override bool IsInstanced => true;

        public override void SetDefaults()
        {
            DisplayName = "Rampage";
            Description = "Kills with this weapon temporarily grant increased damage. Stacks 3x.";
        }

        public void Function(NPC npc, ref int damage)
        {
            if (_timer > 0)
            {
                damage = (int)(damage * (0.1f * _hits));
            }
            if (npc.damage <= 0 || npc.lifeMax <= 5 || npc.friendly)
            {
                return;
            }

            _timer = 210;
            if (_hits < 3)
            {
                _hits++;
            }
        }

        public override void ModifyHitNPC(Player player, Item item, NPC target, ref int damage, ref float knockback, ref bool crit) => Function(target, ref damage);

        public override void ModifyHitNPCWithProj(Player player, Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection) => Function(target, ref damage);

        public override void Update(Player player)
        {
            if (_timer > 0)
            {
                _timer--;
            }
        }
    }
}