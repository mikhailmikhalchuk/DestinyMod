using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace TheDestinyMod.Items.Pets
{
	public class Ghost : ModItem
	{
		public override void SetStaticDefaults() {
			DisplayName.AddTranslation(GameCulture.Polish, "Duch");
			Tooltip.SetDefault("Summons a Ghost, born from the Traveler and a Guardian's faithful companion");
			Tooltip.AddTranslation(GameCulture.Polish, "Przywołuje ducha zdrodzonego z Wędrowca i wierny towarzysz twojego Strażnika");
		}

		public override void SetDefaults() {
			item.damage = 0;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.width = 40;
			item.height = 40;
			item.useAnimation = 20;
			item.useTime = 20;
			item.rare = ItemRarityID.LightRed;
			item.noMelee = true;
			item.value = Item.sellPrice(0, 5, 50, 0);
			item.shoot = ModContent.ProjectileType<Projectiles.Pets.Ghost>();
			item.buffType = ModContent.BuffType<Buffs.Pets.Ghost>();
			item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/Ghost");
		}

		public override void UseStyle(Player player) {
			if (player.whoAmI == Main.myPlayer && player.itemTime == 0) {
				player.AddBuff(item.buffType, 3600, true);
			}
		}
	}
}