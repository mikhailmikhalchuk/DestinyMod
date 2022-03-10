using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using DestinyMod.Common.Items.ItemTypes;

namespace DestinyMod.Content.Items.Weapons.Ranged
{
	public class Drang : Gun
	{
		public override void SetStaticDefaults() => Tooltip.SetDefault("\"Since the Collapse, these pistols have been retooled several times to boost their firepower."
			+ "\nA worn inscription reads, 'To Victor, from Sigrun.'\"");

		public override void DestinySetDefaults()
		{
			Item.damage = 34;
			Item.rare = ItemRarityID.LightRed;
			Item.knockBack = 0;
			Item.useTime = 16;
			Item.UseSound = SoundLoader.GetLegacySoundSlot(Mod, "Assets/Sounds/Item/Weapons/Ranged/HandCannon120");
			Item.shootSpeed = 40f;
			Item.useAnimation = 16;
			Item.value = Item.buyPrice(gold: 1);
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Projectile.NewProjectile(source, new Vector2(position.X, position.Y - 6), velocity, type, damage, knockback, player.whoAmI);
			return false;
		}

		public override Vector2? HoldoutOffset() => new Vector2(5, 2);
	}
}