using DestinyMod.Common.Items.ItemTypes;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace DestinyMod.Content.Items.Weapons.Ranged
{
	public class NoTimeToExplain : Gun
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("No Time to Explain");
			Tooltip.SetDefault("Three round burst"
				+ "\n\"A single word etched onto the inside of the weapon's casing: Now.\"");
		}

		public override void DestinySetDefaults()
		{
			Item.damage = 65;
			Item.autoReuse = true;
			Item.useTime = 4;
			Item.useAnimation = 12;
			Item.value = Item.buyPrice(gold: 1);
			Item.rare = ItemRarityID.Purple;
			Item.UseSound = SoundLoader.GetLegacySoundSlot(Mod, "Sounds/Item/NoTimeToExplain");
			Item.shootSpeed = 16f;
			Item.reuseDelay = 10;
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Projectile.NewProjectile(source, new Vector2(position.X, position.Y - 3), velocity, type, damage, knockback, player.whoAmI);
			return false;
		}

		public override Vector2? HoldoutOffset() => new Vector2(-22, 0);
	}
}