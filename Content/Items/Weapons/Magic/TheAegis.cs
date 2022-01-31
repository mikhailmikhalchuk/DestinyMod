using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using DestinyMod.Common.Items;
using Terraria.DataStructures;
using DestinyMod.Common.ModPlayers;
using Terraria.Audio;

namespace DestinyMod.Content.Items.Weapons.Magic
{
	public class TheAegis : DestinyModItem
	{
		private int Cooldown;

		public override void SetStaticDefaults() => Tooltip.SetDefault("A timelost relic, with the power to protect Guardians from being erased from existence"
			+ "\nLeft click to summon a protective shield"
			+ "\nRight click to fire a powerful blast");

		public override void DestinySetDefaults()
		{
			Item.DamageType = DamageClass.Magic;
			Item.channel = true;
			Item.useTurn = true;
			Item.mana = 7;
			Item.useTime = 25;
			Item.useAnimation = 25;
			Item.useStyle = ItemUseStyleID.Thrust;
			Item.noMelee = true;
			Item.rare = ItemRarityID.Expert;
			Item.shoot = ModContent.ProjectileType<AegisBubble>();
			Item.shootSpeed = 14f;
			Item.noUseGraphic = true;
		}

		public override bool Shoot(Player player, ProjectileSource_Item_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			ItemPlayer itemPlayer = player.GetModPlayer<ItemPlayer>();
			player.itemLocation = player.Center;
			if (player.altFunctionUse == 2 && !player.mount.Active)
			{
				Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<AegisBlast>(), 20, 0, player.whoAmI);
				Cooldown = 300;
				itemPlayer.AegisCharge = 1;
				return false;
			}
			return itemPlayer.AegisCharge <= 0;
		}

		public override void UpdateInventory(Player player)
		{
			if (--Cooldown == 0)
			{
				SoundEngine.PlaySound(SoundID.MaxMana);
			}
		}

		public override bool AltFunctionUse(Player player) => true;

		public override bool CanUseItem(Player player) => !(player.altFunctionUse == 2 && Cooldown > 0);
	}
}