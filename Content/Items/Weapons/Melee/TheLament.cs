using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using DestinyMod.Common.Items;
using Microsoft.Xna.Framework;
using DestinyMod.Common.ModPlayers;
using Terraria.Audio;

namespace DestinyMod.Content.Items.Weapons.Melee
{
    public class TheLament : DestinyModItem
    {
        public override void SetStaticDefaults() => Tooltip.SetDefault("Hold right-click to rev up the blade"
            + "\nWhile revving the blade, movement is inhibited"
            + "\nRevving the blade fully increases swing damage for a short time"
            + "\nDamaging enemies with this weapon restores a small portion of health"
            + "\n\"The last thing the Vex ever heard - the grinding wails of a vicious Banshee.\"");

        public override void DestinySetDefaults()
        {
            Item.damage = 200;
            Item.DamageType = DamageClass.Melee;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 6;
            Item.value = Item.buyPrice(gold: 22, silver: 50);
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = SoundLoader.GetLegacySoundSlot(Mod, "Sounds/Item/RazeLighter");
            Item.autoReuse = true;
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            if (target.damage > 0 && !target.friendly)
            {
                int heal = 5;
                player.statLife += heal;
                player.HealEffect(heal);
            }
        }

        public override void OnHitPvp(Player player, Player target, int damage, bool crit)
        {
            int heal = 5;
            player.statLife += heal;
            player.HealEffect(heal);
        }

        public override void ModifyWeaponDamage(Player player, ref StatModifier damage, ref float flat)
        {
            if (player.GetModPlayer<StatsPlayer>().ChannelTime > 90)
            {
                damage += 0.1f;
            }
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            Item.color = default;
            if (player.GetModPlayer<StatsPlayer>().ChannelTime > 90)
            {
                Item.color = Color.LightPink;
            }
        }

		public override bool? UseItem(Player player)
		{
            StatsPlayer statsPlayer = player.GetModPlayer<StatsPlayer>();
            if (statsPlayer.ChannelTime > 0 && statsPlayer.ChannelTime < 90)
            {
                if (statsPlayer.ChannelTime % 5 == 0)
                {
                    SoundEngine.PlaySound(SoundID.Item22);
                }

                player.maxRunSpeed /= 2;
                player.accRunSpeed /= 2;
                player.dashDelay = 10;
            }

            if (statsPlayer.ChannelTime == 92)
            {
                SoundEngine.PlaySound(SoundID.Item23);
            }
            return base.UseItem(player);
		}

		public override bool AltFunctionUse(Player player) => player.GetModPlayer<StatsPlayer>().ChannelTime <= 90;

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.useStyle = ItemUseStyleID.Shoot;
                Item.UseSound = null;
                Item.noMelee = true;
                Item.channel = true;
            }
            else
            {
                Item.useStyle = ItemUseStyleID.Swing;
                Item.UseSound = SoundLoader.GetLegacySoundSlot(Mod, "Sounds/Item/RazeLighter");
            }
            return base.CanUseItem(player);
        }

        public override Vector2? HoldoutOffset() => new Vector2(3, -17);
    }
}