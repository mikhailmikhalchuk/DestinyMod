using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using DestinyMod.Common.Items.ItemTypes;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using DestinyMod.Common.ModPlayers;
using DestinyMod.Content.Projectiles.Weapons.Super;

namespace DestinyMod.Content.Items.Weapons.Super
{
	public class GoldenGun : SuperItem
	{
		public static int TimesShot = 0;

		public override void DestinySetDefaults()
		{
			Item.damage = 100;
			Item.noMelee = true;
			Item.useTime = 5;
			Item.useAnimation = 5;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.UseSound = new SoundStyle("DestinyMod/Assets/Sounds/Item/Weapons/Super/MidaMultiTool");
			Item.shoot = ModContent.ProjectileType<GoldenGunShot>();
			Item.shootSpeed = 30f;
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			if (++TimesShot % 6 == 0)
			{
				SuperPlayer superPlayer = player.GetModPlayer<SuperPlayer>();
				superPlayer.SuperActiveTime = 0;
				superPlayer.SuperChargeCurrent = 0;
			}
			return true;
		}

		public override Vector2? HoldoutOffset() => new Vector2(5, 2);
	}
}