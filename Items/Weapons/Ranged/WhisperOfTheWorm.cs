using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using TheDestinyMod.Items.Materials;
using TheDestinyMod.Buffs;

namespace TheDestinyMod.Items.Weapons.Ranged
{
	public class WhisperOfTheWorm : ModItem
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Whisper of the Worm");
			Tooltip.SetDefault("\"A Guardian's power makes a rich feeding ground. Do not be revolted\"");
		}
		public override void SetDefaults() {
			item.damage = 250;
			item.ranged = true;
			item.noMelee = true;
			item.width = 124;
			item.height = 40;
			item.useTime = 50;
			item.useAnimation = 50;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.value = Item.buyPrice(0, 1, 0, 0);
			item.rare = ItemRarityID.Purple;
			item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/WhisperOfTheWorm");
			item.shoot = 10;
			item.shootSpeed = 16f;
			item.useAmmo = AmmoID.Bullet;
			item.scale = .80f;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack) {
			if (Main.rand.NextBool(4)) {
				Dust.NewDust(position += Vector2.Normalize(new Vector2(speedX, speedY)) * 90f, 1, 1, DustID.WhiteTorch);
				player.GetModPlayer<DestinyPlayer>().destinyWeaponDelay = 15;
			}
			return true;
		}

        public override bool CanUseItem(Player player) {
			if (player.GetModPlayer<DestinyPlayer>().destinyWeaponDelay > 0) {
				return false;
			}
            return base.CanUseItem(player);
        }

        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI) {
			scale *= 0.8f;
			return true;
		}

		public override Vector2? HoldoutOffset() {
			return new Vector2(-10, -2);
		}
	}
}