using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheDestinyMod.Projectiles.Melee;
using TheDestinyMod.Items.Materials;
using TheDestinyMod.Sounds;
using Microsoft.Xna.Framework;

namespace TheDestinyMod.Items.Weapons.Melee
{
    public class TheLament : ModItem
    {
        public override void SetStaticDefaults() {
            Tooltip.SetDefault("Hold right-click to rev up the blade\nWhile revving the blade, movement is inhibited\nRevving the blade fully increases swing damage for a short time\nDamaging enemies with this weapon restores a small portion of health\n\"The last thing the Vex ever heard - the grinding wails of a vicious Banshee.\"");
        }

        public override void SetDefaults() {
            item.damage = 200;
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

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit) {
            if (target.damage > 0 && !target.friendly)
                player.statLife += 5;
        }

        public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat) {
            if (player.GetModPlayer<DestinyPlayer>().lamentRevUp > 90) {
                add += 0.1f;
            }
        }

        public override void MeleeEffects(Player player, Rectangle hitbox) {
            item.color = default;
            if (player.GetModPlayer<DestinyPlayer>().lamentRevUp > 90) {
                item.color = Color.LightPink;
            }
        }

        public override bool AltFunctionUse(Player player) {
            return player.GetModPlayer<DestinyPlayer>().lamentRevUp <= 90;
        }

        public override bool CanUseItem(Player player) {
            if (player.altFunctionUse == 2) {
                item.useStyle = ItemUseStyleID.HoldingOut;
                item.UseSound = null;
                item.noMelee = true;
            }
            else {
                item.useStyle = ItemUseStyleID.SwingThrow;
                item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/RazeLighter");
                item.noMelee = false;
            }
            return base.CanUseItem(player);
        }

        public override Vector2? HoldoutOffset() {
            return new Vector2(3, -17);
        }

        public override void OnHitPvp(Player player, Player target, int damage, bool crit) {
            player.statLife += 5;
        }
    }
}