using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using DestinyMod.Common.Items.ItemTypes;

namespace DestinyMod.Content.Items.Weapons.Ranged
{
	public class FourthHorseman : Gun
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("The Fourth Horseman");
			Tooltip.SetDefault("Fires a spread of bullets"
				+ "\n\"It's not a holdout weapon; it's a pathfinder.\"");
		}

		public override void DestinySetDefaults()
		{
			Item.damage = 125;
			Item.autoReuse = true;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.knockBack = 4;
			Item.value = Item.buyPrice(gold: 1);
			Item.rare = ItemRarityID.Red;
			Item.UseSound = SoundLoader.GetLegacySoundSlot(Mod, "Sounds/Item/HakkeShotgun");
			Item.reuseDelay = 10;
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			for (int i = 0; i < 4; i++)
			{
				Projectile.NewProjectile(source, position + new Vector2(0, -2), velocity.RotatedByRandom(MathHelper.ToRadians(20)), type, damage, knockback, player.whoAmI);
			}
			return false;
		}

		public override Vector2? HoldoutOffset() => new Vector2(-10, 0);
	}
}