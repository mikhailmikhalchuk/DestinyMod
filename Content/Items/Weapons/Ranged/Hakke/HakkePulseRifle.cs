using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using DestinyMod.Content.Items.Materials;

namespace DestinyMod.Content.Items.Weapons.Ranged.Hakke
{
	public class HakkePulseRifle : HakkeCraftsmanshipWeapon
	{
		public override void SetStaticDefaults() => Tooltip.SetDefault("Three round burst"
			+ "Has a chance to grant the \"Hakke Craftsmanship\" buff on use");

		public override void DestinySetDefaults()
		{
			Item.damage = 14;
			Item.useTime = 4;
			Item.useAnimation = 12;
			Item.knockBack = 4;
			Item.value = Item.buyPrice(gold: 1);
			Item.rare = ItemRarityID.Orange;
			Item.UseSound = SoundLoader.GetLegacySoundSlot(Mod, "Sounds/Item/RedDeath");
			Item.shootSpeed = 16f;
			DestinyModReuseDelay = 14;
			ShootOffset = new Vector2(0, -3);
			SpreadRadians = MathHelper.ToRadians(4);
		}

		public override Vector2? HoldoutOffset() => new Vector2(-18, 0);

		public override void AddRecipes() => CreateRecipe(1)
			.AddIngredient(ModContent.ItemType<RelicIron>(), 40)
			.AddIngredient(ItemID.Bone, 40)
			.AddIngredient(ItemID.HellstoneBar, 8)
			.AddTile(TileID.Anvils)
			.Register();
	}
}