using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using TheDestinyMod.Projectiles.Minions;
using TheDestinyMod.Buffs.Minions;

namespace TheDestinyMod.Items.Weapons.Summon
{
    public class ServitorStaff : ModItem
    {
        public override void SetStaticDefaults() {
            Tooltip.SetDefault("Summons tiny servitors to protect you\nFor every 3 tiny servitors a larger servitor will appear");
            ItemID.Sets.GamepadWholeScreenUseRange[item.type] = true;
            ItemID.Sets.LockOnIgnoresCollision[item.type] = true;
        }

        public override void SetDefaults() {
            item.damage = 10;
            item.summon = true;
            item.mana = 9;
            item.width = 48;
            item.height = 48;
            item.useAnimation = 30;
            item.useTime = 30;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.noMelee = true;
            item.knockBack = 2;
            item.value = Item.buyPrice(0, 6, 0, 0);
            item.rare = ItemRarityID.Orange;
            item.UseSound = SoundID.Item44;
            item.shoot = ModContent.ProjectileType<TinyServitor>();
            item.buffType = ModContent.BuffType<ServitorBuff>();
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack) {
            player.AddBuff(item.buffType, 2);
			Projectile.NewProjectile(Main.MouseWorld, Vector2.Zero, type, damage, knockBack, player.whoAmI);
            return false;
		}
    }
}