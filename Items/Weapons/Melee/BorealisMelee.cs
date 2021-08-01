using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using TheDestinyMod.Projectiles.Ranged;
using TheDestinyMod.Items.Materials;
using TheDestinyMod.Buffs;

namespace TheDestinyMod.Items.Weapons.Melee
{
	public class BorealisMelee : ModItem
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Borealis");
			Tooltip.SetDefault("Right click while holding this weapon to cycle between damage types\n\"Light is a spectrum. Why limit yourself to a single hue?\"");
		}

		public override void SetDefaults() {
			item.damage = 50;
			item.melee = true;
			item.width = 128;
			item.height = 40;
			item.useTime = 28;
			item.useAnimation = 28;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.noMelee = true;
			item.knockBack = 4;
			item.value = Item.buyPrice(0, 1, 0, 0);
			item.rare = ItemRarityID.Yellow;
			item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/BorealisMelee");
			item.autoReuse = false;
			item.shoot = 10;
			item.shootSpeed = 300f;
			item.useAmmo = AmmoID.Bullet;
			item.scale = 0.7f;
		}

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack) {
			Projectile p = Projectile.NewProjectileDirect(new Vector2(position.X, position.Y - 5), new Vector2(speedX, speedY), type, damage, knockBack, player.whoAmI);
			p.ranged = false;
			p.melee = true;
			return false;
        }

        public override bool CanUseItem(Player player) {
			DestinyPlayer dPlayer = player.GetModPlayer<DestinyPlayer>();
			if (player.altFunctionUse == 2 && dPlayer.borealisCooldown == 0) {
				for (int i = 0; i < Main.maxInventory; i++) {
					if (player.inventory[i] == item) {
						byte prefix = item.prefix;
						player.inventory[i].SetDefaults(ModContent.ItemType<Magic.BorealisMagic>());
						player.inventory[i].Prefix(prefix);
						Main.PlaySound(SoundID.Item101);
						dPlayer.borealisCooldown = 15;
						break;
					}
				}
			}
			return player.altFunctionUse != 2;
		}

		public override bool AltFunctionUse(Player player) {
            return true;
        }

        public override Vector2? HoldoutOffset() {
			return new Vector2(-15, -2);
		}
	}
}