using DestinyMod.Common.Items.Modifiers;
using Terraria;

namespace DestinyMod.Content.Items.Perks.Weapon.Traits
{
    public class Frenzy : ItemPerk
    {
        private int _hitTimer;

        private int _hits;

        private int _prevLife;

        private bool _apply;

        public override void SetDefaults()
        {
            DisplayName = "Frenzy";
            Description = "Being in combat for an extended time increases this weapon's damage and firing speed until you are out of combat.";
        }

        public void Function(Player player, NPC target, ref int damage)
        {
            _hits++;

            if (_apply)
            {
                damage = (int)(damage * 1.15f);
            }
        }

        public override void ModifyHitNPC(Player player, Item item, NPC target, ref int damage, ref float knockback, ref bool crit) => Function(player, target, ref damage);

        public override void ModifyHitNPCWithProj(Player player, Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection) => Function(player, target, ref damage);

        public override void UseSpeedMultiplier(Player player, Item item, ref float multiplier)
        {
            if (_apply)
            {
                multiplier *= 1.2f;
            }
        }

        public override void Update(Player player)
        {
            if (_hitTimer > 0)
            {
                _hitTimer--;
                return;
            }

            _hitTimer = 300;
            if (_hits > 15 || player.statLife < _prevLife)
            {
                _apply = true;
                _hits = 0;
                return;
            }
            _apply = false;
            _prevLife = player.statLife;
        }
    }
}