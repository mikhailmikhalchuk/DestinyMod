using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using TheDestinyMod.Projectiles.Ranged;
using TheDestinyMod.Items.Materials;
using System.Collections.Generic;

namespace TheDestinyMod.Items.Weapons.Ranged
{
	public class RatKing : ModItem
	{
		private Player own;

		public override void SetStaticDefaults() {
			Tooltip.SetDefault("Becomes stronger if nearby teammates are using this weapon\n\"We are small, but we are legion.\"");
		}

		public override void SetDefaults() {
			item.damage = 84;
			item.ranged = true;
			item.noMelee = true;
			item.autoReuse = true;
			item.rare = ItemRarityID.Red;
			item.knockBack = 0;
			item.width = 42;
			item.height = 30;
			item.useTime = 15;
			item.crit = 10;
			item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/RatKing");
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.shootSpeed = 10f;
			item.useAnimation = 15;
			item.shoot = 10;
			item.useAmmo = AmmoID.Bullet;
			item.value = Item.buyPrice(0, 1, 0, 0);
			item.scale = 0.8f;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack) {
			own = player;
			Projectile.NewProjectile(position.X, position.Y - 7, speedX, speedY, type, damage, knockBack, player.whoAmI);
			return false;
		}

        public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat) {
			own = player;
			flat += Main.player.Count(p => player.Distance(p.position) < 1600 && p.team == player.team && p.HeldItem.type == ModContent.ItemType<RatKing>() && p.active && p != player) * 12;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips) {
			int num = Main.player.Count(p => own.Distance(p.position) < 1600 && p.team == own.team && p.HeldItem.type == ModContent.ItemType<RatKing>() && p.active && p != own);
			if (num > 0) {
				tooltips.Add(new TooltipLine(mod, "KingBonus", $"This weapon deals {num * 12} increased damage ({num} nearby {(num > 1 ? "players" : "player")})") { overrideColor = Color.Gold });
			}
		}

        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI) {
			scale *= 0.8f;
			return true;
		}

		public override Vector2? HoldoutOffset() {
			return new Vector2(5, 0);
		}

		public override void AddRecipes() {
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<GunsmithMaterials>(), 50);
			recipe.AddIngredient(ItemID.FragmentSolar, 10);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}