using DestinyMod.Common.Items.ItemTypes;
using DestinyMod.Content.Projectiles.Weapons.Ranged;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace DestinyMod.Content.Items.Weapons.Ranged
{
	public class SalvagersSalvo : Gun
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Salvager's Salvo");
			Tooltip.SetDefault("Grenades fired will explode when the fire button is released"
				+ "\n\"The only way out is through.\"");
		}

		public override void DestinySetDefaults()
		{
			Item.damage = 45;
			Item.channel = true;
			Item.useTime = 25;
			Item.useAnimation = 25;
			Item.knockBack = 4;
			Item.value = Item.buyPrice(gold: 1);
			Item.rare = ItemRarityID.Pink;
			Item.UseSound = SoundLoader.GetLegacySoundSlot(Mod, "Sounds/Item/SalvagersSalvo");
			Item.shoot = ModContent.ProjectileType<SalvoGrenade>();
			Item.shootSpeed = 12f;
			Item.useAmmo = ItemID.Grenade;
		}

		public override bool Shoot(Player player, ProjectileSource_Item_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Projectile.NewProjectile(source, new Vector2(position.X, position.Y - 7), velocity, ModContent.ProjectileType<SalvoGrenade>(), damage, knockback, player.whoAmI);
			return false;
		}

		public override Vector2? HoldoutOffset() => new Vector2(-10, -5);
	}
}