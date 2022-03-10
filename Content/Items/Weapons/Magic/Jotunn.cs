using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using DestinyMod.Common.Items;
using DestinyMod.Content.Projectiles.Weapons.Magic;

namespace DestinyMod.Content.Items.Weapons.Magic
{
	public class Jotunn : DestinyModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Jotunn");
			Tooltip.SetDefault("Holding down the trigger charges up a tracking shot"
				+ "\n'Untamed. Destructive. As forceful and chaotic as Ymir himself.'");
		}

		public override void DestinySetDefaults()
		{
			Item.damage = 45;
			Item.DamageType = DamageClass.Magic;
			Item.channel = true;
			Item.mana = 5;
			Item.useTime = 25;
			Item.useAnimation = 25;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true;
			Item.knockBack = 4;
			Item.value = Item.buyPrice(gold: 1);
			Item.rare = ItemRarityID.LightRed;
			Item.shoot = ModContent.ProjectileType<JotunnShot>();
			Item.shootSpeed = 14f;
		}

		public override Vector2? HoldoutOffset() => new Vector2(-10, 5);
	}
}