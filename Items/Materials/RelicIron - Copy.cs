using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace TheDestinyMod.Items.Materials
{
	public class Test : ModItem
	{
		public override void SetStaticDefaults() {
			DisplayName.AddTranslation(GameCulture.Polish, "Reliktowe żelazo");
			Tooltip.SetDefault("A post-Collapse material of extraordinary density");
			Tooltip.AddTranslation(GameCulture.Polish, "Materiał zdobywany po rozpadzie o niezwykłej gęstości");
		}

        public override void SetDefaults() {
			item.height = 16;
			item.width = 18;
			item.maxStack = 999;
			item.value = Item.buyPrice(0, 0, 2, 0);
			item.useTurn = true;
			item.consumable = true;
			item.autoReuse = true;
			item.useTime = 10;
			item.useAnimation = 15;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.createTile = ModContent.TileType<Tiles.VoGTeleport>();
        }
	}
}