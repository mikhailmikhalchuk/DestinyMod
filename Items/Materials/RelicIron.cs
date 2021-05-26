using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;

namespace TheDestinyMod.Items.Materials
{
	public class RelicIron : ModItem
	{
		public override void SetStaticDefaults() {
			DisplayName.AddTranslation(GameCulture.Polish, "Reliktowe żelazo");
			Tooltip.SetDefault("A post-Collapse material of extraordinary density");
			Tooltip.AddTranslation(GameCulture.Polish, "Materiał zdobywany po rozpadzie o niezwykłej gęstości");
		}

        public override void SetDefaults() {
			item.height = 24;
			item.width = 24;
			item.maxStack = 999;
			item.value = Item.buyPrice(0, 0, 10, 0);
        }

		public override bool CanBurnInLava() {
			return true;
		}
	}
}