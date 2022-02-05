using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using DestinyMod.Common.Items;

namespace DestinyMod.Content.Items.Equipables.Pets
{
	public class Ghost : DestinyModItem
	{
		public override void SetStaticDefaults() => Tooltip.SetDefault("Summons a Ghost, born from the Traveler and a Guardian's faithful companion");

		public override void DestinySetDefaults()
		{
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useAnimation = 20;
			Item.useTime = 20;
			Item.rare = ItemRarityID.LightRed;
			Item.value = Item.sellPrice(gold: 5, silver: 50);
			Item.shoot = ModContent.ProjectileType<Projectiles.Pets.Ghost>();
			Item.buffType = ModContent.BuffType<Buffs.Pets.Ghost>();
			Item.UseSound = SoundLoader.GetLegacySoundSlot(Mod, "Sounds/Item/Ghost");
		}

		public override void UseStyle(Player player, Rectangle heldItemFrame)
		{
			if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
			{
				player.AddBuff(Item.buffType, 3600, true);
			}
		}
	}
}