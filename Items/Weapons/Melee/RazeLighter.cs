using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheDestinyMod.Projectiles.Melee;
using TheDestinyMod.Items.Materials;
using TheDestinyMod.Sounds;

namespace TheDestinyMod.Items.Weapons.Melee
{
    public class RazeLighter : ModItem
    {
        public override void SetDefaults() {
            item.damage = 16;
            item.melee = true;
            item.width = 68;
            item.height = 82;
            item.useTime = 7;
            item.useAnimation = 25;
            item.channel = true;
            item.noUseGraphic = true;
            item.noMelee = true;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.knockBack = 6;
            item.value = Item.buyPrice(0, 22, 50, 0);
            item.rare = ItemRarityID.Orange;
            item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/RazeLighter");
            item.autoReuse = true;
            item.shoot = ModContent.ProjectileType<RazeLighterProjectile>();
            item.shootSpeed = 40f;
        }

        public override void AddRecipes() {
            ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.HellstoneBar, 20);
			recipe.AddIngredient(ModContent.ItemType<RelicIron>(), 10);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
        }
    }
}