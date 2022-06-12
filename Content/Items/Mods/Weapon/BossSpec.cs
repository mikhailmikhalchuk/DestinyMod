using DestinyMod.Common.Items;
using DestinyMod.Common.Items.ItemTypes;
using DestinyMod.Common.Items.Modifiers;
using Terraria;
using Terraria.ModLoader;

namespace DestinyMod.Content.Items.Mods.Weapon
{
    public class BossSpec : ItemMod
    {
        public override void SetDefaults()
        {
            DisplayName = "Boss Spec";
            Description = "Increases damage against bosses and vehicles.";
            ApplyType = ItemType.Weapon;
        }

        public static void Function(NPC target, ref int damage)
        {
            if (!target.boss)
            {
                return;
            }

            damage = (int)(damage * 1.078f);
        }

        public override void ModifyHitNPC(Player player, Item item, NPC target, ref int damage, ref float knockback, ref bool crit) => Function(target, ref damage);

        public override void ModifyHitNPCWithProj(Player player, Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection) => Function(target, ref damage);
    }

    public class BossSpecGranter : DestinyModItem, IItemModGranter
    {
        public override string Texture => "DestinyMod/Content/Items/Mods/Weapon/BossSpec";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Boss Spec");
            Tooltip.SetDefault("Increases damage against bosses and vehicles.");
        }

        public override void DestinySetDefaults()
        {
            Item.value = 30000;
        }

        public int ItemModType() => ModifierBase.GetType<BossSpec>();

        public string ItemModName() => "Boss Spec";
    }
}