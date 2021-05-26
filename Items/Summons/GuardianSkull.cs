using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheDestinyMod.Items.Materials;
using TheDestinyMod.NPCs.SepiksPrime;

namespace TheDestinyMod.Items.Summons
{
    public class GuardianSkull : ModItem
    {
        public override string Texture => "Terraria/Item_" + ItemID.Skull;

        public override void SetStaticDefaults() {
            Tooltip.SetDefault("An offering to the House of Devils' High Servitor\nSummons Sepiks Prime");
        }

        public override void SetDefaults() {
            item.width = 32;
            item.height = 32;
            item.maxStack = 20;
            item.rare = ItemRarityID.Blue;
            item.useAnimation = 40;
            item.useTime = 40;
            item.useStyle = ItemUseStyleID.HoldingUp;
            item.consumable = true;
        }

        public override bool CanUseItem(Player player) {
            return !NPC.AnyNPCs(ModContent.NPCType<SepiksPrime>());
        }

        public override bool UseItem(Player player) {
            Main.PlaySound(SoundID.Roar, player.position, 0);
            NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<SepiksPrime>());
            return true;
        }

        public override void AddRecipes() {
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Bone, 75);
			recipe.AddTile(TileID.DemonAltar);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
    }
}