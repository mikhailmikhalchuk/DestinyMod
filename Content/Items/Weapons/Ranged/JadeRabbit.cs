using DestinyMod.Common.Items.ItemTypes;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace DestinyMod.Content.Items.Weapons.Ranged
{
	public class JadeRabbit : Gun
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("The Jade Rabbit");
			Tooltip.SetDefault("'What kind of harebrained scheme have you got in mind this time?'");
		}

		public override void DestinySetDefaults()
		{
			Item.damage = 20;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.knockBack = 4;
			Item.crit = 2;
			Item.value = Item.buyPrice(gold: 1);
			Item.rare = ItemRarityID.Orange;
			Item.UseSound = new SoundStyle("DestinyMod/Assets/Sounds/Item/Weapons/Ranged/JadeRabbit");
			Item.shootSpeed = 300f;
		}

		public override Vector2? HoldoutOffset() => new Vector2(-13, 0);

		public override void AddRecipes() => CreateRecipe(1)
			.AddIngredient(ItemID.Ectoplasm, 20)
			.AddTile(TileID.Anvils)
			.Register();
	}
}