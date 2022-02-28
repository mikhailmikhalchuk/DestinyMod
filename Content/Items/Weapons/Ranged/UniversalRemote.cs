using Microsoft.Xna.Framework;
using DestinyMod.Common.Items.ItemTypes;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace DestinyMod.Content.Items.Weapons.Ranged
{
	public class UniversalRemote : Gun
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Fires a spread of bullets"
				+ "\n\"To the untrained eye this beast is a junker. To the trained eye, however, this junker... is a beast.\"");
		}

		public override void DestinySetDefaults()
		{
			Item.damage = 15;
			Item.rare = ItemRarityID.Yellow;
			Item.knockBack = 0;
			Item.useTime = 55;
			Item.UseSound = SoundLoader.GetLegacySoundSlot(Mod, "Sounds/Item/UniversalRemote");
			Item.shootSpeed = 16f;
			Item.useAnimation = 55;
			Item.value = Item.buyPrice(gold: 1);
		}

		public override bool Shoot(Player player, ProjectileSource_Item_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			for (int i = 0; i < 5; i++)
			{
				Projectile.NewProjectile(source, position, velocity.RotatedByRandom(MathHelper.ToRadians(10)), type, damage, knockback, player.whoAmI);
			}
			return false;
		}

		public override Vector2? HoldoutOffset() => new Vector2(-15, -3);

		public override void AddRecipes() => CreateRecipe(1)
			.AddIngredient(ItemID.Ectoplasm, 20)
			.AddTile(TileID.Anvils)
			.Register();
	}
}