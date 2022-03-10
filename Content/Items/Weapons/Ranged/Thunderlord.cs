using Microsoft.Xna.Framework;
using DestinyMod.Common.Items.ItemTypes;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using DestinyMod.Content.Items.Materials;
using DestinyMod.Common.ModPlayers;
using DestinyMod.Content.Projectiles.Weapons.Ranged;

namespace DestinyMod.Content.Items.Weapons.Ranged
{
	public class Thunderlord : Gun
	{
		public override void SetStaticDefaults() => Tooltip.SetDefault("Fires bullets which create lightning on kills"
			+ "\nFires faster the longer this weapon is used"
			+ "\n\"They rest quiet on fields afar...for this is no ending, but the eye.\"");

		public override void DestinySetDefaults()
		{
			Item.damage = 45;
			Item.autoReuse = true;
			Item.channel = true;
			Item.useTime = 11;
			Item.useAnimation = 11;
			Item.knockBack = 4;
			Item.value = Item.buyPrice(gold: 1);
			Item.rare = ItemRarityID.Yellow;
			Item.UseSound = SoundLoader.GetLegacySoundSlot(Mod, "Assets/Sounds/Item/Weapons/Ranged/Thunderlord");
			Item.shootSpeed = 18f;
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Vector2 perturbedSpeed = velocity.RotatedByRandom(MathHelper.ToRadians(4));
			Projectile.NewProjectile(source, new Vector2(position.X, position.Y - 5), perturbedSpeed, ModContent.ProjectileType<ThunderlordShot>(), damage, knockback, player.whoAmI);
			return false;
		}

		public override float UseTimeMultiplier(Player player) => 1 - Utils.Clamp(player.GetModPlayer<StatsPlayer>().ChannelTime / 30f * 0.05f, 0, 0.5f);

		public override Vector2? HoldoutOffset() => new Vector2(-15, -3);

		public override void AddRecipes() => CreateRecipe(1)
			.AddIngredient(ItemID.Megashark)
			.AddIngredient(ModContent.ItemType<GunsmithMaterials>(), 20)
			.AddIngredient(ItemID.SoulofMight, 15)
			.AddTile(TileID.MythrilAnvil)
			.Register();
	}
}