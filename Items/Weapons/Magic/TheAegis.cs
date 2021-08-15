using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using TheDestinyMod.Items.Materials;

namespace TheDestinyMod.Items.Weapons.Magic
{
	public class TheAegis : ModItem
	{
		private int cooldown;

		private bool notified = true;

        public override void SetStaticDefaults() {
			Tooltip.SetDefault("A timelost relic, with the power to protect Guardians from being erased from existance\nLeft click to summon a protective shield\nRight click to fire a powerful blast");
		}

		public override void SetDefaults() {
			item.magic = true;
			item.channel = true;
			item.useTurn = true;
			item.mana = 7;
			item.width = 68;
			item.height = 56;
			item.useTime = 25;
			item.useAnimation = 25;
			item.useStyle = ItemUseStyleID.Stabbing;
			item.noMelee = true;
			item.value = 0;
			item.rare = ItemRarityID.Expert;
			item.shoot = ModContent.ProjectileType<Projectiles.Magic.AegisBubble>();
			item.shootSpeed = 14f;
			item.scale = 0.8f;
			item.noUseGraphic = true;
		}

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack) {
			DestinyPlayer dPlayer = player.GetModPlayer<DestinyPlayer>();
			player.itemLocation = player.Center;
			if (player.altFunctionUse == 2 && !player.mount.Active) {
				Projectile.NewProjectile(position, new Vector2(speedX, speedY), ModContent.ProjectileType<Projectiles.Magic.AegisBlast>(), 20, 0, player.whoAmI);
				cooldown = 300;
				notified = false;
				dPlayer.aegisCharge = 1;
				player.controlLeft = false;
				player.controlRight = false;
				player.controlUp = false;
				player.controlDown = false;
				player.controlHook = false;
				player.controlJump = false;
				return false;
			}
			if (dPlayer.aegisCharge > 0) {
				return false;
			}
			return true;
        }

        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale) {
			if (cooldown > 0) {
				cooldown--;
			}
			else if (cooldown <= 0 && !notified) {
				Main.PlaySound(SoundID.MaxMana);
				notified = true;
			}
            return base.PreDrawInInventory(spriteBatch, position, frame, drawColor, itemColor, origin, scale);
        }

        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI) {
			scale *= 0.8f;
			return true;
		}

        public override bool AltFunctionUse(Player player) {
            return true;
        }

        public override bool CanUseItem(Player player) {
			if (player.altFunctionUse == 2 && cooldown > 0) {
				return false;
			}
            return base.CanUseItem(player);
        }
	}
}