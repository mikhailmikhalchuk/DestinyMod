using DestinyMod.Common.DamageClasses;
using DestinyMod.Common.ModPlayers;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace DestinyMod.Common.Items.ItemTypes
{
    public abstract class SuperItem : DestinyModItem
    {
        public sealed override void AutomaticSetDefaults()
        {
            base.AutomaticSetDefaults();
            Item.rare = ItemRarityID.Expert;
            Item.DamageType = ModContent.GetInstance<SuperDamageClass>();
            Item.value = 0;
        }

        public override bool CanUseItem(Player player) => player.GetModPlayer<SuperPlayer>().SuperActiveTime == 0;

        public override bool? PrefixChance(int pre, UnifiedRandom rand) => false;
    }
}