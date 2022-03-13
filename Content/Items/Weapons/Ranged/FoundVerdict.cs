using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using DestinyMod.Common.Items.ItemTypes;

namespace DestinyMod.Content.Items.Weapons.Ranged
{
	public class FoundVerdict : Gun
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Fires a spread of bullets"
			+ "\n'Witness.'");
		}

		public override void DestinySetDefaults()
		{
			Item.damage = 25;
			Item.rare = ItemRarityID.Yellow;
			Item.knockBack = 0;
			Item.useTime = 60;
			Item.UseSound = SoundLoader.GetLegacySoundSlot(Mod, "Assets/Sounds/Item/Weapons/Ranged/FoundVerdict");
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.shootSpeed = 16f;
			Item.useAnimation = 60;
			Item.value = Item.buyPrice(gold: 1);
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			for (int i = 0; i < 5; i++)
			{
				Projectile.NewProjectile(source, new Vector2(position.X, position.Y - 2), velocity.RotatedByRandom(MathHelper.ToRadians(10)), type, damage, knockback, player.whoAmI);
			}
			return false;
		}

		public override Vector2? HoldoutOffset() => new Vector2(-12, -3);
	}
}