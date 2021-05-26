using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheDestinyMod.Projectiles.Melee;
using TheDestinyMod.Items.Materials;

namespace TheDestinyMod.Items.Weapons.Melee
{
    public class BoltCaster : ModItem
    {
        public override void SetDefaults() {
            item.damage = 35;
            item.melee = true;
            item.width = 70;
            item.height = 80;
            item.useTime = 7;
            item.useAnimation = 25;
            item.channel = true;
            item.noUseGraphic = true;
            item.noMelee = true;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.knockBack = 6;
            item.value = Item.buyPrice(0, 22, 50, 0);
            item.rare = ItemRarityID.Pink;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.shoot = ModContent.ProjectileType<BoltCasterProjectile>();
            item.shootSpeed = 40f;
        }

        public override void AddRecipes() {
            ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.HallowedBar, 20);
			recipe.AddIngredient(ItemID.SoulofLight, 8);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
        }
    }
}