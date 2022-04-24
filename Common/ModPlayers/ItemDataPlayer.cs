using DestinyMod.Common.GlobalItems;
using DestinyMod.Common.Items;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace DestinyMod.Common.ModPlayers
{
    public class ItemDataPlayer : ModPlayer
    {
        public int LightLevel;

        public override void PostUpdateMiscEffects()
        {
            int itemsConsidered = 0;

            foreach (Item item in Player.armor)
            {
                if (!item.TryGetGlobalItem(out ItemDataItem lightLevelItem))
                {
                    continue;
                }

                itemsConsidered++;
                LightLevel += Utils.Clamp(lightLevelItem.LightLevel, ItemData.MinimumLightLevel, ItemData.MaxmimumLightLevel);
            }

            if (Player.HeldItem.TryGetGlobalItem(out ItemDataItem heldLightLevelItem))
            {
                itemsConsidered++;
                LightLevel += Utils.Clamp(heldLightLevelItem.LightLevel, ItemData.MinimumLightLevel, ItemData.MaxmimumLightLevel);
            }

            LightLevel /= itemsConsidered;
            Player.statDefense += (LightLevel - 1350) / 10;
        }
    }
}
