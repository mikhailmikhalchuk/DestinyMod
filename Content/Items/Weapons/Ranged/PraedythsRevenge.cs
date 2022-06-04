using DestinyMod.Common.Items.ItemTypes;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace DestinyMod.Content.Items.Weapons.Ranged
{
	public class PraedythsRevenge : Gun
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Praedyth's Revenge");
			Tooltip.SetDefault("'Praedyth's fall isn't over... because it hasn't happened yet... and it will happen again.'");
		}

		public override void DestinySetDefaults()
		{
			Item.damage = 50;
			Item.useTime = 28;
			Item.useAnimation = 28;
			Item.knockBack = 4;
			Item.value = Item.buyPrice(gold: 1);
			Item.rare = ItemRarityID.Blue;
			Item.UseSound = new SoundStyle("DestinyMod/Assets/Sounds/Item/Weapons/Ranged/PraedythsRevenge");
			Item.shootSpeed = 300f;
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Projectile.NewProjectile(source, new Vector2(position.X, position.Y - 6), velocity, type, damage, knockback, player.whoAmI);
			return false;
		}

		public override Vector2? HoldoutOffset() => new Vector2(-10, -5);
	}
}