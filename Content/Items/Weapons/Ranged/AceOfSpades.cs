using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using DestinyMod.Items.Materials;
using Terraria.Audio;
using Terraria.DataStructures;
using DestinyMod.Content.Items.Materials;

namespace DestinyMod.Content.Items.Weapons.Ranged
{
	public class AceOfSpades : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "As pików");
			Tooltip.SetDefault("Kills with this weapon cause the target to explode" 
				+ "\nFolding was never an option.");
			Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Zabójstwa tą bronią powodują że cel wybucha" 
				+ "\nSkładanie nigdy nie wchodziło w grę");
		}

		public override void SetDefaults()
		{
			Item.width = 58;
			Item.height = 30;
			Item.useTime = 15;
			Item.useAnimation = 15;
			Item.DamageType = DamageClass.Ranged;
			Item.damage = 135;
			Item.noMelee = true;
			Item.knockBack = 0;
			Item.crit = 10;
			Item.UseSound = SoundLoader.GetLegacySoundSlot(Mod, "Sounds/Item/AceOfSpades");
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.shootSpeed = 10f;
			Item.shoot = ProjectileID.PurificationPowder;
			Item.useAmmo = AmmoID.Bullet;
			Item.value = Item.buyPrice(0, 1, 0, 0);
			Item.rare = ItemRarityID.Yellow;
		}

		public override bool Shoot(Player player, ProjectileSource_Item_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			position.Y -= 7;
			type = ModContent.ProjectileType<AceBullet>();
			return true;
		}

		public override Vector2? HoldoutOffset() => new Vector2(5, 0);

		public override void AddRecipes() => CreateRecipe(1).AddIngredient(ModContent.ItemType<GunsmithMaterials>(), 50)
			.AddIngredient(ItemID.FragmentSolar, 10)
			.AddTile(TileID.LunarCraftingStation)
			.Register();
	}
}