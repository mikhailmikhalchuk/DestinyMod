using System.Collections.Generic;
using DestinyMod.Common.Items;
using DestinyMod.Common.Items.Modifiers;
using DestinyMod.Common.ModPlayers;
using Terraria;
using Microsoft.Xna.Framework;

namespace DestinyMod.Content.Items.Perks.Weapon.Traits
{
    public class PerpetualMotion : ItemPerk
    {
        private int _timer;

        private int _killTimer;

        public override bool IsInstanced => true;

        public override void SetDefaults()
        {
            DisplayName = "Perpetual Motion";
            Description = "This weapon gains bonus stability, bullet speed, and range while the wielder is in motion.";
            //This weapon gains bonus stability, handling, and reload speed while the wielder is in motion.
        }

        public override void Update(Player player)
        {
            if (player.velocity == Vector2.Zero)
            {
                _killTimer++;
            }
            else
            {
                _timer++;
                _killTimer = 0;
            }

            if (_killTimer >= 30)
            {
                _timer = 0;
            }

            ItemDataPlayer itemDataPlayer = player.GetModPlayer<ItemDataPlayer>();
            if (itemDataPlayer.Stability >= 0 && _timer > 120 && _timer < 600)
            {
                itemDataPlayer.Stability += 5;
                itemDataPlayer.Range += 5;
            }
            else if (itemDataPlayer.Stability >= 0 && _timer > 600)
            {
                itemDataPlayer.Stability += 15;
                itemDataPlayer.Range += 15;
            }
        }
    }
}