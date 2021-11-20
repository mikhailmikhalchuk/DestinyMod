using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using TheDestinyMod.Items.Materials;

namespace TheDestinyMod.Items.Weapons.Ranged
{
	public class HauntedEarth : ModItem
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("Increased critical strike chance while not moving\n\"Those we've lost still linger in every place we look. Earth is no place for the living.\"");
		}

		public override void SetDefaults() {
			item.damage = 20;
			item.ranged = true;
			item.width = 88;
			item.height = 34;
			item.useTime = 20;
			item.useAnimation = 20;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.noMelee = true;
			item.knockBack = 4;
			item.crit = 2;
			item.value = Item.buyPrice(0, 1, 0, 0);
			item.rare = ItemRarityID.Orange;
			item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/JadeRabbit");
			item.autoReuse = false;
			item.shoot = 10;
			item.shootSpeed = 300f;
			item.useAmmo = AmmoID.Bullet;
			item.scale = 0.9f;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack) {
			Projectile p = Projectile.NewProjectileDirect(new Vector2(position.X, position.Y - 4), new Vector2(speedX, speedY), type, damage, knockBack, player.whoAmI);
			if (p.extraUpdates < 3)
				p.extraUpdates = 3;
			return false;
		}

        public override void GetWeaponCrit(Player player, ref int crit) {
			if (player.velocity == Vector2.Zero)
				crit += 5;
        }

        public override Vector2? HoldoutOffset() {
			return new Vector2(-18, 2);
		}

		public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI) {
			scale *= 0.9f;
			return true;
		}
	}
}