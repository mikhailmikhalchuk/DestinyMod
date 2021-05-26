using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheDestinyMod.Projectiles.Melee;
using TheDestinyMod.Items.Materials;
using TheDestinyMod.Sounds;

namespace TheDestinyMod.Items.Weapons.Melee
{
    public class TheLament : ModItem
    {
        public override void SetDefaults() {
            item.damage = 1000;
            item.melee = true;
            item.width = 62;
            item.height = 68;
            item.useTime = 20;
            item.useAnimation = 20;
            item.channel = true;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 6;
            item.value = Item.buyPrice(0, 22, 50, 0);
            item.rare = ItemRarityID.Orange;
            item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/RazeLighter");
            item.autoReuse = true;
        }
    }
}