using DestinyMod.Common.Items.ItemTypes;
using DestinyMod.Content.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DestinyMod.Content.Items.Equipables.Armor
{
	[AutoloadEquip(EquipType.Head)]
	public class ProdigalHood : ClassArmor
	{
		public override DestinyClassType ArmorClassType => DestinyClassType.Warlock;

		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("7% increased magic critical strike chance");
		}

		public override void DestinySetDefaults()
		{
			Item.value = Item.sellPrice(0, 0, 90, 0);
			Item.rare = ItemRarityID.Orange;
			Item.defense = 8;
		}

		public override void UpdateEquip(Player player)
		{
			player.GetCritChance(DamageClass.Magic) += 7;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs) =>
			body.type == ModContent.ItemType<ProdigalCuirass>() && legs.type == ModContent.ItemType<ProdigalGreaves>();

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "17% increased ranged damage";
			player.GetDamage(DamageClass.Ranged) += 0.17f;
		}

		public override void AddRecipes() => CreateRecipe(1)
			.AddIngredient(ModContent.ItemType<RelicIron>(), 20)
			.AddIngredient(ModContent.ItemType<PlasteelPlating>(), 10)
			.AddTile(TileID.Anvils)
			.Register();
	}
}