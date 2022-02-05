using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using DestinyMod.Common.ModPlayers;

namespace DestinyMod.Common.Items.ItemTypes
{
    public abstract class ClassArmor : DestinyModItem
    {
        public abstract DestinyClassType ArmorClassType { get; }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            if (Main.LocalPlayer.GetModPlayer<ClassPlayer>().ClassType == ArmorClassType || !DestinyClientConfig.Instance.RestrictClassItems)
            {
                return;
            }

            tooltips.Add(new TooltipLine(Mod, "HasClass", "You must be a " + ArmorClassType + " to equip this")
            {
                overrideColor = new Color(255, 0, 0)
            });
        }

		public override bool CanEquip(Player player) => !DestinyClientConfig.Instance.RestrictClassItems || player.GetModPlayer<ClassPlayer>().ClassType == ArmorClassType;
	}
}