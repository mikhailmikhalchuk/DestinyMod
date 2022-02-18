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
        public int RevUp;

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

        public static void HealBack(Player player)
		{
            int heal = 5;
            player.statLife += heal;
            player.HealEffect(heal);
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            if (target.damage > 0 && !target.friendly)
            {
                HealBack(player);
            }
        }

        public override void OnHitPvp(Player player, Player target, int damage, bool crit) => HealBack(player);

        public override void ModifyWeaponDamage(Player player, ref StatModifier damage, ref float flat)
        {
            if (RevUp > 90)
            {
                damage += 0.1f;
            }
        }

        public override void MeleeEffects(Player player, Rectangle hitbox) => Item.color = RevUp > 90 ? Color.LightPink : default;

        public override bool? UseItem(Player player)
		{
            StatsPlayer statsPlayer = player.GetModPlayer<StatsPlayer>();
            Main.NewText(statsPlayer.DestinyChannelTime + " | " + RevUp);
            if (statsPlayer.DestinyChannelTime > 0)
            {
                player.itemAnimation = 2;
                player.itemTime = 0;

                if (statsPlayer.DestinyChannelTime < 90)
                {
                    if (RevUp < 90)
                    {
                        RevUp = statsPlayer.DestinyChannelTime;
                    }

                    if (statsPlayer.DestinyChannelTime % 5 == 0)
                    {
                        SoundEngine.PlaySound(SoundID.Item22);
                    }
                }
                else
                {
                    RevUp = statsPlayer.DestinyChannelTime;

                    if (RevUp >= 300)
                    {
                        RevUp = 0;
                        statsPlayer.DestinyChannelTime = 0;
                    }
                }
            }

            if (statsPlayer.DestinyChannelTime == 92)
            {
                SoundEngine.PlaySound(SoundID.Item23);
            }
            return base.UseItem(player);
		}

		public override bool AltFunctionUse(Player player) => player.GetModPlayer<StatsPlayer>().DestinyChannelTime <= 90;

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.useStyle = ItemUseStyleID.Shoot;
                Item.UseSound = null;
                Item.noMelee = true;
                DestinyModChannel = true;
            }
            else
            {
                Item.useStyle = ItemUseStyleID.Swing;
                Item.UseSound = SoundLoader.GetLegacySoundSlot(Mod, "Sounds/Item/RazeLighter");
                Item.noMelee = false;
                DestinyModChannel = false;
            }
            return base.CanUseItem(player);
        }

        public override ItemPlayer.IterationContext DeterminePostUpdateRunSpeedsContext(Player player) => ItemPlayer.IterationContext.HeldItem;

        public override void PostUpdateRunSpeeds(Player player)
		{
			if (!Main.mouseRight || player.GetModPlayer<StatsPlayer>().DestinyChannelTime > 90)
			{
                return;
			}

            player.maxRunSpeed /= 2;
            player.accRunSpeed /= 2;
            player.dashDelay = 10;
        }

		public override Vector2? HoldoutOffset() => new Vector2(3, -17);
    }
}