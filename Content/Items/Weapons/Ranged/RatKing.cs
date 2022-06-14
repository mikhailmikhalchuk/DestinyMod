using DestinyMod.Common.Items.ItemTypes;
using DestinyMod.Content.Items.Materials;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace DestinyMod.Content.Items.Weapons.Ranged
{
	public class RatKing : Gun
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Becomes stronger if nearby teammates are using this weapon"
			+ "\n'We are small, but we are legion.'");
		}

		public override void DestinySetDefaults()
		{
			Item.damage = 84;
			Item.autoReuse = true;
			Item.rare = ItemRarityID.Red;
			Item.knockBack = 0;
			Item.useTime = 15;
			Item.crit = 10;
			Item.UseSound = new SoundStyle("DestinyMod/Assets/Sounds/Item/Weapons/Ranged/RatKing");
			Item.useAnimation = 15;
			Item.value = Item.buyPrice(0, 1, 0, 0);
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Projectile.NewProjectile(source, new Vector2(position.X, position.Y - 7), velocity, type, damage, knockback, player.whoAmI);
			return false;
		}

		public static int CalculateQualifiedPlayerCount(Player player)
		{
			int output = 0;
			for (int index = 0; index < Main.maxPlayers; index++)
			{
				Player otherPlayer = Main.player[index];

				if (!player.active || player == otherPlayer || player.team != otherPlayer.team
					|| otherPlayer.HeldItem.type == ModContent.ItemType<RatKing>() || player.DistanceSQ(otherPlayer.Center) >= 2560000)
				{
					continue;
				}

				output++;
			}
			return output;
		}

		public override void ModifyWeaponDamage(Player player, ref StatModifier damage) => damage.Flat += CalculateQualifiedPlayerCount(player) * 12;

		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			int qualifiedPlayerCount = CalculateQualifiedPlayerCount(Main.LocalPlayer);
			if (qualifiedPlayerCount > 0)
			{
				TooltipLine kingBonus = new TooltipLine(Mod, "KingBonus",
					$"This weapon deals {qualifiedPlayerCount * 12} increased damage ({qualifiedPlayerCount} nearby {(qualifiedPlayerCount > 1 ? "players" : "player")})")
				{
					OverrideColor = Color.Gold
				};
				tooltips.Add(kingBonus);
			}
		}

		public override Vector2? HoldoutOffset() => new Vector2(5, 0);

		public override void AddRecipes() => CreateRecipe(1)
			.AddIngredient(ModContent.ItemType<GunsmithMaterials>(), 50)
			.AddIngredient(ItemID.FragmentSolar, 10)
			.AddTile(TileID.LunarCraftingStation)
			.Register();
	}
}