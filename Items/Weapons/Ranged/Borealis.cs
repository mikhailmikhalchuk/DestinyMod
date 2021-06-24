using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using TheDestinyMod.Projectiles.Ranged;
using TheDestinyMod.Items.Materials;
using TheDestinyMod.Buffs;

namespace TheDestinyMod.Items.Weapons.Ranged
{
	public class Borealis : ModItem
	{
		private int cooldown;

		public override void SetStaticDefaults() {
			Tooltip.SetDefault("Right click while holding this weapon to cycle between damage types\n\"Light is a spectrum. Why limit yourself to a single hue?\"");
		}

		public override void SetDefaults() {
			item.damage = 50;
			item.ranged = true;
			item.width = 128;
			item.height = 40;
			item.useTime = 28;
			item.useAnimation = 28;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.noMelee = true;
			item.knockBack = 4;
			item.value = Item.buyPrice(0, 1, 0, 0);
			item.rare = ItemRarityID.Yellow;
			item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/Borealis");
			item.autoReuse = false;
			item.shoot = 10;
			item.shootSpeed = 300f;
			item.useAmmo = AmmoID.Bullet;
			item.scale = 0.7f;
		}

		public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI) {
			scale *= 0.7f;
			Texture2D texture = mod.GetTexture("Items/Weapons/Ranged/Borealis");
			if (item.melee) {
				texture = mod.GetTexture("Items/Weapons/Ranged/BorealisMelee");
			}
			else if (item.magic) {
				texture = mod.GetTexture("Items/Weapons/Ranged/BorealisMagic");
			}
			Vector2 position = item.position - Main.screenPosition + new Vector2(item.width / 2, item.height - texture.Height * 0.5f + 2f);
			spriteBatch.Draw(texture, position, null, lightColor, rotation, texture.Size(), scale, SpriteEffects.None, 0f);
			return false;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack) {
			if (player.altFunctionUse != 2) {
				Projectile p = Projectile.NewProjectileDirect(new Vector2(position.X, position.Y - 5), new Vector2(speedX, speedY), type, damage, knockBack, player.whoAmI);
				if (item.melee) {
					p.melee = true;
					p.ranged = false;
				}
				else if (item.magic) {
					p.magic = true;
					p.ranged = false;
				}
			}
			return false;
		}

        public override bool CanUseItem(Player player) {
			if (player.altFunctionUse == 2 && item.ranged && cooldown <= 0) {
				item.ranged = false;
				item.melee = true;
				Main.PlaySound(SoundID.Item101);
				cooldown = 15;
			}
			else if (player.altFunctionUse == 2 && item.melee && cooldown <= 0) {
				item.melee = false;
				item.magic = true;
				Main.PlaySound(SoundID.Item101);
				cooldown = 15;
			}
			else if (player.altFunctionUse == 2 && item.magic && cooldown <= 0) {
				item.magic = false;
				item.ranged = true;
				Main.PlaySound(SoundID.Item101);
				cooldown = 15;
			}
			return player.altFunctionUse != 2;
        }

        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale) {
			Texture2D texture = mod.GetTexture("Items/Weapons/Ranged/Borealis");
			item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/Borealis");
			if (item.melee) {
				texture = mod.GetTexture("Items/Weapons/Ranged/BorealisMelee");
				item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/BorealisMelee");
			}
			else if (item.magic) {
				texture = mod.GetTexture("Items/Weapons/Ranged/BorealisMagic");
				item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/BorealisMagic");
			}
			if (cooldown > 0) {
				cooldown--;
			}
			spriteBatch.Draw(texture, position, null, drawColor, 0, origin, scale, SpriteEffects.None, 0f);
			return false;
		}

		public override bool AltFunctionUse(Player player) {
            return true;
        }

        public override Vector2? HoldoutOffset() {
			return new Vector2(-15, -2);
		}
	}
}