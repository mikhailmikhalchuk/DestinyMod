using DestinyMod.Common.Items.ItemTypes;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace DestinyMod.Content.Items.Weapons.Ranged.Khvostov
{
	public abstract class Khvostov : Gun
	{
		public override void SetStaticDefaults() => DisplayName.SetDefault("Khvostov 7G-0X");

		public override void AutomaticSetDefaults()
		{
			base.AutomaticSetDefaults();
			Item.knockBack = 4;
			Item.value = Item.buyPrice(gold: 1);
			Item.UseSound = SoundLoader.GetLegacySoundSlot(Mod, "Sounds/Item/HakkeAutoRifle");
			Item.autoReuse = true;
			Item.shootSpeed = 30f;
		}


		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Projectile.NewProjectile(source, new Vector2(position.X, position.Y - 3), velocity.RotatedByRandom(MathHelper.ToRadians(5)), type, damage, knockback, player.whoAmI);
			return false;
		}

		public override Vector2? HoldoutOffset() => new Vector2(-13, 0);
	}
}