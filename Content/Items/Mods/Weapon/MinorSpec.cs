using DestinyMod.Common.Items.ItemTypes;
using DestinyMod.Common.Items.Modifiers;
using Terraria;
using Terraria.ModLoader;

namespace DestinyMod.Content.Items.Mods.Weapon
{
    public class MinorSpec : ItemMod
    {
        public const int RankAndFileHealthPoint = 5000;

        public override void SetDefaults()
        {
            DisplayName = "Minor Spec";
            Description = "Deals extra damage against rank-and-file enemies.";
            ApplyType = ItemType.Weapon;
        }

        public static void Function(NPC target, ref int damage)
        {
            if (target.lifeMax > RankAndFileHealthPoint || target.boss)
            {
                return;
            }

            damage = (int)(damage * 1.078f);
        }

        public override void ModifyHitNPC(Player player, Item item, NPC target, ref int damage, ref float knockback, ref bool crit) => Function(target, ref damage);

        public override void ModifyHitNPCWithProj(Player player, Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection) => Function(target, ref damage);
    }

    public class MinorSpecGranter : ModItem, IItemModGranter
    {
        public override string Texture => "DestinyMod/Content/Items/Mods/Weapon/MinorSpec";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Minor Spec");
            Tooltip.SetDefault("Deals extra damage against rank-and-file enemies.");
        }

        public int ItemModType() => ModifierBase.GetType<MinorSpec>();

        public string ItemModName() => "Minor Spec";
    }
}