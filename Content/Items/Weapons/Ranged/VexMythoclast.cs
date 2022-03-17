using Microsoft.Xna.Framework;
using DestinyMod.Common.Items.ItemTypes;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using DestinyMod.Content.Projectiles.Weapons.Ranged;
using DestinyMod.Common.ModPlayers;
using DestinyMod.Content.Buffs;

namespace DestinyMod.Content.Items.Weapons.Ranged
{
    public class VexMythoclast : Gun
    {
        private int SwapCooldown;

        private bool UsingAltFunction;

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Kills with this weapon grant stacks of Overcharge"
            + "\nRight-click with Overcharge to switch firing modes"
            + "\nHold down the trigger in the alternative firing mode to fire a more powerful shot"
            + "\n'...a causal loop within the weapon's mechanism, suggesting that the firing process somehow binds space and time into...'");
        }

        public override void DestinySetDefaults()
        {
            Item.damage = 85;
            Item.autoReuse = true;
            Item.channel = true;
            Item.rare = ItemRarityID.Yellow;
            Item.knockBack = 0;
            Item.useTime = 18;
            Item.crit = 10;
            Item.UseSound = SoundLoader.GetLegacySoundSlot(Mod, "Assets/Sounds/Item/Weapons/Ranged/VexMythoclast"); //thanks, fillinek
            Item.useAnimation = 18;
            Item.value = Item.buyPrice(gold: 1);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (UsingAltFunction)
            {
                type = ModContent.ProjectileType<VexChargeBullet>();
                damage *= 2;
            }
            else
            {
                Item.autoReuse = true;
                type = ModContent.ProjectileType<VexBullet>();
            }
            Projectile.NewProjectile(source, new Vector2(position.X, position.Y - 4), velocity, type, !UsingAltFunction ? damage * 2 : damage, knockback, player.whoAmI);
            return false;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2 && !UsingAltFunction && SwapCooldown <= 0 && player.HasBuff<Overcharge>())
            {
                CombatText.NewText(player.getRect(), Color.Gold, "Charge Mode!");
                UsingAltFunction = true;
                Item.UseSound = null;
                SoundEngine.PlaySound(SoundID.Item101);
                SwapCooldown = 15;
                Item.color = Color.LightPink;
            }
            else if (player.altFunctionUse == 2 && UsingAltFunction && SwapCooldown <= 0)
            {
                CombatText.NewText(player.getRect(), Color.Gold, "Normal Mode!");
                UsingAltFunction = false;
                Item.UseSound = SoundLoader.GetLegacySoundSlot(Mod, "Assets/Sounds/Item/Weapons/Ranged/VexMythoclast");
                SoundEngine.PlaySound(SoundID.Item101);
                SwapCooldown = 15;
                Item.color = default;
            }
            return player.altFunctionUse != 2;
        }

        public override void UpdateInventory(Player player)
        {
            ItemPlayer itemPlayer = player.GetModPlayer<ItemPlayer>();
            if (itemPlayer.OverchargeStacks <= 0 && UsingAltFunction)
            {
                CombatText.NewText(player.getRect(), Color.Gold, "Overcharge Depleted!");
                UsingAltFunction = false;
                player.ClearBuff(ModContent.BuffType<Overcharge>());
                Item.UseSound = SoundLoader.GetLegacySoundSlot(Mod, "Assets/Sounds/Item/Weapons/Ranged/VexMythoclast");
                Item.color = default;
            }

            SwapCooldown--;
        }

        public override bool AltFunctionUse(Player player) => true;

        public override Vector2? HoldoutOffset() => new Vector2(-5, -2);
    }
}