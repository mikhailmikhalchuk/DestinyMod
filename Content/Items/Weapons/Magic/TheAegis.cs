using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using DestinyMod.Common.Items;
using Terraria.DataStructures;
using DestinyMod.Common.ModPlayers;
using Terraria.Audio;
using DestinyMod.Content.Projectiles.Weapons.Magic;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;

namespace DestinyMod.Content.Items.Weapons.Magic
{
	public class TheAegis : DestinyModItem
	{
		public int Cooldown;

		public static Asset<Texture2D> ShieldTexture { get; private set; }

		public override void SetStaticDefaults()
		{
			ShieldTexture = ModContent.Request<Texture2D>("DestinyMod/Content/Items/Weapons/Magic/TheAegis_Shield");
			Tooltip.SetDefault("A timelost relic, with the power to protect Guardians from being erased from existence"
			+ "\nSummons a protective shield"
			+ "\nRight Click to fire a powerful blast");
		}

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

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
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

		public override ItemPlayer.IterationContext PostUpdateRunSpeedsContext(Player player) => ItemPlayer.IterationContext.HeldItem;

		public override void PostUpdateRunSpeeds(Player player)
		{
			if (player.channel)
			{
				player.maxRunSpeed /= 2;
				player.accRunSpeed /= 2;
				player.dashDelay = 10;
			}
		}

		public override ItemPlayer.IterationContext ModifyDrawInfoContext(Player player) => ItemPlayer.IterationContext.HeldItem;

		public override void ModifyDrawInfo(Player player, ref PlayerDrawSet drawInfo)
		{
			ItemPlayer itemPlayer = player.GetModPlayer<ItemPlayer>();
			if (player.channel && itemPlayer.AegisCharge > 0)
			{
				player.headRotation = player.direction * 0.3f;
				Texture2D shieldTexture = ShieldTexture.Value;
				drawInfo.DrawDataCache.Add(new DrawData(
							shieldTexture,
							drawInfo.ItemLocation - Main.screenPosition + new Vector2(drawInfo.drawPlayer.direction == 1 ? 4 : -4, 20),
							shieldTexture.Frame(),
							new Color(Lighting.GetSubLight(drawInfo.drawPlayer.Center)),
							player.GetModPlayer<ItemPlayer>().AegisCharge > 0 ? 0f : drawInfo.drawPlayer.headRotation - (drawInfo.drawPlayer.direction == 1 ? 0.1f : -0.1f),
							new Vector2(drawInfo.drawPlayer.direction == 1 ? 0 : shieldTexture.Frame().Width, shieldTexture.Frame().Height),
							drawInfo.drawPlayer.HeldItem.scale * 0.8f,
							drawInfo.itemEffect,
							0));
			}
		}

		public override ItemPlayer.IterationContext HideDrawLayersContext(Player player) => ItemPlayer.IterationContext.HeldItem;

		public override void HideDrawLayers(Player player, PlayerDrawSet drawInfo)
		{
			ItemPlayer itemPlayer = player.GetModPlayer<ItemPlayer>();
			if (player.channel && itemPlayer.AegisCharge > 0)
			{
				PlayerDrawLayers.Shield.Hide();
				PlayerDrawLayers.SolarShield.Hide();
			}
		}
	}
}