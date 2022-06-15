using DestinyMod.Common.Items;
using DestinyMod.Common.Items.Modifiers;
using DestinyMod.Common.ModPlayers;
using Terraria;
using Microsoft.Xna.Framework;

namespace DestinyMod.Content.Items.Perks.Weapon.Traits
{
    public class MovingTarget : ItemPerk
    {
        public override bool IsInstanced => true;

        public override void SetDefaults()
        {   
            DisplayName = "Moving Target";
            Description = "Increased movement speed and range when moving while using this weapon.";
            //Increased movement speed and target acquisition when moving while aiming down sights.
        }

        public override void Update(Player player)
        {
            ItemDataPlayer itemDataPlayer = player.GetModPlayer<ItemDataPlayer>();
            if (itemDataPlayer.Range >= 0 && player.velocity != Vector2.Zero)
            {
                itemDataPlayer.Range += 10;
                player.moveSpeed += 0.03f;
            }
        }
    }
}