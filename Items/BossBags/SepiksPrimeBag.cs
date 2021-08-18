using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheDestinyMod.Items;
using TheDestinyMod.NPCs.SepiksPrime;

namespace TheDestinyMod.Items.BossBags
{
	public class SepiksPrimeBag : ModItem
	{
		public override int BossBagNPC => ModContent.NPCType<SepiksPrime>();

		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Treasure Bag");
			Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");
		}

		public override void SetDefaults() {
			item.maxStack = 999;
			item.consumable = true;
			item.width = 32;
			item.height = 32;
			item.rare = ItemRarityID.Orange;
			item.expert = true;
		}

		public override bool CanRightClick() => true;

		public override void OpenBossBag(Player player) {
			player.TryGettingDevArmor();
			if (Main.rand.NextBool(7)) {
				player.QuickSpawnItem(ModContent.ItemType<Vanity.SepiksPrimeMask>());
			}
			switch (Main.rand.Next(4)) {
				case 0:
					player.QuickSpawnItem(ModContent.ItemType<Weapons.Summon.ServitorStaff>());
					break;
				default:
					player.QuickSpawnItem(ItemID.WaterBolt);
					break;
			}
		}
	}
}