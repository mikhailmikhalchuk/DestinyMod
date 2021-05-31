using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using TheDestinyMod.Items.Materials;

namespace TheDestinyMod.Items.Weapons.Magic
{
	public class Divinity : ModItem
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("Fires a solid beam\nSustained damage with the beam cuts the target's defense by 20%\n\"Calibrate reality. Seek inevitability. Embody divinity.\"");
		}

		public override void SetDefaults() {
			item.damage = 40;
			item.magic = true;
			item.channel = true;
			item.mana = 9;
			item.width = 114;
			item.height = 44;
			item.useTime = 20;
			item.useAnimation = 20;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.noMelee = true;
			item.knockBack = 4;
			item.value = Item.buyPrice(0, 1, 0, 0);
			item.rare = ItemRarityID.Yellow;
			item.shoot = ModContent.ProjectileType<Projectiles.Magic.DivinityBeam>();
			item.shootSpeed = 14f;
			item.scale = 0.6f;
		}

		public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI) {
			scale *= 0.6f;
			return true;
		}

		public override Vector2? HoldoutOffset() {
			return new Vector2(-20, 0);
		}
	}
}