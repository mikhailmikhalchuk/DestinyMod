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

        public bool OldRightClickTest;

        public bool RightClickTest;

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Right Click to rev up the blade"
            + "\nWhile revving the blade, movement is inhibited"
            + "\nRevving the blade fully increases swing damage for a short time"
            + "\nDamaging enemies with this weapon restores a small portion of health"
            + "\n'The last thing the Vex ever heard - the grinding wails of a vicious Banshee.'");
        }

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
            Item.UseSound = SoundLoader.GetLegacySoundSlot(Mod, "Assets/Sounds/Item/Weapons/Melee/RazeLighter");
            Item.autoReuse = true;
        }

        private static void HealBack(Player player)
		{
            const int heal = 5;
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

        public override void OnHitPvp(Player player, Player target, int damage, bool crit)
        {
            HealBack(player);
        }

        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            if (RevUp > 90)
            {
                damage += 0.1f;
            }
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            Item.color = RevUp > 90 ? Color.LightPink : default;
        }

        public override bool? UseItem(Player player)
		{
            if (Main.mouseRightRelease || Main.mouseLeft)
			{
                return base.UseItem(player);
            }

            StatsPlayer statsPlayer = player.GetModPlayer<StatsPlayer>();
            if (statsPlayer.DestinyChannelTime > 0)
            {
                player.itemAnimation = player.itemAnimationMax;
                player.itemTime = 0;
                RevUp = statsPlayer.DestinyChannelTime;
                if (RevUp < 90 && RevUp % 5 == 0)
                {
                    SoundEngine.PlaySound(SoundID.Item22);
                }
                
                if (RevUp >= 92)
                {
                    player.itemAnimation = 0;
                    player.itemTime = 0;
                    SoundEngine.PlaySound(SoundID.Item23);
                    return true;
                }
            }

            return base.UseItem(player);
		}

		public override bool AltFunctionUse(Player player) => RevUp <= 90;

        public override bool CanUseItem(Player player)
        {
            OldRightClickTest = RightClickTest;
            if (player.altFunctionUse == 2)
            {
                Item.useStyle = ItemUseStyleID.Shoot;
                Item.useTime = 5;
                Item.useAnimation = 5;
                Item.UseSound = null;
                Item.noMelee = true;
                DestinyModChannel = true;
                RightClickTest = true;
            }
            else
            {
                Item.useStyle = ItemUseStyleID.Swing;
                Item.useTime = 20;
                Item.useAnimation = 20;
                Item.UseSound = SoundLoader.GetLegacySoundSlot(Mod, "Assets/Sounds/Item/Weapons/Ranged/RazeLighter");
                Item.noMelee = false;
                DestinyModChannel = false;
                RightClickTest = false;
            }
            return OldRightClickTest == RightClickTest;
        }

        public override ItemPlayer.IterationContext PostUpdateRunSpeedsContext(Player player) => ItemPlayer.IterationContext.HeldItem;

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