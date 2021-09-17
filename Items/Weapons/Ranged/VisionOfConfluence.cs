using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using TheDestinyMod.Items.Materials;

namespace TheDestinyMod.Items.Weapons.Ranged
{
	public class VisionOfConfluence : ModItem
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Vision of Confluence");
			Tooltip.SetDefault("\"What you have seen will mark you forever.\"");
		}

		public override void SetDefaults() {
			item.damage = 150;
			item.ranged = true;
			item.width = 72;
			item.height = 34;
			item.useTime = 14;
			item.useAnimation = 14;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.noMelee = true;
			item.knockBack = 4;
			item.value = Item.buyPrice(0, 1, 0, 0);
			item.rare = ItemRarityID.Yellow;
			item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/JadeRabbit");
			item.autoReuse = false;
			item.shoot = 10;
			item.shootSpeed = 300f;
			item.useAmmo = AmmoID.Bullet;
			item.scale = 0.75f;
		}

		public override Vector2? HoldoutOffset() {
			return new Vector2(-8, 0);
		}

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack) {
			Projectile.NewProjectile(position.X, position.Y - 6, speedX, speedY, type, damage, knockBack, player.whoAmI);
			return false;
		}

        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI) {
			scale *= 0.75f;
			return true;
		}
	}
}