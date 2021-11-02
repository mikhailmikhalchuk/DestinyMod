using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace TheDestinyMod.Items.Weapons.Supers
{
    public abstract class SuperClass : ModItem
    {
        public override bool CloneNewInstances => true;

        public sealed override void SetDefaults() {
            SetSuperDefaults();
            item.rare = ItemRarityID.Expert;
            item.ranged = false;
            item.melee = false;
            item.magic = false;
            item.summon = false;
            item.thrown = false;
            item.value = 0;
        }

        public virtual void SetSuperDefaults() {
        
        }

        public override bool CanUseItem(Player player) {
            var modPlayer = player.GetModPlayer<DestinyPlayer>();
            return modPlayer.superActiveTime == 0;
        }

        public override bool? PrefixChance(int pre, UnifiedRandom rand) {
            return false;
        }

        public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat) {
            add += player.GetModPlayer<DestinyPlayer>().superDamageAdd;
            mult *= player.GetModPlayer<DestinyPlayer>().superDamageMult;
        }

        public override void GetWeaponKnockback(Player player, ref float knockback) {
            knockback += player.GetModPlayer<DestinyPlayer>().superKnockback;
        }

        public override void GetWeaponCrit(Player player, ref int crit) {
            crit += player.GetModPlayer<DestinyPlayer>().superCrit;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips) {
            TooltipLine damageLine = tooltips.FirstOrDefault(x => x.Name == "Damage" && x.mod == "Terraria");
            if (damageLine != null) {
                string[] splitText = damageLine.text.Split(' ');
                damageLine.text = splitText.First() + " super " + splitText.Last();
            }
        }
    }
}