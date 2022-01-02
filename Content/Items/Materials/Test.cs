using Terraria;
using Terraria.ModLoader;
using DestinyMod.Common.Items.ItemTypes;
using DestinyMod.Common.Utils;

namespace DestinyMod.Content.Items.Materials
{
	public class Test : TileItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.AddTranslation(LanguageUtils.Polish, "Reliktowe żelazo");
			Tooltip.SetDefault("A post-Collapse material of extraordinary density");
			Tooltip.AddTranslation(LanguageUtils.Polish, "Materiał zdobywany po rozpadzie o niezwykłej gęstości");
		}

		public override int TileType => ModContent.TileType<Tiles.VoGTeleport>();

		public override void DestinySetDefaults()
		{
			Item.maxStack = 999;
			Item.value = Item.buyPrice(0, 0, 2, 0);
		}
	}
}