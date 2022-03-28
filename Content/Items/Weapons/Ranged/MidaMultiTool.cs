using DestinyMod.Common.Items.ItemTypes;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace DestinyMod.Content.Items.Weapons.Ranged
{
	public class MidaMultiTool : Gun
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("MIDA Multi-Tool");
			Tooltip.SetDefault("Increases movement speed"
                + "\n'Select application: Ballistic engagement. Entrenching tool. Avionics trawl. Troll smasher. Stellar sextant. List continues.'");
		}

		public override void DestinySetDefaults()
		{
			Item.damage = 35;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.knockBack = 4;
			Item.crit = 2;
			Item.value = Item.buyPrice(gold: 1);
			Item.rare = ItemRarityID.Yellow;
			Item.UseSound = SoundLoader.GetLegacySoundSlot(Mod, "Assets/Sounds/Item/Weapons/Ranged/MidaMultiTool");
			Item.shootSpeed = 30f;
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Projectile.NewProjectile(source, new Vector2(position.X, position.Y - 4), velocity, type, damage, knockback, player.whoAmI);
			return false;
		}

        public override void OnHold(Player player)
        {
			player.moveSpeed += 0.2f;
        }

        public override Vector2? HoldoutOffset() => new Vector2(-12, 0);

		public override void AddRecipes() => CreateRecipe(1)
			.AddIngredient(ItemID.Ectoplasm, 20)
			.AddTile(TileID.Anvils)
			.Register();
	}
}