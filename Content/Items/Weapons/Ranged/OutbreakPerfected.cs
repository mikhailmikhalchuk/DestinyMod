using DestinyMod.Common.Items.ItemTypes;
using DestinyMod.Content.Projectiles.Weapons.Ranged;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace DestinyMod.Content.Items.Weapons.Ranged
{
	public class OutbreakPerfected : Gun
	{
		public override void SetStaticDefaults() => Tooltip.SetDefault("Three round burst"
			+ "\nCreates nanite swarms on critical kills and rapid hits"
			+ "\n\"~directive = KILL while enemies = PRESENT: execute(directive)~\"");

		public override void DestinySetDefaults()
		{
			Item.damage = 30;
			Item.useTime = 6;
			Item.useAnimation = 18;
			Item.knockBack = 0;
			Item.crit = 2;
			Item.value = Item.buyPrice(gold: 1);
			Item.rare = ItemRarityID.LightRed;
			Item.UseSound = SoundLoader.GetLegacySoundSlot(Mod, "Sounds/Item/OutbreakPerfected");
			Item.shootSpeed = 300f;
			DestinyModReuseDelay = 5;
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Projectile.NewProjectile(source, position + new Vector2(0, -4), velocity, ModContent.ProjectileType<OutbreakBullet>(), damage, knockback, player.whoAmI); 
			return false;
		}

		public override Vector2? HoldoutOffset() => new Vector2(-10, -1);
	}
}