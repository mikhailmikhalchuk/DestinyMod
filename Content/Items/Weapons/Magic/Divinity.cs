using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using DestinyMod.Common.Items;
using DestinyMod.Content.Projectiles.Weapons.Magic;

namespace DestinyMod.Content.Items.Weapons.Magic
{
	public class Divinity : DestinyModItem
	{
		public override void SetStaticDefaults() => Tooltip.SetDefault("Fires a solid beam"
			+ "\nSustained damage with the beam cuts the target's defense by 20%"
			+ "\n\"Calibrate reality. Seek inevitability. Embody divinity.\"");

		public override void DestinySetDefaults()
		{
			Item.damage = 40;
			Item.DamageType = DamageClass.Magic;
			Item.channel = true;
			Item.mana = 9;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true;
			Item.knockBack = 4;
			Item.value = Item.buyPrice(gold: 1);
			Item.rare = ItemRarityID.Yellow;
			Item.shoot = ModContent.ProjectileType<DivinityBeam>();
			Item.shootSpeed = 14f;
		}

		public override Vector2? HoldoutOffset() => new Vector2(-20, 0);
	}
}