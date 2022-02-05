using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using DestinyMod.Common.Items;
using DestinyMod.Content.Projectiles.Weapons.Melee;

namespace DestinyMod.Content.Items.Weapons.Melee
{
    public class BoltCatser : DestinyModItem
    {
        public override void DestinySetDefaults()
        {
            Item.damage = 35;
            Item.DamageType = DamageClass.Melee;
            Item.useTime = 7;
            Item.useAnimation = 25;
            Item.channel = true;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 6;
            Item.value = Item.buyPrice(gold: 22, silver: 50);
            Item.rare = ItemRarityID.Pink;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<BoltCasterProjectile>();
            Item.shootSpeed = 40f;
        }

        public override void AddRecipes() => CreateRecipe(1)
            .AddIngredient(ItemID.HallowedBar, 20)
            .AddIngredient(ItemID.SoulofLight, 8)
            .AddTile(TileID.Anvils)
            .Register();
    }
}