using DestinyMod.Common.Items.Modifiers;
using DestinyMod.Content.Items.Perks.Weapon.Barrels;
using DestinyMod.Content.Items.Perks.Weapon.Magazines;
using DestinyMod.Content.Items.Perks.Weapon.Traits;
using Terraria.ModLoader;

namespace DestinyMod.Common.Data
{
    public static class PerkClassifications
    {
        /// <summary>
        /// Barrels for auto rifles, fusion rifles, hand cannons, linear fusion rifles, machine guns, pulse rifles, submachine guns, scout rifles, sidearms, sniper rifles, and trace rifles.
        /// </summary>
        public static ItemPerk[] GenericBarrels => new ItemPerk[]
        {
            ModContent.GetInstance<ArrowheadBrake>(),
            ModContent.GetInstance<ChamberedCompensator>(),
            ModContent.GetInstance<FlutedBarrel>(),
            ModContent.GetInstance<FullBore>(),
        };

        /// <summary>
        /// Barrels for grenade and rocket launchers.
        /// </summary>
        public static ItemPerk[] AllLauncherBarrels => new ItemPerk[]
        {

        };

        /// <summary>
        /// Hafts (barrels) for glaives.
        /// </summary>
        public static ItemPerk[] AllHafts => new ItemPerk[]
        {

        };

        /// <summary>
        /// Barrels for shotguns.
        /// </summary>
        public static ItemPerk[] AllShotgunBarrels => new ItemPerk[]
        {
            ModContent.GetInstance<BarrelShroud>(),
        };

        public static ItemPerk[] AllTraits => new ItemPerk[]
        {
            ModContent.GetInstance<Frenzy>(),
            ModContent.GetInstance<HighCaliberRounds>()
        };
    }
}
