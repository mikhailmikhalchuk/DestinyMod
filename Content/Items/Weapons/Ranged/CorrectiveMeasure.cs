using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using DestinyMod.Common.Items.ItemTypes;

namespace DestinyMod.Content.Items.Weapons.Ranged
{
	public class CorrectiveMeasure : Gun
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("'It breaks the rules of reality as ruthlessly as it shatters your foes.'");
		}

		public override void DestinySetDefaults()
		{
			Item.damage = 45;
			Item.autoReuse = true;
			Item.channel = true;
			Item.useTime = 11;
			Item.useAnimation = 11;
			Item.knockBack = 4;
			Item.value = Item.buyPrice(gold: 1);
			Item.rare = ItemRarityID.Red;
			Item.UseSound = SoundLoader.GetLegacySoundSlot(Mod, "Assets/Sounds/Item/Weapons/Ranged/CorrectiveMeasure");
			Item.shootSpeed = 18f;
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Projectile.NewProjectile(source, new Vector2(position.X, position.Y - 6), velocity.RotatedByRandom(MathHelper.ToRadians(4)), type, damage, knockback, player.whoAmI);
			return false;
		}

		public override Vector2? HoldoutOffset() => new Vector2(-15, -3);
	}
}