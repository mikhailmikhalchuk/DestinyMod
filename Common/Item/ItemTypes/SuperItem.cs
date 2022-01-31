using DestinyMod.Common.ModPlayers;
using System.Collections.Generic;
using System.Linq;
using Terraria;
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
            Item.value = 0;
        }

        public override bool CanUseItem(Player player) => player.GetModPlayer<SuperPlayer>().SuperActiveTime == 0;

        public override bool? PrefixChance(int pre, UnifiedRandom rand) => false;

        public override void ModifyWeaponDamage(Player player, ref StatModifier damage, ref float flat)
        {
            SuperPlayer superPlayer = player.GetModPlayer<SuperPlayer>();
            damage += superPlayer.SuperDamageFlat;
            damage *= superPlayer.SuperDamageMultiplier;
        }

        public override void ModifyWeaponKnockback(Player player, ref StatModifier knockback, ref float flat) => knockback += player.GetModPlayer<SuperPlayer>().SuperKnockback;

        public override void ModifyWeaponCrit(Player player, ref int crit) => crit += player.GetModPlayer<SuperPlayer>().SuperCrit;

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine damageLine = tooltips.FirstOrDefault(tooltip => tooltip.Name == "Damage" && tooltip.mod == "Terraria");
            if (damageLine != null)
            {
                string[] splitText = damageLine.text.Split(' ');
                damageLine.text = splitText.First() + " super " + splitText.Last();
            }
        }
    }
}