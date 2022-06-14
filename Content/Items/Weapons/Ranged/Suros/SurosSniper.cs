using DestinyMod.Common.Items.ItemTypes;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace DestinyMod.Content.Items.Weapons.Ranged.Suros
{
	public class SurosSniper : Gun
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("SUROS Sniper");
			Tooltip.SetDefault("Standard SUROS Sniper Rifle");
		}

		public override void DestinySetDefaults()
		{
			Item.damage = 80;
			Item.useTime = 28;
			Item.useAnimation = 28;
			Item.knockBack = 4;
			Item.value = Item.buyPrice(gold: 1);
			Item.rare = ItemRarityID.Pink;
			Item.UseSound = new SoundStyle("DestinyMod/Assets/Sounds/Item/Weapons/Ranged/JadeRabbit");
			Item.shootSpeed = 300f;
		}

		public override Vector2? HoldoutOffset() => new Vector2(-10, -3);

		public override void AddRecipes() => CreateRecipe(1)
			.AddIngredient(ItemID.Obsidian, 20)
			.AddIngredient(ItemID.HallowedBar, 10)
			.AddTile(TileID.MythrilAnvil)
			.Register();
	}
}