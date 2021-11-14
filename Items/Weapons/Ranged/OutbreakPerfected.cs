using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using TheDestinyMod.Projectiles.Ranged;

namespace TheDestinyMod.Items.Weapons.Ranged
{
    public class OutbreakPerfected : ModItem 
    {
        public override void SetStaticDefaults() {
            Tooltip.SetDefault("Three round burst\nCreates nanite swarms on critical kills and rapid hits\n\"~directive = KILL while enemies = PRESENT: execute(directive)~\"");
        }

		public override void SetDefaults() {
			item.damage = 30;
			item.ranged = true;
			item.width = 74;
			item.height = 34;
			item.useTime = 6;
			item.useAnimation = 18;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.noMelee = true;
			item.knockBack = 0;
			item.crit = 2;
			item.value = Item.buyPrice(0, 1, 0, 0);
			item.rare = ItemRarityID.LightRed;
			item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/OutbreakPerfected");
			item.shoot = 10;
			item.shootSpeed = 300f;
			item.useAmmo = AmmoID.Bullet;
			item.scale = .90f;
		}

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack) {
			Projectile.NewProjectile(position.X, position.Y - 4, speedX, speedY, ModContent.ProjectileType<OutbreakBullet>(), damage, knockBack, player.whoAmI);
			player.GetModPlayer<DestinyPlayer>().destinyWeaponDelay = 5;
			return false;
        }

        public override bool CanUseItem(Player player) {
            return player.GetModPlayer<DestinyPlayer>().destinyWeaponDelay <= 0;
        }

        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI) {
			scale *= 0.9f;
			return true;
		}

		public override Vector2? HoldoutOffset() {
			return new Vector2(-10, -1);
		}
	}
}
