using DestinyMod.Common.Items.ItemTypes;
using DestinyMod.Common.Projectiles;
using DestinyMod.Content.Projectiles.Weapons.Ranged;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace DestinyMod.Content.Items.Weapons.Ranged
{
	public class SleeperSimulant : Gun
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Fires a laser that pierces enemies and bounces off surfaces"
				+ "\n'Subroutine IKELOS: Status=complete. MIDNIGHT EXIGENT: Status=still in progress.'");
		}

		public override void DestinySetDefaults()
		{
			Item.damage = 35;
			Item.channel = true;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.knockBack = 4;
			Item.crit = 2;
			Item.value = Item.buyPrice(gold: 1);
			Item.rare = ItemRarityID.Yellow;
			Item.UseSound = SoundLoader.GetLegacySoundSlot(Mod, "Assets/Sounds/Item/Weapons/Ranged/MidaMultiTool");
			Item.shootSpeed = 30f;
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Projectile.NewProjectile(source, position, velocity + new Vector2(Main.rand.Next(-15, 16) * 0.05f), ModContent.ProjectileType<SleeperBeam>(), damage, knockback, player.whoAmI, 0, 4);
			return false;
		}

		public override Vector2? HoldoutOffset() => new Vector2(-12, 0);
	}
}