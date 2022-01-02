using Terraria;
using DestinyMod.Common.Items;
using DestinyMod.Common.Utils;

namespace DestinyMod.Content.Items.Materials
{
	public class GunsmithMaterials : DestinyModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.AddTranslation(LanguageUtils.Polish, "Materiały Rusznikarskie");
			Tooltip.SetDefault("Used to craft guns");
		}

		public override void DestinySetDefaults()
		{
			Item.maxStack = 999;
			Item.value = Item.buyPrice(0, 0, 1, 0);
		}

		public override bool? CanBurnInLava() => true;
	}
}