using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using TheDestinyMod.Items.Materials;

namespace TheDestinyMod.Items.Weapons.Magic
{
	public class Jotunn : ModItem
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Jötunn");
			Tooltip.SetDefault("Holding down the trigger charges up a tracking shot\n\"Untamed. Destructive. As forceful and chaotic as Ymir himself.\"");
		}

		public override void SetDefaults() {
			item.damage = 45;
			item.magic = true;
			item.channel = true;
			item.mana = 5;
			item.width = 54;
			item.height = 26;
			item.useTime = 25;
			item.useAnimation = 25;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.noMelee = true;
			item.knockBack = 4;
			item.value = Item.buyPrice(0, 1, 0, 0);
			item.rare = ItemRarityID.LightRed;
			item.shoot = ModContent.ProjectileType<Projectiles.Magic.JotunnShot>();
			item.shootSpeed = 14f;
			item.scale = 0.8f;
		}

        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI) {
			scale *= 0.8f;
			return true;
		}

		public override Vector2? HoldoutOffset() {
			return new Vector2(-10, 5);
		}
	}
}