using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using DestinyMod.Content.Projectiles.Weapons.Melee.Glaive;
using DestinyMod.Common.Items.ItemTypes;

namespace DestinyMod.Content.Items.Weapons.Melee.Glaive
{
    public class TheEnigma : GlaiveItem
    {
        public override void DestinySetDefaults()
        {
            Item.damage = 35;
            Item.useTime = 12;
            Item.useAnimation = 12;
            Item.knockBack = 4;
            Item.value = Item.buyPrice(gold: 22, silver: 50);
            Item.rare = ItemRarityID.Pink;
            Item.shoot = ModContent.ProjectileType<TheEnigmaProjectile>();
        }

        public override void AddRecipes() => CreateRecipe(1)
            .AddIngredient(ItemID.HallowedBar, 20)
            .AddIngredient(ItemID.SoulofLight, 8)
            .AddTile(TileID.Anvils)
            .Register();
    }
}