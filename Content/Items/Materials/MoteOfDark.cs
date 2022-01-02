using DestinyMod.Common.Items;
using DestinyMod.Common.Utils;

namespace DestinyMod.Content.Items.Materials
{
	public class MoteOfDark : DestinyModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.AddTranslation(LanguageUtils.Polish, "Odrobina ciemności");
			Tooltip.SetDefault("A mysterious tetrahedral object. The Drifter may like this");
			Tooltip.AddTranslation(LanguageUtils.Polish, "Tajemniczy czworościenny obiekt. Dryfterowi może się to spodobać");
		}

		public override void DestinySetDefaults() => Item.maxStack = 99;
	}
}