using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using DestinyMod.Content.Items.Materials;
using DestinyMod.Common.Items.ItemTypes;
using DestinyMod.Content.Projectiles.Weapons.Ranged;

namespace DestinyMod.Content.Items.Weapons.Ranged
{
	public class AceOfSpades : Gun
	{
		public override void SetStaticDefaults() => Tooltip.SetDefault("Kills with this weapon cause the target to explode" 
			+ "\n\"Folding was never an option.\"");

		public override void DestinySetDefaults()
		{
			Item.useTime = 15;
			Item.useAnimation = 15;
			Item.damage = 135;
			Item.knockBack = 0;
			Item.crit = 10;
			Item.UseSound = SoundLoader.GetLegacySoundSlot(Mod, "Sounds/Item/AceOfSpades");
			Item.value = Item.buyPrice(gold: 1);
			Item.rare = ItemRarityID.Yellow;
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Projectile.NewProjectile(source, new Vector2(position.X, position.Y - 4), velocity, ModContent.ProjectileType<AceBullet>(), damage, knockback, player.whoAmI);
			return false;
		}

		public override Vector2? HoldoutOffset() => new Vector2(3, 2);

		public override void AddRecipes() => CreateRecipe(1)
			.AddIngredient(ModContent.ItemType<GunsmithMaterials>(), 50)
			.AddIngredient(ItemID.FragmentSolar, 10)
			.AddTile(TileID.LunarCraftingStation)
			.Register();
	}
}