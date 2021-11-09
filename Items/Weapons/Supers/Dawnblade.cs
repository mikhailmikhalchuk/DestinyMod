using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheDestinyMod.Items.Weapons.Supers
{
	public class Dawnblade : SuperClass
	{
		public override void SetSuperDefaults() {
			item.damage = 100;
			item.noMelee = true;
			item.melee = true;
			item.width = 76;
			item.height = 76;
			item.useTime = 20;
			item.useAnimation = 20;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.knockBack = 0;
			item.UseSound = SoundID.Item1;
			item.shoot = ModContent.ProjectileType<Projectiles.Super.DawnbladeShot>();
			item.shootSpeed = 30f;
		}

        public override bool OnlyShootOnSwing => true;
	}
}