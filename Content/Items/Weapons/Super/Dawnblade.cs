using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using DestinyMod.Common.Items.ItemTypes;

namespace DestinyMod.Content.Items.Weapons.Ranged
{
	public class Dawnblade : SuperItem
	{
		public override void DestinySetDefaults() 
		{
			Item.damage = 100;
			Item.noMelee = true;
			Item.DamageType = DamageClass.Melee;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 0;
			Item.UseSound = SoundID.Item1;
			Item.shoot = ModContent.ProjectileType<Projectiles.Super.DawnbladeShot>();
			Item.shootSpeed = 30f;
		}

        public override bool OnlyShootOnSwing => true;
	}
}