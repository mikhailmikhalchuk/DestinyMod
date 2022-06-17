using DestinyMod.Common.Items;
using DestinyMod.Common.Items.Modifiers;
using DestinyMod.Common.ModPlayers;
using Terraria;

namespace DestinyMod.Content.Items.Perks.Weapon.KillTrackers
{
    public class EmptyTracker : ItemPerk
    {
        public override void SetDefaults()
        {
            DisplayName = "Tracker Disabled";
            Description = "No tracker is displayed on this weapon.";
        }
    }
}