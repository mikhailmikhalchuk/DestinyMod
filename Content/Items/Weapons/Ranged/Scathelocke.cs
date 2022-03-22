using DestinyMod.Common.Items.ItemTypes;
using DestinyMod.Content.Projectiles.Weapons.Ranged;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace DestinyMod.Content.Items.Weapons.Ranged
{
	public class Scathelocke : Gun
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("'Our eyes squinted, our teeth clenched, our prayers answered.'");
		}

		public override void DestinySetDefaults()
		{
			Item.damage = 10;
			Item.useTime = 6;
			Item.useAnimation = 6;
			Item.knockBack = 4;
			Item.value = Item.buyPrice(gold: 1);
			Item.rare = ItemRarityID.Yellow;
			Item.UseSound = SoundLoader.GetLegacySoundSlot(Mod, "Assets/Sounds/Item/Weapons/Ranged/HakkeAutoRifle");
			Item.autoReuse = true;
			Item.shootSpeed = 16f;
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Projectile.NewProjectile(source, position + new Vector2(0, -2), velocity.RotatedByRandom(MathHelper.ToRadians(3)), type, damage, knockback, player.whoAmI);
			return false;
		}

		public override Vector2? HoldoutOffset() => new Vector2(-5, 1);
	}
}