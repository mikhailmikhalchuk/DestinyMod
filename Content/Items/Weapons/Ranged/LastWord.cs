using DestinyMod.Common.Items.ItemTypes;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace DestinyMod.Content.Items.Weapons.Ranged
{
	public class LastWord : Gun
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("The Last Word");
			Tooltip.SetDefault("\"Yours, until the last flame dies and all words have been spoken.\"");
		}

		public override void DestinySetDefaults()
		{
			Item.damage = 60;
			Item.autoReuse = true;
			Item.rare = ItemRarityID.LightRed;
			Item.knockBack = 0;
			Item.useTime = 14;
			Item.UseSound = SoundLoader.GetLegacySoundSlot(Mod, "Assets/Sounds/Item/Weapons/Ranged/TheLastWord");
			Item.shootSpeed = 40f;
			Item.useAnimation = 14;
			Item.value = Item.buyPrice(gold: 1);
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Projectile.NewProjectile(source, position, velocity.RotatedByRandom(MathHelper.ToRadians(3)), type, damage, knockback, player.whoAmI);
			return false;
		}

		
		public override Vector2? HoldoutOffset() => new Vector2(5, 2);

		public override void AddRecipes() => CreateRecipe(1)
			.AddIngredient(ItemID.Ectoplasm, 20)
			.AddTile(TileID.Anvils)
			.Register();
	}
}