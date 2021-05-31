using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using TheDestinyMod.Items.Materials;

namespace TheDestinyMod.Items.Weapons.Ranged
{
	public class SalvagersSalvo : ModItem
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Salvager's Salvo");
			DisplayName.AddTranslation(GameCulture.Spanish, "Salva del Salvador");
			Tooltip.SetDefault("Grenades fired will explode when the fire button is released\n\"The only way out is through.\"");
		}

		public override void SetDefaults() {
			item.damage = 45;
			item.ranged = true;
			item.channel = true;
			item.width = 74;
			item.height = 32;
			item.useTime = 25;
			item.useAnimation = 25;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.noMelee = true;
			item.knockBack = 4;
			item.value = Item.buyPrice(0, 1, 0, 0);
			item.rare = ItemRarityID.Pink;
			item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/SalvagersSalvo");
			item.shoot = ModContent.ProjectileType<Projectiles.Ranged.SalvoGrenade>();
			item.shootSpeed = 8f;
			item.useAmmo = ItemID.Grenade;
			item.scale = .80f;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack) {
			Projectile.NewProjectile(position.X, position.Y - 7, speedX, speedY, ModContent.ProjectileType<Projectiles.Ranged.SalvoGrenade>(), damage, knockBack, player.whoAmI);
			return false;
		}

		public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI) {
			scale *= 0.8f;
			return true;
		}

		public override Vector2? HoldoutOffset() {
			return new Vector2(-10, -5);
		}
	}
}