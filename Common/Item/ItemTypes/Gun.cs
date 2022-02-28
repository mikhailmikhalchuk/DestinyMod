using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DestinyMod.Common.Items.ItemTypes
{
	public abstract class Gun : DestinyModItem
	{
		public override void AutomaticSetDefaults()
		{
			base.AutomaticSetDefaults();
			Item.DamageType = DamageClass.Ranged;
			Item.noMelee = true;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.shoot = ProjectileID.PurificationPowder;
			Item.shootSpeed = 10;
			Item.useAmmo = AmmoID.Bullet;
		}
	}
}