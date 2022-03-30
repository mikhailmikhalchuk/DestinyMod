using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using DestinyMod.Common.Items;

namespace DestinyMod.Common.Items.ItemTypes
{
	public abstract class GlaiveItem : DestinyModItem
	{
		public override void AutomaticSetDefaults()
		{
			base.AutomaticSetDefaults();
			Item.DamageType = DamageClass.Melee;
			Item.useStyle = ItemUseStyleID.Rapier;
			Item.noUseGraphic = true;
			Item.noMelee = true;
			Item.knockBack = 4;
			Item.shootSpeed = 2.1f;
		}

		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
	}
}