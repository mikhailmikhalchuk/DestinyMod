using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheDestinyMod.Items.Weapons.Supers
{
    public class GoldenGun : SuperClass
    {
		public static int timesShot = 0;

        public override void SetSuperDefaults() {
            item.damage = 100;
			item.noMelee = true;
			item.width = 54;
			item.height = 30;
			item.useTime = 5;
			item.useAnimation = 5;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.knockBack = 0;
			item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/MidaMultiTool");
			item.shoot = ModContent.ProjectileType<Projectiles.Super.GoldenGunShot>();
			item.shootSpeed = 30f;
			item.scale = .8f;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack) {
			DestinyPlayer dPlayer = Main.LocalPlayer.GetModPlayer<DestinyPlayer>();
			timesShot++;
			if (timesShot >= 6) {
				timesShot = 0;
				dPlayer.superActiveTime = 0;
				dPlayer.superChargeCurrent = 0;
			}
			return true;
        }

		public override Vector2? HoldoutOffset() {
			return new Vector2(5, 2);
		}
	}
}