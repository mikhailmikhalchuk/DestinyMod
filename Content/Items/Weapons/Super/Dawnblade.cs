using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using DestinyMod.Common.Items.ItemTypes;
using DestinyMod.Content.Projectiles.Weapons.Super;
using DestinyMod.Common.DamageClasses;

namespace DestinyMod.Content.Items.Weapons.Super
{
	public class Dawnblade : SuperItem
	{
		public override void DestinySetDefaults() 
		{
			Item.damage = 100;
			Item.noMelee = true;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.UseSound = SoundID.Item1;
			Item.shoot = ModContent.ProjectileType<DawnbladeShot>();
			Item.shootSpeed = 30f;
		}
	}
}