using DestinyMod.Common.Items;
using DestinyMod.Common.Items.Modifiers;
using DestinyMod.Common.ModPlayers;
using Terraria;

namespace DestinyMod.Content.Items.Perks.Weapon.Traits
{
    public class ZenMoment : ItemPerk
    {
        private int _timer;

        private float _multiplier;

        public override bool IsInstanced => true;

        public override void SetDefaults()
        {
            DisplayName = "Zen Moment";
            Description = "Causing damage with this weapon increases its stability.";
        }

        public void Function()
        {
            _timer = 420;
            if (_multiplier < 0.66f)
            {
                _multiplier += 0.132f;
            }
        }

        public override void ModifyHitNPC(Player player, Item item, NPC target, ref int damage, ref float knockback, ref bool crit) => Function();

        public override void ModifyHitNPCWithProj(Player player, Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection) => Function();

        public override void Update(Player player)
        {
            ItemDataPlayer itemDataPlayer = player.GetModPlayer<ItemDataPlayer>();
            if (itemDataPlayer.Stability >= 0 && _timer > 0 && ItemData.ItemDatasByID != null && ItemData.ItemDatasByID.TryGetValue(player.HeldItem.type, out ItemData itemData))
            {
                itemDataPlayer.Stability += (int)(itemData.DefaultStability * _multiplier);
                _timer--;
            }
        }
    }
}