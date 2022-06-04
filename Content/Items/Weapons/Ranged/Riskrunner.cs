using DestinyMod.Common.Items.ItemTypes;
using DestinyMod.Content.Projectiles.Weapons.Ranged;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace DestinyMod.Content.Items.Weapons.Ranged
{
	public class Riskrunner : Gun
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("33% chance to not consume ammo"
				+ "\nFires bullets which have a chance to shock enemies on hit"
				+ "\n'Charge your soul and let the electrons sing.'");
		}

		public override void DestinySetDefaults()
		{
			Item.damage = 4;
			Item.useTime = 6;
			Item.useAnimation = 6;
			Item.knockBack = 4;
			Item.value = Item.buyPrice(gold: 1);
			Item.rare = ItemRarityID.Yellow;
			Item.UseSound = new SoundStyle("DestinyMod/Assets/Sounds/Item/Weapons/Ranged/Riskrunner");
			Item.autoReuse = true;
			Item.shootSpeed = 16f;
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Projectile.NewProjectile(source, position + new Vector2(0, -4), velocity.RotatedByRandom(MathHelper.ToRadians(5)), ModContent.ProjectileType<RiskrunnerBullet>(), damage, knockback, player.whoAmI);
			return false;
		}

		public override bool CanConsumeAmmo(Item ammo, Player player) => Main.rand.NextFloat() >= .33f;

		public override Vector2? HoldoutOffset() =>  new Vector2(-20, 1);
	}
}