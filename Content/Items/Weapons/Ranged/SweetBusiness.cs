using DestinyMod.Common.Items.ItemTypes;
using DestinyMod.Common.ModPlayers;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace DestinyMod.Content.Items.Weapons.Ranged
{
	public class SweetBusiness : Gun
	{
		public override void SetStaticDefaults() => Tooltip.SetDefault("10% chance to not consume ammo"
			+ "\nFires faster the longer this weapon is used"
			+ "\n\"...I love my job.\"");

		public override void DestinySetDefaults()
		{
			Item.damage = 18;
			Item.autoReuse = true;
			Item.channel = true;
			Item.rare = ItemRarityID.Red;
			Item.knockBack = 0;
			Item.useTime = 5;
			Item.UseSound = SoundLoader.GetLegacySoundSlot(Mod, "Assets/Sounds/Item/Weapons/Ranged/SweetBusiness");
			Item.shootSpeed = 20f;
			Item.useAnimation = 5;
			Item.value = Item.buyPrice(gold: 1);
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			if (player.GetModPlayer<StatsPlayer>().BusinessReduceUse > 0.7f)
            {
				player.GetModPlayer<StatsPlayer>().BusinessReduceUse -= 0.05f;
			}
			Projectile.NewProjectile(source, new Vector2(position.X, position.Y - 5), velocity.RotatedByRandom(MathHelper.ToRadians(8)), type, damage, knockback, player.whoAmI);
			return false;
		}

		public override float UseTimeMultiplier(Player player) => player.GetModPlayer<StatsPlayer>().BusinessReduceUse;

		public override bool CanConsumeAmmo(Player player) => Main.rand.NextBool(3, 4);

		public override Vector2? HoldoutOffset() => new Vector2(-15, -3);

		public override void AddRecipes() => CreateRecipe(1)
			.AddIngredient(ItemID.Megashark)
			.AddIngredient(ItemID.FragmentVortex, 15)
			.AddTile(TileID.MythrilAnvil)
			.Register();
	}
}